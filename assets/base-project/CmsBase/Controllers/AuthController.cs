using Azure;
using Azure.Core;
using CMS.Utility;
using CMS.Utility.Model;
using CmsBase.DataAccess;
using CmsBase.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NLog;
using RestSharp;
using System.Buffers.Text;
using System.Diagnostics;
using System.Text.Json;

namespace CmsBase.Controllers;

public class AuthController : Controller
{
    private readonly ILogger<AuthController> _logger;
    public readonly CmsDataAccess _da;

    public AuthController(ILogger<AuthController> logger, CmsDataAccess da)
    {
        _logger = logger;
        _da = da;
    }
    public IActionResult ForgotPasswordBasic() => View();
    public IActionResult LoginBasic()
    {
        try
        {
            string clientID = AppConfig.AppSettings("ClientID");
            string UriCallB = AppConfig.AppSettings("UriCallBack");
            string linkLogin = AppConfig.AppSettings("MailServer");

            if (string.IsNullOrWhiteSpace(clientID) || string.IsNullOrWhiteSpace(UriCallB) ||
                string.IsNullOrWhiteSpace(linkLogin) || string.IsNullOrWhiteSpace(AppConfig.AppSettings("SecretKey")))
            {
                ViewData["SsoSetupRequired"] = true;
                return View();
            }

            string UrlLoginMail = linkLogin
                + "/dang-nhap?client_id=" + clientID
                + "&redirect_uri=" + UriCallB
                + "&response_type=code"
                + "&state=123456";

            return Redirect(UrlLoginMail);
        }
        catch (Exception ex)
        {
            _logger.LogError("LoginBasic Exception : " + ex.ToString());

            return View();
        }
    }
    public IActionResult RegisterBasic() => View();

    public IActionResult RedirectLogin(string code, string state)
    {
        _logger.LogInformation($"===== RedirectLogin START =====");
        _logger.LogInformation(
        "Scheme={Scheme}, Host={Host}, PathBase={PathBase}, Path={Path}, IsHttps={IsHttps}",
        Request.Scheme,
        Request.Host,
        Request.PathBase,
        Request.Path,
        Request.IsHttps
    );
        try
        {

            if (!String.IsNullOrEmpty(code))
            {
                string linkLogin = AppConfig.AppSettings("MailServer");
                var client = new RestClient(linkLogin);
                var requestGetToken = new RestRequest("/GetAccessToken", Method.Post);
                var body = new
                {
                    client_id = AppConfig.AppSettings("ClientID"),
                    client_secret = AppConfig.AppSettings("SecretKey"),
                    grant_type = "authorization_code",
                    code = code
                };
                requestGetToken.AddJsonBody(body);
                var resToken =  client.Execute(requestGetToken);
                _logger.LogInformation($"GetToken Success = {resToken.IsSuccessful}");
                if (resToken.IsSuccessful)
                {
                    var json = JsonDocument.Parse(resToken.Content);
                    var root = json.RootElement;

                    if (root.TryGetProperty("access_token", out var token))
                    {
                        var accessToken = token.GetString();
                        // call api get user info
                        if (!string.IsNullOrEmpty(accessToken))
                        {
                            var requestInfo = new RestRequest("/GetUserInfor?oauth_token="+ accessToken, Method.Get);
                            //requestInfo.AddHeader("Authorization", accessToken);
                            var resI4 = client.Execute(requestInfo);
                            _logger.LogInformation($"GetUserInfo Success = {resI4.IsSuccessful}");
                            if (resI4.IsSuccessful)
                            {
                                var result = JsonConvert.DeserializeObject<UserSession>(resI4.Content);
                                _logger.LogInformation($"UserName = {result?.Name}"); // NEW
                                // check trong db xem có quyèn vào không
                                var m_Users = new SP_User_GetByConditionModel();
                                var ListUsers = _da.SP_User_GetByCondition_List(result.Name,"",0);
                                _logger.LogInformation($"DB User Count = {ListUsers?.Count}"); // NEW
                                if (ListUsers != null && ListUsers.Count > 0)
                                {
                                    m_Users = ListUsers[0];
                                }    
                                if (m_Users != null && m_Users.UserID > 0)
                                {
                                    var Log = new UsersLog();
                                    Log.FunctionID = 9999;
                                    Log.ClientIP = AppConfig.GetClientIP(HttpContext);
                                    Log.UserID = m_Users.UserID;
                                    Log.LogType = 1;
                                    Log.FunctionName = "Đăng Nhập Hệ Thống";
                                    Log.Description = "Tài khoản " + m_Users.Username + " Đăng nhập hệ thống";
                                    var insertLog= _da.SP_UserLogs_Insert_Output(m_Users.UserID, Log.FunctionID,Log.FunctionName,Log.Description ,Log.LogType, Log.ClientIP);

                                    if (m_Users.Status == 1)
                                    {
                                        result.IsAdmin = m_Users.IsAdministrator ?? false;
                                        result.Id = m_Users.UserID.ToString();
                                        SessionHelper.SetUser(HttpContext.Session, result);
                                        _logger.LogInformation("SessionHelper.SetUser OK"); // NEW
                                    }
                                    else
                                    {
                                        TempData["ErrorMessage"] = "Tài khoản của bạn đã bị vô hiệu hóa !";
                                    }    
                                    
                                }
                                else
                                {
                                    TempData["ErrorMessage"] = "Tài khoản của bạn không có quyền truy cập !";
                                }


                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                TempData["ErrorMessage"] = "Lấy thông tin user thất bại !";
                            }    
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Không lấy được mã access token !";
                        }
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Không tìm thấy mã code trên link callback !";
                    }
                }    
            }
            else
            {
                TempData["ErrorMessage"] = "Không lấy được mã code !";
            }  
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "RedirectLogin Exception");

            TempData["ErrorMessage"] = ex.Message;

            return RedirectToAction("LoginBasic");
        }
        return RedirectToAction("LoginBasic");
    }

    public IActionResult Logout()
    {
        SessionHelper.Clear(HttpContext.Session);
        return RedirectToAction("LoginBasic");
    }
}

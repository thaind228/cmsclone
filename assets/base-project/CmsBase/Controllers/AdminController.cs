
using CmsBase.Controllers.Common;
using CMS.Utility;
using CMS.Utility.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Security;
using static System.Runtime.InteropServices.JavaScript.JSType;
using CmsBase.DataAccess;

namespace CmsBase.Controllers
{
    [RequireCmsSession]
    public class AdminController : Controller
    {
        private UserFunction Permission { get { return SessionHelper.GetPermission(HttpContext.Session) ?? new UserFunction(); } }
        private readonly ILogger<AdminController> _logger;
        public readonly CmsDataAccess _da;

        public AdminController(ILogger<AdminController> logger, CmsDataAccess da)
        {
            _logger = logger;
            _da = da;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult ManagerFunction()
        {
            var functions = new List<Functions>();
            var session = SessionHelper.GetUser(HttpContext.Session);
            try
            {
                var userValidate = new UserValidate(_da);
                var checkPermission = userValidate.CheckPermissionUser(HttpContext.Session, "ManagerFunction", Int32.Parse(session.Id), session.IsAdmin);
                if (!checkPermission || Permission == null)
                {
                    SessionHelper.Clear(HttpContext.Session);
                    return View("AccessDenied");
                }
                TempData["Permission"] = JsonConvert.SerializeObject(Permission);

                functions = _da.SP_Functions_SelectByCondition_List(-1, "", -1, -1)
                   .Select(f => new Functions
                   {
                       FunctionID = f.FunctionID,
                       FunctionName = f.FunctionName,
                       Url = f.Url,
                       IsDisplay = f.IsDisplay ?? false,
                       IsActive = f.IsActive ?? false,
                       ParentID = f.ParentID ?? 0,
                       FatherName = "",
                       Order = f.Order ?? 0,
                       ActionName = f.ActionName
                   })
                   .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("ManagerFunction Exception : " + ex.ToString());
            }
            return View(functions);
        }

        public IActionResult FunctionInsertUpdate(int functionId = 0)
        {
            var funcInfo = new SP_Functions_GetByFunctionIDModel();
            var session = SessionHelper.GetUser(HttpContext.Session);
            try
            {
                var userValidate = new UserValidate(_da);
                var checkPermission = userValidate.CheckPermissionUser(HttpContext.Session, "ManagerFunction", Int32.Parse(session.Id), session.IsAdmin);
                if (!checkPermission || Permission == null)
                {
                    SessionHelper.Clear(HttpContext.Session);
                    return View("AccessDenied");
                }
                var functions = _da.SP_Functions_SelectByCondition_List(-1, "", -1, -1)
                   .Select(f => new Functions
                   {
                       FunctionID = f.FunctionID,
                       FunctionName = f.FunctionName,
                       Url = f.Url,
                       IsDisplay = f.IsDisplay ?? false,
                       IsActive = f.IsActive ?? false,
                       ParentID = f.ParentID ?? 0,
                       FatherName = "",
                       Order = f.Order ?? 0,
                       ActionName = f.ActionName
                   })
                   .ToList();
                if (functionId > 0)
                {
                    funcInfo = _da.SP_Functions_GetByFunctionID_List(functionId).ToList().FirstOrDefault();
                }
                ViewBag.IsCreate = functionId;

                var lstFAll = _da.SP_Functions_SelectByCondition_List(-1, "", -1, -1).ToList();
                ViewBag.Functions = lstFAll;
            }
            catch (Exception ex)
            {
                _logger.LogError("FunctionInsertUpdate Exception : " + ex.ToString());
            }
            return View(funcInfo);
        }
        [HttpPost]
        public IActionResult SaveFunction()
        {
            string errorText = "";
            int functionId = 0;
            try
            {
                var session = SessionHelper.GetUser(HttpContext.Session);
                int.TryParse(Request.Form["function_id"].FirstOrDefault() ?? "0", out functionId);

                string function_name = Request.Form["function_name"].FirstOrDefault() ?? "";
                if (string.IsNullOrEmpty(function_name))
                {
                    TempData["Error"] = "Tên chức năng không được để trống!";
                    return RedirectToAction("FunctionInsertUpdate", new { functionId = functionId });
                }

                string action_name = Request.Form["action_name"].FirstOrDefault() ?? "";
                string url = Request.Form["url"].FirstOrDefault() ?? "";
                string css_icon = Request.Form["css_icon"].FirstOrDefault() ?? "";

                int parentId = 0;
                int order = 0;
                int.TryParse(Request.Form["ParentID"].FirstOrDefault() ?? "0", out parentId);
                int.TryParse(Request.Form["Order"].FirstOrDefault() ?? "0", out order);

                bool isDisplay = false;
                bool isActive = false;
                // checkbox values: either "1"/"on" or missing
                var isDisplayRaw = Request.Form["IsDisplay"].FirstOrDefault();
                var isActiveRaw = Request.Form["IsActive"].FirstOrDefault();
                if (!string.IsNullOrEmpty(isDisplayRaw) && (isDisplayRaw == "1" || isDisplayRaw.ToLower() == "on" || isDisplayRaw.ToLower() == "true"))
                    isDisplay = true;
                if (!string.IsNullOrEmpty(isActiveRaw) && (isActiveRaw == "1" || isActiveRaw.ToLower() == "on" || isActiveRaw.ToLower() == "true"))
                    isActive = true;

                var res = _da.SP_Functions_Update_Output(functionId, function_name, action_name, url, isDisplay, isActive, parentId, order, css_icon);

                if (res._ResponseCode > 0)
                {
                    TempData["Success"] = functionId == 0 ? "Tạo chức năng thành công!" : "Sửa chức năng thành công!";
                }
                else
                {
                    TempData["Error"] = functionId == 0 ? "Tạo chức năng không thành công!" : "Sửa chức năng không thành công!";
                }
            }
            catch (Exception ex)
            {
                errorText = "Lỗi hệ thống Exception : " + ex.ToString();
                _logger.LogError("SaveFunction : " + ex.ToString());
                TempData["Error"] = errorText;
                return View("FunctionInsertUpdate");
            }

            return RedirectToAction("FunctionInsertUpdate", new { functionId = functionId });
        }

        [HttpPost]
        public IActionResult DeleteFunction(int functionId)
        {
            var session = SessionHelper.GetUser(HttpContext.Session);
            try
            {
                string msg = "";

                var userValidate = new UserValidate(_da);
                var checkPermission = userValidate.CheckPermissionUser(HttpContext.Session, "ManagerFunction", Int32.Parse(session.Id), session.IsAdmin);
                if (!checkPermission || Permission == null || Permission.IsDelete == false)
                {
                    return View("AccessDenied");
                }
                if (functionId > 0)
                {
                    var function = _da.SP_Functions_GetByFunctionID_List(functionId).ToList().FirstOrDefault();
                    if (function == null || function.FunctionID <= 0)
                    {
                        return Json(new { success = false, message = "Không tìm thấy chức năng này" });
                    }
                    else
                    {
                        var res = _da.SP_Functions_Delete_Output(functionId);
                        if (res._ResponseCode > 0)
                        {
                            //ghi log 
                            var Log = new UsersLog();
                            Log.FunctionID = 9999;
                            Log.ClientIP = AppConfig.GetClientIP(HttpContext);
                            Log.UserID = Int32.Parse(session.Id);
                            Log.LogType = 1;
                            Log.FunctionName = "Xóa chức năng";
                            Log.Description = "Tài khoản " + session.Name + " Xóa thành công chức năng " + function.FunctionName;
                            var insertLog = _da.SP_UserLogs_Insert_Output(Int32.Parse(session.Id), Log.FunctionID, Log.FunctionName, Log.Description, Log.LogType, Log.ClientIP);

                            return Json(new { success = true, message = "Xóa chức năng thành công" });
                        }
                        else
                        {
                            switch (res._ResponseCode)
                            {
                                case -24: msg = "Chức năng đang trong trạng thái hoạt động. Hãy tắt trạng thái hoạt động của hệ thống trước khi xóa !"; break;
                                case -25: msg = "Chức năng không tồn tại trong hệ thống"; break;
                                case -99: msg = "Hệ thống đang bận. Vui lòng quay lại sau"; break;
                                default: msg = "Hệ thống đang bận. Vui lòng quay lại sau"; break;
                            }
                        }
                    }

                }
                return Json(new { success = false, message = msg });
            }
            catch (Exception ex)
            {
                _logger.LogError("DeleteFunction : " + ex.ToString());
                return Json(new { success = false, message = "Xóa chức năng không thành công" });
            }
        }

        public IActionResult ManagerUser()
        {
            var session = SessionHelper.GetUser(HttpContext.Session);
            
            var lstUser = new List<SP_User_GetPageModel>();
            try
            {
                var userValidate = new UserValidate(_da);
                var checkPermission = userValidate.CheckPermissionUser(HttpContext.Session, "ManagerUser", Int32.Parse(session.Id), session.IsAdmin);
                if (!checkPermission || Permission == null )
                {
                    return View("AccessDenied");
                }
                var isGrant = -1;
                if (!session.IsAdmin)
                {
                    isGrant = 1;
                }
                lstUser = _da.SP_User_GetPage_List("", -1, isGrant).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("ManagerUser Exception : " + ex.ToString());
            }
            return View(lstUser);
        }

        public IActionResult UserInsertUpdate(int userID = 0)
        {
            var session = SessionHelper.GetUser(HttpContext.Session);
            var userInfo = new SP_User_GetByConditionModel();
            try
            {
                var userValidate = new UserValidate(_da);
                var checkPermission = userValidate.CheckPermissionUser(HttpContext.Session, "ManagerUser", Int32.Parse(session.Id), session.IsAdmin);
                if (!checkPermission || Permission == null)
                {
                    return View("AccessDenied");
                }
                if(userID == 0 && Permission.IsInsert == false)
                {
                    return View("AccessDenied");
                }
                if (userID > 0)
                { 
                    if(Permission.IsUpdate == false)
                        {
                        return View("AccessDenied");
                    }
                    userInfo = _da.SP_User_GetByCondition_List("", "", userID).ToList().FirstOrDefault();
                }

                ViewBag.IsCreate = userID;

                var lstRole = _da.SP_Role_GetAll_List().ToList();
                ViewBag.ListRole = lstRole;
            }
            catch (Exception ex)
            {
                _logger.LogError("UserInsertUpdate Exception : " + ex.ToString());
            }
            return View(userInfo);
        }

        [HttpPost]
        public IActionResult SaveUser()
        {
            string errorText = "";
            int userId = 0;
            try
            {
                var session = SessionHelper.GetUser(HttpContext.Session);
                int.TryParse(Request.Form["user_id"].FirstOrDefault() ?? "0", out userId);
                string user_name = Request.Form["user_name"].FirstOrDefault() ?? Request.Form["user_name"].FirstOrDefault() ?? "";
                if(string.IsNullOrEmpty(user_name))
                {
                    TempData["Error"] = "Tên người dùng không được để trống!";
                    return RedirectToAction("UserInsertUpdate", new { userID = userId });
                }
                string full_name = Request.Form["full_name"].FirstOrDefault() ?? Request.Form["full_name"].FirstOrDefault() ?? "";
                if (string.IsNullOrEmpty(full_name))
                {
                    TempData["Error"] = "Họ tên không được để trống!";
                    return RedirectToAction("UserInsertUpdate", new { userID = userId });
                }
                string email = Request.Form["email"].FirstOrDefault() ?? Request.Form["email"].FirstOrDefault() ?? "";
                if (string.IsNullOrEmpty(email))
                {
                    TempData["Error"] = "Email không được để trống!";
                    return RedirectToAction("UserInsertUpdate", new { userID = userId });
                }
                string password = Request.Form["password"].FirstOrDefault() ?? Request.Form["password"].FirstOrDefault() ?? "";
                string confirm_password = Request.Form["confirm_password"].FirstOrDefault() ?? Request.Form["confirm_password"].FirstOrDefault() ?? "";
                if(password != confirm_password)
                {
                    TempData["Error"] = "Mật khẩu và xác nhận mật khẩu không khớp!";
                    return RedirectToAction("UserInsertUpdate", new { userID = userId });
                }

                int status = 0;
                int role = 0;
                bool isActive = false;  
                password = string.IsNullOrEmpty(password) ? string.Empty : CommonHelper.Md5(password);
                int.TryParse(Request.Form["Status"].FirstOrDefault() ?? Request.Form["Status"].FirstOrDefault() ?? "1", out status);
                int.TryParse(Request.Form["Role"].FirstOrDefault() ?? Request.Form["Role"].FirstOrDefault() ?? "1", out role);
                bool isAdmin = false;



                if (role == 2 || role == 3)
                {
                    isAdmin = false;
                }
                else if (role == 1)
                {
                    isAdmin = true;
                }
                
                if(status == 1)
                {
                    isActive = true;
                }    
                var res = _da.SP_User_InsertUpdate_Output(userId, user_name, email, full_name,  password, isActive, isAdmin, session.Name, role);
                bool isSuccess = false;
                if (res._ResponseStatus > 0)
                {
                    isSuccess = true;
                }
                if (isSuccess)
                {
                    TempData["Success"] = userId == 0 ? "Tạo tài khoản thành công!" : "Sửa tài khoản thành công!";
                }
                else
                {
                    TempData["Error"] = userId == 0 ? "Tạo tài khoản không thành công!" : "Sửa tài khoản không thành công!";
                }
            }
            catch (Exception ex)
            {
                errorText = "Lỗi hệ thống Exception : " + ex.ToString();
                TempData["Error"] = errorText;
                return View("UserInsertUpdate");
            }
            return RedirectToAction("UserInsertUpdate", new { userID = userId });
        }


        [HttpPost]
        public IActionResult DeleteUser(int userid)
        {
            try
            {
                string msg = "";

                var session = SessionHelper.GetUser(HttpContext.Session);

                var userValidate = new UserValidate(_da);
                var checkPermission = userValidate.CheckPermissionUser(HttpContext.Session, "ManagerUser", Int32.Parse(session.Id), session.IsAdmin);
                if (!checkPermission || Permission == null || Permission.IsDelete == false)
                {
                    return View("AccessDenied");
                }
                if (userid > 0)
                {
                    var userI4 = _da.SP_User_GetByCondition_List("", "", userid).ToList().FirstOrDefault();
                    if (userI4 == null || userI4.UserID <= 0)
                    {
                        return Json(new { success = false, message = "Không tìm thấy tài khoản" });
                    }
                    else
                    {
                        var res = _da.SP_User_Delete_Output(userid);

                        if (res._ResponseCode > 0)
                        {
                            //ghi log 
                            var Log = new UsersLog();
                            Log.FunctionID = 9999;
                            Log.ClientIP = AppConfig.GetClientIP(HttpContext);
                            Log.UserID = Int32.Parse(session.Id);
                            Log.LogType = 1;
                            Log.FunctionName = "Xóa user";
                            Log.Description = "Tài khoản " + session.Name + " Xóa thành công tài khoản " + userI4.Username;
                            var insertLog = _da.SP_UserLogs_Insert_Output(Int32.Parse(session.Id), Log.FunctionID, Log.FunctionName, Log.Description, Log.LogType, Log.ClientIP);

                            return Json(new { success = true, message = "Xóa tài khoản thành công" });
                        }
                        else
                        {
                            switch (res._ResponseCode)
                            {
                                case -50: msg = "Tài Khoản không tồn tại"; break;
                                case -99: msg = "Hệ thống đang bận. Vui lòng quay lại sau"; break;
                                default: msg = "Hệ thống đang bận. Vui lòng quay lại sau"; break;
                            }
                        }
                    }
                    
                }
                return Json(new { success = false, message = msg });
            }
            catch (Exception ex)
            {
                _logger.LogError("DeleteUser : " + ex.ToString());
                return Json(new { success = false, message = "Xóa User không thành công" });
            }
        }

        [HttpPost]
        public IActionResult ActionUser(int userid, int type = 0)
        {
            try
            {
                string msg = "";

                var session = SessionHelper.GetUser(HttpContext.Session);
                var userValidate = new UserValidate(_da);
                var checkPermission = userValidate.CheckPermissionUser(HttpContext.Session, "ManagerUser", Int32.Parse(session.Id), session.IsAdmin);
                if (!checkPermission || Permission == null || Permission.IsUpdate == false)
                {
                    return View("AccessDenied");
                }
                if (userid > 0)
                {
                    var userI4 = _da.SP_User_GetByCondition_List("", "", userid).ToList().FirstOrDefault();
                    if (userI4 == null || userI4.UserID <= 0)
                    {
                        return Json(new { success = false, message = "Không tìm thấy tài khoản" });
                    }
                    else
                    {
                        var res = _da.SP_User_UpdateActive_Output(userid);

                        if (res._ResponseCode > 0)
                        {
                            //ghi log 
                            var Log = new UsersLog();
                            Log.FunctionID = 9999;
                            Log.ClientIP = AppConfig.GetClientIP(HttpContext);
                            Log.UserID = Int32.Parse(session.Id);
                            Log.LogType = 1;
                            Log.FunctionName = "Xóa user";
                            if(type == 0)
                            {
                                Log.Description = "Tài khoản " + session.Name + " vô hiệu hóa tài khoản " + userI4.Username;
                                msg = "Vô hiệu hóa tài khoản thành công";
                            }
                            else
                            {
                                Log.Description = "Tài khoản " + session.Name + " kích hoạt tài khoản " + userI4.Username;
                                msg = "Kích hoạt tài khoản thành công";
                            }    

                            var insertLog = _da.SP_UserLogs_Insert_Output(Int32.Parse(session.Id), Log.FunctionID, Log.FunctionName, Log.Description, Log.LogType, Log.ClientIP);

                            return Json(new { success = true, message = msg });
                        }
                        else
                        {
                            switch (res._ResponseCode)
                            {
                                case -50: msg = "Tài Khoản không tồn tại"; break;
                                case -99: msg = "Hệ thống đang bận. Vui lòng quay lại sau"; break;
                                default: msg = "Hệ thống đang bận. Vui lòng quay lại sau"; break;
                            }
                        }
                    }

                }
                return Json(new { success = false, message = msg });
            }
            catch (Exception ex)
            {
                _logger.LogError("DeleteUser : " + ex.ToString());
                return Json(new { success = false, message = "Xóa User không thành công" });
            }
        }


        public IActionResult GetGrantUser(int userId)
        {
            var session = SessionHelper.GetUser(HttpContext.Session);
            try
            {
                var listFunction = new List<FunctionsGrant>();

                var userInfo = _da.SP_User_GetByCondition_List("", "", userId).FirstOrDefault();
                var userGrant = _da.SP_UserFunction_GetByUserID_List(userId).ToList();

                var currentUserId = Int32.Parse(session.Id);

                // Keep Order from SP so we can preserve display order
                var functionsProjection = new List<(int FunctionID, int ParentID, string FunctionName, string CssIcon, int Order)>();

                if (session.IsAdmin)
                {
                    var lstFunctions = _da.SP_Functions_SelectByCondition_List(-1, "", -1, -1).ToList();
                    functionsProjection = lstFunctions
                        .Select(f => (f.FunctionID, f.ParentID ?? 0, f.FunctionName ?? "", f.CssIcon ?? "", f.Order ?? int.MaxValue))
                        .ToList();
                }
                else
                {
                    var lst = _da.SP_Function_GetByUserID_List(currentUserId, 1).ToList();
                    functionsProjection = lst
                        .Select(f => (f.FunctionID, f.ParentID ?? 0, f.FunctionName ?? "", f.CssIcon ?? "", f.Order ?? int.MaxValue))
                        .ToList();
                }

                // Preserve the same display order as the source list by ordering on the stored Order value
                var orderedFunctions = functionsProjection
                    .OrderBy(f => f.Order)
                    .ThenBy(f => f.FunctionID)
                    .ToList();
                
                // Merge: for each function in the ordered list, take grant fields from userGrant (if any)
                foreach (var fn in orderedFunctions)
                {
                    var ug = userGrant.FirstOrDefault(x => x.FunctionID == fn.FunctionID);
                    var fg = new FunctionsGrant
                    {
                        FunctionID = fn.FunctionID,
                        ParentID = fn.ParentID,
                        FunctionName = fn.FunctionName,
                        FatherName = "", // can map from ParentName if needed
                        CssIcon = fn.CssIcon,
                        IsReport = false,
                        IsGrant = ug?.IsGrant ?? false,
                        IsInsert = ug?.IsInsert ?? false,
                        IsUpdate = ug?.IsUpdate ?? false,
                        IsDelete = ug?.IsDelete ?? false
                    };
                    listFunction.Add(fg);
                }

                // Append any userGrant entries referencing functions not present in the source list
                var missingGrants = userGrant.Where(ug => !orderedFunctions.Any(f => f.FunctionID == ug.FunctionID));
                foreach (var mg in missingGrants)
                {
                    var fg = new FunctionsGrant
                    {
                        FunctionID = mg.FunctionID,
                        ParentID = mg.ParentID ?? 0,
                        FunctionName = mg.ActionName ?? "",
                        FatherName = mg.ParentName ?? "",
                        CssIcon = "",
                        IsReport = false,
                        IsGrant = mg.IsGrant,
                        IsInsert = mg.IsInsert,
                        IsUpdate = mg.IsUpdate,
                        IsDelete = mg.IsDelete
                    };
                    listFunction.Add(fg);
                }

                // Expose target user id and user info to the view for binding
                ViewBag.TargetUserId = userId;
                ViewBag.UserInfo = userInfo;

                return View(listFunction);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetGrantUser : " + ex.ToString());
            }
            return View();
        }


        [HttpPost]
        public IActionResult UpdateFunctionOrder([FromBody] List<OrderUpdateModel> items)
        {
            try
            {

                var session = SessionHelper.GetUser(HttpContext.Session);
                var userValidate = new UserValidate(_da);
                if (session is null || !int.TryParse(session.Id, out var userId) ||
                    !userValidate.CheckPermissionUser(HttpContext.Session, "ManagerFunction", userId, session.IsAdmin) ||
                    !Permission.IsGrant || !Permission.IsUpdate)
                {
                    return StatusCode(StatusCodes.Status403Forbidden, new { success = false, message = "Bạn không có quyền cập nhật thứ tự." });
                }

                if (items == null || !items.Any())
                    return Json(new { success = false, message = "Không có dữ liệu để cập nhật" });

                // Call SP_Functions_UpdateOrder_Execute once for each changed item
                foreach (var it in items)
                {
                    try
                    {
                        // Expected stored-proc wrapper: _da.SP_Functions_UpdateOrder_Execute(functionId, parentId, order)
                        // If the actual signature differs, adjust parameters accordingly.
                        _da.SP_Functions_UpdateOrder_Execute(it.id, it.parentId, it.order);
                    }
                    catch (Exception innerEx)
                    {
                        // log and continue to attempt remaining updates
                        _logger.LogWarning("UpdateFunctionOrder - item failed: id={Id}, err={Err}", it.id, innerEx.ToString());
                    }
                }

                return Json(new { success = true, message = "Cập nhật thứ tự thành công" });
            }
            catch (Exception ex)
            {
                _logger.LogError("UpdateFunctionOrder : " + ex.ToString());
                return Json(new { success = false, message = "Cập nhật thứ tự thất bại" });
            }
        }


        public IActionResult SaveGrantUser(List<AddGrantFunction> ListAddGrant, List<DeleteGrantFunction> ListDelGrant, int UserID)
        {
            var session = SessionHelper.GetUser(HttpContext.Session);
            int Id = Int32.Parse(session.Id);
            int Result = -99;
            string msg = "";
            try
            {
                // check permission
                var userValidate = new UserValidate(_da);
                var checkPermission = userValidate.CheckPermissionUser(HttpContext.Session, "ManagerFunction", Id, session.IsAdmin);
                if (!checkPermission || Permission == null)
                {
                    SessionHelper.Clear(HttpContext.Session);
                    return View("AccessDenied");
                }
                var m_user = _da.SP_User_GetByCondition_List("","",UserID).ToList().FirstOrDefault();
                if (m_user == null || m_user.UserID <= 0)
                {
                    return Json(new { success = false, message = "Tài khoản phân quyền không tồn tại"});
                }
                string ListRole = string.Empty;
                string ListDel = string.Empty;
                if (ListDelGrant != null && ListDelGrant.Count > 0)
                {
                    foreach (var Grant in ListDelGrant)
                    {
                        ListDel += Grant.FunctionId + ",";
                    }
                }
                if (ListAddGrant != null && ListAddGrant.Count > 0)
                {
                    foreach (var Grant in ListAddGrant)
                    {
                        ListRole += Grant.FunctionId + "," + Grant.IsGrant + "," + Grant.IsInsert + "," + Grant.IsUpdate + "," + Grant.IsDelete + ";";
                    }
                }


                if (!string.IsNullOrEmpty(ListRole))
                {
                    if (!session.IsAdmin && Permission.IsGrant)
                    {
                        if (!string.IsNullOrEmpty(ListDel))
                        {
                            ListDel = ListDel.Substring(0, ListDel.Length - 1);
                            var res =_da.SP_UserFunction_DeleteList_Output(UserID, ListDel);
                            Result = res._ResponseCode;
                        }
                    }
                    ListRole = ListRole.Substring(0, ListRole.Length - 1);
                    var res2 = _da.SP_UserFunction_Insert_Output(UserID, ListRole, Id, session.IsAdmin);
                    Result = res2._ResponseCode;
                }
                else
                {
                    if (!session.IsAdmin && Permission.IsGrant)
                    {
                        if (!string.IsNullOrEmpty(ListDel))
                        {
                            ListDel = ListDel.Substring(0, ListDel.Length - 1);
                            var res3 = _da.SP_UserFunction_DeleteList_Output(UserID, ListDel);
                            Result = res3._ResponseCode;
                        }
                    }
                    else if (session.IsAdmin)
                    {
                        var res4 = _da.SP_UserFunction_DeleteAllByUserID_Output(UserID);
                        Result = res4._ResponseCode;
                    }
                }

                if (Result >= 0)
                {
                    var role = string.Empty;
                    if (!string.IsNullOrEmpty(ListRole))
                    {
                        msg = "Phân quyền Thành Công";
                        role = ListRole;
                    }
                    else
                    {
                        msg = "Xóa quyền Thành Công";
                        role = ListDel;
                    }


                    var Log = new UsersLog();
                    Log.FunctionID = Permission.FunctionID;
                    Log.ClientIP = AppConfig.GetClientIP(HttpContext);
                    Log.UserID = Id;
                    Log.LogType = 1;
                    Log.FunctionName = Permission.FunctionName;
                    Log.Description = msg + " tài khoản : " + m_user.Username;
                    var insertLog = _da.SP_UserLogs_Insert_Output(Id, Log.FunctionID, Log.FunctionName, Log.Description, Log.LogType, Log.ClientIP);
                    return Json(new { success = true, message = msg });
                }
                else switch (Result)
                {
                    case -50:
                        msg = "Tài khoản không tồn tại";
                        break;
                    case -600:
                        msg = "Tham số truyền vào không hợp lệ";
                        break;
                    default:
                        msg = "Hệ thống đang bận. Vui lòng quay lại sau";
                        break;
                }
                 
                return Json(new { success = false, message = msg });
            }
            catch (Exception ex)
            {
                _logger.LogError("SaveGrantUser : " + ex.ToString());
                return Json(new { success = false, message = "Cập nhật quyền thất bại" });
            }
        }
    }
}

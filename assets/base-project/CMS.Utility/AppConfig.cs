using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Utility
{
    public static class AppConfig
    {
        private static int max_request = 0;
        private static int time_request = 0;
        private static List<string> ip_white_list = new List<string>() { };
        private static int redis_client = 0;

        public static IConfiguration? configuration { get; set; }
        public static string GetClientIP(HttpContext context)
        {
            var ip = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(ip))
            {
                ip = context.Request.Headers["X-Real-IP"].FirstOrDefault();
            }
            if (string.IsNullOrEmpty(ip))
            {
                ip = context.Connection.RemoteIpAddress?.ToString();
            }
            // Lấy IP đầu tiên nếu có nhiều IP
            if (!string.IsNullOrEmpty(ip) && ip.Contains(","))
            {
                ip = ip.Split(',')[0].Trim();
            }
            return ip ?? "unknown";
        }
        public static int GetMaxRequest()
        {
            if (max_request > 0)
            {
                return max_request;
            }
            max_request = Convert.ToInt32(configuration?.GetSection("appSettings")["max_request"] ?? "600");
            return max_request;
        }
        public static int GetTimeRequest()
        {
            if (time_request > 0)
            {
                return time_request;
            }
            time_request = Convert.ToInt32(configuration?.GetSection("appSettings")["time_request"] ?? "2");
            return time_request;
        }
        public static List<string> GetIPWhiteList()
        {
            if (ip_white_list.Count > 0)
            {
                return ip_white_list;
            }
            string ip = AppSettings("ip_white_list") ?? "['117.103.']";
            var ip_white_list_temp = JsonConvert.DeserializeObject<List<string>>(ip);
            if (ip_white_list_temp.Count > 0)
            {
                ip_white_list_temp.RemoveAll(xxx => string.IsNullOrWhiteSpace(xxx));
                ip_white_list = ip_white_list_temp;
            }
            return ip_white_list;
        }
        public static int GetRedisClient()
        {
            if (redis_client > 0)
            {
                return redis_client;
            }
            redis_client = Convert.ToInt32(AppSettings("redis_client") ?? "100");
            return redis_client;
        }
        public static int GetRedisConfig()
        {
            try
            {
                string MachineName1 = Environment.MachineName;
                string hostName = System.Net.Dns.GetHostName();
                string hostName2 = System.Environment.GetEnvironmentVariable("COMPUTERNAME");

            }
            catch { }
            if (redis_client > 0)
            {
                return redis_client;
            }
            redis_client = Convert.ToInt32(AppSettings("redis_client") ?? "100");
            return redis_client;
        }

        public static string ConnectionString(string Name)
        {
            return configuration?.GetConnectionString(Name) ?? "";
        }
        public static string AppSettings(string Name)
        {
            return configuration?.GetSection("appSettings")[Name] ?? "";
        }
        public static string JWT(string Name)
        {
            return configuration?.GetSection("JWT")[Name] ?? "";
        }
        public static string GetEmailInfo(string Name)
        {
            return configuration?.GetSection("MailInfo")[Name] ?? "";
        }
    }
}

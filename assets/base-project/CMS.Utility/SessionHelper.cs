using CMS.Utility;
using CMS.Utility.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CMS.Utility
{
    public static class SessionHelper
    {
        private const string KEY = "UserSSO";
        private const string PERMISSION = "PERMISSION";
        public static void SetPermission(ISession session, UserFunction permission)
            => session.SetObject(PERMISSION, permission);
        public static void SetUser(ISession session, UserSession user)
            => session.SetObject(KEY, user);       

        public static UserSession? GetUser(ISession session)
            => session.GetObject<UserSession>(KEY); 
        
        public static UserFunction? GetPermission(ISession session)
            => session.GetObject<UserFunction>(PERMISSION);

        public static bool IsLoggedIn(ISession session)
            => GetUser(session) != null;

        public static void Clear(ISession session)
        {
            session.Remove(KEY);
            session.Remove(PERMISSION);
        }
    }
}

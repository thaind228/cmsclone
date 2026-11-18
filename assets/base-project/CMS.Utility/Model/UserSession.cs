using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Utility.Model
{
    public class UserSession
    {
        public int ResponseCode { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
        public string IpAddress { get; set; }
        public string LoginTime { get; set; }
        public bool IsAdmin { get; set; } = false;
    }
    public class UsersLog
    {
        public int LogID { get; set; }
        public int UserID { get; set; }
        public int FunctionID { get; set; }
        public DateTime CreatedTime { get; set; }
        public string Description { get; set; }
        public int LogType { get; set; }
        public string Fullname { get; set; }
        public string FunctionName { get; set; }
        public string ClientIP { get; set; }
        public string Username { get; set; }
    }
    public class Functions
    {
        public int FunctionID { get; set; }
        public string FunctionName { get; set; }
        public string Url { get; set; }
        public bool IsDisplay { get; set; }
        public bool IsActive { get; set; }
        public int ParentID { get; set; }
        public string FatherName { get; set; }
        public int Order { get; set; }
        public string ActionName { get; set; } //Ten Action
    }
    public class OrderUpdateModel
    {
        public int id { get; set; }
        public int parentId { get; set; }
        public int order { get; set; }
    }
    public class TreeDataFunction
    {
        public int FunctionID { get; set; }
        public string FunctionName { get; set; }
        public int ParentID { get; set; }
        public List<TreeDataFunction> Childrent { get; set; }
        public string CssIcon { get; set; }
        public bool IsReport { get; set; }
        public bool IsView { get; set; }
        public bool IsGrant { get; set; }
        public bool IsInsert { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }
    }
    public class FunctionsGrant
    {
        public int FunctionID { get; set; }
        public int ParentID { get; set; }
        public string FunctionName { get; set; }
        public string FatherName { get; set; }
        public bool IsReport { get; set; }
        public string CssIcon { get; set; }
        public bool IsGrant { get; set; }
        public bool IsInsert { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }
    }
    public class UserFunction
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public int FunctionID { get; set; }
        public string FunctionName { get; set; }
        public string ActionName { get; set; }
        public int FatherID { get; set; }
        public string FatherName { get; set; }
        public bool IsGrant { get; set; }
        public bool IsInsert { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }
        public int CreatedUserID { get; set; }
        public bool IsRead { get; set; }
    }


    public class AddGrantFunction
    {
        public int FunctionId { get; set; }
        public bool IsGrant { get; set; }
        public bool IsInsert { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }
    }

    public class DeleteGrantFunction
    {
        public int FunctionId { get; set; }
    }
}

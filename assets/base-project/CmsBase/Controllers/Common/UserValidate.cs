
using CMS.Utility;
using CMS.Utility.Model;
using CmsBase.DataAccess;

namespace CmsBase.Controllers.Common
{
    public class UserValidate
    {
        public readonly CmsDataAccess _da;
        public UserValidate(CmsDataAccess da)
        {
            _da = da;
        }
        public bool CheckPermissionUser(ISession session,string Action, int UserId, bool IsAdmin = false)
        {

            try
            {
                if (string.IsNullOrEmpty(Action))
                {
                    return false;
                }
                var m_function = _da.SP_Functions_GetByActionName_List(Action).FirstOrDefault();
                if (m_function == null || m_function.FunctionID <= 0)
                    return false;

                var permission = new UserFunction();
                //Nếu user là admin -> full quyền
                if (IsAdmin)
                {
                    permission.FunctionID = m_function.FunctionID;
                    permission.FunctionName = m_function.FunctionName;
                    permission.ActionName = Action;
                    permission.UserID = UserId;
                    permission.IsGrant = true;
                    permission.IsInsert = true;
                    permission.IsUpdate = true;
                    permission.IsDelete = true;
                }
                else //Lấy quyền của user khi truy cập chức năng
                {
                    var permissionDB = _da.SP_CheckPermission_List(UserId, Action).FirstOrDefault();
                    if (permissionDB is null || permissionDB.FunctionID != m_function.FunctionID || !permissionDB.IsGrant)
                        return false;

                    permission.FunctionID = permissionDB.FunctionID;
                    permission.FunctionName = permissionDB.FunctionName;
                    permission.ActionName = permissionDB.ActionName;
                    permission.UserID = permissionDB.UserID;
                    permission.IsGrant = permissionDB.IsGrant;
                    permission.IsInsert = permissionDB.IsInsert;
                    permission.IsUpdate = permissionDB.IsUpdate;
                    permission.IsDelete = permissionDB.IsDelete;
                }

                if (permission == null || permission.FunctionID == 0)
                {
                    SessionHelper.Clear(session);
                    return false;
                }
                SessionHelper.SetPermission(session, permission);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

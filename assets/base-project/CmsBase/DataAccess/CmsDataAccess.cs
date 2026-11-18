using CMS.Utility;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CmsBase.DataAccess
{
    /// <summary>Model tự sinh từ SP_CheckPermission</summary>
    public class SP_CheckPermissionModel
    {
        public int UserID { get; set; }
        public int FunctionID { get; set; }
        public string FunctionName { get; set; } = "";
        public string ActionName { get; set; } = "";
        public bool IsGrant { get; set; }
        public bool IsInsert { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }
    }

    /// <summary>Model tự sinh từ SP_Function_GetByUserID</summary>
    public class SP_Function_GetByUserIDModel
    {
        public int FunctionID { get; set; }
        public string FunctionName { get; set; } = "";
        public string Url { get; set; } = "";
        public bool? IsDisplay { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedTime { get; set; }
        public int? ParentID { get; set; }
        public int? Order { get; set; }
        public string CssIcon { get; set; } = "";
        public string ActionName { get; set; } = "";
    }

    /// <summary>Model tự sinh từ SP_Functions_GetByActionName</summary>
    public class SP_Functions_GetByActionNameModel
    {
        public int FunctionID { get; set; }
        public string FunctionName { get; set; } = "";
        public string Url { get; set; } = "";
        public bool? IsDisplay { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedTime { get; set; }
        public int? ParentID { get; set; }
        public int? Order { get; set; }
        public string CssIcon { get; set; } = "";
        public string ActionName { get; set; } = "";
        public string ParentName { get; set; } = "";
    }

    /// <summary>Model tự sinh từ SP_Functions_GetByFunctionID</summary>
    public class SP_Functions_GetByFunctionIDModel
    {
        public int FunctionID { get; set; }
        public string FunctionName { get; set; } = "";
        public string Url { get; set; } = "";
        public bool? IsDisplay { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedTime { get; set; }
        public int? ParentID { get; set; }
        public int? Order { get; set; }
        public string CssIcon { get; set; } = "";
        public string ActionName { get; set; } = "";
        public string ParentName { get; set; } = "";
    }

    /// <summary>Model tự sinh từ SP_Functions_GetPage</summary>
    public class SP_Functions_GetPageModel
    {
        public long? STT { get; set; }
        public int FunctionID { get; set; }
        public string FunctionName { get; set; } = "";
        public string Url { get; set; } = "";
        public bool? IsDisplay { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedTime { get; set; }
        public int? ParentID { get; set; }
        public int? Order { get; set; }
        public string CssIcon { get; set; } = "";
        public string ActionName { get; set; } = "";
        public string ParentName { get; set; } = "";
    }

    /// <summary>Model tự sinh từ SP_Functions_GetParent</summary>
    public class SP_Functions_GetParentModel
    {
        public int FunctionID { get; set; }
        public string FunctionName { get; set; } = "";
        public string Url { get; set; } = "";
        public bool? IsDisplay { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedTime { get; set; }
        public int? ParentID { get; set; }
        public int? Order { get; set; }
        public string CssIcon { get; set; } = "";
        public string ActionName { get; set; } = "";
        public string ParentName { get; set; } = "";
    }

    /// <summary>Model tự sinh từ SP_Functions_SelectByCondition</summary>
    public class SP_Functions_SelectByConditionModel
    {
        public int FunctionID { get; set; }
        public string FunctionName { get; set; } = "";
        public string Url { get; set; } = "";
        public bool? IsDisplay { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedTime { get; set; }
        public int? ParentID { get; set; }
        public int? Order { get; set; }
        public string CssIcon { get; set; } = "";
        public string ActionName { get; set; } = "";
        public string ParentName { get; set; } = "";
    }

    /// <summary>Model tự sinh từ SP_Role_GetAll</summary>
    public class SP_Role_GetAllModel
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; } = "";
    }

    /// <summary>Model tự sinh từ SP_User_GetAll_ByStatus</summary>
    public class SP_User_GetAll_ByStatusModel
    {
        public int UserID { get; set; }
        public string Username { get; set; } = "";
        public string Email { get; set; } = "";
        public string FullName { get; set; } = "";
        public string Password { get; set; } = "";
        public bool? IsAdministrator { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string CreatedUser { get; set; } = "";
        public int? Status { get; set; }
    }

    /// <summary>Model tự sinh từ SP_User_GetByCondition</summary>
    public class SP_User_GetByConditionModel
    {
        public int UserID { get; set; }
        public string Username { get; set; } = "";
        public string Email { get; set; } = "";
        public string FullName { get; set; } = "";
        public string Password { get; set; } = "";
        public bool? IsAdministrator { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string CreatedUser { get; set; } = "";
        public int? Status { get; set; }
        public int? RoleID { get; set; }
    }

    /// <summary>Model tự sinh từ SP_User_GetList_ByRoleFunctionID</summary>
    public class SP_User_GetList_ByRoleFunctionIDModel
    {
        public int UserID { get; set; }
        public int FunctionID { get; set; }
        public bool IsGrant { get; set; }
        public bool IsInsert { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }
    }

    /// <summary>Model tự sinh từ SP_User_GetPage</summary>
    public class SP_User_GetPageModel
    {
        public int UserID { get; set; }
        public string Username { get; set; } = "";
        public string Email { get; set; } = "";
        public string FullName { get; set; } = "";
        public string Password { get; set; } = "";
        public bool? IsAdministrator { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string CreatedUser { get; set; } = "";
        public int? Status { get; set; }
    }

    /// <summary>Model tự sinh từ SP_UserFunction_GetByUserID</summary>
    public class SP_UserFunction_GetByUserIDModel
    {
        public int UserID { get; set; }
        public int FunctionID { get; set; }
        public bool IsGrant { get; set; }
        public bool IsInsert { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedTime { get; set; }
        public int CratedUserID { get; set; }
        public string ActionName { get; set; } = "";
        public int? ParentID { get; set; }
        public string ParentName { get; set; } = "";
    }

    /// <summary>Model tự sinh từ SP_UserLogs_GetPages</summary>
    public class SP_UserLogs_GetPagesModel
    {
        public int LogID { get; set; }
        public int UserID { get; set; }
        public int FunctionID { get; set; }
        public string Username { get; set; } = "";
        public string Fullname { get; set; } = "";
        public string FunctionName { get; set; } = "";
        public DateTime CreatedTime { get; set; }
        public string Description { get; set; } = "";
        public int? LogType { get; set; }
        public string ClientIP { get; set; } = "";
    }

    /// <summary>OUTPUT parameters của SP_ErrorLog_Delete</summary>
    public class SP_ErrorLog_DeleteResult
    {
        public int ErrorCode { get; set; } = default;
    }

    /// <summary>OUTPUT parameters của SP_Functions_Delete</summary>
    public class SP_Functions_DeleteResult
    {
        public int _ResponseCode { get; set; } = default;
    }

    /// <summary>OUTPUT parameters của SP_Functions_GetPage</summary>
    public class SP_Functions_GetPageResult
    {
        public int _TotalRecord { get; set; } = default;
    }

    /// <summary>OUTPUT parameters của SP_Functions_Update</summary>
    public class SP_Functions_UpdateResult
    {
        public int _ResponseCode { get; set; } = default;
    }

    /// <summary>OUTPUT parameters của SP_LogError</summary>
    public class SP_LogErrorResult
    {
        public int _ErrorCode { get; set; } = default;
    }

    /// <summary>OUTPUT parameters của SP_User_Authenticate</summary>
    public class SP_User_AuthenticateResult
    {
        public long _ResponseStatus { get; set; } = default;
    }

    /// <summary>OUTPUT parameters của SP_User_ChangePassword</summary>
    public class SP_User_ChangePasswordResult
    {
        public int _ResponseCode { get; set; } = default;
    }

    /// <summary>OUTPUT parameters của SP_User_Delete</summary>
    public class SP_User_DeleteResult
    {
        public int _ResponseCode { get; set; } = default;
    }

    /// <summary>OUTPUT parameters của SP_User_InsertUpdate</summary>
    public class SP_User_InsertUpdateResult
    {
        public int _ResponseStatus { get; set; } = default;
    }

    /// <summary>OUTPUT parameters của SP_User_UpdateActive</summary>
    public class SP_User_UpdateActiveResult
    {
        public int _ResponseCode { get; set; } = default;
    }

    /// <summary>OUTPUT parameters của SP_UserFunction_Delete</summary>
    public class SP_UserFunction_DeleteResult
    {
        public int _ResponseCode { get; set; } = default;
    }

    /// <summary>OUTPUT parameters của SP_UserFunction_DeleteAllByUserID</summary>
    public class SP_UserFunction_DeleteAllByUserIDResult
    {
        public int _ResponseCode { get; set; } = default;
    }

    /// <summary>OUTPUT parameters của SP_UserFunction_DeleteList</summary>
    public class SP_UserFunction_DeleteListResult
    {
        public int _ResponseCode { get; set; } = default;
    }

    /// <summary>OUTPUT parameters của SP_UserFunction_Insert</summary>
    public class SP_UserFunction_InsertResult
    {
        public int _ResponseCode { get; set; } = default;
    }

    /// <summary>OUTPUT parameters của SP_UserFunction_InsertList_byFunctionID</summary>
    public class SP_UserFunction_InsertList_byFunctionIDResult
    {
        public int _ResponseCode { get; set; } = default;
    }

    /// <summary>OUTPUT parameters của SP_UserFunction_InsertOne</summary>
    public class SP_UserFunction_InsertOneResult
    {
        public int _ResponseCode { get; set; } = default;
    }

    /// <summary>OUTPUT parameters của SP_UserLogs_Delete</summary>
    public class SP_UserLogs_DeleteResult
    {
        public int _ResponseCode { get; set; } = default;
    }

    /// <summary>OUTPUT parameters của SP_UserLogs_Insert</summary>
    public class SP_UserLogs_InsertResult
    {
        public int _ResponseCode { get; set; } = default;
    }

    public partial class CmsDataAccess
    {
        private readonly DBHelper _db;

        public CmsDataAccess(DBHelper db)
        {
            _db = db;
        }

        // ── SP_CheckPermission ─────────────────────────────────────────────
        /// <summary>Trả về List<SP_CheckPermissionModel></summary>
        public List<SP_CheckPermissionModel> SP_CheckPermission_List(int _UserID, string _ActionName)
        {
            return _db.GetListSP<SP_CheckPermissionModel>("SP_CheckPermission", new SqlParameter[] {
                new SqlParameter("@_UserID", (object)_UserID ?? DBNull.Value),
                new SqlParameter("@_ActionName", (object)_ActionName ?? DBNull.Value)
            });
        }

        /// <summary>Trả về SP_CheckPermissionModel đầu tiên (hoặc null)</summary>
        public SP_CheckPermissionModel SP_CheckPermission_Single(int _UserID, string _ActionName)
        {
            return _db.GetInstanceSP<SP_CheckPermissionModel>("SP_CheckPermission", new SqlParameter[] {
                new SqlParameter("@_UserID", (object)_UserID ?? DBNull.Value),
                new SqlParameter("@_ActionName", (object)_ActionName ?? DBNull.Value)
            });
        }

        /// <summary>Execute INSERT/UPDATE/DELETE</summary>
        public int SP_CheckPermission_Execute(int _UserID, string _ActionName)
        {
            return _db.ExecuteNonQuerySP("SP_CheckPermission", new SqlParameter[] {
                new SqlParameter("@_UserID", (object)_UserID ?? DBNull.Value),
                new SqlParameter("@_ActionName", (object)_ActionName ?? DBNull.Value)
            });
        }

        /// <summary>Trả về scalar (COUNT, ID...)</summary>
        public object SP_CheckPermission_Scalar(int _UserID, string _ActionName)
        {
            return _db.ExecuteScalarSP("SP_CheckPermission", new SqlParameter[] {
                new SqlParameter("@_UserID", (object)_UserID ?? DBNull.Value),
                new SqlParameter("@_ActionName", (object)_ActionName ?? DBNull.Value)
            });
        }

        // ── SP_ErrorLog_Delete ─────────────────────────────────────────────
        /// <summary>Trả về List + OUTPUT parameters</summary>
        public (List<T> Data, SP_ErrorLog_DeleteResult Output) SP_ErrorLog_Delete<T>(DateTime FromDate, DateTime ToDate) where T : new()
        {
            var conn = _db.OpenConnection();
            try
            {
                using var cmd = new SqlCommand("SP_ErrorLog_Delete", conn)
                {
                    CommandType    = CommandType.StoredProcedure,
                    CommandTimeout = 450
                };

                // INPUT
                cmd.Parameters.AddWithValue("@FromDate", (object)FromDate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ToDate", (object)ToDate ?? DBNull.Value);

                // OUTPUT
                var p_ErrorCode = new SqlParameter("@ErrorCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(p_ErrorCode);

                var data = new List<T>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new T();

                        // Map reader -> T via reflection (case-insensitive)
                        var propMap = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                                               .ToDictionary(p => p.Name, StringComparer.OrdinalIgnoreCase);

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            var colName = reader.GetName(i);
                            if (!propMap.TryGetValue(colName, out var prop)) continue;
                            if (!prop.CanWrite) continue;

                            var val = reader.GetValue(i);
                            if (val == DBNull.Value) continue;

                            var targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                            try
                            {
                                var safeValue = Convert.ChangeType(val, targetType);
                                prop.SetValue(item, safeValue);
                            }
                            catch
                            {
                                // ignore conversion errors — optional: log
                            }
                        }

                        data.Add(item);
                    }
                }

                var output = new SP_ErrorLog_DeleteResult
                {
                    ErrorCode = p_ErrorCode.Value == DBNull.Value ? default : (int)p_ErrorCode.Value,
                };

                return (data, output);
            }
            finally
            {
                _db.CloseConnection(conn);
            }
        }

        /// <summary>Chỉ lấy OUTPUT</summary>
        public SP_ErrorLog_DeleteResult SP_ErrorLog_Delete_Output(DateTime FromDate, DateTime ToDate)
        {
            var conn = _db.OpenConnection();
            try
            {
                using var cmd = new SqlCommand("SP_ErrorLog_Delete", conn)
                {
                    CommandType    = CommandType.StoredProcedure,
                    CommandTimeout = 450
                };

                // INPUT
                cmd.Parameters.AddWithValue("@FromDate", (object)FromDate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ToDate", (object)ToDate ?? DBNull.Value);

                // OUTPUT
                var p_ErrorCode = new SqlParameter("@ErrorCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(p_ErrorCode);

                cmd.ExecuteNonQuery();

                return new SP_ErrorLog_DeleteResult
                {
                    ErrorCode = p_ErrorCode.Value == DBNull.Value ? default : (int)p_ErrorCode.Value,
                };
            }
            finally
            {
                _db.CloseConnection(conn);
            }
        }

        // ── SP_ErrorLog_GetPage ─────────────────────────────────────────────
        /// <summary>Trả về List<T></summary>
        public List<T> SP_ErrorLog_GetPage_List<T>(DateTime FromDate, DateTime ToDate)
        {
            return _db.GetListSP<T>("SP_ErrorLog_GetPage", new SqlParameter[] {
                new SqlParameter("@FromDate", (object)FromDate ?? DBNull.Value),
                new SqlParameter("@ToDate", (object)ToDate ?? DBNull.Value)
            });
        }

        /// <summary>Trả về T đầu tiên (hoặc null)</summary>
        public T SP_ErrorLog_GetPage_Single<T>(DateTime FromDate, DateTime ToDate)
        {
            return _db.GetInstanceSP<T>("SP_ErrorLog_GetPage", new SqlParameter[] {
                new SqlParameter("@FromDate", (object)FromDate ?? DBNull.Value),
                new SqlParameter("@ToDate", (object)ToDate ?? DBNull.Value)
            });
        }

        /// <summary>Execute INSERT/UPDATE/DELETE</summary>
        public int SP_ErrorLog_GetPage_Execute(DateTime FromDate, DateTime ToDate)
        {
            return _db.ExecuteNonQuerySP("SP_ErrorLog_GetPage", new SqlParameter[] {
                new SqlParameter("@FromDate", (object)FromDate ?? DBNull.Value),
                new SqlParameter("@ToDate", (object)ToDate ?? DBNull.Value)
            });
        }

        /// <summary>Trả về scalar (COUNT, ID...)</summary>
        public object SP_ErrorLog_GetPage_Scalar(DateTime FromDate, DateTime ToDate)
        {
            return _db.ExecuteScalarSP("SP_ErrorLog_GetPage", new SqlParameter[] {
                new SqlParameter("@FromDate", (object)FromDate ?? DBNull.Value),
                new SqlParameter("@ToDate", (object)ToDate ?? DBNull.Value)
            });
        }

        // ── SP_Function_GetByUserID ─────────────────────────────────────────────
        /// <summary>Trả về List<SP_Function_GetByUserIDModel></summary>
        public List<SP_Function_GetByUserIDModel> SP_Function_GetByUserID_List(int _UserID, int _GetGrant)
        {
            return _db.GetListSP<SP_Function_GetByUserIDModel>("SP_Function_GetByUserID", new SqlParameter[] {
                new SqlParameter("@_UserID", (object)_UserID ?? DBNull.Value),
                new SqlParameter("@_GetGrant", (object)_GetGrant ?? DBNull.Value)
            });
        }

        /// <summary>Trả về SP_Function_GetByUserIDModel đầu tiên (hoặc null)</summary>
        public SP_Function_GetByUserIDModel SP_Function_GetByUserID_Single(int _UserID, int _GetGrant)
        {
            return _db.GetInstanceSP<SP_Function_GetByUserIDModel>("SP_Function_GetByUserID", new SqlParameter[] {
                new SqlParameter("@_UserID", (object)_UserID ?? DBNull.Value),
                new SqlParameter("@_GetGrant", (object)_GetGrant ?? DBNull.Value)
            });
        }

        /// <summary>Execute INSERT/UPDATE/DELETE</summary>
        public int SP_Function_GetByUserID_Execute(int _UserID, int _GetGrant)
        {
            return _db.ExecuteNonQuerySP("SP_Function_GetByUserID", new SqlParameter[] {
                new SqlParameter("@_UserID", (object)_UserID ?? DBNull.Value),
                new SqlParameter("@_GetGrant", (object)_GetGrant ?? DBNull.Value)
            });
        }

        /// <summary>Trả về scalar (COUNT, ID...)</summary>
        public object SP_Function_GetByUserID_Scalar(int _UserID, int _GetGrant)
        {
            return _db.ExecuteScalarSP("SP_Function_GetByUserID", new SqlParameter[] {
                new SqlParameter("@_UserID", (object)_UserID ?? DBNull.Value),
                new SqlParameter("@_GetGrant", (object)_GetGrant ?? DBNull.Value)
            });
        }

        // ── SP_Functions_Delete ─────────────────────────────────────────────
        /// <summary>Trả về List + OUTPUT parameters</summary>
        public (List<T> Data, SP_Functions_DeleteResult Output) SP_Functions_Delete<T>(int _FunctionID) where T : new()
        {
            var conn = _db.OpenConnection();
            try
            {
                using var cmd = new SqlCommand("SP_Functions_Delete", conn)
                {
                    CommandType    = CommandType.StoredProcedure,
                    CommandTimeout = 450
                };

                // INPUT
                cmd.Parameters.AddWithValue("@_FunctionID", (object)_FunctionID ?? DBNull.Value);

                // OUTPUT
                var p__ResponseCode = new SqlParameter("@_ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(p__ResponseCode);

                var data = new List<T>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new T();

                        // Map reader -> T via reflection (case-insensitive)
                        var propMap = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                                               .ToDictionary(p => p.Name, StringComparer.OrdinalIgnoreCase);

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            var colName = reader.GetName(i);
                            if (!propMap.TryGetValue(colName, out var prop)) continue;
                            if (!prop.CanWrite) continue;

                            var val = reader.GetValue(i);
                            if (val == DBNull.Value) continue;

                            var targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                            try
                            {
                                var safeValue = Convert.ChangeType(val, targetType);
                                prop.SetValue(item, safeValue);
                            }
                            catch
                            {
                                // ignore conversion errors — optional: log
                            }
                        }

                        data.Add(item);
                    }
                }

                var output = new SP_Functions_DeleteResult
                {
                    _ResponseCode = p__ResponseCode.Value == DBNull.Value ? default : (int)p__ResponseCode.Value,
                };

                return (data, output);
            }
            finally
            {
                _db.CloseConnection(conn);
            }
        }

        /// <summary>Chỉ lấy OUTPUT</summary>
        public SP_Functions_DeleteResult SP_Functions_Delete_Output(int _FunctionID)
        {
            var conn = _db.OpenConnection();
            try
            {
                using var cmd = new SqlCommand("SP_Functions_Delete", conn)
                {
                    CommandType    = CommandType.StoredProcedure,
                    CommandTimeout = 450
                };

                // INPUT
                cmd.Parameters.AddWithValue("@_FunctionID", (object)_FunctionID ?? DBNull.Value);

                // OUTPUT
                var p__ResponseCode = new SqlParameter("@_ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(p__ResponseCode);

                cmd.ExecuteNonQuery();

                return new SP_Functions_DeleteResult
                {
                    _ResponseCode = p__ResponseCode.Value == DBNull.Value ? default : (int)p__ResponseCode.Value,
                };
            }
            finally
            {
                _db.CloseConnection(conn);
            }
        }

        // ── SP_Functions_GetByActionName ─────────────────────────────────────────────
        /// <summary>Trả về List<SP_Functions_GetByActionNameModel></summary>
        public List<SP_Functions_GetByActionNameModel> SP_Functions_GetByActionName_List(string _ActionName)
        {
            return _db.GetListSP<SP_Functions_GetByActionNameModel>("SP_Functions_GetByActionName", new SqlParameter[] {
                new SqlParameter("@_ActionName", (object)_ActionName ?? DBNull.Value)
            });
        }

        /// <summary>Trả về SP_Functions_GetByActionNameModel đầu tiên (hoặc null)</summary>
        public SP_Functions_GetByActionNameModel SP_Functions_GetByActionName_Single(string _ActionName)
        {
            return _db.GetInstanceSP<SP_Functions_GetByActionNameModel>("SP_Functions_GetByActionName", new SqlParameter[] {
                new SqlParameter("@_ActionName", (object)_ActionName ?? DBNull.Value)
            });
        }

        /// <summary>Execute INSERT/UPDATE/DELETE</summary>
        public int SP_Functions_GetByActionName_Execute(string _ActionName)
        {
            return _db.ExecuteNonQuerySP("SP_Functions_GetByActionName", new SqlParameter[] {
                new SqlParameter("@_ActionName", (object)_ActionName ?? DBNull.Value)
            });
        }

        /// <summary>Trả về scalar (COUNT, ID...)</summary>
        public object SP_Functions_GetByActionName_Scalar(string _ActionName)
        {
            return _db.ExecuteScalarSP("SP_Functions_GetByActionName", new SqlParameter[] {
                new SqlParameter("@_ActionName", (object)_ActionName ?? DBNull.Value)
            });
        }

        // ── SP_Functions_GetByFunctionID ─────────────────────────────────────────────
        /// <summary>Trả về List<SP_Functions_GetByFunctionIDModel></summary>
        public List<SP_Functions_GetByFunctionIDModel> SP_Functions_GetByFunctionID_List(int _FunctionID)
        {
            return _db.GetListSP<SP_Functions_GetByFunctionIDModel>("SP_Functions_GetByFunctionID", new SqlParameter[] {
                new SqlParameter("@_FunctionID", (object)_FunctionID ?? DBNull.Value)
            });
        }

        /// <summary>Trả về SP_Functions_GetByFunctionIDModel đầu tiên (hoặc null)</summary>
        public SP_Functions_GetByFunctionIDModel SP_Functions_GetByFunctionID_Single(int _FunctionID)
        {
            return _db.GetInstanceSP<SP_Functions_GetByFunctionIDModel>("SP_Functions_GetByFunctionID", new SqlParameter[] {
                new SqlParameter("@_FunctionID", (object)_FunctionID ?? DBNull.Value)
            });
        }

        /// <summary>Execute INSERT/UPDATE/DELETE</summary>
        public int SP_Functions_GetByFunctionID_Execute(int _FunctionID)
        {
            return _db.ExecuteNonQuerySP("SP_Functions_GetByFunctionID", new SqlParameter[] {
                new SqlParameter("@_FunctionID", (object)_FunctionID ?? DBNull.Value)
            });
        }

        /// <summary>Trả về scalar (COUNT, ID...)</summary>
        public object SP_Functions_GetByFunctionID_Scalar(int _FunctionID)
        {
            return _db.ExecuteScalarSP("SP_Functions_GetByFunctionID", new SqlParameter[] {
                new SqlParameter("@_FunctionID", (object)_FunctionID ?? DBNull.Value)
            });
        }

        // ── SP_Functions_GetPage ─────────────────────────────────────────────
        /// <summary>Trả về List + OUTPUT parameters</summary>
        public (List<SP_Functions_GetPageModel> Data, SP_Functions_GetPageResult Output) SP_Functions_GetPage(string _Keyword, int _Status, int _CurrPage, int _RecordPerPage)
        {
            var conn = _db.OpenConnection();
            try
            {
                using var cmd = new SqlCommand("SP_Functions_GetPage", conn)
                {
                    CommandType    = CommandType.StoredProcedure,
                    CommandTimeout = 450
                };

                // INPUT
                cmd.Parameters.AddWithValue("@_Keyword", (object)_Keyword ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_Status", (object)_Status ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_CurrPage", (object)_CurrPage ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_RecordPerPage", (object)_RecordPerPage ?? DBNull.Value);

                // OUTPUT
                var p__TotalRecord = new SqlParameter("@_TotalRecord", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(p__TotalRecord);

                var data = new List<SP_Functions_GetPageModel>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new SP_Functions_GetPageModel();

                        item.STT = reader["STT"] == DBNull.Value ? default : (long)reader["STT"];
                        item.FunctionID = reader["FunctionID"] == DBNull.Value ? default : (int)reader["FunctionID"];
                        item.FunctionName = reader["FunctionName"] == DBNull.Value ? default : (string)reader["FunctionName"];
                        item.Url = reader["Url"] == DBNull.Value ? default : (string)reader["Url"];
                        item.IsDisplay = reader["IsDisplay"] == DBNull.Value ? default : (bool)reader["IsDisplay"];
                        item.IsActive = reader["IsActive"] == DBNull.Value ? default : (bool)reader["IsActive"];
                        item.CreatedTime = reader["CreatedTime"] == DBNull.Value ? default : (DateTime)reader["CreatedTime"];
                        item.ParentID = reader["ParentID"] == DBNull.Value ? default : (int)reader["ParentID"];
                        item.Order = reader["Order"] == DBNull.Value ? default : (int)reader["Order"];
                        item.CssIcon = reader["CssIcon"] == DBNull.Value ? default : (string)reader["CssIcon"];
                        item.ActionName = reader["ActionName"] == DBNull.Value ? default : (string)reader["ActionName"];
                        item.ParentName = reader["ParentName"] == DBNull.Value ? default : (string)reader["ParentName"];

                        data.Add(item);
                    }
                }

                var output = new SP_Functions_GetPageResult
                {
                    _TotalRecord = p__TotalRecord.Value == DBNull.Value ? default : (int)p__TotalRecord.Value,
                };

                return (data, output);
            }
            finally
            {
                _db.CloseConnection(conn);
            }
        }

        /// <summary>Chỉ lấy OUTPUT</summary>
        public SP_Functions_GetPageResult SP_Functions_GetPage_Output(string _Keyword, int _Status, int _CurrPage, int _RecordPerPage)
        {
            var conn = _db.OpenConnection();
            try
            {
                using var cmd = new SqlCommand("SP_Functions_GetPage", conn)
                {
                    CommandType    = CommandType.StoredProcedure,
                    CommandTimeout = 450
                };

                // INPUT
                cmd.Parameters.AddWithValue("@_Keyword", (object)_Keyword ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_Status", (object)_Status ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_CurrPage", (object)_CurrPage ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_RecordPerPage", (object)_RecordPerPage ?? DBNull.Value);

                // OUTPUT
                var p__TotalRecord = new SqlParameter("@_TotalRecord", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(p__TotalRecord);

                cmd.ExecuteNonQuery();

                return new SP_Functions_GetPageResult
                {
                    _TotalRecord = p__TotalRecord.Value == DBNull.Value ? default : (int)p__TotalRecord.Value,
                };
            }
            finally
            {
                _db.CloseConnection(conn);
            }
        }

        // ── SP_Functions_GetParent ─────────────────────────────────────────────
        /// <summary>Trả về List<SP_Functions_GetParentModel></summary>
        public List<SP_Functions_GetParentModel> SP_Functions_GetParent_List(int _ParentID)
        {
            return _db.GetListSP<SP_Functions_GetParentModel>("SP_Functions_GetParent", new SqlParameter[] {
                new SqlParameter("@_ParentID", (object)_ParentID ?? DBNull.Value)
            });
        }

        /// <summary>Trả về SP_Functions_GetParentModel đầu tiên (hoặc null)</summary>
        public SP_Functions_GetParentModel SP_Functions_GetParent_Single(int _ParentID)
        {
            return _db.GetInstanceSP<SP_Functions_GetParentModel>("SP_Functions_GetParent", new SqlParameter[] {
                new SqlParameter("@_ParentID", (object)_ParentID ?? DBNull.Value)
            });
        }

        /// <summary>Execute INSERT/UPDATE/DELETE</summary>
        public int SP_Functions_GetParent_Execute(int _ParentID)
        {
            return _db.ExecuteNonQuerySP("SP_Functions_GetParent", new SqlParameter[] {
                new SqlParameter("@_ParentID", (object)_ParentID ?? DBNull.Value)
            });
        }

        /// <summary>Trả về scalar (COUNT, ID...)</summary>
        public object SP_Functions_GetParent_Scalar(int _ParentID)
        {
            return _db.ExecuteScalarSP("SP_Functions_GetParent", new SqlParameter[] {
                new SqlParameter("@_ParentID", (object)_ParentID ?? DBNull.Value)
            });
        }

        // ── SP_Functions_SelectByCondition ─────────────────────────────────────────────
        /// <summary>Trả về List<SP_Functions_SelectByConditionModel></summary>
        public List<SP_Functions_SelectByConditionModel> SP_Functions_SelectByCondition_List(int _ParentID, string _FunctionName, int _IsActive, int _IsDisplay)
        {
            return _db.GetListSP<SP_Functions_SelectByConditionModel>("SP_Functions_SelectByCondition", new SqlParameter[] {
                new SqlParameter("@_ParentID", (object)_ParentID ?? DBNull.Value),
                new SqlParameter("@_FunctionName", (object)_FunctionName ?? DBNull.Value),
                new SqlParameter("@_IsActive", (object)_IsActive ?? DBNull.Value),
                new SqlParameter("@_IsDisplay", (object)_IsDisplay ?? DBNull.Value)
            });
        }

        /// <summary>Trả về SP_Functions_SelectByConditionModel đầu tiên (hoặc null)</summary>
        public SP_Functions_SelectByConditionModel SP_Functions_SelectByCondition_Single(int _ParentID, string _FunctionName, int _IsActive, int _IsDisplay)
        {
            return _db.GetInstanceSP<SP_Functions_SelectByConditionModel>("SP_Functions_SelectByCondition", new SqlParameter[] {
                new SqlParameter("@_ParentID", (object)_ParentID ?? DBNull.Value),
                new SqlParameter("@_FunctionName", (object)_FunctionName ?? DBNull.Value),
                new SqlParameter("@_IsActive", (object)_IsActive ?? DBNull.Value),
                new SqlParameter("@_IsDisplay", (object)_IsDisplay ?? DBNull.Value)
            });
        }

        /// <summary>Execute INSERT/UPDATE/DELETE</summary>
        public int SP_Functions_SelectByCondition_Execute(int _ParentID, string _FunctionName, int _IsActive, int _IsDisplay)
        {
            return _db.ExecuteNonQuerySP("SP_Functions_SelectByCondition", new SqlParameter[] {
                new SqlParameter("@_ParentID", (object)_ParentID ?? DBNull.Value),
                new SqlParameter("@_FunctionName", (object)_FunctionName ?? DBNull.Value),
                new SqlParameter("@_IsActive", (object)_IsActive ?? DBNull.Value),
                new SqlParameter("@_IsDisplay", (object)_IsDisplay ?? DBNull.Value)
            });
        }

        /// <summary>Trả về scalar (COUNT, ID...)</summary>
        public object SP_Functions_SelectByCondition_Scalar(int _ParentID, string _FunctionName, int _IsActive, int _IsDisplay)
        {
            return _db.ExecuteScalarSP("SP_Functions_SelectByCondition", new SqlParameter[] {
                new SqlParameter("@_ParentID", (object)_ParentID ?? DBNull.Value),
                new SqlParameter("@_FunctionName", (object)_FunctionName ?? DBNull.Value),
                new SqlParameter("@_IsActive", (object)_IsActive ?? DBNull.Value),
                new SqlParameter("@_IsDisplay", (object)_IsDisplay ?? DBNull.Value)
            });
        }

        // ── SP_Functions_Update ─────────────────────────────────────────────
        /// <summary>Trả về List + OUTPUT parameters</summary>
        public (List<T> Data, SP_Functions_UpdateResult Output) SP_Functions_Update<T>(int _FunctionID, string _FunctionName, string _ActionName, string _Url, bool _IsDisplay, bool _IsActive, int _ParentID, int _Order, string _CssIcon) where T : new()
        {
            var conn = _db.OpenConnection();
            try
            {
                using var cmd = new SqlCommand("SP_Functions_Update", conn)
                {
                    CommandType    = CommandType.StoredProcedure,
                    CommandTimeout = 450
                };

                // INPUT
                cmd.Parameters.AddWithValue("@_FunctionID", (object)_FunctionID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_FunctionName", (object)_FunctionName ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_ActionName", (object)_ActionName ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_Url", (object)_Url ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_IsDisplay", (object)_IsDisplay ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_IsActive", (object)_IsActive ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_ParentID", (object)_ParentID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_Order", (object)_Order ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_CssIcon", (object)_CssIcon ?? DBNull.Value);

                // OUTPUT
                var p__ResponseCode = new SqlParameter("@_ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(p__ResponseCode);

                var data = new List<T>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new T();

                        // Map reader -> T via reflection (case-insensitive)
                        var propMap = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                                               .ToDictionary(p => p.Name, StringComparer.OrdinalIgnoreCase);

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            var colName = reader.GetName(i);
                            if (!propMap.TryGetValue(colName, out var prop)) continue;
                            if (!prop.CanWrite) continue;

                            var val = reader.GetValue(i);
                            if (val == DBNull.Value) continue;

                            var targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                            try
                            {
                                var safeValue = Convert.ChangeType(val, targetType);
                                prop.SetValue(item, safeValue);
                            }
                            catch
                            {
                                // ignore conversion errors — optional: log
                            }
                        }

                        data.Add(item);
                    }
                }

                var output = new SP_Functions_UpdateResult
                {
                    _ResponseCode = p__ResponseCode.Value == DBNull.Value ? default : (int)p__ResponseCode.Value,
                };

                return (data, output);
            }
            finally
            {
                _db.CloseConnection(conn);
            }
        }

        /// <summary>Chỉ lấy OUTPUT</summary>
        public SP_Functions_UpdateResult SP_Functions_Update_Output(int _FunctionID, string _FunctionName, string _ActionName, string _Url, bool _IsDisplay, bool _IsActive, int _ParentID, int _Order, string _CssIcon)
        {
            var conn = _db.OpenConnection();
            try
            {
                using var cmd = new SqlCommand("SP_Functions_Update", conn)
                {
                    CommandType    = CommandType.StoredProcedure,
                    CommandTimeout = 450
                };

                // INPUT
                cmd.Parameters.AddWithValue("@_FunctionID", (object)_FunctionID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_FunctionName", (object)_FunctionName ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_ActionName", (object)_ActionName ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_Url", (object)_Url ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_IsDisplay", (object)_IsDisplay ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_IsActive", (object)_IsActive ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_ParentID", (object)_ParentID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_Order", (object)_Order ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_CssIcon", (object)_CssIcon ?? DBNull.Value);

                // OUTPUT
                var p__ResponseCode = new SqlParameter("@_ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(p__ResponseCode);

                cmd.ExecuteNonQuery();

                return new SP_Functions_UpdateResult
                {
                    _ResponseCode = p__ResponseCode.Value == DBNull.Value ? default : (int)p__ResponseCode.Value,
                };
            }
            finally
            {
                _db.CloseConnection(conn);
            }
        }

        // ── SP_Functions_UpdateOrder ─────────────────────────────────────────────
        /// <summary>Trả về List<T></summary>
        public List<T> SP_Functions_UpdateOrder_List<T>(int _FunctionID, int _ParentID, int _Order)
        {
            return _db.GetListSP<T>("SP_Functions_UpdateOrder", new SqlParameter[] {
                new SqlParameter("@_FunctionID", (object)_FunctionID ?? DBNull.Value),
                new SqlParameter("@_ParentID", (object)_ParentID ?? DBNull.Value),
                new SqlParameter("@_Order", (object)_Order ?? DBNull.Value)
            });
        }

        /// <summary>Trả về T đầu tiên (hoặc null)</summary>
        public T SP_Functions_UpdateOrder_Single<T>(int _FunctionID, int _ParentID, int _Order)
        {
            return _db.GetInstanceSP<T>("SP_Functions_UpdateOrder", new SqlParameter[] {
                new SqlParameter("@_FunctionID", (object)_FunctionID ?? DBNull.Value),
                new SqlParameter("@_ParentID", (object)_ParentID ?? DBNull.Value),
                new SqlParameter("@_Order", (object)_Order ?? DBNull.Value)
            });
        }

        /// <summary>Execute INSERT/UPDATE/DELETE</summary>
        public int SP_Functions_UpdateOrder_Execute(int _FunctionID, int _ParentID, int _Order)
        {
            return _db.ExecuteNonQuerySP("SP_Functions_UpdateOrder", new SqlParameter[] {
                new SqlParameter("@_FunctionID", (object)_FunctionID ?? DBNull.Value),
                new SqlParameter("@_ParentID", (object)_ParentID ?? DBNull.Value),
                new SqlParameter("@_Order", (object)_Order ?? DBNull.Value)
            });
        }

        /// <summary>Trả về scalar (COUNT, ID...)</summary>
        public object SP_Functions_UpdateOrder_Scalar(int _FunctionID, int _ParentID, int _Order)
        {
            return _db.ExecuteScalarSP("SP_Functions_UpdateOrder", new SqlParameter[] {
                new SqlParameter("@_FunctionID", (object)_FunctionID ?? DBNull.Value),
                new SqlParameter("@_ParentID", (object)_ParentID ?? DBNull.Value),
                new SqlParameter("@_Order", (object)_Order ?? DBNull.Value)
            });
        }

        // ── SP_LogError ─────────────────────────────────────────────
        /// <summary>Trả về List + OUTPUT parameters</summary>
        public (List<T> Data, SP_LogErrorResult Output) SP_LogError<T>() where T : new()
        {
            var conn = _db.OpenConnection();
            try
            {
                using var cmd = new SqlCommand("SP_LogError", conn)
                {
                    CommandType    = CommandType.StoredProcedure,
                    CommandTimeout = 450
                };

                // INPUT

                // OUTPUT
                var p__ErrorCode = new SqlParameter("@_ErrorCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(p__ErrorCode);

                var data = new List<T>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new T();

                        // Map reader -> T via reflection (case-insensitive)
                        var propMap = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                                               .ToDictionary(p => p.Name, StringComparer.OrdinalIgnoreCase);

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            var colName = reader.GetName(i);
                            if (!propMap.TryGetValue(colName, out var prop)) continue;
                            if (!prop.CanWrite) continue;

                            var val = reader.GetValue(i);
                            if (val == DBNull.Value) continue;

                            var targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                            try
                            {
                                var safeValue = Convert.ChangeType(val, targetType);
                                prop.SetValue(item, safeValue);
                            }
                            catch
                            {
                                // ignore conversion errors — optional: log
                            }
                        }

                        data.Add(item);
                    }
                }

                var output = new SP_LogErrorResult
                {
                    _ErrorCode = p__ErrorCode.Value == DBNull.Value ? default : (int)p__ErrorCode.Value,
                };

                return (data, output);
            }
            finally
            {
                _db.CloseConnection(conn);
            }
        }

        /// <summary>Chỉ lấy OUTPUT</summary>
        public SP_LogErrorResult SP_LogError_Output()
        {
            var conn = _db.OpenConnection();
            try
            {
                using var cmd = new SqlCommand("SP_LogError", conn)
                {
                    CommandType    = CommandType.StoredProcedure,
                    CommandTimeout = 450
                };

                // INPUT

                // OUTPUT
                var p__ErrorCode = new SqlParameter("@_ErrorCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(p__ErrorCode);

                cmd.ExecuteNonQuery();

                return new SP_LogErrorResult
                {
                    _ErrorCode = p__ErrorCode.Value == DBNull.Value ? default : (int)p__ErrorCode.Value,
                };
            }
            finally
            {
                _db.CloseConnection(conn);
            }
        }

        // ── SP_Role_GetAll ─────────────────────────────────────────────
        /// <summary>Trả về List<SP_Role_GetAllModel></summary>
        public List<SP_Role_GetAllModel> SP_Role_GetAll_List()
        {
            return _db.GetListSP<SP_Role_GetAllModel>("SP_Role_GetAll");
        }

        /// <summary>Trả về SP_Role_GetAllModel đầu tiên (hoặc null)</summary>
        public SP_Role_GetAllModel SP_Role_GetAll_Single()
        {
            return _db.GetInstanceSP<SP_Role_GetAllModel>("SP_Role_GetAll");
        }

        /// <summary>Execute INSERT/UPDATE/DELETE</summary>
        public int SP_Role_GetAll_Execute()
        {
            return _db.ExecuteNonQuerySP("SP_Role_GetAll");
        }

        /// <summary>Trả về scalar (COUNT, ID...)</summary>
        public object SP_Role_GetAll_Scalar()
        {
            return _db.ExecuteScalarSP("SP_Role_GetAll");
        }

        // ── SP_User_Authenticate ─────────────────────────────────────────────
        /// <summary>Trả về List + OUTPUT parameters</summary>
        public (List<T> Data, SP_User_AuthenticateResult Output) SP_User_Authenticate<T>(string _Username, string _Password, string _ClientIP) where T : new()
        {
            var conn = _db.OpenConnection();
            try
            {
                using var cmd = new SqlCommand("SP_User_Authenticate", conn)
                {
                    CommandType    = CommandType.StoredProcedure,
                    CommandTimeout = 450
                };

                // INPUT
                cmd.Parameters.AddWithValue("@_Username", (object)_Username ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_Password", (object)_Password ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_ClientIP", (object)_ClientIP ?? DBNull.Value);

                // OUTPUT
                var p__ResponseStatus = new SqlParameter("@_ResponseStatus", SqlDbType.BigInt) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(p__ResponseStatus);

                var data = new List<T>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new T();

                        // Map reader -> T via reflection (case-insensitive)
                        var propMap = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                                               .ToDictionary(p => p.Name, StringComparer.OrdinalIgnoreCase);

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            var colName = reader.GetName(i);
                            if (!propMap.TryGetValue(colName, out var prop)) continue;
                            if (!prop.CanWrite) continue;

                            var val = reader.GetValue(i);
                            if (val == DBNull.Value) continue;

                            var targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                            try
                            {
                                var safeValue = Convert.ChangeType(val, targetType);
                                prop.SetValue(item, safeValue);
                            }
                            catch
                            {
                                // ignore conversion errors — optional: log
                            }
                        }

                        data.Add(item);
                    }
                }

                var output = new SP_User_AuthenticateResult
                {
                    _ResponseStatus = p__ResponseStatus.Value == DBNull.Value ? default : (long)p__ResponseStatus.Value,
                };

                return (data, output);
            }
            finally
            {
                _db.CloseConnection(conn);
            }
        }

        /// <summary>Chỉ lấy OUTPUT</summary>
        public SP_User_AuthenticateResult SP_User_Authenticate_Output(string _Username, string _Password, string _ClientIP)
        {
            var conn = _db.OpenConnection();
            try
            {
                using var cmd = new SqlCommand("SP_User_Authenticate", conn)
                {
                    CommandType    = CommandType.StoredProcedure,
                    CommandTimeout = 450
                };

                // INPUT
                cmd.Parameters.AddWithValue("@_Username", (object)_Username ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_Password", (object)_Password ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_ClientIP", (object)_ClientIP ?? DBNull.Value);

                // OUTPUT
                var p__ResponseStatus = new SqlParameter("@_ResponseStatus", SqlDbType.BigInt) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(p__ResponseStatus);

                cmd.ExecuteNonQuery();

                return new SP_User_AuthenticateResult
                {
                    _ResponseStatus = p__ResponseStatus.Value == DBNull.Value ? default : (long)p__ResponseStatus.Value,
                };
            }
            finally
            {
                _db.CloseConnection(conn);
            }
        }

        // ── SP_User_ChangePassword ─────────────────────────────────────────────
        /// <summary>Trả về List + OUTPUT parameters</summary>
        public (List<T> Data, SP_User_ChangePasswordResult Output) SP_User_ChangePassword<T>(string _Username, string _PasswordOld, string _PasswordNew) where T : new()
        {
            var conn = _db.OpenConnection();
            try
            {
                using var cmd = new SqlCommand("SP_User_ChangePassword", conn)
                {
                    CommandType    = CommandType.StoredProcedure,
                    CommandTimeout = 450
                };

                // INPUT
                cmd.Parameters.AddWithValue("@_Username", (object)_Username ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_PasswordOld", (object)_PasswordOld ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_PasswordNew", (object)_PasswordNew ?? DBNull.Value);

                // OUTPUT
                var p__ResponseCode = new SqlParameter("@_ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(p__ResponseCode);

                var data = new List<T>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new T();

                        // Map reader -> T via reflection (case-insensitive)
                        var propMap = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                                               .ToDictionary(p => p.Name, StringComparer.OrdinalIgnoreCase);

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            var colName = reader.GetName(i);
                            if (!propMap.TryGetValue(colName, out var prop)) continue;
                            if (!prop.CanWrite) continue;

                            var val = reader.GetValue(i);
                            if (val == DBNull.Value) continue;

                            var targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                            try
                            {
                                var safeValue = Convert.ChangeType(val, targetType);
                                prop.SetValue(item, safeValue);
                            }
                            catch
                            {
                                // ignore conversion errors — optional: log
                            }
                        }

                        data.Add(item);
                    }
                }

                var output = new SP_User_ChangePasswordResult
                {
                    _ResponseCode = p__ResponseCode.Value == DBNull.Value ? default : (int)p__ResponseCode.Value,
                };

                return (data, output);
            }
            finally
            {
                _db.CloseConnection(conn);
            }
        }

        /// <summary>Chỉ lấy OUTPUT</summary>
        public SP_User_ChangePasswordResult SP_User_ChangePassword_Output(string _Username, string _PasswordOld, string _PasswordNew)
        {
            var conn = _db.OpenConnection();
            try
            {
                using var cmd = new SqlCommand("SP_User_ChangePassword", conn)
                {
                    CommandType    = CommandType.StoredProcedure,
                    CommandTimeout = 450
                };

                // INPUT
                cmd.Parameters.AddWithValue("@_Username", (object)_Username ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_PasswordOld", (object)_PasswordOld ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_PasswordNew", (object)_PasswordNew ?? DBNull.Value);

                // OUTPUT
                var p__ResponseCode = new SqlParameter("@_ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(p__ResponseCode);

                cmd.ExecuteNonQuery();

                return new SP_User_ChangePasswordResult
                {
                    _ResponseCode = p__ResponseCode.Value == DBNull.Value ? default : (int)p__ResponseCode.Value,
                };
            }
            finally
            {
                _db.CloseConnection(conn);
            }
        }

        // ── SP_User_Delete ─────────────────────────────────────────────
        /// <summary>Trả về List + OUTPUT parameters</summary>
        public (List<T> Data, SP_User_DeleteResult Output) SP_User_Delete<T>(int _UserID) where T : new()
        {
            var conn = _db.OpenConnection();
            try
            {
                using var cmd = new SqlCommand("SP_User_Delete", conn)
                {
                    CommandType    = CommandType.StoredProcedure,
                    CommandTimeout = 450
                };

                // INPUT
                cmd.Parameters.AddWithValue("@_UserID", (object)_UserID ?? DBNull.Value);

                // OUTPUT
                var p__ResponseCode = new SqlParameter("@_ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(p__ResponseCode);

                var data = new List<T>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new T();

                        // Map reader -> T via reflection (case-insensitive)
                        var propMap = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                                               .ToDictionary(p => p.Name, StringComparer.OrdinalIgnoreCase);

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            var colName = reader.GetName(i);
                            if (!propMap.TryGetValue(colName, out var prop)) continue;
                            if (!prop.CanWrite) continue;

                            var val = reader.GetValue(i);
                            if (val == DBNull.Value) continue;

                            var targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                            try
                            {
                                var safeValue = Convert.ChangeType(val, targetType);
                                prop.SetValue(item, safeValue);
                            }
                            catch
                            {
                                // ignore conversion errors — optional: log
                            }
                        }

                        data.Add(item);
                    }
                }

                var output = new SP_User_DeleteResult
                {
                    _ResponseCode = p__ResponseCode.Value == DBNull.Value ? default : (int)p__ResponseCode.Value,
                };

                return (data, output);
            }
            finally
            {
                _db.CloseConnection(conn);
            }
        }

        /// <summary>Chỉ lấy OUTPUT</summary>
        public SP_User_DeleteResult SP_User_Delete_Output(int _UserID)
        {
            var conn = _db.OpenConnection();
            try
            {
                using var cmd = new SqlCommand("SP_User_Delete", conn)
                {
                    CommandType    = CommandType.StoredProcedure,
                    CommandTimeout = 450
                };

                // INPUT
                cmd.Parameters.AddWithValue("@_UserID", (object)_UserID ?? DBNull.Value);

                // OUTPUT
                var p__ResponseCode = new SqlParameter("@_ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(p__ResponseCode);

                cmd.ExecuteNonQuery();

                return new SP_User_DeleteResult
                {
                    _ResponseCode = p__ResponseCode.Value == DBNull.Value ? default : (int)p__ResponseCode.Value,
                };
            }
            finally
            {
                _db.CloseConnection(conn);
            }
        }

        // ── SP_User_GetAll_ByStatus ─────────────────────────────────────────────
        /// <summary>Trả về List<SP_User_GetAll_ByStatusModel></summary>
        public List<SP_User_GetAll_ByStatusModel> SP_User_GetAll_ByStatus_List(int _Status)
        {
            return _db.GetListSP<SP_User_GetAll_ByStatusModel>("SP_User_GetAll_ByStatus", new SqlParameter[] {
                new SqlParameter("@_Status", (object)_Status ?? DBNull.Value)
            });
        }

        /// <summary>Trả về SP_User_GetAll_ByStatusModel đầu tiên (hoặc null)</summary>
        public SP_User_GetAll_ByStatusModel SP_User_GetAll_ByStatus_Single(int _Status)
        {
            return _db.GetInstanceSP<SP_User_GetAll_ByStatusModel>("SP_User_GetAll_ByStatus", new SqlParameter[] {
                new SqlParameter("@_Status", (object)_Status ?? DBNull.Value)
            });
        }

        /// <summary>Execute INSERT/UPDATE/DELETE</summary>
        public int SP_User_GetAll_ByStatus_Execute(int _Status)
        {
            return _db.ExecuteNonQuerySP("SP_User_GetAll_ByStatus", new SqlParameter[] {
                new SqlParameter("@_Status", (object)_Status ?? DBNull.Value)
            });
        }

        /// <summary>Trả về scalar (COUNT, ID...)</summary>
        public object SP_User_GetAll_ByStatus_Scalar(int _Status)
        {
            return _db.ExecuteScalarSP("SP_User_GetAll_ByStatus", new SqlParameter[] {
                new SqlParameter("@_Status", (object)_Status ?? DBNull.Value)
            });
        }

        // ── SP_User_GetByCondition ─────────────────────────────────────────────
        /// <summary>Trả về List<SP_User_GetByConditionModel></summary>
        public List<SP_User_GetByConditionModel> SP_User_GetByCondition_List(string _UserName, string _Email, int _UserID)
        {
            return _db.GetListSP<SP_User_GetByConditionModel>("SP_User_GetByCondition", new SqlParameter[] {
                new SqlParameter("@_UserName", (object)_UserName ?? DBNull.Value),
                new SqlParameter("@_Email", (object)_Email ?? DBNull.Value),
                new SqlParameter("@_UserID", (object)_UserID ?? DBNull.Value)
            });
        }

        /// <summary>Trả về SP_User_GetByConditionModel đầu tiên (hoặc null)</summary>
        public SP_User_GetByConditionModel SP_User_GetByCondition_Single(string _UserName, string _Email, int _UserID)
        {
            return _db.GetInstanceSP<SP_User_GetByConditionModel>("SP_User_GetByCondition", new SqlParameter[] {
                new SqlParameter("@_UserName", (object)_UserName ?? DBNull.Value),
                new SqlParameter("@_Email", (object)_Email ?? DBNull.Value),
                new SqlParameter("@_UserID", (object)_UserID ?? DBNull.Value)
            });
        }

        /// <summary>Execute INSERT/UPDATE/DELETE</summary>
        public int SP_User_GetByCondition_Execute(string _UserName, string _Email, int _UserID)
        {
            return _db.ExecuteNonQuerySP("SP_User_GetByCondition", new SqlParameter[] {
                new SqlParameter("@_UserName", (object)_UserName ?? DBNull.Value),
                new SqlParameter("@_Email", (object)_Email ?? DBNull.Value),
                new SqlParameter("@_UserID", (object)_UserID ?? DBNull.Value)
            });
        }

        /// <summary>Trả về scalar (COUNT, ID...)</summary>
        public object SP_User_GetByCondition_Scalar(string _UserName, string _Email, int _UserID)
        {
            return _db.ExecuteScalarSP("SP_User_GetByCondition", new SqlParameter[] {
                new SqlParameter("@_UserName", (object)_UserName ?? DBNull.Value),
                new SqlParameter("@_Email", (object)_Email ?? DBNull.Value),
                new SqlParameter("@_UserID", (object)_UserID ?? DBNull.Value)
            });
        }

        // ── SP_User_GetList_ByRoleFunctionID ─────────────────────────────────────────────
        /// <summary>Trả về List<SP_User_GetList_ByRoleFunctionIDModel></summary>
        public List<SP_User_GetList_ByRoleFunctionIDModel> SP_User_GetList_ByRoleFunctionID_List(int _FunctionID, short _IsGrant)
        {
            return _db.GetListSP<SP_User_GetList_ByRoleFunctionIDModel>("SP_User_GetList_ByRoleFunctionID", new SqlParameter[] {
                new SqlParameter("@_FunctionID", (object)_FunctionID ?? DBNull.Value),
                new SqlParameter("@_IsGrant", (object)_IsGrant ?? DBNull.Value)
            });
        }

        /// <summary>Trả về SP_User_GetList_ByRoleFunctionIDModel đầu tiên (hoặc null)</summary>
        public SP_User_GetList_ByRoleFunctionIDModel SP_User_GetList_ByRoleFunctionID_Single(int _FunctionID, short _IsGrant)
        {
            return _db.GetInstanceSP<SP_User_GetList_ByRoleFunctionIDModel>("SP_User_GetList_ByRoleFunctionID", new SqlParameter[] {
                new SqlParameter("@_FunctionID", (object)_FunctionID ?? DBNull.Value),
                new SqlParameter("@_IsGrant", (object)_IsGrant ?? DBNull.Value)
            });
        }

        /// <summary>Execute INSERT/UPDATE/DELETE</summary>
        public int SP_User_GetList_ByRoleFunctionID_Execute(int _FunctionID, short _IsGrant)
        {
            return _db.ExecuteNonQuerySP("SP_User_GetList_ByRoleFunctionID", new SqlParameter[] {
                new SqlParameter("@_FunctionID", (object)_FunctionID ?? DBNull.Value),
                new SqlParameter("@_IsGrant", (object)_IsGrant ?? DBNull.Value)
            });
        }

        /// <summary>Trả về scalar (COUNT, ID...)</summary>
        public object SP_User_GetList_ByRoleFunctionID_Scalar(int _FunctionID, short _IsGrant)
        {
            return _db.ExecuteScalarSP("SP_User_GetList_ByRoleFunctionID", new SqlParameter[] {
                new SqlParameter("@_FunctionID", (object)_FunctionID ?? DBNull.Value),
                new SqlParameter("@_IsGrant", (object)_IsGrant ?? DBNull.Value)
            });
        }

        // ── SP_User_GetPage ─────────────────────────────────────────────
        /// <summary>Trả về List<SP_User_GetPageModel></summary>
        public List<SP_User_GetPageModel> SP_User_GetPage_List(string _Keyword, int _Status, int _IsGrant)
        {
            return _db.GetListSP<SP_User_GetPageModel>("SP_User_GetPage", new SqlParameter[] {
                new SqlParameter("@_Keyword", (object)_Keyword ?? DBNull.Value),
                new SqlParameter("@_Status", (object)_Status ?? DBNull.Value),
                new SqlParameter("@_IsGrant", (object)_IsGrant ?? DBNull.Value)
            });
        }

        /// <summary>Trả về SP_User_GetPageModel đầu tiên (hoặc null)</summary>
        public SP_User_GetPageModel SP_User_GetPage_Single(string _Keyword, int _Status, int _IsGrant)
        {
            return _db.GetInstanceSP<SP_User_GetPageModel>("SP_User_GetPage", new SqlParameter[] {
                new SqlParameter("@_Keyword", (object)_Keyword ?? DBNull.Value),
                new SqlParameter("@_Status", (object)_Status ?? DBNull.Value),
                new SqlParameter("@_IsGrant", (object)_IsGrant ?? DBNull.Value)
            });
        }

        /// <summary>Execute INSERT/UPDATE/DELETE</summary>
        public int SP_User_GetPage_Execute(string _Keyword, int _Status, int _IsGrant)
        {
            return _db.ExecuteNonQuerySP("SP_User_GetPage", new SqlParameter[] {
                new SqlParameter("@_Keyword", (object)_Keyword ?? DBNull.Value),
                new SqlParameter("@_Status", (object)_Status ?? DBNull.Value),
                new SqlParameter("@_IsGrant", (object)_IsGrant ?? DBNull.Value)
            });
        }

        /// <summary>Trả về scalar (COUNT, ID...)</summary>
        public object SP_User_GetPage_Scalar(string _Keyword, int _Status, int _IsGrant)
        {
            return _db.ExecuteScalarSP("SP_User_GetPage", new SqlParameter[] {
                new SqlParameter("@_Keyword", (object)_Keyword ?? DBNull.Value),
                new SqlParameter("@_Status", (object)_Status ?? DBNull.Value),
                new SqlParameter("@_IsGrant", (object)_IsGrant ?? DBNull.Value)
            });
        }

        // ── SP_User_InsertUpdate ─────────────────────────────────────────────
        /// <summary>Trả về List + OUTPUT parameters</summary>
        public (List<T> Data, SP_User_InsertUpdateResult Output) SP_User_InsertUpdate<T>(int _UserID, string _Username, string _Email, string _FullName, string _Password, bool _IsActive, bool _IsAdministrator, string _CreatedUser, int _RoleID) where T : new()
        {
            var conn = _db.OpenConnection();
            try
            {
                using var cmd = new SqlCommand("SP_User_InsertUpdate", conn)
                {
                    CommandType    = CommandType.StoredProcedure,
                    CommandTimeout = 450
                };

                // INPUT
                cmd.Parameters.AddWithValue("@_UserID", (object)_UserID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_Username", (object)_Username ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_Email", (object)_Email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_FullName", (object)_FullName ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_Password", (object)_Password ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_IsActive", (object)_IsActive ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_IsAdministrator", (object)_IsAdministrator ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_CreatedUser", (object)_CreatedUser ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_RoleID", (object)_RoleID ?? DBNull.Value);

                // OUTPUT
                var p__ResponseStatus = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(p__ResponseStatus);

                var data = new List<T>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new T();

                        // Map reader -> T via reflection (case-insensitive)
                        var propMap = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                                               .ToDictionary(p => p.Name, StringComparer.OrdinalIgnoreCase);

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            var colName = reader.GetName(i);
                            if (!propMap.TryGetValue(colName, out var prop)) continue;
                            if (!prop.CanWrite) continue;

                            var val = reader.GetValue(i);
                            if (val == DBNull.Value) continue;

                            var targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                            try
                            {
                                var safeValue = Convert.ChangeType(val, targetType);
                                prop.SetValue(item, safeValue);
                            }
                            catch
                            {
                                // ignore conversion errors — optional: log
                            }
                        }

                        data.Add(item);
                    }
                }

                var output = new SP_User_InsertUpdateResult
                {
                    _ResponseStatus = p__ResponseStatus.Value == DBNull.Value ? default : (int)p__ResponseStatus.Value,
                };

                return (data, output);
            }
            finally
            {
                _db.CloseConnection(conn);
            }
        }

        /// <summary>Chỉ lấy OUTPUT</summary>
        public SP_User_InsertUpdateResult SP_User_InsertUpdate_Output(int _UserID, string _Username, string _Email, string _FullName, string _Password, bool _IsActive, bool _IsAdministrator, string _CreatedUser, int _RoleID)
        {
            var conn = _db.OpenConnection();
            try
            {
                using var cmd = new SqlCommand("SP_User_InsertUpdate", conn)
                {
                    CommandType    = CommandType.StoredProcedure,
                    CommandTimeout = 450
                };

                // INPUT
                cmd.Parameters.AddWithValue("@_UserID", (object)_UserID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_Username", (object)_Username ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_Email", (object)_Email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_FullName", (object)_FullName ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_Password", (object)_Password ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_IsActive", (object)_IsActive ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_IsAdministrator", (object)_IsAdministrator ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_CreatedUser", (object)_CreatedUser ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_RoleID", (object)_RoleID ?? DBNull.Value);

                // OUTPUT
                var p__ResponseStatus = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(p__ResponseStatus);

                cmd.ExecuteNonQuery();

                return new SP_User_InsertUpdateResult
                {
                    _ResponseStatus = p__ResponseStatus.Value == DBNull.Value ? default : (int)p__ResponseStatus.Value,
                };
            }
            finally
            {
                _db.CloseConnection(conn);
            }
        }

        // ── SP_User_UpdateActive ─────────────────────────────────────────────
        /// <summary>Trả về List + OUTPUT parameters</summary>
        public (List<T> Data, SP_User_UpdateActiveResult Output) SP_User_UpdateActive<T>(int _UserID) where T : new()
        {
            var conn = _db.OpenConnection();
            try
            {
                using var cmd = new SqlCommand("SP_User_UpdateActive", conn)
                {
                    CommandType    = CommandType.StoredProcedure,
                    CommandTimeout = 450
                };

                // INPUT
                cmd.Parameters.AddWithValue("@_UserID", (object)_UserID ?? DBNull.Value);

                // OUTPUT
                var p__ResponseCode = new SqlParameter("@_ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(p__ResponseCode);

                var data = new List<T>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new T();

                        // Map reader -> T via reflection (case-insensitive)
                        var propMap = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                                               .ToDictionary(p => p.Name, StringComparer.OrdinalIgnoreCase);

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            var colName = reader.GetName(i);
                            if (!propMap.TryGetValue(colName, out var prop)) continue;
                            if (!prop.CanWrite) continue;

                            var val = reader.GetValue(i);
                            if (val == DBNull.Value) continue;

                            var targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                            try
                            {
                                var safeValue = Convert.ChangeType(val, targetType);
                                prop.SetValue(item, safeValue);
                            }
                            catch
                            {
                                // ignore conversion errors — optional: log
                            }
                        }

                        data.Add(item);
                    }
                }

                var output = new SP_User_UpdateActiveResult
                {
                    _ResponseCode = p__ResponseCode.Value == DBNull.Value ? default : (int)p__ResponseCode.Value,
                };

                return (data, output);
            }
            finally
            {
                _db.CloseConnection(conn);
            }
        }

        /// <summary>Chỉ lấy OUTPUT</summary>
        public SP_User_UpdateActiveResult SP_User_UpdateActive_Output(int _UserID)
        {
            var conn = _db.OpenConnection();
            try
            {
                using var cmd = new SqlCommand("SP_User_UpdateActive", conn)
                {
                    CommandType    = CommandType.StoredProcedure,
                    CommandTimeout = 450
                };

                // INPUT
                cmd.Parameters.AddWithValue("@_UserID", (object)_UserID ?? DBNull.Value);

                // OUTPUT
                var p__ResponseCode = new SqlParameter("@_ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(p__ResponseCode);

                cmd.ExecuteNonQuery();

                return new SP_User_UpdateActiveResult
                {
                    _ResponseCode = p__ResponseCode.Value == DBNull.Value ? default : (int)p__ResponseCode.Value,
                };
            }
            finally
            {
                _db.CloseConnection(conn);
            }
        }

        // ── SP_UserFunction_Delete ─────────────────────────────────────────────
        /// <summary>Trả về List + OUTPUT parameters</summary>
        public (List<T> Data, SP_UserFunction_DeleteResult Output) SP_UserFunction_Delete<T>(int _UserID, int _FunctionID) where T : new()
        {
            var conn = _db.OpenConnection();
            try
            {
                using var cmd = new SqlCommand("SP_UserFunction_Delete", conn)
                {
                    CommandType    = CommandType.StoredProcedure,
                    CommandTimeout = 450
                };

                // INPUT
                cmd.Parameters.AddWithValue("@_UserID", (object)_UserID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_FunctionID", (object)_FunctionID ?? DBNull.Value);

                // OUTPUT
                var p__ResponseCode = new SqlParameter("@_ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(p__ResponseCode);

                var data = new List<T>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new T();

                        // Map reader -> T via reflection (case-insensitive)
                        var propMap = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                                               .ToDictionary(p => p.Name, StringComparer.OrdinalIgnoreCase);

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            var colName = reader.GetName(i);
                            if (!propMap.TryGetValue(colName, out var prop)) continue;
                            if (!prop.CanWrite) continue;

                            var val = reader.GetValue(i);
                            if (val == DBNull.Value) continue;

                            var targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                            try
                            {
                                var safeValue = Convert.ChangeType(val, targetType);
                                prop.SetValue(item, safeValue);
                            }
                            catch
                            {
                                // ignore conversion errors — optional: log
                            }
                        }

                        data.Add(item);
                    }
                }

                var output = new SP_UserFunction_DeleteResult
                {
                    _ResponseCode = p__ResponseCode.Value == DBNull.Value ? default : (int)p__ResponseCode.Value,
                };

                return (data, output);
            }
            finally
            {
                _db.CloseConnection(conn);
            }
        }

        /// <summary>Chỉ lấy OUTPUT</summary>
        public SP_UserFunction_DeleteResult SP_UserFunction_Delete_Output(int _UserID, int _FunctionID)
        {
            var conn = _db.OpenConnection();
            try
            {
                using var cmd = new SqlCommand("SP_UserFunction_Delete", conn)
                {
                    CommandType    = CommandType.StoredProcedure,
                    CommandTimeout = 450
                };

                // INPUT
                cmd.Parameters.AddWithValue("@_UserID", (object)_UserID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_FunctionID", (object)_FunctionID ?? DBNull.Value);

                // OUTPUT
                var p__ResponseCode = new SqlParameter("@_ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(p__ResponseCode);

                cmd.ExecuteNonQuery();

                return new SP_UserFunction_DeleteResult
                {
                    _ResponseCode = p__ResponseCode.Value == DBNull.Value ? default : (int)p__ResponseCode.Value,
                };
            }
            finally
            {
                _db.CloseConnection(conn);
            }
        }

        // ── SP_UserFunction_DeleteAllByUserID ─────────────────────────────────────────────
        /// <summary>Trả về List + OUTPUT parameters</summary>
        public (List<T> Data, SP_UserFunction_DeleteAllByUserIDResult Output) SP_UserFunction_DeleteAllByUserID<T>(int _UserID) where T : new()
        {
            var conn = _db.OpenConnection();
            try
            {
                using var cmd = new SqlCommand("SP_UserFunction_DeleteAllByUserID", conn)
                {
                    CommandType    = CommandType.StoredProcedure,
                    CommandTimeout = 450
                };

                // INPUT
                cmd.Parameters.AddWithValue("@_UserID", (object)_UserID ?? DBNull.Value);

                // OUTPUT
                var p__ResponseCode = new SqlParameter("@_ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(p__ResponseCode);

                var data = new List<T>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new T();

                        // Map reader -> T via reflection (case-insensitive)
                        var propMap = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                                               .ToDictionary(p => p.Name, StringComparer.OrdinalIgnoreCase);

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            var colName = reader.GetName(i);
                            if (!propMap.TryGetValue(colName, out var prop)) continue;
                            if (!prop.CanWrite) continue;

                            var val = reader.GetValue(i);
                            if (val == DBNull.Value) continue;

                            var targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                            try
                            {
                                var safeValue = Convert.ChangeType(val, targetType);
                                prop.SetValue(item, safeValue);
                            }
                            catch
                            {
                                // ignore conversion errors — optional: log
                            }
                        }

                        data.Add(item);
                    }
                }

                var output = new SP_UserFunction_DeleteAllByUserIDResult
                {
                    _ResponseCode = p__ResponseCode.Value == DBNull.Value ? default : (int)p__ResponseCode.Value,
                };

                return (data, output);
            }
            finally
            {
                _db.CloseConnection(conn);
            }
        }

        /// <summary>Chỉ lấy OUTPUT</summary>
        public SP_UserFunction_DeleteAllByUserIDResult SP_UserFunction_DeleteAllByUserID_Output(int _UserID)
        {
            var conn = _db.OpenConnection();
            try
            {
                using var cmd = new SqlCommand("SP_UserFunction_DeleteAllByUserID", conn)
                {
                    CommandType    = CommandType.StoredProcedure,
                    CommandTimeout = 450
                };

                // INPUT
                cmd.Parameters.AddWithValue("@_UserID", (object)_UserID ?? DBNull.Value);

                // OUTPUT
                var p__ResponseCode = new SqlParameter("@_ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(p__ResponseCode);

                cmd.ExecuteNonQuery();

                return new SP_UserFunction_DeleteAllByUserIDResult
                {
                    _ResponseCode = p__ResponseCode.Value == DBNull.Value ? default : (int)p__ResponseCode.Value,
                };
            }
            finally
            {
                _db.CloseConnection(conn);
            }
        }

        // ── SP_UserFunction_DeleteList ─────────────────────────────────────────────
        /// <summary>Trả về List + OUTPUT parameters</summary>
        public (List<T> Data, SP_UserFunction_DeleteListResult Output) SP_UserFunction_DeleteList<T>(int _UserID, string _FunctionID) where T : new()
        {
            var conn = _db.OpenConnection();
            try
            {
                using var cmd = new SqlCommand("SP_UserFunction_DeleteList", conn)
                {
                    CommandType    = CommandType.StoredProcedure,
                    CommandTimeout = 450
                };

                // INPUT
                cmd.Parameters.AddWithValue("@_UserID", (object)_UserID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_FunctionID", (object)_FunctionID ?? DBNull.Value);

                // OUTPUT
                var p__ResponseCode = new SqlParameter("@_ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(p__ResponseCode);

                var data = new List<T>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new T();

                        // Map reader -> T via reflection (case-insensitive)
                        var propMap = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                                               .ToDictionary(p => p.Name, StringComparer.OrdinalIgnoreCase);

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            var colName = reader.GetName(i);
                            if (!propMap.TryGetValue(colName, out var prop)) continue;
                            if (!prop.CanWrite) continue;

                            var val = reader.GetValue(i);
                            if (val == DBNull.Value) continue;

                            var targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                            try
                            {
                                var safeValue = Convert.ChangeType(val, targetType);
                                prop.SetValue(item, safeValue);
                            }
                            catch
                            {
                                // ignore conversion errors — optional: log
                            }
                        }

                        data.Add(item);
                    }
                }

                var output = new SP_UserFunction_DeleteListResult
                {
                    _ResponseCode = p__ResponseCode.Value == DBNull.Value ? default : (int)p__ResponseCode.Value,
                };

                return (data, output);
            }
            finally
            {
                _db.CloseConnection(conn);
            }
        }

        /// <summary>Chỉ lấy OUTPUT</summary>
        public SP_UserFunction_DeleteListResult SP_UserFunction_DeleteList_Output(int _UserID, string _FunctionID)
        {
            var conn = _db.OpenConnection();
            try
            {
                using var cmd = new SqlCommand("SP_UserFunction_DeleteList", conn)
                {
                    CommandType    = CommandType.StoredProcedure,
                    CommandTimeout = 450
                };

                // INPUT
                cmd.Parameters.AddWithValue("@_UserID", (object)_UserID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_FunctionID", (object)_FunctionID ?? DBNull.Value);

                // OUTPUT
                var p__ResponseCode = new SqlParameter("@_ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(p__ResponseCode);

                cmd.ExecuteNonQuery();

                return new SP_UserFunction_DeleteListResult
                {
                    _ResponseCode = p__ResponseCode.Value == DBNull.Value ? default : (int)p__ResponseCode.Value,
                };
            }
            finally
            {
                _db.CloseConnection(conn);
            }
        }

        // ── SP_UserFunction_GetByUserID ─────────────────────────────────────────────
        /// <summary>Trả về List<SP_UserFunction_GetByUserIDModel></summary>
        public List<SP_UserFunction_GetByUserIDModel> SP_UserFunction_GetByUserID_List(int _UserID)
        {
            return _db.GetListSP<SP_UserFunction_GetByUserIDModel>("SP_UserFunction_GetByUserID", new SqlParameter[] {
                new SqlParameter("@_UserID", (object)_UserID ?? DBNull.Value)
            });
        }

        /// <summary>Trả về SP_UserFunction_GetByUserIDModel đầu tiên (hoặc null)</summary>
        public SP_UserFunction_GetByUserIDModel SP_UserFunction_GetByUserID_Single(int _UserID)
        {
            return _db.GetInstanceSP<SP_UserFunction_GetByUserIDModel>("SP_UserFunction_GetByUserID", new SqlParameter[] {
                new SqlParameter("@_UserID", (object)_UserID ?? DBNull.Value)
            });
        }

        /// <summary>Execute INSERT/UPDATE/DELETE</summary>
        public int SP_UserFunction_GetByUserID_Execute(int _UserID)
        {
            return _db.ExecuteNonQuerySP("SP_UserFunction_GetByUserID", new SqlParameter[] {
                new SqlParameter("@_UserID", (object)_UserID ?? DBNull.Value)
            });
        }

        /// <summary>Trả về scalar (COUNT, ID...)</summary>
        public object SP_UserFunction_GetByUserID_Scalar(int _UserID)
        {
            return _db.ExecuteScalarSP("SP_UserFunction_GetByUserID", new SqlParameter[] {
                new SqlParameter("@_UserID", (object)_UserID ?? DBNull.Value)
            });
        }

        // ── SP_UserFunction_Insert ─────────────────────────────────────────────
        /// <summary>Trả về List + OUTPUT parameters</summary>
        public (List<T> Data, SP_UserFunction_InsertResult Output) SP_UserFunction_Insert<T>(int _UserID, string _PermissionOfFnIDs, int _CreatedUserID, bool _IsAdmin) where T : new()
        {
            var conn = _db.OpenConnection();
            try
            {
                using var cmd = new SqlCommand("SP_UserFunction_Insert", conn)
                {
                    CommandType    = CommandType.StoredProcedure,
                    CommandTimeout = 450
                };

                // INPUT
                cmd.Parameters.AddWithValue("@_UserID", (object)_UserID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_PermissionOfFnIDs", (object)_PermissionOfFnIDs ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_CreatedUserID", (object)_CreatedUserID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_IsAdmin", (object)_IsAdmin ?? DBNull.Value);

                // OUTPUT
                var p__ResponseCode = new SqlParameter("@_ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(p__ResponseCode);

                var data = new List<T>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new T();

                        // Map reader -> T via reflection (case-insensitive)
                        var propMap = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                                               .ToDictionary(p => p.Name, StringComparer.OrdinalIgnoreCase);

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            var colName = reader.GetName(i);
                            if (!propMap.TryGetValue(colName, out var prop)) continue;
                            if (!prop.CanWrite) continue;

                            var val = reader.GetValue(i);
                            if (val == DBNull.Value) continue;

                            var targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                            try
                            {
                                var safeValue = Convert.ChangeType(val, targetType);
                                prop.SetValue(item, safeValue);
                            }
                            catch
                            {
                                // ignore conversion errors — optional: log
                            }
                        }

                        data.Add(item);
                    }
                }

                var output = new SP_UserFunction_InsertResult
                {
                    _ResponseCode = p__ResponseCode.Value == DBNull.Value ? default : (int)p__ResponseCode.Value,
                };

                return (data, output);
            }
            finally
            {
                _db.CloseConnection(conn);
            }
        }

        /// <summary>Chỉ lấy OUTPUT</summary>
        public SP_UserFunction_InsertResult SP_UserFunction_Insert_Output(int _UserID, string _PermissionOfFnIDs, int _CreatedUserID, bool _IsAdmin)
        {
            var conn = _db.OpenConnection();
            try
            {
                using var cmd = new SqlCommand("SP_UserFunction_Insert", conn)
                {
                    CommandType    = CommandType.StoredProcedure,
                    CommandTimeout = 450
                };

                // INPUT
                cmd.Parameters.AddWithValue("@_UserID", (object)_UserID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_PermissionOfFnIDs", (object)_PermissionOfFnIDs ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_CreatedUserID", (object)_CreatedUserID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_IsAdmin", (object)_IsAdmin ?? DBNull.Value);

                // OUTPUT
                var p__ResponseCode = new SqlParameter("@_ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(p__ResponseCode);

                cmd.ExecuteNonQuery();

                return new SP_UserFunction_InsertResult
                {
                    _ResponseCode = p__ResponseCode.Value == DBNull.Value ? default : (int)p__ResponseCode.Value,
                };
            }
            finally
            {
                _db.CloseConnection(conn);
            }
        }

        // ── SP_UserFunction_InsertList_byFunctionID ─────────────────────────────────────────────
        /// <summary>Trả về List + OUTPUT parameters</summary>
        public (List<T> Data, SP_UserFunction_InsertList_byFunctionIDResult Output) SP_UserFunction_InsertList_byFunctionID<T>(int _FunctionID, string _PermissionOfUsers, int _CreatedUserID) where T : new()
        {
            var conn = _db.OpenConnection();
            try
            {
                using var cmd = new SqlCommand("SP_UserFunction_InsertList_byFunctionID", conn)
                {
                    CommandType    = CommandType.StoredProcedure,
                    CommandTimeout = 450
                };

                // INPUT
                cmd.Parameters.AddWithValue("@_FunctionID", (object)_FunctionID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_PermissionOfUsers", (object)_PermissionOfUsers ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_CreatedUserID", (object)_CreatedUserID ?? DBNull.Value);

                // OUTPUT
                var p__ResponseCode = new SqlParameter("@_ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(p__ResponseCode);

                var data = new List<T>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new T();

                        // Map reader -> T via reflection (case-insensitive)
                        var propMap = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                                               .ToDictionary(p => p.Name, StringComparer.OrdinalIgnoreCase);

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            var colName = reader.GetName(i);
                            if (!propMap.TryGetValue(colName, out var prop)) continue;
                            if (!prop.CanWrite) continue;

                            var val = reader.GetValue(i);
                            if (val == DBNull.Value) continue;

                            var targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                            try
                            {
                                var safeValue = Convert.ChangeType(val, targetType);
                                prop.SetValue(item, safeValue);
                            }
                            catch
                            {
                                // ignore conversion errors — optional: log
                            }
                        }

                        data.Add(item);
                    }
                }

                var output = new SP_UserFunction_InsertList_byFunctionIDResult
                {
                    _ResponseCode = p__ResponseCode.Value == DBNull.Value ? default : (int)p__ResponseCode.Value,
                };

                return (data, output);
            }
            finally
            {
                _db.CloseConnection(conn);
            }
        }

        /// <summary>Chỉ lấy OUTPUT</summary>
        public SP_UserFunction_InsertList_byFunctionIDResult SP_UserFunction_InsertList_byFunctionID_Output(int _FunctionID, string _PermissionOfUsers, int _CreatedUserID)
        {
            var conn = _db.OpenConnection();
            try
            {
                using var cmd = new SqlCommand("SP_UserFunction_InsertList_byFunctionID", conn)
                {
                    CommandType    = CommandType.StoredProcedure,
                    CommandTimeout = 450
                };

                // INPUT
                cmd.Parameters.AddWithValue("@_FunctionID", (object)_FunctionID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_PermissionOfUsers", (object)_PermissionOfUsers ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_CreatedUserID", (object)_CreatedUserID ?? DBNull.Value);

                // OUTPUT
                var p__ResponseCode = new SqlParameter("@_ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(p__ResponseCode);

                cmd.ExecuteNonQuery();

                return new SP_UserFunction_InsertList_byFunctionIDResult
                {
                    _ResponseCode = p__ResponseCode.Value == DBNull.Value ? default : (int)p__ResponseCode.Value,
                };
            }
            finally
            {
                _db.CloseConnection(conn);
            }
        }

        // ── SP_UserFunction_InsertOne ─────────────────────────────────────────────
        /// <summary>Trả về List + OUTPUT parameters</summary>
        public (List<T> Data, SP_UserFunction_InsertOneResult Output) SP_UserFunction_InsertOne<T>(int _UserID, int _FunctionID, bool _IsGant, bool _IsInsert, bool _IsUpdate, bool _IsDelete, int _CreatedUserID) where T : new()
        {
            var conn = _db.OpenConnection();
            try
            {
                using var cmd = new SqlCommand("SP_UserFunction_InsertOne", conn)
                {
                    CommandType    = CommandType.StoredProcedure,
                    CommandTimeout = 450
                };

                // INPUT
                cmd.Parameters.AddWithValue("@_UserID", (object)_UserID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_FunctionID", (object)_FunctionID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_IsGant", (object)_IsGant ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_IsInsert", (object)_IsInsert ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_IsUpdate", (object)_IsUpdate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_IsDelete", (object)_IsDelete ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_CreatedUserID", (object)_CreatedUserID ?? DBNull.Value);

                // OUTPUT
                var p__ResponseCode = new SqlParameter("@_ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(p__ResponseCode);

                var data = new List<T>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new T();

                        // Map reader -> T via reflection (case-insensitive)
                        var propMap = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                                               .ToDictionary(p => p.Name, StringComparer.OrdinalIgnoreCase);

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            var colName = reader.GetName(i);
                            if (!propMap.TryGetValue(colName, out var prop)) continue;
                            if (!prop.CanWrite) continue;

                            var val = reader.GetValue(i);
                            if (val == DBNull.Value) continue;

                            var targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                            try
                            {
                                var safeValue = Convert.ChangeType(val, targetType);
                                prop.SetValue(item, safeValue);
                            }
                            catch
                            {
                                // ignore conversion errors — optional: log
                            }
                        }

                        data.Add(item);
                    }
                }

                var output = new SP_UserFunction_InsertOneResult
                {
                    _ResponseCode = p__ResponseCode.Value == DBNull.Value ? default : (int)p__ResponseCode.Value,
                };

                return (data, output);
            }
            finally
            {
                _db.CloseConnection(conn);
            }
        }

        /// <summary>Chỉ lấy OUTPUT</summary>
        public SP_UserFunction_InsertOneResult SP_UserFunction_InsertOne_Output(int _UserID, int _FunctionID, bool _IsGant, bool _IsInsert, bool _IsUpdate, bool _IsDelete, int _CreatedUserID)
        {
            var conn = _db.OpenConnection();
            try
            {
                using var cmd = new SqlCommand("SP_UserFunction_InsertOne", conn)
                {
                    CommandType    = CommandType.StoredProcedure,
                    CommandTimeout = 450
                };

                // INPUT
                cmd.Parameters.AddWithValue("@_UserID", (object)_UserID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_FunctionID", (object)_FunctionID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_IsGant", (object)_IsGant ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_IsInsert", (object)_IsInsert ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_IsUpdate", (object)_IsUpdate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_IsDelete", (object)_IsDelete ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_CreatedUserID", (object)_CreatedUserID ?? DBNull.Value);

                // OUTPUT
                var p__ResponseCode = new SqlParameter("@_ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(p__ResponseCode);

                cmd.ExecuteNonQuery();

                return new SP_UserFunction_InsertOneResult
                {
                    _ResponseCode = p__ResponseCode.Value == DBNull.Value ? default : (int)p__ResponseCode.Value,
                };
            }
            finally
            {
                _db.CloseConnection(conn);
            }
        }

        // ── SP_UserLogs_Delete ─────────────────────────────────────────────
        /// <summary>Trả về List + OUTPUT parameters</summary>
        public (List<T> Data, SP_UserLogs_DeleteResult Output) SP_UserLogs_Delete<T>(DateTime _Fromdate, DateTime _Todate, int _UserID, int _FunctionID) where T : new()
        {
            var conn = _db.OpenConnection();
            try
            {
                using var cmd = new SqlCommand("SP_UserLogs_Delete", conn)
                {
                    CommandType    = CommandType.StoredProcedure,
                    CommandTimeout = 450
                };

                // INPUT
                cmd.Parameters.AddWithValue("@_Fromdate", (object)_Fromdate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_Todate", (object)_Todate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_UserID", (object)_UserID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_FunctionID", (object)_FunctionID ?? DBNull.Value);

                // OUTPUT
                var p__ResponseCode = new SqlParameter("@_ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(p__ResponseCode);

                var data = new List<T>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new T();

                        // Map reader -> T via reflection (case-insensitive)
                        var propMap = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                                               .ToDictionary(p => p.Name, StringComparer.OrdinalIgnoreCase);

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            var colName = reader.GetName(i);
                            if (!propMap.TryGetValue(colName, out var prop)) continue;
                            if (!prop.CanWrite) continue;

                            var val = reader.GetValue(i);
                            if (val == DBNull.Value) continue;

                            var targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                            try
                            {
                                var safeValue = Convert.ChangeType(val, targetType);
                                prop.SetValue(item, safeValue);
                            }
                            catch
                            {
                                // ignore conversion errors — optional: log
                            }
                        }

                        data.Add(item);
                    }
                }

                var output = new SP_UserLogs_DeleteResult
                {
                    _ResponseCode = p__ResponseCode.Value == DBNull.Value ? default : (int)p__ResponseCode.Value,
                };

                return (data, output);
            }
            finally
            {
                _db.CloseConnection(conn);
            }
        }

        /// <summary>Chỉ lấy OUTPUT</summary>
        public SP_UserLogs_DeleteResult SP_UserLogs_Delete_Output(DateTime _Fromdate, DateTime _Todate, int _UserID, int _FunctionID)
        {
            var conn = _db.OpenConnection();
            try
            {
                using var cmd = new SqlCommand("SP_UserLogs_Delete", conn)
                {
                    CommandType    = CommandType.StoredProcedure,
                    CommandTimeout = 450
                };

                // INPUT
                cmd.Parameters.AddWithValue("@_Fromdate", (object)_Fromdate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_Todate", (object)_Todate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_UserID", (object)_UserID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_FunctionID", (object)_FunctionID ?? DBNull.Value);

                // OUTPUT
                var p__ResponseCode = new SqlParameter("@_ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(p__ResponseCode);

                cmd.ExecuteNonQuery();

                return new SP_UserLogs_DeleteResult
                {
                    _ResponseCode = p__ResponseCode.Value == DBNull.Value ? default : (int)p__ResponseCode.Value,
                };
            }
            finally
            {
                _db.CloseConnection(conn);
            }
        }

        // ── SP_UserLogs_GetPages ─────────────────────────────────────────────
        /// <summary>Trả về List<SP_UserLogs_GetPagesModel></summary>
        public List<SP_UserLogs_GetPagesModel> SP_UserLogs_GetPages_List(DateTime _Fromdate, DateTime _Todate, int _UserID, int _FunctionID)
        {
            return _db.GetListSP<SP_UserLogs_GetPagesModel>("SP_UserLogs_GetPages", new SqlParameter[] {
                new SqlParameter("@_Fromdate", (object)_Fromdate ?? DBNull.Value),
                new SqlParameter("@_Todate", (object)_Todate ?? DBNull.Value),
                new SqlParameter("@_UserID", (object)_UserID ?? DBNull.Value),
                new SqlParameter("@_FunctionID", (object)_FunctionID ?? DBNull.Value)
            });
        }

        /// <summary>Trả về SP_UserLogs_GetPagesModel đầu tiên (hoặc null)</summary>
        public SP_UserLogs_GetPagesModel SP_UserLogs_GetPages_Single(DateTime _Fromdate, DateTime _Todate, int _UserID, int _FunctionID)
        {
            return _db.GetInstanceSP<SP_UserLogs_GetPagesModel>("SP_UserLogs_GetPages", new SqlParameter[] {
                new SqlParameter("@_Fromdate", (object)_Fromdate ?? DBNull.Value),
                new SqlParameter("@_Todate", (object)_Todate ?? DBNull.Value),
                new SqlParameter("@_UserID", (object)_UserID ?? DBNull.Value),
                new SqlParameter("@_FunctionID", (object)_FunctionID ?? DBNull.Value)
            });
        }

        /// <summary>Execute INSERT/UPDATE/DELETE</summary>
        public int SP_UserLogs_GetPages_Execute(DateTime _Fromdate, DateTime _Todate, int _UserID, int _FunctionID)
        {
            return _db.ExecuteNonQuerySP("SP_UserLogs_GetPages", new SqlParameter[] {
                new SqlParameter("@_Fromdate", (object)_Fromdate ?? DBNull.Value),
                new SqlParameter("@_Todate", (object)_Todate ?? DBNull.Value),
                new SqlParameter("@_UserID", (object)_UserID ?? DBNull.Value),
                new SqlParameter("@_FunctionID", (object)_FunctionID ?? DBNull.Value)
            });
        }

        /// <summary>Trả về scalar (COUNT, ID...)</summary>
        public object SP_UserLogs_GetPages_Scalar(DateTime _Fromdate, DateTime _Todate, int _UserID, int _FunctionID)
        {
            return _db.ExecuteScalarSP("SP_UserLogs_GetPages", new SqlParameter[] {
                new SqlParameter("@_Fromdate", (object)_Fromdate ?? DBNull.Value),
                new SqlParameter("@_Todate", (object)_Todate ?? DBNull.Value),
                new SqlParameter("@_UserID", (object)_UserID ?? DBNull.Value),
                new SqlParameter("@_FunctionID", (object)_FunctionID ?? DBNull.Value)
            });
        }

        // ── SP_UserLogs_Insert ─────────────────────────────────────────────
        /// <summary>Trả về List + OUTPUT parameters</summary>
        public (List<T> Data, SP_UserLogs_InsertResult Output) SP_UserLogs_Insert<T>(int _UserID, int _FunctionID, string _FunctionName, string _Description, int _LogType, string _ClientIP) where T : new()
        {
            var conn = _db.OpenConnection();
            try
            {
                using var cmd = new SqlCommand("SP_UserLogs_Insert", conn)
                {
                    CommandType    = CommandType.StoredProcedure,
                    CommandTimeout = 450
                };

                // INPUT
                cmd.Parameters.AddWithValue("@_UserID", (object)_UserID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_FunctionID", (object)_FunctionID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_FunctionName", (object)_FunctionName ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_Description", (object)_Description ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_LogType", (object)_LogType ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_ClientIP", (object)_ClientIP ?? DBNull.Value);

                // OUTPUT
                var p__ResponseCode = new SqlParameter("@_ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(p__ResponseCode);

                var data = new List<T>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new T();

                        // Map reader -> T via reflection (case-insensitive)
                        var propMap = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                                               .ToDictionary(p => p.Name, StringComparer.OrdinalIgnoreCase);

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            var colName = reader.GetName(i);
                            if (!propMap.TryGetValue(colName, out var prop)) continue;
                            if (!prop.CanWrite) continue;

                            var val = reader.GetValue(i);
                            if (val == DBNull.Value) continue;

                            var targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                            try
                            {
                                var safeValue = Convert.ChangeType(val, targetType);
                                prop.SetValue(item, safeValue);
                            }
                            catch
                            {
                                // ignore conversion errors — optional: log
                            }
                        }

                        data.Add(item);
                    }
                }

                var output = new SP_UserLogs_InsertResult
                {
                    _ResponseCode = p__ResponseCode.Value == DBNull.Value ? default : (int)p__ResponseCode.Value,
                };

                return (data, output);
            }
            finally
            {
                _db.CloseConnection(conn);
            }
        }

        /// <summary>Chỉ lấy OUTPUT</summary>
        public SP_UserLogs_InsertResult SP_UserLogs_Insert_Output(int _UserID, int _FunctionID, string _FunctionName, string _Description, int _LogType, string _ClientIP)
        {
            var conn = _db.OpenConnection();
            try
            {
                using var cmd = new SqlCommand("SP_UserLogs_Insert", conn)
                {
                    CommandType    = CommandType.StoredProcedure,
                    CommandTimeout = 450
                };

                // INPUT
                cmd.Parameters.AddWithValue("@_UserID", (object)_UserID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_FunctionID", (object)_FunctionID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_FunctionName", (object)_FunctionName ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_Description", (object)_Description ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_LogType", (object)_LogType ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@_ClientIP", (object)_ClientIP ?? DBNull.Value);

                // OUTPUT
                var p__ResponseCode = new SqlParameter("@_ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(p__ResponseCode);

                cmd.ExecuteNonQuery();

                return new SP_UserLogs_InsertResult
                {
                    _ResponseCode = p__ResponseCode.Value == DBNull.Value ? default : (int)p__ResponseCode.Value,
                };
            }
            finally
            {
                _db.CloseConnection(conn);
            }
        }
    }
}

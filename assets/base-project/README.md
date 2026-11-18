# CmsBase

Project nền ASP.NET Core 9 MVC cho CMS gọi stored procedure SQL Server.

```powershell
dotnet build CmsBase.sln
dotnet run --project CmsBase/CmsBase.csproj --launch-profile https
```

## Cấu hình môi trường

- `ConnectionStrings__CMS` trỏ đến cơ sở dữ liệu đã có các SP, tài khoản và cấu hình menu quản trị hệ thống của CMS.
- `appSettings__MailServer`, `appSettings__ClientID`, `appSettings__SecretKey` và `appSettings__UriCallBack` cấu hình đăng nhập SSO. Callback trỏ tới `/Auth/RedirectLogin` trên địa chỉ ứng dụng thực tế.
- `appSettings__UrlRoot` khớp URL localhost của profile chạy chính. MailServer mẫu là `https://mail.vtc.vn/betasso`.
- `Cms__Name` và `Cms__PathBase` cấu hình tên hiển thị và đường dẫn gốc khi triển khai dưới thư mục con.
- Đặt các giá trị `Storage__Endpoint`, `Storage__BucketName`, `Storage__AccessKey`, `Storage__SecretKey`, `Storage__PublicUrl` và tùy chọn `Storage__Folder` khi dùng chức năng CDN.

Dùng biến môi trường hoặc cấu hình riêng của nơi triển khai cho giá trị thật. Khi chưa cấu hình SSO, project mở được trang đăng nhập để báo trạng thái cấu hình.

## Nối nghiệp vụ vào project nền

`CmsBase/Controllers/AdminController.cs`, `Views/Admin` và các SP trong `DataAccess/CmsDataAccess.cs` chứa phần quản trị hệ thống. Menu và quyền đọc từ cơ sở dữ liệu qua `VerticalMenuViewComponent` và `UserValidate`.

Khi thêm nhóm nghiệp vụ, đặt mã gọi SP và model đầu vào/đầu ra trong `DataAccess`, dùng `CmsDataAccess` dạng `partial` nếu cần nhận cùng `DBHelper`. Thêm controller và Razor view theo từng nhóm chức năng. Dùng `[RequireCmsSession]` cho màn hình nội bộ, sau đó kiểm tra quyền của thao tác qua `UserValidate`. Liên kết nhóm menu với controller/action và đồng bộ `ActionName` trong cấu hình phân quyền.

View dùng chung sidebar, giao diện sáng/tối và các lớp `cms-list-card`, `cms-list-filters`, `cms-table-region`. Bật plugin qua `ViewData` ở view cần dùng. Ví dụ, `UseSelect2` bật Select2 và `initCmsSelect`.

`CmsBase/DataAccess/spDataAccess.tt` là tùy chọn sinh DAO cho SP nghiệp vụ. Điền danh sách `schema.procedure` và connection string trên bản sao, rồi chạy **Run Custom Tool**. Với CLI .NET Framework, dùng `TextTransform.exe DataAccess/spDataAccess.tt -out DataAccess/spDataAccess.Generated.cs`. Nếu CLI lấy nhầm phiên bản System.Data, truyền `-P` với thư mục chứa `System.Data.dll` đúng kiến trúc của TextTransform. File `spDataAccess.Generated.cs` bổ sung vào lớp DAO dạng partial. T4 chỉ mô tả result set đầu tiên bằng metadata. Với SP dùng nhiều result set hoặc schema động, dùng hợp đồng đã xác minh để viết adapter phù hợp.

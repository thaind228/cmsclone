# CmsBase

Project nền ASP.NET Core 9 MVC cho CMS gọi stored procedure SQL Server.

```powershell
dotnet build CmsBase.sln
dotnet run --project CmsBase/CmsBase.csproj --launch-profile https
```

## Cấu hình môi trường

Cấu hình chính nằm tại `CmsBase/app/config/appsettings.json`, với `app` cùng cấp `Controllers`. Project nạp tệp từ content root và đưa tệp vào thư mục build/publish. Có thể ghi đè cấu hình triển khai bằng biến môi trường.

- `ConnectionStrings__CMS` trỏ đến cơ sở dữ liệu đã có các SP, tài khoản và cấu hình menu quản trị hệ thống của CMS.
- `appSettings__MailServer`, `appSettings__ClientID`, `appSettings__SecretKey` và `appSettings__UriCallBack` cấu hình đăng nhập SSO. Callback trỏ tới `/Auth/RedirectLogin` trên địa chỉ ứng dụng thực tế.
- Khi thay `appSettings:UrlRoot` cho máy local, cập nhật scheme và port tương ứng trong `applicationUrl` của profile chính ở `Properties/launchSettings.json`. Chạy profile đó và kiểm tra dòng `Now listening on`. MailServer mẫu là `https://mail.vtc.vn/betasso`.
- `Cms__Name` và `Cms__PathBase` cấu hình tên hiển thị và đường dẫn gốc khi triển khai dưới thư mục con.
- Giữ nguyên các giá trị `Storage` đã cấp khi sao chép cấu hình. Bản mẫu có sẵn endpoint, bucket, URL công khai và thư mục. Điền khóa truy cập đã cấp vào cấu hình local khi tạo CMS.

Dùng biến môi trường hoặc cấu hình riêng của nơi triển khai cho giá trị thật. Khi chưa cấu hình SSO, project mở được trang đăng nhập để báo trạng thái cấu hình.

## Nối nghiệp vụ vào project nền

`CmsBase/Controllers/AdminController.cs`, `Views/Admin` và các SP trong `DataAccess/CmsDataAccess.cs` chứa phần quản trị hệ thống. Menu và quyền đọc từ cơ sở dữ liệu qua `VerticalMenuViewComponent` và `UserValidate`.

Khi thêm nhóm nghiệp vụ, đặt mã gọi SP và model đầu vào/đầu ra trong `DataAccess`, dùng `CmsDataAccess` dạng `partial` nếu cần nhận cùng `DBHelper`. Thêm controller và Razor view theo từng nhóm chức năng.

View dùng chung sidebar, giao diện sáng/tối và các lớp `cms-list-card`, `cms-list-filters`, `cms-table-region`. Bật plugin qua `ViewData` ở view cần dùng. Ví dụ, `UseSelect2` bật Select2 và `initCmsSelect`.

`CmsBase/DataAccess/spDataAccess.tt` là tùy chọn sinh DAO cho SP nghiệp vụ. Điền danh sách `schema.procedure` và connection string trên bản sao, rồi chạy **Run Custom Tool**. Với CLI .NET Framework, dùng `TextTransform.exe DataAccess/spDataAccess.tt -out DataAccess/spDataAccess.Generated.cs`. Nếu CLI lấy nhầm phiên bản System.Data, truyền `-P` với thư mục chứa `System.Data.dll` đúng kiến trúc của TextTransform. File `spDataAccess.Generated.cs` bổ sung vào lớp DAO dạng partial. T4 chỉ mô tả result set đầu tiên bằng metadata. Với SP dùng nhiều result set hoặc schema động, dùng hợp đồng đã xác minh để viết adapter phù hợp.

## Menu và phân quyền

1. Dùng **Quản trị hệ thống → Quản lý chức năng** để đăng ký nhóm menu nghiệp vụ riêng. Tạo nhóm cấp gốc (`ParentID = 0`) và liên kết con cho từng màn hình qua `ParentID` của nhóm. Đặt `IsActive` và `IsDisplay` cho nhóm và liên kết, khai báo thứ tự hiển thị, `Url` dạng `/Controller/Action` và `ActionName` duy nhất cho từng màn hình. Dùng đúng `ActionName` đó khi kiểm tra quyền. Với màn hình đã đăng ký, cập nhật bản ghi hiện có để tránh trùng.
2. Dùng `[RequireCmsSession]` và kiểm tra quyền phía server trên mọi action của màn hình, gồm cả AJAX và POST. Gọi `UserValidate.CheckPermissionUser` với ID, cờ admin từ session và `ActionName` của chức năng. Chỉ tiếp tục nếu kiểm tra thành công và có `IsGrant`. Với thêm, sửa, xóa, kiểm tra thêm quyền tương ứng `IsInsert`, `IsUpdate`, `IsDelete` trước khi gọi SP. Hiển thị các nút thao tác theo cùng quyền đã kiểm tra.
3. Đăng ký menu và thử cấp quyền trong DB kiểm thử được chỉ định. Chỉ ghi vào DB thật khi người dùng đã yêu cầu. Nếu chưa thể đăng ký vào DB đích, bàn giao bảng đăng ký với tên nhóm, tên chức năng, URL, `ActionName`, thứ tự và quyền cần cấp cho từng màn hình. Ghi rõ menu đang chờ đăng ký hoặc cấp quyền.
4. Kiểm tra admin thấy menu đã đăng ký, user có quyền thấy liên kết được cấp, và user thiếu quyền không thấy liên kết hoặc chạy được action bằng URL trực tiếp. Thử riêng trường hợp user có quyền xem nhưng thiếu quyền ghi.

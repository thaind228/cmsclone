# CmsBase

Project nền ASP.NET Core 9 MVC cho CMS gọi stored procedure SQL Server.

```powershell
dotnet build CmsBase.sln
dotnet run --project CmsBase/CmsBase.csproj --launch-profile https
```

## Cấu hình môi trường

Cấu hình local nằm tại `CmsBase/app/config/appsettings.json`, với `app` cùng cấp `Controllers`. Project nạp tệp từ content root và đưa tệp vào thư mục build/publish.

Trên pod Linux, mount cấu hình triển khai vào đường dẫn tuyệt đối `/config/appsettings.json`. Ứng dụng tự dùng tệp được mount nếu tệp có mặt lúc khởi động, và dùng tệp local khi đường dẫn mount chưa có. Cùng một mã nguồn dùng được ở cả hai môi trường. Biến môi trường và tham số CLI có thể ghi đè giá trị từ JSON.

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

## Chọn ảnh trên form

Bật `ViewData["UseImagePicker"] = true;` trong view chính dùng layout CMS, kể cả khi các trường thêm/sửa nằm trong partial tải qua AJAX. Giữ tên, kiểu input và validation hiện có. Đặt input URL ảnh và nút **Browse** trong cùng `input-group`; mỗi cặp điền vào ô của chính nó.

```cshtml
<label asp-for="ImageUrl" class="form-label">URL ảnh</label>
<div class="input-group">
    <input asp-for="ImageUrl" class="form-control" data-cms-image-url />
    <button type="button" class="btn btn-outline-secondary" data-cms-image-browse>Browse</button>
</div>
<span asp-validation-for="ImageUrl" class="text-danger"></span>
```

Popup dùng `StorageController.ImagePicker` và lọc tệp ảnh. Khi người dùng bấm **Chọn ảnh**, popup điền URL CDN và phát sự kiện `input` và `change`. Tài khoản có phiên CMS có thể duyệt và chọn ảnh. Tải lên, tạo thư mục và xóa dùng quyền tương ứng của chức năng `Upload`.

## Menu và phân quyền

1. Dùng dữ liệu chức năng trong DB làm nguồn duy nhất cho các mục menu nghiệp vụ. Đăng ký qua **Quản trị hệ thống → Quản lý chức năng** hoặc qua script gọi SP quản lý chức năng hiện có. Theo mẫu của `AdminController.SaveFunction`, `SP_Functions_Update` nhận `FunctionID = 0` để tạo bản ghi và dùng ID hiện có để cập nhật. Xác minh schema và chữ ký SP trên DB đích khi viết script.
   - Tạo hoặc tìm nhóm cấp gốc (`ParentID = 0`) trước, tra lại `FunctionID` thực tế của nhóm và dùng ID đó làm `ParentID` cho từng màn hình. Mỗi màn hình có `ActionName` duy nhất, `Url` dạng `/Controller/Action`, thứ tự hiển thị, `IsActive` và `IsDisplay`. Dùng đúng `ActionName` khi kiểm tra quyền.
   - Tra cứu bản ghi theo `ActionName` ổn định trước khi ghi. Giữ `FunctionID` của bản ghi hiện có khi cập nhật để giữ các quyền đã cấp. Kiểm tra mã kết quả của SP và tra lại dữ liệu đã lưu. Chạy lại bước đăng ký phải dùng lại các bản ghi đã có.
2. Dùng `[RequireCmsSession]` và kiểm tra quyền phía server trên mọi action của màn hình, gồm cả AJAX và POST. Gọi `UserValidate.CheckPermissionUser` với ID, cờ admin từ session và `ActionName` của chức năng. Chỉ tiếp tục nếu kiểm tra thành công và có `IsGrant`. Với thêm, sửa, xóa, kiểm tra thêm quyền tương ứng `IsInsert`, `IsUpdate`, `IsDelete` trước khi gọi SP. Hiển thị các nút thao tác theo cùng quyền đã kiểm tra.
3. Thực hiện đăng ký vào DB được phép cập nhật và xác nhận nhóm, mục con đã có trong DB. Cấp quyền cho đúng user theo yêu cầu qua phần phân quyền hiện có. Nếu chưa được phép ghi hoặc chưa kết nối được DB đích, bàn giao script đăng ký chạy lại được kèm danh sách quyền cần cấp. Ghi rõ menu đang chờ chạy script hoặc cấp quyền.
4. Kiểm tra admin thấy menu đã đăng ký, user có quyền thấy liên kết được cấp, và user thiếu quyền không thấy liên kết hoặc chạy được action bằng URL trực tiếp. Thử riêng trường hợp user có quyền xem nhưng thiếu quyền ghi.

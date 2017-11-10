# POS_4_ASOWELL

**Main Feature:**
Phần mềm quản lý nhà hàng (cung cấp các dịch vụ ăn uống, kho trữ, thống kê...)
  - Phục vụ đặt hàng/gọi món, nhập đơn hàng kho, Cho phép nhân viên đăng nhập và tự động chấm công khi đăng xuất
  - Quản lý nguyên vật liệu trong kho (giám sát/thông báo các trạng thái nguyên vật liệu: còn, hết)
  - Phần mềm được chia cấp: dành cho nhân viên và dành cho quản lý
    + Employee: thực hiện order, điều chỉnh tài khoản của bản thân, nhập hoá đơn nguyên liệu vào kho, thực hiện order cho khách hàng,...
    + Admin: quản lý thông tin nhân viên, lương, lịch làm việc, nguyên liệu trong kho, menu sản phẩm (món ăn,...), tài khoản admin
  - Hiển thị Thống kê Thu chi theo tuần/tháng/năm

Phần mềm được viết trên nền tảng C# .NET với giao diện đồ hoạ WPF, Material Design. Kết hợp một số thư viện mở MaterialdesigninXAML, dragablz, mahapp.metro, ADO.NET, EntityFramework...
Nền tảng trên Android, vẫn đang được lên kế hoạch


**Note:**
 - File "nn" là file rỗng, bù nhìn, giúp giữ cho folder tồn tại khi mà folder không chưa file nào khác
 - Các thành viên tham gia phát triển hãy xem kĩ phần code style requirement
 - Phần precession trong readme.md dùng để báo cáo tiến độ phát triển, do các thành phiên được phân chia để code và cập nhật các trạng thái mới nhất của project (có thể báo các phần đã xong, chưa hoàn thành, gặp khó khăn, yêu cầu bàn luận giúp đỡ, lỗi, vướng mắt,.....). Các phần phân công được chia ra rõ ràng vì vậy khi cập nhật tiến trình cho phần nào thì chỉ viết nội dung liên quan đến phần đó, nếu ngoài lề sẽ bị xoá
 - Các thành viên tham gia phát triển phải tích cực giao tiếp và hỗ trợ nhau nhiều nhất có thể để hoàn thành phần mềm


# Develop Team:
    - Lưu Đức Trung (đã tham gia)
    - Nguyễn Hoàng Nam (đã tham gia)
    - Lê Đức Anh (đã tham gia)
    
    -**Team process**:
    


# Software's procession:
    -[] : áp dụng bất đồng bộ (asynchronous hay còn gọi là lập trình đa tiến đoạn) để giúp chương trình thực thi đa tác vụ hiệu quả hơn (cái này cần thảo luận)

  - **Database and WebService**:
    - [x] : Kết nối vào thao tác truy xuất xử lý dữ liệu (local/cloud azure) bằng ADO.NET Entity Framework
    - Thiết kế cho phép dữ liệu được thao tác bởi nhiều chương trình chạy đồng thời cùng một lúc một cách an toàn, kiểm tra và ngăn chận các tình huống bất đồng bộ (concurrency error)
    - [] : webservice cung cấp dịch vụ về dữ liệu (Data service)


  - **LoginWindow**:
    - [x] : đăng nhập cho nhân viên và quản lý
    - [] : chức năng thay đổi cấu hình đăng nhập database, sau khi chỉnh sửa và đăng nhập thành công, chương trình tự động lưu dữ liệu của database đó (lần sau không cần điều chỉnh nữa). Thông tin database sẽ được lưu vào databaseinfo.txt
    - [] : mã hoá mật khẩu người dùng

  - **EmployeeWorkspaceWindow**:
    - [x] : Nhân viên đăng nhập, tự động lấy thời gian bắt đầu đăng nhập. Khi nhân viên đăng xuất, tự động lấy thời gian đăng xuất, chấm công và phát sinh WorkingHistory trong ngày (store procedure trong database se tự động cập nhật vào bảng lương của tháng hiện tại của nhân viên)
    - [x] : của sổ hiển thị sơ đồ chỗ ngồi (dựa trên map của nhà hàng, cho phép tùy chỉnh). Khi nhân viên chọn bàn thì tiến hành navigate và cập nhật current table
    - [x] : các khung xuất menu, thêm fiter món ăn theo bản chữ cái, thiết kế nút tìm món ăn (thêm ảnh cho món ăn nếu cần). Ngoài ra còn có khung xuất các nguyên liệu (để phục vụ chắc năng nhập hóa đơn mua nguyên liệu)
    - [x] : khung nhập vào thao tác order, tác động trực tiếp vào currenttable
    - [x] : xây dựng khung Setting cho các tuỳ chỉnh một số thuộc tính trong EmployeeWorkspaceWindow, các tuỳ chỉnh lưu vào settinginfo.txt
    - [x] : xây dựng khung thông tin nhân viên, cho phép nhân viên chỉnh sủa tài khoản, thông tin cá nhân (nhớ yêu cầu nhập lại mật khẩu thì mới cho phép lưu thay đổi)
    
    Phần sau này là thao tác in và thanh toán bill
    
    - [x] : Cho phép thanh toán và lưu dữ liệu đã order vào database
    - [] : Tiến hành thiết kế form của bill
    - [] : In bill thông qua printer. Tính năng tuỳ chỉnh hiển thị văn bản in trước khi in
    - [] : In thêm note của từng món ăn trong bill (kitchen print)
    - [] : In report (theo dạng tài liệu pdf hoặc dạng hoá đơn liệt kê) sau một ngày làm việc
    - [] : thêm chức năng lưu nhật kí phòng trường hợp chương trình bị đóng đột ngột (Nhưng lưu thông tin nhân viên thì chưa khả thi). Có thể giải quyết bằng cách cho phép sau 5-10 phút sẽ tự động lưu thời điểm hiện tại và nhân viên hiện tại vào file
    - [] : swap/merge bill
    - [] : xuất bill dưới dạng pdf
    - [] : cho phép chia bill ra để thanh toán theo từng đợt
    
(*) dự kiến :  Bổ sung thông tin khách hàng, thêm chức năng xuất thông tin và hình ảnh nhận diện khách hàng quen thuộc, khi xuất danh sách khách hàng cho nhân viên xem có thể sort theo số lần khách hàng đã đến quán


  - **WareHouseWindow**:
    + [] : đăng nhập/đăng xuất
    - [] : khung nhập hóa đơn mua nguyên liệu vào kho (gần giống với khung nhập order)
    + [] : xây dựng cửa sổ theo dõi, thống kê lượng nguyên liệu sử dụng và lương nguyên liệu đưa vào kho
    + [] : thống kê các món ăn theo số lần được gọi
    + [] : chức năng kiểm tra và thông báo khi lượng nguyên liệu gần hết (cần thông tin các món ăn, nguyên liệu, công thức, và hệ thống quy đổi đơn vị dùng, đơn vị mua và đơn vị lưu trữ nguyên liệu)
    + [] : xây dựng cửa sổ nhập đơn hàng, sau khi nhân viên nhập đơn hàng sẽ tự động sinh dữ liệu ReceitpNote và ReceiptNoteDetails trong database


  - **AdminWorkspaceWindow**:
    + [] : Employee information form (giao diện/xem/xoá/sửa thông tin/tìm kiếm(theo tên, ?))
    + [] : Salary N`ote information form (giao diện/xem/xoá/sửa thông tin)
    + [] : Customer information form (giao diện/xem/xoá/sửa thông tin/tìm kiếm(theo tên, ?))
    + [] : Product information form (giao diện/xem/xoá/sửa thông tin/tìm kiếm(theo tên, ?))
    + [] : Ingredient information form (giao diện/xem/xoá/sửa thông tin/tìm kiếm(theo tên, ?))
    + [] : Cho phép thiết kế món ăn mới (map món ăn và lượng nguyên liệu tho công thức)
    + [] : Order information form (giao diện/xem/tìm kiếm(theo ngày, ?))
    + [] : ReceiptNote information form (giao diện/xem/tìm kiếm(theo ngày, ?))
    + [] : Admin profile information form (giao diện/xem/sửa thông tin) _ không có quyền xóa bất kì admin nào
    + [] : xuất report cho dữ liệu order/order details, empschedule/salarynote
    + [] : xuất report cho dữ liệu receiptnote/receiptnote details
    + [] : hiển thị data chart cho order/order details, empschedule/salarynote, receipnote/receiptnote details
    
(*) dự kiến : Chức năng cho phép admin thêm lịch làm việc cho nhân viên hằng tuần, và nhân viên chỉ có thể login trong khung giờ mà admin đã sắp xếp (nếu login trễ hơn tức là đã đi làm trễ, logout sớm hơn giờ đã định => thông báo cho admin, trừ lương). Chức năng kiểm toán thu chi theo ngày/tháng/năm. Chức năng đồ hoạ vẽ biểu đồ thu nhập. 

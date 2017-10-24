# cPOS-4-``*WELL``
Big Project (cái tên cPOS-4-*well tức là POS System for _WELL restaurant phát triển bởi CProg-ITTeam)
hiện nhà hàng vẫn chưa thống nhất tên (dự kiến là SoWell nhưng lại có ý định đặt SunWell nên mình để thành *Well luôn cho lẹ)

**Main Feature:**
Phần mềm quản lý nhà hàng (cung cấp các dịch vụ ăn uống, kho trữ, thống kê...)
  - Phục vụ đặt hàng/gọi món, nhập đơn hàng kho, Cho phép nhân viên đăng nhập và tự động chấm công khi đăng xuất
  - Quản lý nguyên vật liệu trong kho (giám sát/thông báo các trạng thái nguyên vật liệu: còn, hết)
  - Phần mềm được chia cấp: dành cho nhân viên và dành cho quản lý
    + Employee: thực hiện order, điều chỉnh tài khoản của bản thân, nhập hoá đơn nguyên liệu vào kho, thực hiện order cho khách hàng,...
    + Admin: quản lý thông tin nhân viên, lương, lịch làm việc, nguyên liệu trong kho, menu sản phẩm (món ăn,...), tài khoản admin
  - Hiển thị Thống kê Thu chi theo tuần/tháng/năm

Phần mềm được viết trên nền tảng C# .NET với giao diện đồ hoạ WPF, Material Design. Kết hợp một số thư viện mở MaterialdesigninXAML, dragablz, mahapp.metro, ADO.NET, EntityFramework...
Đây là phiên bản chính thức dựa trên một số nền tảng của ứng dụng POS trước


**Note:**
 - File "nn" là file rỗng, bù nhìn, giúp giữ cho folder tồn tại khi mà folder không chưa file nào khác
 - Các thành viên tham gia phát triển hãy xem kĩ phần code style requirement
 - Phần precession trong readme.md dùng để báo cáo tiến độ phát triển, do các thành phiên được phân chia để code và cập nhật các trạng thái mới nhất của project (có thể báo các phần đã xong, chưa hoàn thành, gặp khó khăn, yêu cầu bàn luận giúp đỡ, lỗi, vướng mắt,.....). Các phần phân công được chia ra rõ ràng vì vậy khi cập nhật tiến trình cho phần nào thì chỉ viết nội dung liên quan đến phần đó, nếu ngoài lề sẽ bị xoá


# Develop Team:
    - Lưu Đức Trung (đã tham gia)
    - Nguyễn Hoàng Nam (đã tham gia)
    - Lê Đức Anh (đã tham gia)
    
    -**Team process**:
    


# Software's procession:
  - **Database and WebService**:
    - [] : connect to database (local/cloud azure) with ADO.NET Entity Framework


  - **LoginWindow**:
    - [] : đăng nhập cho nhân viên và quản lý
    - [] : chức năng thay đổi cấu hình đăng nhập database, sau khi chỉnh sửa và đăng nhập thành công, chương trình tự động lưu dữ liệu của database đó (lần sau không cần điều chỉnh nữa). Thông tin database sẽ được lưu vào databaseinfo.txt
    - [] : mã hoá mật khẩu người dùng

  - **EmployeeWorkspaceWindow**:
    - [] : đăng nhập/xuất của nhân viên. Tự động chấm công và phát sinh WorkingLog trong ngày, bảng lương của tháng hiện tại vào trong database
    - [] : các khung xuất menu (thêm ảnh cho món ăn nếu cần)
    - [] : của sổ hiển thị sơ đồ chỗ ngồi (dựa trên map của nhà hàng)
    - [] : cửa sổ nhập bill order, sau khi khách hàng tính tiền sẽ tự động sinh dữ liệu Order và OrderDetails trong database
    - [] : xây dựng của sổ Setting cho các tuỳ chỉnh trong EmployeeWorkspaceWindow, các tuỳ chỉnh lưu vào settinginfo.txt
    - [] : xây dựng chức năng cho phép nhân viên chỉnh sủa tài khoản cá nhân
    - [] : In bill thông qua printer. Tính năng tuỳ chỉnh hiển thị văn bản in trước khi in
    - [] : In thêm note của từng món ăn trong bill (kitchen print)
    - [] : In report (theo dạng tài liệu pdf hoặc dạng hoá đơn liệt kê) sau một ngày làm việc
    - [] : thêm chức năng lưu nhật kí phòng trường hợp chương trình bị đóng đột ngột (Nhưng lưu thông tin nhân viên thì chưa khả thi)
    - [] : swap/merge table
    - [] : xuất bill dưới dạng pdf
    - [] : cho phép chia bill ra để thanh toán theo từng đợt
    
(*) dự kiến :  Bổ sung thông tin khách hàng, thêm chức năng xuất thông tin và hình ảnh nhận diện khách hàng quen thuộc, khi xuất danh sách khách hàng cho nhân viên xem có thể sort theo số lần khách hàng đã đến quán, thêm chức năng gửi nhận tin nhắn qua mạng internet


  - **WareHouseWindow**:
    + [] : xây dựng cửa sổ theo dõi lượng nguyên liệu sử dụng và lương nguyên liệu đưa vào kho
    + [] : chức năng kiểm tra và thông báo khi lượng nguyên liệu gần hết
    + [] : xây dựng cửa sổ nhập đơn hàng, sau khi nhân viên nhập đơn hàng sẽ tự động sinh dữ liệu ReceitpNote và ReceiptNoteDetails trong database


  - **AdminWorkspaceWindow**:
  	+ [] : Employee information form (giao diện/xem/xoá**/sửa thông tin/tìm kiếm(theo tên, ?))
    + [] : Salary N`ote information form (giao diện/xem/xoá**/sửa thông tin)
  	+ [] : Customer information form (giao diện/xem/xoá**/sửa thông tin/tìm kiếm(theo tên, ?))
  	+ [] : Food information form (giao diện/xem/xoá**/sửa thông tin/tìm kiếm(theo tên, ?))
  	+ [] : Ingredient information form (giao diện/xem/xoá**/sửa thông tin/tìm kiếm(theo tên, ?))
  	+ [] : Order information form (giao diện/xem/tìm kiếm(theo ngày, ?))
    + [] : ReceiptNote information form (giao diện/xem/tìm kiếm(theo ngày, ?))
  	+ [] : Admin profile information form (giao diện/xem/sửa thông tin) _ không có quyền xóa bất kì admin nào
  	+ [] : xuất report cho dữ liệu order/order details, empschedule/salarynote
    + [] : xuất report cho dữ liệu receiptnote/receiptnote details
	+ [] : hiển thị data chart cho order/order details, empschedule/salarynote, receipnote/receiptnote details
    
(*) dự kiến : Chức năng cho phép admin thêm lịch làm việc cho nhân viên hằng tuần, và nhân viên chỉ có thể login trong khung giờ mà admin đã sắp xếp (nếu login trễ hơn tức là đã đi làm trễ, logout sớm hơn giờ đã định => thông báo cho admin, trừ lương). Chức năng kiểm toán thu chi theo ngày/tháng/năm. Chức năng đồ hoạ vẽ biểu đồ thu nhập. 

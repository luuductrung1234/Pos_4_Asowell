# POS_4_ASOWELL

**Status:**  building


**Main Feature:**
Phần mềm quản lý nhà hàng (cung cấp các dịch vụ ăn uống, kho trữ, thống kê...)

  o	Xây dựng phần mềm POS gồm các chức năng chính:

    •	Phần làm việc dành cho nhân viên: thực hiện các thao tác order phù hợp với business requirement của Asowel restaurant, xem thông tin cá nhân của nhân viên, tự động chấm công khi nhân viên đăng nhập vào phần mềm. Nhiều nhân viên có thể tham gia đăng nhập, và cùng tham gia order
    
    •	Phần làm việc dành cho quản lý: quản lý tất cả thông tin và dữ liệu liên quan đến nhà hàng: Nhân viên, Khách hàng, Sản phẩm (món ăn), Nguyên liệu, Hoá đơn bán hàng, Hoá đơn nhập kho. Thống kê tài chính, thu (lượng bán ra), chi (lượng mua nguyên liệu và các chi phí phát sinh) của toàn hệ thống nhà hàng. Xuất báo cáo
    
    •	Phần làm việc dành cho Kiểm kho: Theo dõi và thu thập dữ liệu về kho (lượng hàng tồn, lượng đã sử dụng). Nhập hoá đơn nhập kho. Thông kê dữ liệu liên quan đến kho. Xuất báo cáo
    
  o	Chất lượng cần đảm bảo:
    
    •	An toàn  trong các sự cố bất ngờ: ví dụ như mất điện, phần cứng hỏng
    
    •	Bảo toàn dữ liệu, mã hoá các tài khoản
    
    •	Truy xuất dữ liệu, thực thi các tác vụ chuyên môn (order, xuất báo cáo) nhanh gọn.


Phần mềm được viết trên nền tảng C# .NET với giao diện đồ hoạ WPF, phong cách Material Design. Kết hợp một số thư viện mở MaterialdesigninXAMLToolkit, dragablz, mahapp.metro, livechart, ADO.NET, EntityFramework, PDFReport,...
Nền tảng trên Mobile và Web vẫn đang được lên kế hoạch


**Note:**
 - Các thành viên tham gia phát triển hãy xem kĩ phần code style requirement


# Develop Team:
    - Lưu Đức Trung (team leader, back-end architecter, security)
    - Nguyễn Hoàng Nam (main UI designer/creator, software event manipulator, data analysis)
    - Lê Đức Anh (main back-end developer, software event manipulator)
    
    -**Team process**(in future):
      - Lưu Đức Trung (team leader, back-end/web service/cloud service architect)
      - Nguyễn Hoàng Nam (Mobile version developer)
      - Lê Đức Anh (WebApp version developer)
    
    -**Contact**:
      luuductrung@itcomma.com
      luuductrung1234@gmail.com
      0927333668


# Software's procession:
    - [x] : áp dụng bất đồng bộ (asynchronous hay còn gọi là lập trình đa tiến đoạn) để giúp chương trình thực thi đa tác vụ hiệu quả hơn (cái này cần thảo luận)

  - **Database and WebService**:
    - [x] : Kết nối vào thao tác truy xuất xử lý dữ liệu (local) bằng ADO.NET Entity Framework
    - [x] : Kết nối vào thao tác truy xuất xử lý dữ liệu (cloud/web API) bằng ADO.NET Entity Framework
    - [x]  : Thiết kế cho phép dữ liệu được thao tác bởi nhiều chương trình chạy đồng thời cùng một lúc một cách an toàn, kiểm tra và ngăn chận các tình huống bất đồng bộ (concurrency error)
    - [x]  : Áp dụng Silence Retries để ngầm khắc phục các lỗi liên kết và truy vấn dữ liệu từ database 
    - []  : webservice cung cấp dịch vụ về dữ liệu (Data service) và xử lý các tác vụ đa tầng (n-tier architect). Kết hợp lưu trữ đồng bộ giữ local database và cloud, tăng độ an toàn khi có sự cố bất ngờ xảy ra


  - **LoginWindow**:
    - [x] : đăng nhập cho nhân viên và quản lý, đăng nhập bằng Employee Code
    - [x] : chức năng thay đổi cấu hình đăng nhập database, sau khi chỉnh sửa và đăng nhập thành công, chương trình tự động lưu dữ liệu của database đó (lần sau không cần điều chỉnh nữa). Thông tin database sẽ được lưu vào databaseinfo.txt
    - [x] : mã hoá mật khẩu người dùng

  - **EmployeeWorkspaceWindow**:
    - [x] : Nhân viên đăng nhập, tự động lấy thời gian bắt đầu đăng nhập. Khi nhân viên đăng xuất, tự động lấy thời gian đăng xuất, chấm công và phát sinh WorkingHistory trong ngày (store procedure trong database se tự động cập nhật vào bảng lương của tháng hiện tại của nhân viên)
    - [x] : Nhiều nhân viên đăng nhập cùng một lúc, đều được chấm công
    - [x] : Cửa sổ hiển thị sơ đồ chỗ ngồi (dựa trên map của nhà hàng, cho phép tùy chỉnh). Khi nhân viên chọn bàn thì tiến hành navigate đến cửa sổ Entry (order)
    - [x] : Yêu cầu danh tính cấp độ admin để có thể chỉnh sửa các Table đã được Pin
    - [x] : Yêu cầu xác thực danh tính mỗi khi tiến hành order, sau đó có thể thực thi order cho đến khi nào cá nhân đó kết thúc phiên làm việc hiện tại.
    - [x] : Xác thực bằng Employee Code
    - [x] : Lưu thông tin ID của tất cả nhân viên tác động lên cùng một order
    - [x] : các khung xuất menu, thêm fiter món ăn theo bản chữ cái, thiết kế nút tìm món ăn (thêm ảnh cho món ăn nếu cần). Ngoài ra còn có khung xuất các nguyên liệu (để phục vụ chắc năng nhập hóa đơn mua nguyên liệu)
    - [x] : khung nhập vào thao tác order, tác động trực tiếp vào bàn hiện tại
    - [x] : Yêu cầu danh tính admin để có thể xóa các thông tin order trong bàn hiện tại
    - [x] : xây dựng khung Setting cho các tuỳ chỉnh một số thuộc tính trong EmployeeWorkspaceWindow, các tuỳ chỉnh lưu vào settinginfo.txt
    - [x] : xây dựng khung thông tin nhân viên, cho phép nhân viên chỉnh sủa tài khoản, thông tin cá nhân
    
    Phần sau này là thao tác in và thanh toán bill
    
    - [x] : Cho phép thanh toán và lưu dữ liệu đã order vào database
    - [x] : Tiến hành thiết kế form của bill
    - [x] : In bill thông qua printer. Tính năng tuỳ chỉnh hiển thị văn bản in trước khi in (order bill thì chia hai cho bar và kitchen, temporary bil và receipt bill)
    - [x] : Tùy chỉnh máy in
    - [x] : In thêm note của từng món ăn trong bill (kitchen print)
    - [x] : xuất bill dưới dạng pdf
    - [x] : In end of day report (theo dạng tài liệu pdf hoặc dạng hoá đơn liệt kê)
    - [x] : thêm chức năng lưu nhật kí phòng trường hợp chương trình bị đóng đột ngột (Nhưng lưu thông tin nhân viên thì chưa khả thi). Có thể giải quyết bằng cách cho phép sau 5-10 phút sẽ tự động lưu thời điểm hiện tại và nhân viên hiện tại vào file
    - []  : Lưu lịch sử làm việc của các nhân viên 
    - [x] : swap/merge bill
    - [x] : cho phép chia bill ra để thanh toán theo từng ghế
    - [x] : Không cho đăng xuất khi còn bàn đang order
    
(*) dự kiến :  Bổ sung thông tin khách hàng, thêm chức năng xuất thông tin và hình ảnh nhận diện khách hàng quen thuộc, khi xuất danh sách khách hàng cho nhân viên xem có thể sort theo số lần khách hàng đã đến quán


  - **WareHouseWindow**:
    - [x] : đăng nhập/đăng xuất
    - [x] : khung nhập hóa đơn mua nguyên liệu vào kho (gần giống với khung nhập order)
    - [x] : xây dựng cửa sổ theo dõi, thống kê lượng nguyên liệu sử dụng và lương nguyên liệu đưa vào kho
    - [x] : thống kê các nguyên liệu theo lượng sử dụng
    - [x] : chức năng kiểm tra và thông báo khi lượng nguyên liệu gần hết (cần thông tin các món ăn, nguyên liệu, công thức, và hệ thống quy đổi đơn vị dùng, đơn vị mua và đơn vị lưu trữ nguyên liệu)
    - [x] : xây dựng cửa sổ nhập đơn hàng, sau khi nhân viên nhập đơn hàng sẽ tự động sinh dữ liệu ReceitpNote và ReceiptNoteDetails trong database
    - []  : Kho cho AdPress

  - **AdminWorkspaceWindow**:
    - [x] : Employee information form (giao diện/xem/xoá/sửa thông tin/tìm kiếm(theo tên, ?))
    - [x] : Salary N`ote information form (giao diện/xem/xoá/sửa thông tin)
    - [x] : Customer information form (giao diện/xem/xoá/sửa thông tin/tìm kiếm(theo tên, ?))
    - [x] : Product information form (giao diện/xem/xoá/tìm kiếm(theo tên, ?))
    - [x] : Ingredient information form (giao diện/xem/xoá/sửa thông tin/tìm kiếm(theo tên, ?))
    - [x] : Cho phép thiết kế món ăn mới (map món ăn và lượng nguyên liệu tho công thức)
    - [x] : Order information form (giao diện/xem/tìm kiếm(theo ngày, ?))
    - [x] : ReceiptNote information form (giao diện/xem/tìm kiếm(theo ngày, ?))
    - [x] : Admin profile information form (giao diện/xem/sửa thông tin) _ không có quyền xóa bất kì admin nào
    - [x] : xuất report cho dữ liệu order/order details, empschedule/salarynote
    - [x] : xuất report cho dữ liệu receiptnote/receiptnote details
    - [x] : xuất end of day report
    - [x] : hiển thị data chart cho order/order details,, receipnote/receiptnote details. Thống kê thu nhập 
    - [x] : hiện thị data chart cho  salaryNote/workingHistory và product(theo lượng được gọi)
    
(*) dự kiến : Chức năng cho phép admin thêm lịch làm việc cho nhân viên hằng tuần, và nhân viên chỉ có thể login trong khung giờ mà admin đã sắp xếp (nếu login trễ hơn tức là đã đi làm trễ, logout sớm hơn giờ đã định => thông báo cho admin, trừ lương). Chức năng kiểm toán thu chi theo ngày/tháng/năm. Chức năng đồ hoạ vẽ biểu đồ thu nhập. 

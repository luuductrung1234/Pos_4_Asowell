# Software Requirement:
Yêu cầu tiêu chuẩn dành cho phần mềm dựa trên từng loại đối tượng (nhân viên/quản lý/kế toán)


**Product Owner Story and Requirement:**
	- Cho phép chỉnh sửa cấu hình liên kết tới database (yêu cầu mật khẩu)

	Nhân viên:
		- các nhân viên không đứng quầy, sẽ không đăng nhập vào phần mềm, thay vào đó là quét vân tay. Lấy thông tin từ máy quét vân tay 
		để lưu lịch sử làm việc

		- Đăng nhập vào ứng dụng
		- Hiển thị tên và số giờ đã làm việc
		- Có thể chỉnh sửa thông tin cá nhân
		- Đăng xuất và lưu thông tin giờ-bắt-đầu-làm, giờ-kết-thúc, số giờ đã làm được
		- Có thể xem danh sách các loại món trong menu
		- Menu được phân loại theo loại món ăn
		- Có thể lọc món ăn theo phương thức nấu
		- Có thể duyệt món ăn theo tên
		- Có thể xem danh sách bàn theo sơ đồ, số chỗ ngồi trong bàn
		- Có thể chỉnh sửa sơ đồ bàn ghế, và một số thông tin của bàn (ghi chú, số chỗ ngồi,...)

		- Bắt đầu order bằng cách mở sơ đồ bàn và chọn vào bàn cụ thể, Thông tin bàn và thông tin order hiện tại của bàn đó sẽ được hiển thị
		- Hiển thị menu các món song song với thông tin bàn-order để tiến hành order
		- Thông tin bàn-order: số bàn, hiển thị chỗ ngồi đã được chọn/còn trống, ngày hiện tại, liệt kê các món trong hóa đơn (số lượng, tên món,
		có thể thêm ghi chú và xóa bỏ món), Thông tin giảm giá, Tổng số tiền.
		-> Thông tin bàn-order sẽ được lưu giữ tạm, tự động cập nhật nếu có thêm chỉnh sửa
		-> Có thể in bill (in bếp, in bar), xóa bill, thanh toán (in thanh toán) -> thông tin order chính thức lưu vào database

		- Nhập hóa đơn mua nguyên liệu, Khung nhập gồm: ngày hiện tại, liệt kê các nguyên liệu nhập (tên, số lượng, giá tiền, có thể thêm
		ghi chú và xóa bỏ), Tổng số tiền. Tự động lấy thông tin của nhân viên nhập
		-> Thông tin hóa đơn sẽ được lưu giữ tạm, tự động cập nhật nếu có thêm chỉnh sửa
		-> Có thể Lưu, xóa hóa đơn (yêu cầu mật khẩu của nhân viên  hiện tại) -> thông tin hóa đơn chính thức lưu vào database
		
		- Có thể chỉnh sửa cấu hình của phần mềm (tổng số món ăn có thể hiển thị trong cửa sổ menu, thời gian phần mềm tự đóng, ngôn ngữ
		các món ăn được giảm giá theo sự kiện,....)

		- Lưu trữ tạm thông tin các bàn-order hiện tại, nếu máy tắt đột ngột sẽ tự động load lên vào lần sau

		- Trong quá trình hoạt động, yêu cầu hai máy chạy song song dẫn đến một số thao tác bị trùng lặp và xung đột
		Hướng giải quyết 1:   Các thông tin phải thường xuyên đồng bộ với nhau thông qua socket, Khi các thao tác order xảy ra, hai máy sẽ
		liên lạc với nhau và hủy thao tác đó, sau đó hiện thông báo cho người dùng kiểm tra lại
		Hướng giải quyết 2: Hai máy sẽ thực thi và remote call đến web service để thực thi các thao tác backend. Và web service sẽ đảm nhiệm việc
		xử lý đồng bộ


	Quản lý:
		- Thông tin nhân viên
		- Thông tin cá nhân của quản lý (chức vụ cấp cao: chỉnh sửa thông tin của quản lý khác)
		- Lịch sử làm việc và thông tin lương
		- Tạo lịch làm việc
		- Thông tin món ăn - công thức
		- Thông tin nguyên liệu
		- Thông tin order
		- Thông tin hóa đơn nhập hàng
		- Thống kê (biểu diễn dữ liệu). Xuất báo cáo


	Nhân viên kiểm kho:
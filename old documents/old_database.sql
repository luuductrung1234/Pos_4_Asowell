Use master
go

CREATE DATABASE COMMACOFFEESHOP
GO

USE COMMACOFFEESHOP
GO

-- EMPLOYEE
create table tbAdmin (
	ad_id varchar(10) not null			constraint pk_adminid primary key ,
	username varchar(50) not null,
	pass varchar(50) not null,
	name varchar(50) not null,
)
go

create table tbEmployee (
	em_id varchar(10) not null			constraint pk_employeeid primary key,
	username varchar(50) not null,
	pass varchar(50) not null,
	name nvarchar(50) not null,
	birth date not null,				constraint chk_birthday check(year(birth) <= (year(getdate()) - 18)),
	startday date not null,
	hour_wage int not null				constraint chk_hourwage check(hour_wage >= 0),

	
	addr nvarchar(200) null,
	email varchar(50) null,
	phone varchar(20) null,
	em_role int	not null				constraint chk_employeerole check(em_role = 1 or em_role = 2),   -- 1: nhân viên quầy nước, 2: nhân viên bếp, 3: đã xóa(N/A)
	manager varchar(10) not null		constraint fk_manager foreign key references tbAdmin(ad_id),
	deleted int not null 	constraint chk_emp_deleted check (deleted = 0 or deleted = 1)						--0: còn tồn tại, 1: đã xóa
)
go


create table tbEmpSchedule(
	sche_id varchar(10)			constraint pk_scheid primary key,
	em_id varchar(10)			constraint fk_employee foreign key references tbEmployee(em_id),
	workday date				constraint def_workday default getdate(),
	starthour int not null		constraint chk_starthour check(starthour >= 8 and starthour <= 23),
	startminute int not null	constraint chk_startminute check(startminute >= 0 and startminute <= 59),
	endhour int not null		constraint chk_endhour check(endhour >= 8 and endhour <= 23),
	endminute int not null		constraint chk_endminute check(endminute >= 0 and endminute <= 59)
)
go
-- END EMPLOYEE


-- CUSTOMER
create table tbCustomer(
	cus_id varchar(10)				constraint pk_customerid primary key,
	name nvarchar(40) not null,
	phone varchar(20) null,
	email varchar(50) null,
	discount int not null		constraint chk_discount_percent check (discount >= 0 and discount <= 100),
	deleted int not null 	constraint chk_cus_deleted check (deleted = 0 or deleted = 1)						--0: còn tồn tại, 1: đã xóa
)
go
-- CUSTOMER


-- INVENTORY
create table tbFood(
	food_id varchar(10)				constraint pk_menufoodid primary key,
	name nvarchar(50) not null,
	info nvarchar(100) null,
	price money not null,
	isdrink tinyint not null		constraint chk_isdrink_or_eat check (isdrink = 1 or isdrink = 0 or isdrink = 2),     -- 1: thức ăn, 0: thức uống, 2: những thứ khác
	deleted int not null 	constraint chk_food_deleted check (deleted = 0 or deleted = 1)						--0: còn tồn tại, 1: đã xóa
)
go

create table tbFoodMaterial (
	fm_id varchar(10) not null			constraint pk_itemfoodid primary key,
	name nvarchar(50) not null,
	info nvarchar(100) null,
	usefor tinyint not null				constraint chk_ismaterial_fordrink_or_eat check (usefor = 1 or usefor = 0 or usefor = 2 or usefor = 3),     	-- 1: for food, 0: for drink, 2: không sử dụng theo KL nhưng biết lượng dùng, 3: những thứ khác
	fmtype nvarchar(50) not null,
	unit_buy nvarchar(100) not null,
	standard_price money not null		constraint chk_stanndard_price check(standard_price >= 0),
	supplier nvarchar(200) null,
	deleted int not null 	constraint chk_fm_deleted check (deleted = 0 or deleted = 1)						--0: còn tồn tại, 1: đã xóa
)
go


create table tbFoodDetails(
	fd_id varchar(10) not null		constraint pk_fooddetails primary key,
	food_id varchar(10) not null	constraint fk_foodid foreign key references tbFood(food_id),
	fm_id varchar(10) not null		constraint fk_foodmaterialid foreign key references tbFoodMaterial(fm_id),
	quan float not null				constraint chk_use_quantity check(quan >= 0),
	unit_use nvarchar(100) not null
)
go

-- END INVENTORY


-- INCOME
create table tbOrder (
	order_id varchar(10) not null	constraint pk_orderid primary key,
	cus_id varchar(10)				constraint fk_customerdiscount foreign key references tbCustomer(cus_id),
	ordertable int not null,               -- số bàn
	ordertime date not null,               -- thời gian order
	price money not null,
	customerpay money not null,
	payback money not null
)
go

create table tbOrderDetails(
	order_id varchar(10) not null		constraint fk_order foreign key references tbOrder(order_id),
	food_id varchar(10) not null		constraint fk_foodinmenu foreign key references tbFood(food_id),
										constraint pk_orderdetails primary key (order_id, food_id),
	quan int not null					constraint chk_order_quantity check(quan >= 0),
)
go
-- END INCOME



-- EXPOSE
create table tbReceiptNote(
	rn_id varchar(10)				constraint pk_receivepnoteid primary key,
	em_id varchar(10)				constraint fk_employee_buy foreign key references tbEmployee(em_id),
	rday date not null,													-- ngày nhập kho
	total_amount money not null		constraint chk_payment check(total_amount >= 0),     -- số tiền chi
)
go

create table tbReceiptNoteDetails(
	rn_id varchar(10)			constraint fk_receivenoteid foreign key references tbReceiptNote(rn_id),
	fm_id varchar(10)			constraint fk_foodmaterial foreign key references tbFoodMaterial(fm_id),
								constraint pk_buymaterialdetails primary key (rn_id, fm_id),
	quan int not null			constraint chk_buy_quantity check(quan >= 0),
	item_price money not null,	constraint chk_price_per_item check(item_price >= 0),
	note varchar(300) null
)
go

create table tbSalaryNote(
	sn_id varchar(10)			constraint pk_salarynoteid primary key,
	em_id varchar(10)			constraint fk_employeeid foreign key references tbEmployee(em_id),
	date_pay date null,									                    -- ngày trả lương
	salary_value money not null,							                 -- số tiền lương
	work_hour float not null	constraint chk_workhour check(work_hour >= 0),
	for_month int not null		constraint chk_month check(for_month >= 1 and for_month <= 12),
	for_year int not null,													-- lương của tháng/năm nào
	is_paid tinyint not null	constraint chk_is_salary_was_paid check(is_paid = 0 or is_paid = 1),		-- 1: đã trả lương, 0: chưa trả lương
)
go
-- END EXPOSE

-- chỉnh sửa

alter table tbEmpSchedule
add result_salary varchar(10)	constraint fk_result_salarypay foreign key references tbSalaryNote(sn_id)		-- tham chiếu đến bảng lương để trả kết quả lương của lịch làm trong 1 ngày
go


create trigger salaryvalue_update
on tbEmpSchedule
for insert
as
begin
	declare @starthour int
	declare @startminute int
	declare @endhour int
	declare @endminute int
	declare @foremp varchar(10)
	declare @formonth int
	declare @foryear int
	declare @workhour float
	select @starthour = starthour from inserted
	select @startminute = startminute from inserted
	select @endhour = endhour from inserted
	select @endminute = endminute from inserted
	select @foremp = em_id from inserted
	select @formonth = month(workday) from inserted
	select @foryear = year(workday) from inserted
	select @workhour = cast((cast(@endhour as float) - cast(@starthour as float)) + ((cast(@endminute as float) - cast(@startminute as float))/cast(60.0 as float)) as float)

	update tbSalaryNote
	set work_hour += @workhour
	where em_id = @foremp and for_month = @formonth and for_year = @foryear

	update tbSalaryNote
	set salary_value = work_hour * (select hour_wage from tbEmployee E where E.em_id = @foremp)
	where em_id = @foremp and for_month = @formonth and for_year = @foryear
end
go
-- kết thúc chỉnh sửa


insert into tbAdmin values
('AD00000001', 'admin_username1', 'password1', N'Ricky Park'),
('AD00000002', 'admin_username2', 'password2', N'Ton That Vinh'),
('AD00000003', 'admin_username3', 'password3', N'Luu Duc Trung')
go


insert into tbEmployee values
('EM00000001', 'emp_username1', 	'password1',	N'Phạm Thanh Bình', 	'1996-01-01',	'2017-07-01',	10,		N'KTX trường Tôn Đức Thắng',						'example_email1@gmail.com',		'0969876940',	1,	'AD00000001', 0),
('EM00000002', 'emp_username2', 	'password2',	N'Nguyễn Khánh Duy', 	'1996-01-01',	'2017-07-01',	12,		N'KTX trường Tôn Đức Thắng',						'example_email2@gmail.com',		'0964753827',	1,	'AD00000001', 0),
('EM00000003', 'emp_username3', 	'password3',	N'Lý Đông Nghi', 		'1996-01-01',	'2017-07-01',	11,		N'19 Nguyễn Hữu Thọ, Tân Phong, Quận 7',			'example_email3@gmail.com',		'01677048100',	1,	'AD00000001', 0),
('EM00000004', 'emp_username4', 	'password4',	N'Bảo Nguyên', 			'1996-01-01',	'2017-07-01',	12,		N'1017/34 Lê Văn Lương, Phước Kiển, Nhà Bè',		'example_email4@gmail.com',		'0965164474',	1,	'AD00000001', 0),
('EM00000005', 'emp_username5', 	'password5',	N'Lương Nhật Duy', 		'1996-01-01',	'2017-07-01',	11,		N'10/7 Lý Phục Mang, Quận 7',						'desmonmiles8996@gmail.com',	'01215925627',	1,	'AD00000001', 0),
('EM00000006', 'emp_username6', 	'password6',	N'Đinh Thanh Hưng', 	'1996-01-01',	'2017-07-01',	10,		N'1558A phường 7, Quận 8',							'example_email5@gmail.com',		'01207305775',	1,	'AD00000001', 0),
('EM00000007', 'emp_username7', 	'password7',	N'Ngọc Phấn', 			'1996-01-01',	'2017-07-01',	10,		N'585 Nguyễn Hữu Thọ, phường Tân Phong, Quận 7',	'example_email6@gmail.com',		'01242095099',	1,	'AD00000001', 0),
('EM00000008', 'emp_username8', 	'password8',	N'Hữu Phát', 			'1996-01-01',	'2017-07-01',	10,		N'19 Nguyễn Hữu Thọ, Tân Phong, Quận 7',			'example_email7@gmail.com',		'01663598586',	1,	'AD00000001', 0),
('EM00000009', 'emp_username9', 	'password9',	N'Phan Việt Nhân', 		'1996-01-01',	'2017-07-01',	10,		N'19 Nguyễn Hữu Thọ, Tân Phong, Quận 7',			'example_email8@gmail.com',		'0916466886',	1,	'AD00000001', 0),
('EM00000010', 'emp_username10', 	'password10',	N'Nguyễn Thị Diễm My', 	'1996-01-01',	'2017-07-01',	10,		N'19 Nguyễn Hữu Thọ, Tân Phong, Quận 7',			'example_email9@gmail.com',		'0973035001',	1,	'AD00000001', 0),
('EM00000011', 'emp_username11', 	'password11',	N'Phan Thanh Hằng', 	'1996-01-01',	'2017-07-01',	10,		N'599 Lê Văn Lương, Quận 7',						'example_email10@gmail.com',	'0948846462',	1,	'AD00000001', 0),
('EM00000012', 'emp_username12', 	'password12',	N'Đặng Anh Thư', 		'1996-01-01',	'2017-07-01',	10,		N'41  Bùi Huy Bích, phường 12, Quận 8',				'example_email11@gmail.com',	'01216956119',	1,	'AD00000001', 0),
('EM00000013', 'emp_username13', 	'password13',	N'Hà Nguyễn Nhật Minh', '1996-01-01',	'2017-07-01',	11,		N'122/27/30/1/2 Tôn Đản, phường 10, Quận 4',		'nhatminh157@yahoo.com.vn',		'01208459569',	1,	'AD00000001', 0),
('EM00000014', 'emp_username14', 	'password14',	N'Phan Hữu Tiến', 		'1996-01-01',	'2017-07-01',	13,		N'458/10/2 Huỳnh Tấn Phát, Quận 7',					'example_email12.@gmail.com',	'0902448327',	1,	'AD00000001', 0),
('EM00000015', 'emp_username15', 	'password15',	N'Ngô Thanh Hiếu', 		'1996-01-01',	'2017-07-01',	10,		N'KTX trường Tôn Đức Thắng',						'ngohieu2698@gmail.com',		'0969945661',	2,	'AD00000002', 0)
go


insert into tbCustomer values
('CUS0000001', N'Lưu Đức Trung',		'',		'',		20, 0),
('CUS0000002', N'Phạm Thanh Bình',		'',		'',		20, 0),
('CUS0000003', N'Nguyễn Khánh Duy',		'',		'',		20, 0),
('CUS0000004', N'Lý Đông Nghi',			'',		'',		20, 0),
('CUS0000005', N'Bảo Nguyên',			'',		'',		20, 0),
('CUS0000006', N'Lương Nhật Duy',		'',		'',		20, 0),
('CUS0000007', N'Đinh Thanh Hưng',		'',		'',		20, 0),
('CUS0000008', N'Ngọc Phấn',			'',		'',		20, 0),
('CUS0000009', N'Hữu Phát',				'',		'',		20, 0),
('CUS0000010', N'Phan Việt Nhân',		'',		'',		20, 0),
('CUS0000011', N'Nguyễn Thị Diễm My',	'',		'',		20, 0),
('CUS0000012', N'Phan Thanh Hằng',		'',		'',		20, 0),
('CUS0000013', N'Đặng Anh Thư',			'',		'',		20, 0),
('CUS0000014', N'Hà Nguyễn Nhật Minh',	'',		'',		20, 0),
('CUS0000015', N'Phan Hữu Tiến',		'',		'',		20, 0),
('CUS0000016', N'Ngô Thanh Hiếu',		'',		'',		20, 0),
('CUS0000017', N'Guest',		'',		'',		0, 0)
go


insert into tbFoodMaterial values
('FM00000000',	N'Other purchase',			N'',		3,	N'chi phí',		N'lần',		0,		N'', 0),
('FM00000001',	N'pepsi', 					N'',		0,	N'khô',			N'thùng',	130,	N'chị Kiều: 01648360321', 0),
('FM00000002',	N'aquafina', 				N'',		0,	N'khô',			N'thùng',	90,		N'chị Kiều: 01648360321', 0),
('FM00000003',	N'7up', 					N'',		0,	N'khô',			N'thùng',	150,	N'chị Kiều: 01648360321', 0),
('FM00000004',	N'water bottle (big)', 		N'',		0,	N'khô',			N'bình',	90,		N'0836026692', 0),
('FM00000005',	N'Coffee Bean', 			N'',		0,	N'khô',			N'kí',		0,	N'mr Vĩnh', 0),
('FM00000006',	N'Trung Nguyen coffee S', 	N'',		0,	N'khô',			N'bịch',	45,	N'Coopxtra (2nd floor)', 0),
('FM00000007',	N'Dalat milk', 				N'',		0,	N'sữa',			N'hộp',		0,	N'ms.Huyen: 0933336118', 0),
('FM00000008',	N'Dutch Lady milk', 		N'',		0,	N'sữa',			N'hộp',		0,	N'Chợ Tân Mỹ, Red Dragon Fly shop', 0),
('FM00000009',	N'Condense milk', 			N'',		0,	N'sữa',			N'hộp',		0,	N'Chợ Tân Mỹ, Red Dragon Fly shop', 0),
('FM00000010',	N'Soda', 					N'',		0,	N'khô',			N'thùng',	0,	N'Chợ Tân Mỹ, Red Dragon Fly shop', 0),
('FM00000011',	N'Whipping cream', 			N'',		0,	N'sữa',			N'hộp',		0,	N'Coopxtra (3nd floor)', 0),
('FM00000012',	N'Cream cheese', 			N'',		0,	N'sữa',			N'hộp',		0,	N'Coopxtra (3nd floor)', 0),
('FM00000013',	N'Milk Tea Powder', 		N'',		0,	N'khô',			N'hộp',		0,	N'Coopxtra (2nd floor)', 0),
('FM00000014',	N'Matcha Tea Powder', 		N'',		0,	N'khô',			N'hộp',		0,	N'Coopxtra (2nd floor)', 0),
('FM00000015',	N'Durian coffee Powder', 	N'',		0,	N'khô',			N'hộp',		0,	N'Lotte Mart', 0),
('FM00000016',	N'Peach Tea Bag(cozy)', 	N'',		0,	N'khô',			N'hộp',		0,	N'Coopxtra (2nd floor)', 0),
('FM00000017',	N'Strawberry Tea Bag(cozy)',N'',		0,	N'khô',			N'hộp',		0,	N'Lotte Mart', 0),
('FM00000018',	N'Apple Tea bag(cozy)', 	N'',		0,	N'khô',			N'hộp',		0,	N'Coopxtra (2nd floor)', 0),
('FM00000019',	N'Lemon Tea bag(cozy)', 	N'',		0,	N'khô',			N'hộp',		0,	N'Coopxtra (2nd floor)', 0),
('FM00000020',	N'Cacao Powder', 			N'',		0,	N'khô',			N'bịch',	0,	N'Ham Nghi', 0),
('FM00000021',	N'sugar (bar)', 			N'',		0,	N'khô',			N'kí',		0,	N'grocery', 0),
('FM00000022',	N'Icing sugar', 			N'',		0,	N'khô',			N'kí',		0,	N'Ham Nghi', 0),
('FM00000023',	N'Peach Can', 				N'',		0,	N'khô',			N'lon',		0,	N'0906677787', 0),
('FM00000024',	N'Mandarin orange Can', 	N'',		0,	N'khô',			N'lon',		0,	N'Ham Nghi', 0),
('FM00000028',	N'Mint leaf', 				N'',		0,	N'rau củ',		N'kí',		0,	N'Super market or coopxtra', 0),
('FM00000029',	N'Blue curacao syrup', 		N'',		0,	N'khô',			N'chai',	0,	N'0906677787', 0),
('FM00000030',	N'Peach syrup', 			N'',		0,	N'khô',			N'chai',	0,	N'0906677787', 0),
('FM00000031',	N'Ginger honey sauce', 		N'',		0,	N'khô',			N'bình',	0,	N'Skymart 0854101219 save point (0751)', 0),
('FM00000088',	N'Chocalate bar',			N'',		0,	N'khô',			N'kí',		0,	N'mr Vĩnh', 0),
('FM00000032',	N'Ketchup (bar)', 			N'',		0,	N'khô',			N'chai',	0,	N'Coopxtra (3nd floor)', 0),
('FM00000033',	N'Chilli sauce (bar)',		N'',		0,	N'khô',			N'chai',	0,	N'Coopxtra (3nd floor)', 0),
('FM00000027',	N'Lemon', 					N'',		2,	N'rau củ',		N'trái',	0,	N'Super market or coopxtra', 0),
('FM00000025',	N'Yellow orange', 			N'',		2,	N'rau củ',		N'trái',	0,	N'Queenland Mart', 0),
('FM00000026',	N'Green orange', 			N'',		2,	N'rau củ',		N'trái',	0,	N'Super market or Tien Staff or,….', 0),
('FM00000034',	N'ice', 					N'',		3,	N'khô',			N'bịch',	0,	N'01645422630', 0),
('FM00000035',	N'Plastic cup', 			N'',		3,	N'vật dụng',	N'lóc',		0,	N'Son staff : 01264463379', 0),
('FM00000036',	N'Plastic cover', 			N'',		3,	N'vật dụng',	N'lóc',		0,	N'Son staff : 01264463379', 0),
('FM00000037',	N'Paper cup', 				N'',		3,	N'vật dụng',	N'lóc',		0,	N'Son staff : 01264463379', 0),
('FM00000038',	N'Black cover', 			N'',		3,	N'vật dụng',	N'lóc',		0,	N'Son staff : 01264463379', 0),
('FM00000039',	N'Straw', 					N'',		3,	N'vật dụng',	N'lóc',		0,	N'Son staff : 01264463379', 0),
('FM00000040',	N'Toilet paper', 			N'',		3,	N'vật dụng',	N'cuộn',	0,	N'Coopxtra (2nd floor)', 0),
('FM00000041',	N'Napkin', 					N'',		3,	N'vật dụng',	N'lóc',		0,	N'Coopxtra (2nd floor)', 0),
('FM00000042',	N'Aroma', 					N'',		3,	N'vật dụng',	N'bình',	0,	N'Lazada', 0),
('FM00000043',	N'Trash bag', 				N'',		3,	N'vật dụng',	N'cuộn',	0,	N'Coopxtra (2nd floor)', 0),
('FM00000044',	N'Print paper', 			N'',		3,	N'vật dụng',	N'cuộn',	0,	N'maybanhang.net staff: 0912700646', 0),
('FM00000045',	N'Bag T', 					N'',		3,	N'vật dụng',	N'lóc',		0,	N'Lesable', 0)									
go


insert into tbFood values		-- đồ uống
('F000000030',	N'pepsi',					N'',		25,		0, 0),
('F000000031',	N'7up',						N'',		25,		0, 0),
('F000000032',	N'water',					N'',		25,		0, 0),
('F000000033',	N'black coffee',			N'',		30,		0, 0),
('F000000034',	N'coffee milk',				N'',		35,		0, 0),
('F000000035',	N'cream coffee',			N'',		40,		0, 0),
('F000000036',	N'americano',				N'',		40,		0, 0),
('F000000037',	N'durian coffee',			N'',		50,		0, 0),
('F000000038',	N'coffee latte',			N'',		50,		0, 0),
('F000000039',	N'cappucino',				N'',		50,		0, 0),
('F000000040',	N'orange coffee',			N'',		50,		0, 0),
('F000000041',	N'tiramisu coffee',			N'',		50,		0, 0),
('F000000042',	N'chocolate coffee',		N'',		60,		0, 0),
('F000000043',	N'caramel cofffee',			N'',		60,		0, 0),
('F000000044',	N'strawberry tea',			N'',		30,		0, 0),
('F000000045',	N'lemon tea',				N'',		30,		0, 0),
('F000000046',	N'apple tea',				N'',		30,		0, 0),
('F000000047',	N'milk tea',				N'',		40,		0, 0),
('F000000048',	N'peach tea',				N'',		50,		0, 0),
('F000000049',	N'matcha latte',			N'',		50,		0, 0),
('F000000050',	N'ginger honey latte',		N'',		50,		0, 0),
('F000000051',	N'hot choco',				N'',		60,		0, 0),
('F000000052',	N'ice choco',				N'',		60,		0, 0),
('F000000053',	N'orange juice (trái nhỏ)',	N'',		40,		0, 0),
('F000000054',	N'orange ade',				N'',		40,		0, 0),
('F000000055',	N'lemonade',				N'',		40,		0, 0),
('F000000056',	N'orange juice (trái lớn)',	N'',		40,		0, 0),
('F000000057',	N'orange juice (trái vừa)',	N'',		40,		0, 0)
go

insert into tbFood values		-- thức ăn
('F000000001',	N'plain yogurt',			N'',		25,		1, 0),
('F000000002',	N'choco fondue',			N'',		70,		1, 0),
('F000000003',	N'choco cloud',				N'',		55,		1, 0),
('F000000004',	N'custard bread tower',		N'',		70,		1, 0),
('F000000005',	N'choco muffin',			N'',		25,		1, 0),
('F000000006',	N'apple muffin',			N'',		25,		1, 0),
('F000000007',	N'greentea muffin',			N'',		25,		1, 0),
('F000000008',	N'banana cake',				N'',		25,		1, 0),
('F000000009',	N'carrot cake',				N'',		25,		1, 0),
('F000000010',	N'french fries',			N'',		35,		1, 0),
('F000000011',	N'french toast',			N'',		45,		1, 0),
('F000000012',	N'tiramisu cake',			N'',		45,		1, 0),
('F000000013',	N'cheese hotdog',			N'',		35,		1, 0),
('F000000014',	N'cereal & milk',			N'',		50,		1, 0),
('F000000015',	N'honey butter bread',		N'',		70,		1, 0),
('F000000016',	N'pumpkin soup',			N'',		35,		1, 0),
('F000000017',	N'chilli fries',			N'',		50,		1, 0),
('F000000018',	N'tortillas nachos',		N'',		50,		1, 0),
('F000000019',	N'chicken melt',			N'',		50,		1, 0),
('F000000020',	N'comma club',				N'',		55,		1, 0),
('F000000021',	N'gourmet berger',			N'',		60,		1, 0),
('F000000022',	N'spaghetti bolognese',		N'',		55,		1, 0),
('F000000023',	N'spaghetti carbonara',		N'',		55,		1, 0),
('F000000024',	N'noodle eggs omelette',	N'',		45,		1, 0),
('F000000025',	N'chicken burrito',			N'',		60,		1, 0),
('F000000026',	N'hawaiian pizza',			N'',		60,		1, 0),
('F000000027',	N'comma pizza',			N'',		60,		1, 0),
('F000000028',	N'chicken cajun salad',		N'',		55,		1, 0),
('F000000029',	N'bibimbob',				N'',		60,		1, 0)	
go

insert into tbFoodDetails values
('FD00000001', 'F000000030',	'FM00000001',	1,		N'chai'			),
('FD00000002', 'F000000031',	'FM00000002',	1,		N'chai'			),
('FD00000003', 'F000000032',	'FM00000003',	1,		N'chai'			),
('FD00000004', 'F000000033',	'FM00000006',	50,		N'ml'			),
('FD00000005', 'F000000033',	'FM00000021',	1,		N'lần syrup'	),
('FD00000006', 'F000000034',	'FM00000006',	40,		N'ml'			),
('FD00000007', 'F000000034',	'FM00000009',	20,		N'ml'			),
('FD00000008', 'F000000035',	'FM00000006',	40,		N'ml'			),
('FD00000009', 'F000000035',	'FM00000007',	1,		N'milkF'		),
('FD00000010', 'F000000035',	'FM00000021',	2,		N'lần syrup'	),
('FD00000011', 'F000000035',	'FM00000022',	10,		N'gram'			),
('FD00000012', 'F000000036',	'FM00000005',	1,		N'shot'			),
('FD00000013', 'F000000036',	'FM00000021',	1,		N'lần syrup'	),
('FD00000014', 'F000000037',	'FM00000007',	1,		N'Milk(1Cup)'	),
('FD00000015', 'F000000037',	'FM00000009',	10,		N'ml'			),
('FD00000016', 'F000000037',	'FM00000015',	1,		N'gói'			),
('FD00000017', 'F000000038',	'FM00000005',	1,		N'shot'			),
('FD00000018', 'F000000038',	'FM00000007',	1,		N'Milk(1Cup)'	),
('FD00000019', 'F000000038',	'FM00000021',	2,		N'lần dường'	),
('FD00000020', 'F000000039',	'FM00000005',	1,		N'shot'		),
('FD00000021', 'F000000039',	'FM00000007',	1,		N'Milk(1Cup)'	),
('FD00000022', 'F000000039',	'FM00000021',	2,		N'lần syrup'	),
('FD00000023', 'F000000042',	'FM00000005',	2,		N'shot'			),
('FD00000024', 'F000000042',	'FM00000007',	1,		N'Milk(1Cup)'	),
('FD00000025', 'F000000042',	'FM00000021',	3,		N'lần syrup'	),
('FD00000026', 'F000000042',	'FM00000007',	1,		N'nl choco'		),
('FD00000027', 'F000000042',	'FM00000021',	1,		N'nl choco'		),
('FD00000028', 'F000000042',	'FM00000088',	1,		N'nl choco'		),
('FD00000029', 'F000000043',	'FM00000005',	1,		N'shot'			),
('FD00000030', 'F000000043',	'FM00000007',	1,		N'Milk(1Cup)'	),
('FD00000031', 'F000000043',	'FM00000021',	3,		N'lần syrup'	),
('FD00000032', 'F000000043',	'FM00000021',	1,		N'nl caramel'	),
('FD00000033', 'F000000040',	'FM00000005',	1,		N'shot'			),
('FD00000034', 'F000000040',	'FM00000007',	1,		N'Milk(1Cup)'	),
('FD00000035', 'F000000040',	'FM00000021',	2,		N'lần syrup'	),
('FD00000036', 'F000000040',	'FM00000024',	20,		N'gram'			),
('FD00000037', 'F000000040',	'FM00000007',	0.5,	N'milkF'		),
('FD00000038', 'F000000041',	'FM00000005',	1,		N'shot'		),
('FD00000039', 'F000000041',	'FM00000007',	1,		N'Milk(1Cup)'	),
('FD00000040', 'F000000041',	'FM00000020',	10,		N'gram'			),
('FD00000041', 'F000000041',	'FM00000021',	2,		N'lần syrup'	),
('FD00000042', 'F000000041',	'FM00000022',	5,		N'gram'		),
('FD00000043', 'F000000041',	'FM00000007',	0.5,	N'milkF'		),
('FD00000044', 'F000000041',	'FM00000011',	1,		N'nl tiramisu'	),
('FD00000045', 'F000000041',	'FM00000012',	1,		N'nl tiramisu'	),
('FD00000046', 'F000000041',	'FM00000021',	1,		N'nl tiramisu'	),
('FD00000047', 'F000000044',	'FM00000017',	1,		N'túi lọc'		),
('FD00000048', 'F000000044',	'FM00000021',	1,		N'lần syrup'	),
('FD00000049', 'F000000045',	'FM00000019',	1,		N'túi lọc'		),
('FD00000050', 'F000000045',	'FM00000021',	1,		N'lần syrup'	),
('FD00000051', 'F000000046',	'FM00000018',	1,		N'túi lọc'		),
('FD00000052', 'F000000046',	'FM00000021',	1,		N'lần syrup'	),
('FD00000053', 'F000000047',	'FM00000009',	15,		N'ml'			),
('FD00000054', 'F000000047',	'FM00000013',	1.5,	N'gói'			),
('FD00000055', 'F000000048',	'FM00000016',	1,		N'gói'			),
('FD00000056', 'F000000048',	'FM00000021',	4,		N'lần syrup'	),
('FD00000057', 'F000000048',	'FM00000023',	3,		N'miếng'		),
('FD00000058', 'F000000048',	'FM00000030',	4,		N'lần syrup'	),
('FD00000059', 'F000000049',	'FM00000007',	1,		N'Milk(1Cup)'	),
('FD00000060', 'F000000049',	'FM00000014',	1.5,	N'gói'			),
('FD00000061', 'F000000050',	'FM00000007',	1,		N'Milk(1Cup)'	),
('FD00000062', 'F000000050',	'FM00000031',	20,		N'ml'			),
('FD00000063', 'F000000051',	'FM00000021',	1,		N'lần syrup'	),
('FD00000064', 'F000000051',	'FM00000007',	1,		N'nl choco'		),
('FD00000065', 'F000000051',	'FM00000021',	1,		N'nl choco'		),
('FD00000066', 'F000000051',	'FM00000088',	1,		N'nl choco'		),
('FD00000067', 'F000000052',	'FM00000021',	1,		N'lần syrup'	),
('FD00000068', 'F000000052',	'FM00000007',	1,		N'nl choco'		),
('FD00000069', 'F000000052',	'FM00000021',	1,		N'nl choco'		),
('FD00000070', 'F000000052',	'FM00000088',	1,		N'nl choco'		),
('FD00000071', 'F000000057',	'FM00000026',	1,		N'trái vừa'		),
('FD00000072', 'F000000053',	'FM00000026',	2,		N'trái nhỏ'		),
('FD00000073', 'F000000056',	'FM00000026',	0.5,	N'trái lớn'		),
('FD00000074', 'F000000054',	'FM00000026',	1,		N'trái'		),
('FD00000075', 'F000000054',	'FM00000003',	100,	N'ml'			),
('FD00000076', 'F000000054',	'FM00000021',	1,		N'lần syrup'	),
('FD00000077', 'F000000055',	'FM00000010',	100,	N'ml'			),
('FD00000078', 'F000000055',	'FM00000021',	6,		N'lần syrup'	),
('FD00000079', 'F000000055',	'FM00000029',	4,		N'lần syrup'	),
('FD00000080', 'F000000055',	'FM00000027',	1,		N'trái'			)
go



select * from tbAdmin
go
select * from tbEmployee
go
select * from tbFoodMaterial
go
select * from tbFood
go
select *  from tbFoodDetails
go
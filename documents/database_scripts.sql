USE DBAsowell
GO

-- EMPLOYEE
create table AdminRes (
	ad_id varchar(10) not null			constraint pk_adminid primary key ,
	username varchar(50) not null,
	pass varchar(50) not null,
	name varchar(50) not null,
)
GO

create table Employee (
	emp_id varchar(10) not null			constraint pk_employeeid primary key,
	manager varchar(10) not null		constraint fk_manager foreign key references AdminRes(ad_id),
	username varchar(50) not null,
	pass varchar(50) not null,
	name nvarchar(50) not null,
	birth date not null,				constraint chk_birthday check(year(birth) <= (year(getdate()) - 18)),
	startday date not null,
	hour_wage int not null				constraint chk_hourwage check(hour_wage >= 0),

	
	addr nvarchar(200) null,
	email varchar(100) null,
	phone varchar(20) null,
	emp_role int	not null				constraint chk_employeerole check(emp_role = 1 or emp_role = 2),   -- 1: nhân viên quầy nước, 2: nhân viên bếp, 3: đã xóa(N/A)
	deleted int not null 	constraint chk_emp_deleted check (deleted = 0 or deleted = 1)						--0: còn tồn tại, 1: đã xóa
)
go
-- END EMPLOYEE


-- CUSTOMER
create table Customer(
	cus_id varchar(10)				constraint pk_customerid primary key,
	name nvarchar(50) not null,
	phone varchar(20) null,
	email varchar(100) null,
	discount int not null		constraint chk_discount_percent check (discount >= 0 and discount <= 100),
	deleted int not null 	constraint chk_cus_deleted check (deleted = 0 or deleted = 1)						--0: còn tồn tại, 1: đã xóa
)
go
-- CUSTOMER


-- INVENTORY
create table WareHouse(
	warehouse_id varchar(10) not null	constraint pk_warehouseid primary key,
	contain float						constraint chk_storing_contain check(contain >= 0)
)

create table Product(
	product_id varchar(10)				constraint pk_productid primary key,
	name nvarchar(50) not null,
	imageLink varchar(500) null,
	info nvarchar(100) null,
	price money not null,
	is_todrink tinyint not null			constraint chk_isdrink_or_eat check (is_todrink = 1 or is_todrink = 0 or is_todrink = 2),     -- 1: thức ăn, 0: thức uống, 2: những thứ khác
	deleted int not null 				constraint chk_food_deleted check (deleted = 0 or deleted = 1)						--0: còn tồn tại, 1: đã xóa
)
go

create table Ingredient (
	igd_id varchar(10) not null			constraint pk_ingredientid primary key,
	warehouse_id varchar(10) not null	constraint fk_warehouseid foreign key references WareHouse(warehouse_id),
	name nvarchar(100) not null,
	info nvarchar(300) null,
	usefor tinyint not null				constraint chk_ingredient_usefor_drink_or_eat check (usefor = 1 or usefor = 0 or usefor = 2 or usefor = 3),     	-- 1: for food, 0: for drink, 2: không sử dụng theo KL nhưng biết lượng dùng, 3: những thứ khác
	igd_type nvarchar(100) not null,
	unit_buy nvarchar(100) not null,
	standard_price money not null		constraint chk_stanndard_price check(standard_price >= 0),
	deleted int not null 	constraint chk_igd_deleted check (deleted = 0 or deleted = 1)						--0: còn tồn tại, 1: đã xóa
)
go


create table ProductDetails(
	pdetail_id varchar(10) not null	constraint pk_productdetailsid primary key,
	product_id varchar(10) not null	constraint fk_productid foreign key references Product(product_id),
	igd_id varchar(10) not null		constraint fk_ingredientid foreign key references Ingredient(igd_id),
	quan float not null				constraint chk_use_quantity check(quan >= 0),
	unit_use nvarchar(100) not null
)
go
-- END INVENTORY


-- INCOME
create table OrderNote (
	ordernote_id varchar(10) not null	constraint pk_OrderNoteid primary key,
	cus_id varchar(10)					constraint fk_customerowner foreign key references Customer(cus_id),
	emp_id varchar(10)					constraint fk_employeerespond foreign key references Employee(emp_id),
	ordertable int not null,               -- số bàn
	ordertime date not null,               -- thời gian order
	total_price money not null,
	customer_pay money not null,
	pay_back money not null
)
go

create table OrderNoteDetails(
	ordernote_id varchar(10) not null	constraint fk_OrderNote foreign key references OrderNote(ordernote_id),
	product_id varchar(10) not null		constraint fk_foodinmenu foreign key references Product(product_id),
										constraint pk_OrderNotedetails primary key (ordernote_id, product_id),
	quan int not null					constraint chk_OrderNote_quantity check(quan >= 0),
	note varchar(500) null,
	stats varchar(100) null
)
go
-- END INCOME



-- EXPOSE
create table ReceiptNote(
	rn_id varchar(10)				constraint pk_receivepnoteid primary key,
	emp_id varchar(10)				constraint fk_employee_buy foreign key references Employee(emp_id),
	inday date not null,													-- ngày nhập kho
	total_amount money not null		constraint chk_payment check(total_amount >= 0),     -- số tiền chi
)
go

create table ReceiptNoteDetails(
	rn_id varchar(10)			constraint fk_receivenoteid foreign key references ReceiptNote(rn_id),
	igd_id varchar(10)			constraint fk_ingredient foreign key references Ingredient(igd_id),
								constraint pk_buymaterialdetails primary key (rn_id, igd_id),
	quan int not null			constraint chk_buy_quantity check(quan >= 0),
	item_price money not null,	constraint chk_price_per_item check(item_price >= 0),
	note varchar(500) null
)
go

create table SalaryNote(
	sn_id varchar(10) not null	constraint pk_salarynoteid primary key,
	emp_id varchar(10) not null	constraint fk_employeeid foreign key references Employee(emp_id),
	date_pay date null,									                    -- ngày trả lương
	salary_value money not null,							                 -- số tiền lương
	work_hour float not null	constraint chk_workhour check(work_hour >= 0),
	for_month int not null		constraint chk_month check(for_month >= 1 and for_month <= 12),
	for_year int not null,													-- lương của tháng/năm nào
	is_paid tinyint not null	constraint chk_is_salary_was_paid check(is_paid = 0 or is_paid = 1),		-- 1: đã trả lương, 0: chưa trả lương
)
go



create table WorkingHistory(
	wh_id varchar(10)			constraint pk_workhistoryid primary key,
	result_salary varchar(10)	constraint fk_salarynote foreign key references SalaryNote(sn_id),
	emp_id varchar(10)			constraint fk_employee foreign key references Employee(emp_id),
	workday date				constraint def_workday default getdate(),
	starthour int not null		constraint chk_starthour check(starthour >= 8 and starthour <= 23),
	startminute int not null	constraint chk_startminute check(startminute >= 0 and startminute <= 59),
	endhour int not null		constraint chk_endhour check(endhour >= 8 and endhour <= 23),
	endminute int not null		constraint chk_endminute check(endminute >= 0 and endminute <= 59)
)
go
-- END EXPOSE

-- auto execute trigger
--create trigger salaryvalue_update
--on WorkingHistory
--for insert
--as
--begin
--	declare @starthour int
--	declare @startminute int
--	declare @endhour int
--	declare @endminute int
--	declare @foremp varchar(10)
--	declare @formonth int
--	declare @foryear int
--	declare @workhour float
--	select @starthour = starthour from inserted
--	select @startminute = startminute from inserted
--	select @endhour = endhour from inserted
--	select @endminute = endminute from inserted
--	select @foremp = emp_id from inserted
--	select @formonth = month(workday) from inserted
--	select @foryear = year(workday) from inserted
--	select @workhour = cast((cast(@endhour as float) - cast(@starthour as float)) + ((cast(@endminute as float) - cast(@startminute as float))/cast(60.0 as float)) as float)

--	update SalaryNote
--	set work_hour += @workhour
--	where emp_id = @foremp and for_month = @formonth and for_year = @foryear

--	update SalaryNote
--	set salary_value = work_hour * (select hour_wage from Employee E where E.emp_id = @foremp)
--	where emp_id = @foremp and for_month = @formonth and for_year = @foryear
--end
--go
-- end auto execute trigger


insert into AdminRes values
('AD00000001', 'admin_username1', 'password2', N'Ton That Vinh'),
('AD00000002', 'admin_username2', 'password3', N'Luu Duc Trung')
go


insert into Employee values
('EMP0000001',	'AD00000001', 'emp_username1', 	'password1',	N'Phạm Thanh Bình', 	'1996-01-01',	'2017-10-01',	10,		N'KTX trường Tôn Đức Thắng',						'example_email1@gmail.com',		'0969876940',	1, 0),
('EMP0000002',	'AD00000001', 'emp_username2', 	'password2',	N'Nguyễn Khánh Duy', 	'1996-01-01',	'2017-10-01',	12,		N'KTX trường Tôn Đức Thắng',						'example_email2@gmail.com',		'0964753827',	1, 0),
('EMP0000003',	'AD00000001', 'emp_username3', 	'password3',	N'Lý Đông Nghi', 		'1996-01-01',	'2017-10-01',	11,		N'19 Nguyễn Hữu Thọ, Tân Phong, Quận 7',			'example_email3@gmail.com',		'01677048100',	1, 0),
('EMP0000004',	'AD00000001', 'emp_username4', 	'password4',	N'Bảo Nguyên', 			'1996-01-01',	'2017-10-01',	12,		N'1017/34 Lê Văn Lương, Phước Kiển, Nhà Bè',		'example_email4@gmail.com',		'0965164474',	2, 0),
('EMP0000005',	'AD00000001', 'emp_username5', 	'password5',	N'Lương Nhật Duy', 		'1996-01-01',	'2017-10-01',	11,		N'10/7 Lý Phục Mang, Quận 7',						'example_email5@gmail.com',		'01215925627',	2, 0),
('EMP0000006',	'AD00000001', 'emp_username6', 	'password6',	N'Đinh Thanh Hưng', 	'1996-01-01',	'2017-10-01',	10,		N'1558A phường 7, Quận 8',							'example_email6@gmail.com',		'01207305775',	2, 0)
go


insert into Customer values
('CUS0000001', N'Guest',				'',		'',		0, 0),
('CUS0000002', N'Lưu Đức Trung',		'',		'',		20, 0),
('CUS0000003', N'Phạm Thanh Bình',		'',		'',		20, 0),
('CUS0000004', N'Nguyễn Khánh Duy',		'',		'',		20, 0),
('CUS0000005', N'Lý Đông Nghi',			'',		'',		20, 0),
('CUS0000006', N'Bảo Nguyên',			'',		'',		20, 0),
('CUS0000007', N'Lương Nhật Duy',		'',		'',		20, 0),
('CUS0000008', N'Đinh Thanh Hưng',		'',		'',		20, 0),
('CUS0000009', N'Ngọc Phấn',			'',		'',		20, 0),
('CUS0000010', N'Hữu Phát',				'',		'',		20, 0),
('CUS0000011', N'Phan Việt Nhân',		'',		'',		20, 0),
('CUS0000012', N'Nguyễn Thị Diễm My',	'',		'',		20, 0),
('CUS0000013', N'Phan Thanh Hằng',		'',		'',		20, 0),
('CUS0000014', N'Đặng Anh Thư',			'',		'',		20, 0),
('CUS0000015', N'Hà Nguyễn Nhật Minh',	'',		'',		20, 0),
('CUS0000016', N'Phan Hữu Tiến',		'',		'',		20, 0),
('CUS0000017', N'Ngô Thanh Hiếu',		'',		'',		20, 0)
go


insert into WareHouse values
('WAH0000001',	0),
('WAH0000002',	0),
('WAH0000003',	0),
('WAH0000004',	0),
('WAH0000005',	0),
('WAH0000006',	0),
('WAH0000007',	0),
('WAH0000008',	0),
('WAH0000009',	0),
('WAH0000010',	0),
('WAH0000011',	0),
('WAH0000012',	0),
('WAH0000013',	0),
('WAH0000014',	0),
('WAH0000015',	0),
('WAH0000016',	0),
('WAH0000017',	0),
('WAH0000018',	0),
('WAH0000019',	0),
('WAH0000020',	0),
('WAH0000021',	0),
('WAH0000022',	0),
('WAH0000023',	0),
('WAH0000024',	0),
('WAH0000025',	0),
('WAH0000026',	0),
('WAH0000027',	0),
('WAH0000028',	0),
('WAH0000029',	0),
('WAH0000030',	0),
('WAH0000031',	0),
('WAH0000032',	0),
('WAH0000033',	0),
('WAH0000034',	0),
('WAH0000035',	0),
('WAH0000036',	0),
('WAH0000037',	0),
('WAH0000038',	0),
('WAH0000039',	0),
('WAH0000040',	0),
('WAH0000041',	0),
('WAH0000042',	0),
('WAH0000043',	0),
('WAH0000044',	0),
('WAH0000045',	0),
('WAH0000046',	0),
('WAH0000047',	0)
go

insert into Ingredient values
('IGD0000001',	'WAH0000001',	N'pepsi', 					N'',		0,	N'khô',			N'thùng',	130,0),
('IGD0000002',	'WAH0000002',	N'aquafina', 				N'',		0,	N'khô',			N'thùng',	90,	0),
('IGD0000003',	'WAH0000003',	N'7up', 					N'',		0,	N'khô',			N'thùng',	150,0),
('IGD0000004',	'WAH0000004',	N'water bottle (big)', 		N'',		0,	N'khô',			N'bình',	90,	0),
('IGD0000005',	'WAH0000005',	N'Coffee Bean', 			N'',		0,	N'khô',			N'kí',		0,	0),
('IGD0000006',	'WAH0000006',	N'Trung Nguyen coffee S', 	N'',		0,	N'khô',			N'bịch',	45,	0),
('IGD0000007',	'WAH0000007',	N'Dalat milk', 				N'',		0,	N'sữa',			N'hộp',		0,	0),
('IGD0000008',	'WAH0000008',	N'Dutch Lady milk', 		N'',		0,	N'sữa',			N'hộp',		0,	0),
('IGD0000009',	'WAH0000009',	N'Condense milk', 			N'',		0,	N'sữa',			N'hộp',		0,	0),
('IGD0000010',	'WAH0000010',	N'Soda', 					N'',		0,	N'khô',			N'thùng',	0,	0),
('IGD0000011',	'WAH0000011',	N'Whipping cream', 			N'',		0,	N'sữa',			N'hộp',		0,	0),
('IGD0000012',	'WAH0000012',	N'Cream cheese', 			N'',		0,	N'sữa',			N'hộp',		0,	0),
('IGD0000013',	'WAH0000013',	N'Milk Tea Powder', 		N'',		0,	N'khô',			N'hộp',		0,	0),
('IGD0000014',	'WAH0000014',	N'Matcha Tea Powder', 		N'',		0,	N'khô',			N'hộp',		0,	0),
('IGD0000015',	'WAH0000015',	N'Durian coffee Powder', 	N'',		0,	N'khô',			N'hộp',		0,	0),
('IGD0000016',	'WAH0000016',	N'Peach Tea Bag(cozy)', 	N'',		0,	N'khô',			N'hộp',		0,	0),
('IGD0000017',	'WAH0000017',	N'Strawberry Tea Bag(cozy)',N'',		0,	N'khô',			N'hộp',		0,	0),
('IGD0000018',	'WAH0000018',	N'Apple Tea bag(cozy)', 	N'',		0,	N'khô',			N'hộp',		0,	0),
('IGD0000019',	'WAH0000019',	N'Lemon Tea bag(cozy)', 	N'',		0,	N'khô',			N'hộp',		0,	0),
('IGD0000020',	'WAH0000020',	N'Cacao Powder', 			N'',		0,	N'khô',			N'bịch',	0,	0),
('IGD0000021',	'WAH0000021',	N'sugar (bar)', 			N'',		0,	N'khô',			N'kí',		0,	0),
('IGD0000022',	'WAH0000022',	N'Icing sugar', 			N'',		0,	N'khô',			N'kí',		0,	0),
('IGD0000023',	'WAH0000023',	N'Peach Can', 				N'',		0,	N'khô',			N'lon',		0,	0),
('IGD0000024',	'WAH0000024',	N'Mandarin orange Can', 	N'',		0,	N'khô',			N'lon',		0,	0),
('IGD0000028',	'WAH0000028',	N'Mint leaf', 				N'',		0,	N'rau củ',		N'kí',		0,	0),
('IGD0000029',	'WAH0000029',	N'Blue curacao syrup', 		N'',		0,	N'khô',			N'chai',	0,	0),
('IGD0000030',	'WAH0000030',	N'Peach syrup', 			N'',		0,	N'khô',			N'chai',	0,	0),
('IGD0000031',	'WAH0000031',	N'Ginger honey sauce', 		N'',		0,	N'khô',			N'bình',	0,	0),
('IGD0000032',	'WAH0000032',	N'Ketchup (bar)', 			N'',		0,	N'khô',			N'chai',	0,	0),
('IGD0000033',	'WAH0000033',	N'Chilli sauce (bar)',		N'',		0,	N'khô',			N'chai',	0,	0),
('IGD0000027',	'WAH0000027',	N'Lemon', 					N'',		2,	N'rau củ',		N'trái',	0,	0),
('IGD0000025',	'WAH0000025',	N'Yellow orange', 			N'',		2,	N'rau củ',		N'trái',	0,	0),
('IGD0000026',	'WAH0000026',	N'Green orange', 			N'',		2,	N'rau củ',		N'trái',	0,	0),
('IGD0000034',	'WAH0000034',	N'ice', 					N'',		3,	N'khô',			N'bịch',	0,	0),
('IGD0000035',	'WAH0000035',	N'Plastic cup', 			N'',		3,	N'vật dụng',	N'lóc',		0,	0),
('IGD0000036',	'WAH0000036',	N'Plastic cover', 			N'',		3,	N'vật dụng',	N'lóc',		0,	0),
('IGD0000037',	'WAH0000037',	N'Paper cup', 				N'',		3,	N'vật dụng',	N'lóc',		0,	0),
('IGD0000038',	'WAH0000038',	N'Black cover', 			N'',		3,	N'vật dụng',	N'lóc',		0,	0),
('IGD0000039',	'WAH0000039',	N'Straw', 					N'',		3,	N'vật dụng',	N'lóc',		0,	0),
('IGD0000040',	'WAH0000040',	N'Toilet paper', 			N'',		3,	N'vật dụng',	N'cuộn',	0,	0),
('IGD0000041',	'WAH0000041',	N'Napkin', 					N'',		3,	N'vật dụng',	N'lóc',		0,	0),
('IGD0000042',	'WAH0000042',	N'Aroma', 					N'',		3,	N'vật dụng',	N'bình',	0,	0),
('IGD0000043',	'WAH0000043',	N'Trash bag', 				N'',		3,	N'vật dụng',	N'cuộn',	0,	0),
('IGD0000044',	'WAH0000044',	N'Print paper', 			N'',		3,	N'vật dụng',	N'cuộn',	0,	0),
('IGD0000045',	'WAH0000045',	N'Bag T', 					N'',		3,	N'vật dụng',	N'lóc',		0,	0),
('IGD0000046',	'WAH0000046',	N'Chocalate bar',			N'',		0,	N'khô',			N'kí',		0,	0),
('IGD0000047',	'WAH0000047',	N'Other purchase',			N'',		3,	N'chi phí',		N'lần',		0,	0)
go


insert into Product
	([product_id], [name], [info], [price], [type], [deleted], [Discount])
values		-- đồ uống
('P000000030',	N'pepsi',					N'',		25,		0, 0, 0),
('P000000031',	N'7up',						N'',		25,		0, 0, 0),
('P000000032',	N'water',					N'',		25,		0, 0, 0),
('P000000033',	N'black coffee',			N'',		30,		0, 0, 0),
('P000000034',	N'coffee milk',				N'',		35,		0, 0, 0),
('P000000035',	N'cream coffee',			N'',		40,		0, 0, 0),
('P000000036',	N'americano',				N'',		40,		0, 0, 0),
('P000000037',	N'durian coffee',			N'',		50,		0, 0, 0),
('P000000038',	N'coffee latte',			N'',		50,		0, 0, 0),
('P000000039',	N'cappucino',				N'',		50,		0, 0, 0),
('P000000040',	N'orange coffee',			N'',		50,		0, 0, 0),
('P000000041',	N'tiramisu coffee',			N'',		50,		0, 0, 0),
('P000000042',	N'chocolate coffee',		N'',		60,		0, 0, 0),
('P000000043',	N'caramel cofffee',			N'',		60,		0, 0, 0),
('P000000044',	N'strawberry tea',			N'',		30,		0, 0, 0),
('P000000045',	N'lemon tea',				N'',		30,		0, 0, 0),
('P000000046',	N'apple tea',				N'',		30,		0, 0, 0),
('P000000047',	N'milk tea',				N'',		40,		0, 0, 0),
('P000000048',	N'peach tea',				N'',		50,		0, 0, 0),
('P000000049',	N'matcha latte',			N'',		50,		0, 0, 0),
('P000000050',	N'ginger honey latte',		N'',		50,		0, 0, 0),
('P000000051',	N'hot choco',				N'',		60,		0, 0, 0),
('P000000052',	N'ice choco',				N'',		60,		0, 0, 0),
('P000000053',	N'orange juice (trái nhỏ)',	N'',		40,		0, 0, 0),
('P000000054',	N'orange ade',				N'',		40,		0, 0, 0),
('P000000055',	N'lemonade',				N'',		40,		0, 0, 0),
('P000000056',	N'orange juice (trái lớn)',	N'',		40,		0, 0, 0),
('P000000057',	N'orange juice (trái vừa)',	N'',		40,		0, 0, 0)
go

insert into Product 
	([product_id], [name], [info], [price], [type], [deleted], [Discount])
values		-- thức ăn
('P000000001',	N'plain yogurt',			N'',		25,		1, 0, 0),
('P000000002',	N'choco fondue',			N'',		70,		1, 0, 0),
('P000000003',	N'choco cloud',				N'',		55,		1, 0, 0),
('P000000004',	N'custard bread tower',		N'',		70,		1, 0, 0),
('P000000005',	N'choco muffin',			N'',		25,		1, 0, 0),
('P000000006',	N'apple muffin',			N'',		25,		1, 0, 0),
('P000000007',	N'greentea muffin',			N'',		25,		1, 0, 0),
('P000000008',	N'banana cake',				N'',		25,		1, 0, 0),
('P000000009',	N'carrot cake',				N'',		25,		1, 0, 0),
('P000000010',	N'french fries',			N'',		35,		1, 0, 0),
('P000000011',	N'french toast',			N'',		45,		1, 0, 0),
('P000000012',	N'tiramisu cake',			N'',		45,		1, 0, 0),
('P000000013',	N'cheese hotdog',			N'',		35,		1, 0, 0),
('P000000014',	N'cereal & milk',			N'',		50,		1, 0, 0),
('P000000015',	N'honey butter bread',		N'',		70,		1, 0, 0),
('P000000016',	N'pumpkin soup',			N'',		35,		1, 0, 0),
('P000000017',	N'chilli fries',			N'',		50,		1, 0, 0),
('P000000018',	N'tortillas nachos',		N'',		50,		1, 0, 0),
('P000000019',	N'chicken melt',			N'',		50,		1, 0, 0),
('P000000020',	N'comma club',				N'',		55,		1, 0, 0),
('P000000021',	N'gourmet berger',			N'',		60,		1, 0, 0),
('P000000022',	N'spaghetti bolognese',		N'',		55,		1, 0, 0),
('P000000023',	N'spaghetti carbonara',		N'',		55,		1, 0, 0),
('P000000024',	N'noodle eggs omelette',	N'',		45,		1, 0, 0),
('P000000025',	N'chicken burrito',			N'',		60,		1, 0, 0),
('P000000026',	N'hawaiian pizza',			N'',		60,		1, 0, 0),
('P000000027',	N'comma pizza',				N'',		60,		1, 0, 0),
('P000000028',	N'chicken cajun salad',		N'',		55,		1, 0, 0),
('P000000029',	N'bibimbob',				N'',		60,		1, 0, 0)	
go

insert into ProductDetails values
('PD00000001','P000000031',	'IGD0000002',	1,		N'chai'			),
('PD00000002','P000000032',	'IGD0000003',	1,		N'chai'			),
('PD00000003','P000000033',	'IGD0000006',	50,		N'ml'			),
('PD00000004','P000000033',	'IGD0000021',	1,		N'lần syrup'	),
('PD00000005','P000000034',	'IGD0000006',	40,		N'ml'			),
('PD00000006','P000000034',	'IGD0000009',	20,		N'ml'			),
('PD00000007','P000000035',	'IGD0000006',	40,		N'ml'			),
('PD00000008','P000000035',	'IGD0000007',	1,		N'milkF'		),
('PD00000009','P000000035',	'IGD0000021',	2,		N'lần syrup'	),
('PD00000010','P000000035',	'IGD0000022',	10,		N'gram'			),
('PD00000011','P000000036',	'IGD0000005',	1,		N'shot'			),
('PD00000012','P000000036',	'IGD0000021',	1,		N'lần syrup'	),
('PD00000013','P000000037',	'IGD0000007',	1,		N'Milk(1Cup)'	),
('PD00000014','P000000037',	'IGD0000009',	10,		N'ml'			),
('PD00000015','P000000037',	'IGD0000015',	1,		N'gói'			),
('PD00000016','P000000038',	'IGD0000005',	1,		N'shot'			),
('PD00000017','P000000038',	'IGD0000007',	1,		N'Milk(1Cup)'	),
('PD00000018','P000000038',	'IGD0000021',	2,		N'lần dường'	),
('PD00000019','P000000039',	'IGD0000005',	1,		N'shot'		),
('PD00000020','P000000039',	'IGD0000007',	1,		N'Milk(1Cup)'	),
('PD00000021','P000000039',	'IGD0000021',	2,		N'lần syrup'	),
('PD00000022','P000000042',	'IGD0000005',	2,		N'shot'			),
('PD00000023','P000000042',	'IGD0000007',	1,		N'Milk(1Cup)'	),
('PD00000024','P000000042',	'IGD0000021',	3,		N'lần syrup'	),
('PD00000028','P000000042',	'IGD0000007',	1,		N'nl choco'		),
('PD00000029','P000000042',	'IGD0000021',	1,		N'nl choco'		),
('PD00000030','P000000042',	'IGD0000046',	1,		N'nl choco'		),
('PD00000031','P000000043',	'IGD0000005',	1,		N'shot'			),
('PD00000088','P000000043',	'IGD0000007',	1,		N'Milk(1Cup)'	),
('PD00000032','P000000043',	'IGD0000021',	3,		N'lần syrup'	),
('PD00000033','P000000043',	'IGD0000021',	1,		N'nl caramel'	),
('PD00000027','P000000040',	'IGD0000005',	1,		N'shot'			),
('PD00000025','P000000040',	'IGD0000007',	1,		N'Milk(1Cup)'	),
('PD00000026','P000000040',	'IGD0000021',	2,		N'lần syrup'	),
('PD00000034','P000000040',	'IGD0000024',	20,		N'gram'			),
('PD00000035','P000000040',	'IGD0000007',	0.5,	N'milkF'		),
('PD00000036','P000000041',	'IGD0000005',	1,		N'shot'		),
('PD00000037','P000000041',	'IGD0000007',	1,		N'Milk(1Cup)'	),
('PD00000038','P000000041',	'IGD0000020',	10,		N'gram'			),
('PD00000039','P000000041',	'IGD0000021',	2,		N'lần syrup'	),
('PD00000040','P000000041',	'IGD0000022',	5,		N'gram'		),
('PD00000041','P000000041',	'IGD0000007',	0.5,	N'milkF'		),
('PD00000042','P000000041',	'IGD0000011',	1,		N'nl tiramisu'	),
('PD00000043','P000000041',	'IGD0000012',	1,		N'nl tiramisu'	),
('PD00000044','P000000041',	'IGD0000021',	1,		N'nl tiramisu'	),
('PD00000045','P000000044',	'IGD0000017',	1,		N'túi lọc'		),
('PD00000046','P000000044',	'IGD0000021',	1,		N'lần syrup'	),
('PD00000047','P000000045',	'IGD0000019',	1,		N'túi lọc'		),
('PD00000048','P000000045',	'IGD0000021',	1,		N'lần syrup'	),
('PD00000049','P000000046',	'IGD0000018',	1,		N'túi lọc'		),
('PD00000050','P000000046',	'IGD0000021',	1,		N'lần syrup'	),
('PD00000051','P000000047',	'IGD0000009',	15,		N'ml'			),
('PD00000052','P000000047',	'IGD0000013',	1.5,	N'gói'			),
('PD00000053','P000000048',	'IGD0000016',	1,		N'gói'			),
('PD00000054','P000000048',	'IGD0000021',	4,		N'lần syrup'	),
('PD00000055','P000000048',	'IGD0000023',	3,		N'miếng'		),
('PD00000056','P000000048',	'IGD0000030',	4,		N'lần syrup'	),
('PD00000057','P000000049',	'IGD0000007',	1,		N'Milk(1Cup)'	),
('PD00000058','P000000049',	'IGD0000014',	1.5,	N'gói'			),
('PD00000059','P000000050',	'IGD0000007',	1,		N'Milk(1Cup)'	),
('PD00000060','P000000050',	'IGD0000031',	20,		N'ml'			),
('PD00000061','P000000051',	'IGD0000021',	1,		N'lần syrup'	),
('PD00000062','P000000051',	'IGD0000007',	1,		N'nl choco'		),
('PD00000063','P000000051',	'IGD0000021',	1,		N'nl choco'		),
('PD00000064','P000000051',	'IGD0000046',	1,		N'nl choco'		),
('PD00000065','P000000052',	'IGD0000021',	1,		N'lần syrup'	),
('PD00000066','P000000052',	'IGD0000007',	1,		N'nl choco'		),
('PD00000067','P000000052',	'IGD0000021',	1,		N'nl choco'		),
('PD00000068','P000000052',	'IGD0000046',	1,		N'nl choco'		),
('PD00000069','P000000057',	'IGD0000026',	1,		N'trái vừa'		),
('PD00000070','P000000053',	'IGD0000026',	2,		N'trái nhỏ'		),
('PD00000071','P000000056',	'IGD0000026',	0.5,	N'trái lớn'		),
('PD00000072','P000000054',	'IGD0000026',	1,		N'trái'		),
('PD00000073','P000000054',	'IGD0000003',	100,	N'ml'			),
('PD00000074','P000000054',	'IGD0000021',	1,		N'lần syrup'	),
('PD00000075','P000000055',	'IGD0000010',	100,	N'ml'			),
('PD00000076','P000000055',	'IGD0000021',	6,		N'lần syrup'	),
('PD00000077','P000000055',	'IGD0000029',	4,		N'lần syrup'	),
('PD00000078','P000000055',	'IGD0000027',	1,		N'trái'			),
('PD00000079','P000000030',	'IGD0000001',	1,		N'chai'			)
go
 


select * from AdminRes
go
select *from ReceiptNote
go
select *from ReceiptNoteDetails
go
select * from Employee
go
select * from Customer
go
select * from Ingredient
go
select * from WareHouse
go 
select * from Product
go
select *  from ProductDetails
go

select * from SalaryNote
select * from WorkingHistory

select * from OrderNote
select * from OrderNoteDetails

delete WorkingHistory
delete SalaryNote

insert into SalaryNote
	([sn_id], [emp_id], [salary_value], [work_hour], [for_month], [for_year], [is_paid])
values
('SAN0000001', 'EMP0000001', 300, 30, 10, 2017, 0),
('SAN0000002', 'EMP0000002', 240, 20, 10, 2017, 0),
('SAN0000003', 'EMP0000003', 110, 10, 10, 2017, 0),
('SAN0000004', 'EMP0000001', 300, 30, 11, 2017, 0),
('SAN0000005', 'EMP0000002', 240, 20, 11, 2017, 0),
('SAN0000006', 'EMP0000003', 110, 10, 11, 2017, 0),
('SAN0000007', 'EMP0000001', 300, 30, 12, 2017, 0),
('SAN0000008', 'EMP0000002', 240, 20, 12, 2017, 0),
('SAN0000009', 'EMP0000003', 110, 10, 12, 2017, 0),
('SAN0000010', 'EMP0000001', 300, 30, 10, 2018, 0),
('SAN0000011', 'EMP0000002', 240, 20, 10, 2018, 0),
('SAN0000012', 'EMP0000003', 110, 10, 10, 2018, 0),
('SAN0000013', 'EMP0000001', 300, 30, 11, 2018, 0),
('SAN0000014', 'EMP0000002', 240, 20, 11, 2018, 0),
('SAN0000015', 'EMP0000003', 110, 10, 11, 2018, 0),
('SAN0000016', 'EMP0000001', 300, 30, 12, 2018, 0),
('SAN0000017', 'EMP0000002', 240, 20, 12, 2018, 0),
('SAN0000018', 'EMP0000003', 110, 10, 12, 2018, 0)


insert into WorkingHistory 
	([wh_id], [result_salary], [emp_id], [startTime], [endTime])
values		-- thức ăn
('WOH0000001', 'SAN0000001',	'EMP0000001', '2017-11-01 08:0:0', '2017-11-01 12:0:0'),
('WOH0000002', 'SAN0000001',	'EMP0000001', '2017-11-02 08:0:0', '2017-11-02 12:0:0'),
('WOH0000003', 'SAN0000001',	'EMP0000001', '2017-11-03 08:0:0', '2017-11-03 12:0:0'),
('WOH0000004', 'SAN0000001',	'EMP0000001', '2017-11-06 08:0:0', '2017-11-06 12:0:0'),
('WOH0000005', 'SAN0000001',	'EMP0000001', '2017-11-10 08:0:0', '2017-11-10 12:0:0'),
('WOH0000006', 'SAN0000001',	'EMP0000001', '2017-11-11 12:0:0', '2017-11-11 22:0:0'),
('WOH0000007', 'SAN0000002',	'EMP0000002', '2017-11-03 12:0:0', '2017-11-03 22:0:0'),
('WOH0000008', 'SAN0000002',	'EMP0000002', '2017-11-04 08:0:0', '2017-11-04 18:0:0'),
('WOH0000009', 'SAN0000003',	'EMP0000003', '2017-11-01 08:0:0', '2017-11-01 12:0:0'),
('WOH0000010', 'SAN0000003',	'EMP0000003', '2017-11-04 08:0:0', '2017-11-04 12:0:0'),
('WOH0000011', 'SAN0000003',	'EMP0000003', '2017-11-04 08:0:0', '2017-11-04 10:0:0')
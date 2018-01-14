USE DBpos
GO

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


insert into AdminRes 
	([ad_id], [username], [pass], [name], [ad_role])
values
		-- 1:software admin		2:user admin
('AD00000001', 'luuductrung1234', '8ZICVab+mWcy8267/ruOWo6ERgyWNJSdYi4RjpVqxzIsd5DN+IMHtAhAB0N5ld6zWEaK07QVeQaQlBlLwvlwRPD5dSFu8u9kjBL2HTfEB+Q=', N'Luu Duc Trung', 1)
go



insert into Employee 
	([emp_id], [manager], [username], [pass], [name], [birth], [startday], [hour_wage], [addr], [email], [phone], [emp_role], [deleted], [emp_code] )
values
('EMP0000001',	'AD00000001', 'emp_username1', 	'password1',	N'Phạm Thanh Bình', 	'1996-01-01',	'2017-10-01',	10,		N'KTX trường Tôn Đức Thắng',						'example_email1@gmail.com',		'0969876940',	1, 0, 1111),
('EMP0000002',	'AD00000001', 'emp_username2', 	'password2',	N'Nguyễn Khánh Duy', 	'1996-01-01',	'2017-10-01',	12,		N'KTX trường Tôn Đức Thắng',						'example_email2@gmail.com',		'0964753827',	1, 0, 2222),
('EMP0000003',	'AD00000001', 'emp_username3', 	'password3',	N'Lý Đông Nghi', 		'1996-01-01',	'2017-10-01',	11,		N'19 Nguyễn Hữu Thọ, Tân Phong, Quận 7',			'example_email3@gmail.com',		'01677048100',	1, 0, 3333),
('EMP0000004',	'AD00000001', 'emp_username4', 	'password4',	N'Bảo Nguyên', 			'1996-01-01',	'2017-10-01',	12,		N'1017/34 Lê Văn Lương, Phước Kiển, Nhà Bè',		'example_email4@gmail.com',		'0965164474',	2, 0, 4444),
('EMP0000005',	'AD00000001', 'emp_username5', 	'password5',	N'Lương Nhật Duy', 		'1996-01-01',	'2017-10-01',	11,		N'10/7 Lý Phục Mang, Quận 7',						'example_email5@gmail.com',		'01215925627',	2, 0, 5555),
('EMP0000006',	'AD00000001', 'emp_username6', 	'password6',	N'Đinh Thanh Hưng', 	'1996-01-01',	'2017-10-01',	10,		N'1558A phường 7, Quận 8',							'example_email6@gmail.com',		'01207305775',	2, 0, 6666),
('EMP0000007',	'AD00000001', 'emp_username7', 	'password7',	N'Trần Anh Tuấn', 		'1996-01-01',	'2017-10-01',	10,		N'Lâm Văn Bền phường Tân Quy, Quận 7',				'example_email7@gmail.com',		'09093343424',	3, 0, 7777)
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


insert into WareHouse 
	([warehouse_id], [contain], [std_contain])
values
('WAH0000001',	2000, 5000),		--N'ml',	   
('WAH0000002',	6000, 5000),		--N'ml',	   
('WAH0000003',	5000, 5000),		--N'ml',	   
('WAH0000004',	10000, 5000),		--N'ml',	   
('WAH0000005',	20000, 10000),		--N'g',		
('WAH0000006',	8000, 10000),		--N'g',	   
('WAH0000007',	5000, 5000),		--N'ml',		
('WAH0000008',	5000, 5000),		--N'ml',		
('WAH0000009',	2000, 5000),		--N'ml',		
('WAH0000010',	3000, 5000),		--N'ml',
('WAH0000011',	4000, 6000),		--N'ml',		
('WAH0000012',	9000, 6000),		--N'ml',		
('WAH0000013',	1000, 2000),		--N'g',		
('WAH0000014',	2000, 2000),		--N'g',		
('WAH0000015',	4000, 3000),		--N'g',		
('WAH0000016',	4000, 2000),		--N'g',		
('WAH0000017',	4000, 2000),		--N'g',		
('WAH0000018',	3500, 2000),		--N'g',		
('WAH0000019',	2600, 2000),		--N'g',		
('WAH0000020',	6000, 3000),		--N'g',	   
('WAH0000021',	7000, 5000),		--N'g',		
('WAH0000022',	5000, 3000),		--N'g',		
('WAH0000023',	5000, 5000),		--N'g',		
('WAH0000024',	10000, 5000),		--N'g',		
('WAH0000028',	2000, 2000),		--N'g',		
('WAH0000029',	5000, 5000),		--N'ml',	   
('WAH0000030',	1000, 5000),		--N'ml',	   
('WAH0000031',	1000, 6000),		--N'g',	   
('WAH0000032',	2000, 2000),		--N'g',	   
('WAH0000033',	3000, 2000),		--N'g',	   
('WAH0000027',	4000, 5000),		--N'g',	   
('WAH0000025',	5000, 4000),		--N'g',	   
('WAH0000026',	1000, 10000),		--N'g',	   
('WAH0000034',	2000, 2000),		--N'g',	   	
('WAH0000046',	6000, 5000),		--N'g',		
('WAH0000047',	0, 0)				--N'time',   
go

insert into Ingredient values
('IGD0000001',	'WAH0000001',	N'pepsi', 					N'',		0,	N'dry',			N'liter',	    130,0),
('IGD0000002',	'WAH0000002',	N'aquafina', 				N'',		0,	N'dry',			N'liter',	    90,	0),
('IGD0000003',	'WAH0000003',	N'7up', 					N'',		0,	N'dry',			N'liter',	    150,0),
('IGD0000004',	'WAH0000004',	N'water bottle (big)', 		N'',		0,	N'dry',			N'liter',	    90,	0),
('IGD0000005',	'WAH0000005',	N'Coffee Bean', 			N'',		0,	N'dry',			N'kilogram',		0,	0),
('IGD0000006',	'WAH0000006',	N'Trung Nguyen coffee S', 	N'',		0,	N'dry',			N'kilogram',	    45,	0),
('IGD0000007',	'WAH0000007',	N'Dalat milk', 				N'',		0,	N'dairy',		N'liter',		0,	0),
('IGD0000008',	'WAH0000008',	N'Dutch Lady milk', 		N'',		0,	N'dairy',		N'liter',		0,	0),
('IGD0000009',	'WAH0000009',	N'Condense milk', 			N'',		0,	N'dairy',		N'liter',		0,	0),
('IGD0000010',	'WAH0000010',	N'Soda', 					N'',		0,	N'dairy',		N'liter',	0,	0),
('IGD0000011',	'WAH0000011',	N'Whipping cream', 			N'',		0,	N'dairy',		N'liter',		0,	0),
('IGD0000012',	'WAH0000012',	N'Cream cheese', 			N'',		0,	N'dairy',		N'liter',		0,	0),
('IGD0000013',	'WAH0000013',	N'Milk Tea Powder', 		N'',		0,	N'dry',			N'kilogram',		0,	0),
('IGD0000014',	'WAH0000014',	N'Matcha Tea Powder', 		N'',		0,	N'dry',			N'kilogram',		0,	0),
('IGD0000015',	'WAH0000015',	N'Durian coffee Powder', 	N'',		0,	N'dry',			N'kilogram',		0,	0),
('IGD0000016',	'WAH0000016',	N'Peach Tea Bag(cozy)', 	N'',		0,	N'dry',			N'kilogram',		0,	0),
('IGD0000017',	'WAH0000017',	N'Strawberry Tea Bag(cozy)',N'',		0,	N'dry',			N'kilogram',		0,	0),
('IGD0000018',	'WAH0000018',	N'Apple Tea bag(cozy)', 	N'',		0,	N'dry',			N'kilogram',		0,	0),
('IGD0000019',	'WAH0000019',	N'Lemon Tea bag(cozy)', 	N'',		0,	N'dry',			N'kilogram',		0,	0),
('IGD0000020',	'WAH0000020',	N'Cacao Powder', 			N'',		0,	N'dry',			N'kilogram',	    0,	0),
('IGD0000021',	'WAH0000021',	N'sugar (bar)', 			N'',		0,	N'dry',			N'kilogram',		0,	0),
('IGD0000022',	'WAH0000022',	N'Icing sugar', 			N'',		0,	N'dry',			N'kilogram',		0,	0),
('IGD0000023',	'WAH0000023',	N'Peach Can', 				N'',		0,	N'dry',			N'kilogram',		0,	0),
('IGD0000024',	'WAH0000024',	N'Mandarin orange Can', 	N'',		0,	N'dry',			N'kilogram',		0,	0),
('IGD0000028',	'WAH0000028',	N'Mint leaf', 				N'',		0,	N'vegetable',	N'kilogram',		0,	0),
('IGD0000029',	'WAH0000029',	N'Blue curacao syrup', 		N'',		0,	N'dry',			N'liter',	    0,	0),
('IGD0000030',	'WAH0000030',	N'Peach syrup', 			N'',		0,	N'dry',			N'liter',	    0,	0),
('IGD0000031',	'WAH0000031',	N'Ginger honey sauce', 		N'',		0,	N'dry',			N'kilogram',	    0,	0),
('IGD0000032',	'WAH0000032',	N'Ketchup (bar)', 			N'',		0,	N'dry',			N'kilogram',	    0,	0),
('IGD0000033',	'WAH0000033',	N'Chilli sauce (bar)',		N'',		0,	N'dry',			N'kilogram',	    0,	0),
('IGD0000027',	'WAH0000027',	N'Lemon', 					N'',		2,	N'vegetable',	N'kilogram',	    0,	0),
('IGD0000025',	'WAH0000025',	N'Yellow orange', 			N'',		2,	N'vegetable',	N'kilogram',	    0,	0),
('IGD0000026',	'WAH0000026',	N'Green orange', 			N'',		2,	N'vegetable',	N'kilogram',	    0,	0),
('IGD0000034',	'WAH0000034',	N'ice', 					N'',		3,	N'dry',			N'kilogram',	    0,	0),
('IGD0000046',	'WAH0000046',	N'Chocalate bar',			N'',		0,	N'dry',			N'kilogram',		0,	0),
('IGD0000047',	'WAH0000047',	N'Other purchase',			N'',		3,	N'fee',		    N'time',    0,	0)
go



--	0:Beverage	2:Beer	3:Wine	6:Coffee	7:Cocktail		1:Food	4:Snack	 5:Other
insert into Product
	([product_id], [name], [info], [price], [type], [deleted], [Discount], [std_stats], [ImageLink])
values		-- đồ uống
('P000000030',	N'Pepsi',					N'',		25,		0, 0, 0, 'Drink',	'pepsi.jpg'),
('P000000031',	N'7up',						N'',		25,		0, 0, 0, 'Drink',	'seven.jpg'),
('P000000032',	N'Water',					N'',		25,		0, 0, 0, 'Drink',	'Aqua.jpg'),
('P000000033',	N'Black Coffee',			N'',		30,		6, 0, 0, 'Drink',	'BlackCoffee.jpg'),
('P000000034',	N'Coffee Milk',				N'',		35,		6, 0, 0, 'Drink',	'MilkCoffee.jpg'),
('P000000035',	N'Cream Coffee',			N'',		40,		6, 0, 0, 'Drink',	'CreamCoffee.jpg'),
('P000000036',	N'Americano',				N'',		40,		6, 0, 0, 'Drink',	'Americano.jpg'),
('P000000037',	N'Durian Coffee',			N'',		50,		6, 0, 0, 'Drink',	''),
('P000000038',	N'Coffee Latte',			N'',		50,		6, 0, 0, 'Drink',	'CoffeeLatte.png'),
('P000000039',	N'Cappucino',				N'',		50,		6, 0, 0, 'Drink',	'Capuchino.jpg'),
('P000000040',	N'Orange Coffee',			N'',		50,		6, 0, 0, 'Drink',	''),
('P000000041',	N'Tiramisu Coffee',			N'',		50,		6, 0, 0, 'Drink',	''),
('P000000042',	N'Chocolate Coffee',		N'',		60,		6, 0, 0, 'Drink',	'ChocoLatte.jpg'),
('P000000043',	N'Caramel Cofffee',			N'',		60,		6, 0, 0, 'Drink',	'CaramelCoffee.jpg'),
('P000000044',	N'Strawberry Tea',			N'',		30,		0, 0, 0, 'Drink',	''),
('P000000045',	N'Lemon Tea',				N'',		30,		0, 0, 0, 'Drink',	''),
('P000000046',	N'Apple Tea',				N'',		30,		0, 0, 0, 'Drink',	''),
('P000000047',	N'Milk Tea',				N'',		40,		0, 0, 0, 'Drink',	'MilkTea.jpg'),
('P000000048',	N'Peach Tea',				N'',		50,		0, 0, 0, 'Drink',	'PeachTea.png'),
('P000000049',	N'Matcha Latte',			N'',		50,		0, 0, 0, 'Drink',	'MatchaLatte.jpg'),
('P000000050',	N'Ginger Honey Latte',		N'',		50,		0, 0, 0, 'Drink',	''),
('P000000051',	N'Hot Choco',				N'',		60,		0, 0, 0, 'Drink',	''),
('P000000052',	N'Ice Choco',				N'',		60,		0, 0, 0, 'Drink',	''),
('P000000053',	N'Orange Juice',			N'',		40,		0, 0, 0, 'Drink',	'OrangeJuice.jpg'),
('P000000054',	N'Orange Ade',				N'',		40,		0, 0, 0, 'Drink',	''),
('P000000055',	N'Lemonade',				N'',		40,		0, 0, 0, 'Drink',	'Lemonade.jpg'),

('P000000058',	N'SACRED FIRE GOLDEN ALE',	N'',		40,		2, 0, 0, 'Drink',	'sacred-fire-golden-ale.png'),
('P000000059',	N'PITILESS FOLLY PALE ALE',	N'',		40,		2, 0, 0, 'Drink',	'pitiless-folly-pale-ale.png'),
('P000000060',	N'KURTZ INSANE IPA',		N'',		40,		2, 0, 0, 'Drink',	'kurtz-insane-ipa.jpg'),
('P000000061',	N'PATIENT WILDERNESS WHEAT ALE',	N'',40,		2, 0, 0, 'Drink',	'patient-wilderness-wheat.jpg'),
('P000000062',	N'PRIMEVAL FOREST PILSNER',	N'',		40,		2, 0, 0, 'Drink',	'primeval-forest-dilsner.jpg'),
('P000000063',	N'Double IPA (Winter)',		N'',		50,		2, 0, 0, 'Drink',	'double-ipa.png'),
('P000000064',	N'Christmas Tripel IPA',	N'',		50,		2, 0, 0, 'Drink',	''),

('P000000065',	N'Bordeaux',				N'',		40,		3, 0, 0, 'Drink',	''),
('P000000066',	N'Champagne',				N'',		40,		3, 0, 0, 'Drink',	''),
('P000000067',	N'Chianti',					N'',		40,		3, 0, 0, 'Drink',	''),
('P000000068',	N'Rioja',					N'',		45,		3, 0, 0, 'Drink',	''),
('P000000069',	N'Priorat',					N'',		40,		3, 0, 0, 'Drink',	''),
('P000000070',	N'Port',					N'',		40,		3, 0, 0, 'Drink',	''),
('P000000071',	N'Meritage',				N'',		40,		3, 0, 0, 'Drink',	''),
('P000000072',	N'Côtes du Rhône',			N'',		40,		3, 0, 0, 'Drink',	''),
('P000000073',	N'Martinez',				N'',		45,		7, 0, 0, 'Drink',	''),
('P000000074',	N'Martini',					N'',		45,		7, 0, 0, 'Drink',	''),
('P000000075',	N'Mint Julep',				N'',		45,		7, 0, 0, 'Drink',	''),
('P000000076',	N'Last Word',				N'',		45,		7, 0, 0, 'Drink',	''),
('P000000077',	N'Jack Rose',				N'',		45,		7, 0, 0, 'Drink',	''),
('P000000078',	N'Bloody Mary',				N'',		50,		7, 0, 0, 'Drink',	''),
('P000000079',	N'Negroni',					N'',		50,		7, 0, 0, 'Drink',	''),
('P000000080',	N'Whiskey Sour',			N'',		50,		7, 0, 0, 'Drink',	'')


insert into Product 
	([product_id], [name], [info], [price], [type], [deleted], [Discount], [std_stats], [ImageLink])
values		-- thức ăn
('P000000081',	N'Fruit Platter',			N'',		50,		1, 0, 0, 'BreakFast',	'FruitPlatterWithGreekYoghurt.jpeg'),
('P000000082',	N'Bread Basket',			N'',		50,		1, 0, 0, 'BreakFast',	'BreadBasket.jpg'),
('P000000083',	N'Banana Bread',			N'',		50,		1, 0, 0, 'BreakFast',	'BananaBread.jpg'),
('P000000084',	N'Cinnamon Toast',			N'',		50,		1, 0, 0, 'BreakFast',	'CinamonToast.jpg'),
('P000000085',	N'French Toast',			N'',		50,		1, 0, 0, 'BreakFast',	'FrenchToast.jpg'),
('P000000086',	N'Lemon Pancake',			N'',		60,		1, 0, 0, 'BreakFast',	'LemonPancake.jpg'),
('P000000087',	N'Braised Mix Mushroom',	N'',		70,		1, 0, 0, 'BreakFast',	'BraisedMushroomOntoast.jpg'),
('P000000088',	N'Fried/Poach Eggs',		N'',		70,		1, 0, 0, 'BreakFast',	''),
('P000000089',	N'Bake Eggs with Chorizo',	N'',		70,		1, 0, 0, 'BreakFast',	'BakeEggsWithChorizoandSpinach.jpg'),
('P000000090',	N'Ham Cheese Omelet',		N'',		70,		1, 0, 0, 'BreakFast',	'HamCheeseAndAsparagusOmelette.jpg'),
																
('P000000091',	N'Cheese melt',				N'',		50,		1, 0, 0, 'KigBreakFast',	'CheeseMeltOnToast.png'),
('P000000092',	N'Honey Butter Bread',		N'',		70,		1, 0, 0, 'KigBreakFast',	'HoneyButterBread.jpg'),
('P000000093',	N'Chocolate Fruits',		N'',		70,		1, 0, 0, 'KigBreakFast',	'ChcocolateFruitStack.png'),
('P000000094',	N'Fried Egg Toast',			N'',		80,		1, 0, 0, 'KigBreakFast',	'fried-egg-on-toast.jpg'),

('P000000095',	N'Sweet Potatoes Fries',	N'',		50,		1, 0, 0, 'Starter',	'Sweet_Potatoes_fries.jpg'),
('P000000096',	N'Roasted Garlic Bread',	N'',		60,		1, 0, 0, 'Starter',	'roastedGarlicBread.jpg'),
('P000000097',	N'Mix Italian Olives',		N'',		60,		1, 0, 0, 'Starter',	'ItalianMixOlives.jpg'),
('P000000098',	N'Freshly shuck',			N'',		80,		1, 0, 0, 'Starter',	'freshlyShuckedOysters.jpg'),
('P000000099',	N'Warm Edamame',			N'',		80,		1, 0, 0, 'Starter',	'warm_Edamame.jpg'),
('P000000100',	N'Bocconcini Bruschetta',	N'',		60,		1, 0, 0, 'Starter',	'BoconcciniBruschetta.jpg'),
('P000000101',	N'Duck Liver Pate',			N'',		70,		1, 0, 0, 'Starter',	'Duck_liver_Pate.jpg'),
('P000000102',	N'3 Cheese Board',			N'',		80,		1, 0, 0, 'Starter',	'3CheeseBoard.jpg'),
('P000000103',	N'Dips Board',				N'',		60,		1, 0, 0, 'Starter',	'3DipsBoard.jpg'),
('P000000104',	N'Chorizos roll',			N'',		80,		1, 0, 0, 'Starter',	'ChorizoSausageRolls.jpg'),

('P000000105',	N'Garden Green',			N'',		80,		1, 0, 0, 'Starter',		'GardenGreenSalad.jpg'),
('P000000106',	N'Roasted Vegs',			N'',		80,		1, 0, 0, 'Starter',		'RoastedVegAndRocketSalad.jpeg'),
('P000000107',	N'Caesar Salad',			N'',		80,		1, 0, 0, 'Starter',		'ChickenCaesarSalad.jpg'),
('P000000108',	N'Cajun Chicken',			N'',		80,		1, 0, 0, 'Starter',		'CajunChickenSalad.jpg'),
('P000000109',	N'Minute Steak',			N'',		80,		1, 0, 0, 'Starter',		'MinuteSteakSandwich.jpg'),
('P000000110',	N'Chicken Melt',			N'',		80,		1, 0, 0, 'Starter',		'ChickenMelt.jpg'),
('P000000111',	N'House Burger',			N'',		80,		1, 0, 0, 'Starter',		'HouseBurger.jpg'),
('P000000112',	N'Spaghetti Bolognese',		N'',		80,		1, 0, 0, 'Starter',		'SpaghettiBolognese.jpg'),
('P000000113',	N'Salmon Tagliatelle',		N'',		80,		1, 0, 0, 'Starter',		'SalmonTagliatelle.jpg'),
('P000000114',	N'Chicken Pesto',			N'',		80,		1, 0, 0, 'Starter',		'SpaghettiChickenPesto.jpg'),
('P000000115',	N'Seafood Marinara',		N'',		80,		1, 0, 0, 'Starter',		'SeafoodMarinara.jpeg'),

('P000000116',	N'Mussle Pot Blue Cheese',	N'',		100,	1, 0, 0, 'Main',	'BlueCheeseMusselPot.jpg'),
('P000000117',	N'Mussel Pot Tomatoes',		N'',		100,	1, 0, 0, 'Main',	'RoastedTomatoesMusselPot.jpg'),
('P000000118',	N'Mussel Pot Thai',			N'',		100,	1, 0, 0, 'Main',	'TomYumMusselPot.jpg'),
('P000000119',	N'Grilled Salmon',			N'',		120,	1, 0, 0, 'Main',	'GrilledSalmonBrochette.jpg'),
('P000000120',	N'1/2 Chicken',				N'',		100,	1, 0, 0, 'Main',	'RoastedHalfChicken.jpg'),
('P000000121',	N'Soak Duck',				N'',		120,	1, 0, 0, 'Main',	'HODSoakDuckBreast.jpg'),
('P000000122',	N'BBQ Ribs',				N'',		150,	1, 0, 0, 'Main',	'RoastedBabyBackRibs.jpg'),
('P000000123',	N'Lamb Rack',				N'',		150,	1, 0, 0, 'Main',	'WholeRackOfLamb.jpg'),
('P000000124',	N'Rib Eye',					N'',		150,	1, 0, 0, 'Main',	'RibeyeSteak.jpg')


--insert into Product 
--	([product_id], [name], [info], [price], [type], [deleted], [Discount], [std_stats], [ImageLink])
--values		-- thức ăn
--('P000000001',	N'plain yogurt',			N'',		25,		1, 0, 0, 'Dessert'),
--('P000000002',	N'choco fondue',			N'',		70,		1, 0, 0, 'Dessert'),
--('P000000003',	N'choco cloud',				N'',		55,		1, 0, 0, 'Dessert'),
--('P000000004',	N'custard bread tower',		N'',		70,		1, 0, 0, 'Dessert'),
--('P000000005',	N'choco muffin',			N'',		25,		1, 0, 0, 'Dessert'),
--('P000000006',	N'apple muffin',			N'',		25,		1, 0, 0, 'Dessert'),
--('P000000007',	N'greentea muffin',			N'',		25,		1, 0, 0, 'Dessert'),
--('P000000008',	N'banana cake',				N'',		25,		1, 0, 0, 'Dessert'),
--('P000000009',	N'carrot cake',				N'',		25,		1, 0, 0, 'Dessert'),
--('P000000010',	N'french fries',			N'',		35,		1, 0, 0, 'Starter'),
--('P000000011',	N'french toast',			N'',		45,		1, 0, 0, 'Main'),
--('P000000012',	N'tiramisu cake',			N'',		45,		1, 0, 0, 'Dessert'),
--('P000000013',	N'cheese hotdog',			N'',		35,		1, 0, 0, 'Starter'),
--('P000000014',	N'cereal & milk',			N'',		50,		1, 0, 0, 'Main'),
--('P000000015',	N'honey butter bread',		N'',		70,		1, 0, 0, 'Main'),
--('P000000016',	N'pumpkin soup',			N'',		35,		1, 0, 0, 'Starter'),
--('P000000017',	N'chilli fries',			N'',		50,		1, 0, 0, 'Starter'),
--('P000000018',	N'tortillas nachos',		N'',		50,		1, 0, 0, 'Main'),
--('P000000019',	N'chicken melt',			N'',		50,		1, 0, 0, 'Main'),
--('P000000020',	N'comma club',				N'',		55,		1, 0, 0, 'Main'),
--('P000000021',	N'gourmet berger',			N'',		60,		1, 0, 0, 'Main'),
--('P000000022',	N'spaghetti bolognese',		N'',		55,		1, 0, 0, 'Main'),
--('P000000023',	N'spaghetti carbonara',		N'',		55,		1, 0, 0, 'Main'),
--('P000000024',	N'noodle eggs omelette',	N'',		45,		1, 0, 0, 'Main'),
--('P000000025',	N'chicken burrito',			N'',		60,		1, 0, 0, 'Main'),
--('P000000026',	N'hawaiian pizza',			N'',		60,		1, 0, 0, 'Main'),
--('P000000027',	N'comma pizza',				N'',		60,		1, 0, 0, 'Main'),
--('P000000028',	N'chicken cajun salad',		N'',		55,		1, 0, 0, 'Main'),
--('P000000029',	N'bibimbob',				N'',		60,		1, 0, 0, 'Main')	


--insert into ProductDetails values
--('PD00000001','P000000031',	'IGD0000002',	250,	N'mililiter'			),
--('PD00000002','P000000032',	'IGD0000003',	250,	N'mililiter'			),
--('PD00000003','P000000033',	'IGD0000006',	50,		N'mililiter'			),
--('PD00000004','P000000033',	'IGD0000021',	5,		N'mililiter'	),
--('PD00000005','P000000034',	'IGD0000006',	40,		N'mililiter'			),
--('PD00000006','P000000034',	'IGD0000009',	20,		N'mililiter'			),
--('PD00000007','P000000035',	'IGD0000006',	40,		N'mililiter'			),
--('PD00000008','P000000035',	'IGD0000007',	1,		N'milkF'		),
--('PD00000009','P000000035',	'IGD0000021',	2,		N'lần syrup'	),
--('PD00000010','P000000035',	'IGD0000022',	10,		N'gram'			),
--('PD00000011','P000000036',	'IGD0000005',	1,		N'shot'			),
--('PD00000012','P000000036',	'IGD0000021',	1,		N'lần syrup'	),
--('PD00000013','P000000037',	'IGD0000007',	1,		N'Milk(1Cup)'	),
--('PD00000014','P000000037',	'IGD0000009',	10,		N'ml'			),
--('PD00000015','P000000037',	'IGD0000015',	1,		N'gói'			),
--('PD00000016','P000000038',	'IGD0000005',	1,		N'shot'			),
--('PD00000017','P000000038',	'IGD0000007',	1,		N'Milk(1Cup)'	),
--('PD00000018','P000000038',	'IGD0000021',	2,		N'lần dường'	),
--('PD00000019','P000000039',	'IGD0000005',	1,		N'shot'		),
--('PD00000020','P000000039',	'IGD0000007',	1,		N'Milk(1Cup)'	),
--('PD00000021','P000000039',	'IGD0000021',	2,		N'lần syrup'	),
--('PD00000022','P000000042',	'IGD0000005',	2,		N'shot'			),
--('PD00000023','P000000042',	'IGD0000007',	1,		N'Milk(1Cup)'	),
--('PD00000024','P000000042',	'IGD0000021',	3,		N'lần syrup'	),
--('PD00000028','P000000042',	'IGD0000007',	1,		N'nl choco'		),
--('PD00000029','P000000042',	'IGD0000021',	1,		N'nl choco'		),
--('PD00000030','P000000042',	'IGD0000046',	1,		N'nl choco'		),
--('PD00000031','P000000043',	'IGD0000005',	1,		N'shot'			),
--('PD00000088','P000000043',	'IGD0000007',	1,		N'Milk(1Cup)'	),
--('PD00000032','P000000043',	'IGD0000021',	3,		N'lần syrup'	),
--('PD00000033','P000000043',	'IGD0000021',	1,		N'nl caramel'	),
--('PD00000027','P000000040',	'IGD0000005',	1,		N'shot'			),
--('PD00000025','P000000040',	'IGD0000007',	1,		N'Milk(1Cup)'	),
--('PD00000026','P000000040',	'IGD0000021',	2,		N'lần syrup'	),
--('PD00000034','P000000040',	'IGD0000024',	20,		N'gram'			),
--('PD00000035','P000000040',	'IGD0000007',	0.5,	N'milkF'		),
--('PD00000036','P000000041',	'IGD0000005',	1,		N'shot'		),
--('PD00000037','P000000041',	'IGD0000007',	1,		N'Milk(1Cup)'	),
--('PD00000038','P000000041',	'IGD0000020',	10,		N'gram'			),
--('PD00000039','P000000041',	'IGD0000021',	2,		N'lần syrup'	),
--('PD00000040','P000000041',	'IGD0000022',	5,		N'gram'		),
--('PD00000041','P000000041',	'IGD0000007',	0.5,	N'milkF'		),
--('PD00000042','P000000041',	'IGD0000011',	1,		N'nl tiramisu'	),
--('PD00000043','P000000041',	'IGD0000012',	1,		N'nl tiramisu'	),
--('PD00000044','P000000041',	'IGD0000021',	1,		N'nl tiramisu'	),
--('PD00000045','P000000044',	'IGD0000017',	1,		N'túi lọc'		),
--('PD00000046','P000000044',	'IGD0000021',	1,		N'lần syrup'	),
--('PD00000047','P000000045',	'IGD0000019',	1,		N'túi lọc'		),
--('PD00000048','P000000045',	'IGD0000021',	1,		N'lần syrup'	),
--('PD00000049','P000000046',	'IGD0000018',	1,		N'túi lọc'		),
--('PD00000050','P000000046',	'IGD0000021',	1,		N'lần syrup'	),
--('PD00000051','P000000047',	'IGD0000009',	15,		N'ml'			),
--('PD00000052','P000000047',	'IGD0000013',	1.5,	N'gói'			),
--('PD00000053','P000000048',	'IGD0000016',	1,		N'gói'			),
--('PD00000054','P000000048',	'IGD0000021',	4,		N'lần syrup'	),
--('PD00000055','P000000048',	'IGD0000023',	3,		N'miếng'		),
--('PD00000056','P000000048',	'IGD0000030',	4,		N'lần syrup'	),
--('PD00000057','P000000049',	'IGD0000007',	1,		N'Milk(1Cup)'	),
--('PD00000058','P000000049',	'IGD0000014',	1.5,	N'gói'			),
--('PD00000059','P000000050',	'IGD0000007',	1,		N'Milk(1Cup)'	),
--('PD00000060','P000000050',	'IGD0000031',	20,		N'ml'			),
--('PD00000061','P000000051',	'IGD0000021',	1,		N'lần syrup'	),
--('PD00000062','P000000051',	'IGD0000007',	1,		N'nl choco'		),
--('PD00000063','P000000051',	'IGD0000021',	1,		N'nl choco'		),
--('PD00000064','P000000051',	'IGD0000046',	1,		N'nl choco'		),
--('PD00000065','P000000052',	'IGD0000021',	1,		N'lần syrup'	),
--('PD00000066','P000000052',	'IGD0000007',	1,		N'nl choco'		),
--('PD00000067','P000000052',	'IGD0000021',	1,		N'nl choco'		),
--('PD00000068','P000000052',	'IGD0000046',	1,		N'nl choco'		),
--('PD00000069','P000000057',	'IGD0000026',	1,		N'trái vừa'		),
--('PD00000070','P000000053',	'IGD0000026',	2,		N'trái nhỏ'		),
--('PD00000071','P000000056',	'IGD0000026',	0.5,	N'trái lớn'		),
--('PD00000072','P000000054',	'IGD0000026',	1,		N'trái'		),
--('PD00000073','P000000054',	'IGD0000003',	100,	N'ml'			),
--('PD00000074','P000000054',	'IGD0000021',	1,		N'lần syrup'	),
--('PD00000075','P000000055',	'IGD0000010',	100,	N'ml'			),
--('PD00000076','P000000055',	'IGD0000021',	6,		N'lần syrup'	),
--('PD00000077','P000000055',	'IGD0000029',	4,		N'lần syrup'	),
--('PD00000078','P000000055',	'IGD0000027',	1,		N'trái'			),
--('PD00000079','P000000030',	'IGD0000001',	1,		N'chai'			)



insert into SalaryNote
	([sn_id], [emp_id], [salary_value], [work_hour], [for_month], [for_year], [is_paid])
values
('SAN0000001', 'EMP0000001', 300, 30, 11, 2017, 0),
('SAN0000002', 'EMP0000002', 240, 20, 11, 2017, 0),
('SAN0000003', 'EMP0000003', 110, 10, 11, 2017, 0),
('SAN0000004', 'EMP0000004', 300, 30, 11, 2017, 0),
('SAN0000005', 'EMP0000005', 240, 20, 11, 2017, 0),
('SAN0000006', 'EMP0000006', 110, 10, 11, 2017, 0),
('SAN0000007', 'EMP0000001', 300, 30, 10, 2017, 1),
('SAN0000008', 'EMP0000002', 240, 20, 10, 2017, 1),
('SAN0000009', 'EMP0000003', 110, 10, 10, 2017, 1),
('SAN0000010', 'EMP0000004', 300, 30, 10, 2017, 1),
('SAN0000011', 'EMP0000005', 240, 20, 10, 2017, 1),
('SAN0000012', 'EMP0000006', 110, 10, 10, 2017, 1),
('SAN0000013', 'EMP0000001', 300, 30, 9, 2017, 1),
('SAN0000014', 'EMP0000002', 240, 20, 9, 2017, 1),
('SAN0000015', 'EMP0000003', 110, 10, 9, 2017, 1),
('SAN0000016', 'EMP0000004', 300, 30, 9, 2017, 1),
('SAN0000017', 'EMP0000005', 240, 20, 9, 2017, 1),
('SAN0000018', 'EMP0000006', 110, 10, 9, 2017, 1)

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
('WOH0000011', 'SAN0000003',	'EMP0000003', '2017-11-04 08:0:0', '2017-11-04 10:0:0'),
('WOH0000012', 'SAN0000004',	'EMP0000004', '2017-11-01 08:0:0', '2017-11-01 12:0:0'),
('WOH0000013', 'SAN0000004',	'EMP0000004', '2017-11-02 08:0:0', '2017-11-02 12:0:0'),
('WOH0000014', 'SAN0000004',	'EMP0000004', '2017-11-03 08:0:0', '2017-11-03 12:0:0'),
('WOH0000015', 'SAN0000004',	'EMP0000004', '2017-11-06 08:0:0', '2017-11-06 12:0:0'),
('WOH0000016', 'SAN0000004',	'EMP0000004', '2017-11-10 08:0:0', '2017-11-10 12:0:0'),
('WOH0000017', 'SAN0000004',	'EMP0000004', '2017-11-11 12:0:0', '2017-11-11 22:0:0'),
('WOH0000018', 'SAN0000005',	'EMP0000005', '2017-11-03 12:0:0', '2017-11-03 22:0:0'),
('WOH0000019', 'SAN0000005',	'EMP0000005', '2017-11-04 08:0:0', '2017-11-04 18:0:0'),
('WOH0000020', 'SAN0000006',	'EMP0000006', '2017-11-01 08:0:0', '2017-11-01 12:0:0'),
('WOH0000021', 'SAN0000006',	'EMP0000006', '2017-11-04 08:0:0', '2017-11-04 12:0:0'),
('WOH0000022', 'SAN0000006',	'EMP0000006', '2017-11-04 08:0:0', '2017-11-04 10:0:0'),
('WOH0000023', 'SAN0000007',	'EMP0000001', '2017-10-01 08:0:0', '2017-10-01 12:0:0'),
('WOH0000024', 'SAN0000007',	'EMP0000001', '2017-10-02 08:0:0', '2017-10-02 12:0:0'),
('WOH0000025', 'SAN0000007',	'EMP0000001', '2017-10-03 08:0:0', '2017-10-03 12:0:0'),
('WOH0000026', 'SAN0000007',	'EMP0000001', '2017-10-06 08:0:0', '2017-10-06 12:0:0'),
('WOH0000027', 'SAN0000007',	'EMP0000001', '2017-10-10 08:0:0', '2017-10-10 12:0:0'),
('WOH0000028', 'SAN0000007',	'EMP0000001', '2017-10-11 12:0:0', '2017-10-11 22:0:0'),
('WOH0000029', 'SAN0000008',	'EMP0000002', '2017-10-03 12:0:0', '2017-10-03 22:0:0'),
('WOH0000030', 'SAN0000008',	'EMP0000002', '2017-10-04 08:0:0', '2017-10-04 18:0:0'),
('WOH0000031', 'SAN0000009',	'EMP0000003', '2017-10-01 08:0:0', '2017-10-01 12:0:0'),
('WOH0000032', 'SAN0000009',	'EMP0000003', '2017-10-04 08:0:0', '2017-10-04 12:0:0'),
('WOH0000033', 'SAN0000009',	'EMP0000003', '2017-10-04 08:0:0', '2017-10-04 10:0:0'),
('WOH0000034', 'SAN0000010',	'EMP0000004', '2017-10-01 08:0:0', '2017-10-01 12:0:0'),
('WOH0000035', 'SAN0000010',	'EMP0000004', '2017-10-02 08:0:0', '2017-10-02 12:0:0'),
('WOH0000036', 'SAN0000010',	'EMP0000004', '2017-10-03 08:0:0', '2017-10-03 12:0:0'),
('WOH0000037', 'SAN0000010',	'EMP0000004', '2017-10-06 08:0:0', '2017-10-06 12:0:0'),
('WOH0000038', 'SAN0000010',	'EMP0000004', '2017-10-10 08:0:0', '2017-10-10 12:0:0'),
('WOH0000039', 'SAN0000010',	'EMP0000004', '2017-10-11 12:0:0', '2017-10-11 22:0:0'),
('WOH0000040', 'SAN0000011',	'EMP0000005', '2017-10-03 12:0:0', '2017-10-03 22:0:0'),
('WOH0000041', 'SAN0000011',	'EMP0000005', '2017-10-04 08:0:0', '2017-10-04 18:0:0'),
('WOH0000042', 'SAN0000012',	'EMP0000006', '2017-10-01 08:0:0', '2017-10-01 12:0:0'),
('WOH0000043', 'SAN0000012',	'EMP0000006', '2017-10-04 08:0:0', '2017-10-04 12:0:0'),
('WOH0000044', 'SAN0000012',	'EMP0000006', '2017-10-04 08:0:0', '2017-10-04 10:0:0'),
('WOH0000045', 'SAN0000013',	'EMP0000001', '2017-09-01 08:0:0', '2017-09-01 12:0:0'),
('WOH0000046', 'SAN0000013',	'EMP0000001', '2017-09-02 08:0:0', '2017-09-02 12:0:0'),
('WOH0000047', 'SAN0000013',	'EMP0000001', '2017-09-03 08:0:0', '2017-09-03 12:0:0'),
('WOH0000048', 'SAN0000013',	'EMP0000001', '2017-09-06 08:0:0', '2017-09-06 12:0:0'),
('WOH0000049', 'SAN0000013',	'EMP0000001', '2017-09-10 08:0:0', '2017-09-10 12:0:0'),
('WOH0000050', 'SAN0000013',	'EMP0000001', '2017-09-11 12:0:0', '2017-09-11 22:0:0'),
('WOH0000051', 'SAN0000014',	'EMP0000002', '2017-09-03 12:0:0', '2017-09-03 22:0:0'),
('WOH0000052', 'SAN0000014',	'EMP0000002', '2017-09-04 08:0:0', '2017-09-04 18:0:0'),
('WOH0000053', 'SAN0000015',	'EMP0000003', '2017-09-01 08:0:0', '2017-09-01 12:0:0'),
('WOH0000054', 'SAN0000015',	'EMP0000003', '2017-09-04 08:0:0', '2017-09-04 12:0:0'),
('WOH0000055', 'SAN0000015',	'EMP0000003', '2017-09-04 08:0:0', '2017-09-04 10:0:0'),
('WOH0000056', 'SAN0000016',	'EMP0000004', '2017-09-01 08:0:0', '2017-09-01 12:0:0'),
('WOH0000057', 'SAN0000016',	'EMP0000004', '2017-09-02 08:0:0', '2017-09-02 12:0:0'),
('WOH0000058', 'SAN0000016',	'EMP0000004', '2017-09-03 08:0:0', '2017-09-03 12:0:0'),
('WOH0000059', 'SAN0000016',	'EMP0000004', '2017-09-06 08:0:0', '2017-09-06 12:0:0'),
('WOH0000060', 'SAN0000016',	'EMP0000004', '2017-09-10 08:0:0', '2017-09-10 12:0:0'),
('WOH0000061', 'SAN0000016',	'EMP0000004', '2017-09-11 12:0:0', '2017-09-11 22:0:0'),
('WOH0000062', 'SAN0000017',	'EMP0000005', '2017-09-03 12:0:0', '2017-09-03 22:0:0'),
('WOH0000063', 'SAN0000017',	'EMP0000005', '2017-09-04 08:0:0', '2017-09-04 18:0:0'),
('WOH0000064', 'SAN0000018',	'EMP0000006', '2017-09-01 08:0:0', '2017-09-01 12:0:0'),
('WOH0000065', 'SAN0000018',	'EMP0000006', '2017-09-04 08:0:0', '2017-09-04 12:0:0'),
('WOH0000066', 'SAN0000018',	'EMP0000006', '2017-09-04 08:0:0', '2017-09-04 10:0:0')



insert into [OrderNote]
	([ordernote_id], [cus_id], [emp_id], [ordertable], [ordertime], [totalPrice_nonDisc], [total_price], [customer_pay], [pay_back], [discount], [pay_method])
values
('ORD0000001', 'CUS0000001', 'EMP0000001', 1, '2017-10-08 17:57:28',	 139,		139,		139,	0		, 0,	0),
('ORD0000002', 'CUS0000001', 'EMP0000001', 2, '2017-10-10 19:50:28',	 300.5,		300.5,		300.5,	0		, 0,	0),
('ORD0000003', 'CUS0000001', 'EMP0000004', 3, '2017-10-11 10:00:28',	 271.5,		271.5,		271.5,	0		, 0,	0),
('ORD0000004', 'CUS0000001', 'EMP0000001', 1, '2017-10-12 13:20:28',	 681.5,		681.5,	 	681.5,	0		, 0,	0),
('ORD0000005', 'CUS0000001', 'EMP0000001', 2, '2017-10-12 17:00:28',	 352.5,		352.5,		352.5,	0		, 0,	3),
('ORD0000006', 'CUS0000001', 'EMP0000002', 3, '2017-10-12 14:30:28',	 266,		266,		266,	0		, 0,	3),
('ORD0000007', 'CUS0000001', 'EMP0000001', 1, '2017-10-12 20:00:28',	 139,		139,		200,	61		, 0,	0),
('ORD0000008', 'CUS0000001', 'EMP0000002', 4, '2017-10-12 20:10:28',	 641,		641,		641,	0		, 0,	4),
('ORD0000009', 'CUS0000001', 'EMP0000001', 2, '2017-10-12 20:11:28',	 289,		289,		289,	0		, 0,	4),
('ORD0000010', 'CUS0000001', 'EMP0000004', 1, '2017-10-13 08:20:28',	 185,		185,		185,	0		, 0,	0),
('ORD0000011', 'CUS0000001', 'EMP0000005', 2, '2017-10-13 11:25:28',	 416,		416,		416,	0		, 0,	0),
('ORD0000012', 'CUS0000001', 'EMP0000004', 3, '2017-10-13 11:30:28',	 450.5,		450.5,		450.5,	0		, 0,	3),
('ORD0000013', 'CUS0000001', 'EMP0000004', 4, '2017-10-13 11:35:28',	 346.5,		346.5,		346.5,	0		, 0,	0),
('ORD0000014', 'CUS0000001', 'EMP0000004', 1, '2017-10-13 12:00:28',	 554.5,		554.5,		600,	45.5	, 0,	0),
('ORD0000015', 'CUS0000001', 'EMP0000005', 8, '2017-10-13 12:05:28',	 422,		422,		422,	0		, 0,	0),
('ORD0000016', 'CUS0000001', 'EMP0000005', 8, '2017-10-13 13:15:28',	 358,		358,		358,	0		, 0,	3),
('ORD0000017', 'CUS0000001', 'EMP0000002', 8, '2017-10-14 10:00:28',	 300.5,		300.5,		300.5,	0		, 0,	0),
('ORD0000018', 'CUS0000001', 'EMP0000002', 8, '2017-10-14 11:20:28',	 358,		358,		358,	0		, 0,	4),
																											
('ORD0000019', 'CUS0000001', 'EMP0000001', 2, '2017-11-08 10:57:28',	 323.5,		323.5,		323.5,	0		, 0,	4),
('ORD0000020', 'CUS0000001', 'EMP0000001', 2, '2017-11-10 16:50:28',	 450.5,		450.5,		450.5,	0		, 0,	3),
('ORD0000021', 'CUS0000001', 'EMP0000004', 3, '2017-11-11 09:04:28',	 537,		537,		537,	0		, 0,	3),
('ORD0000022', 'CUS0000001', 'EMP0000001', 1, '2017-11-12 08:30:28',	 479.5,		479.5,		479.5,	0		, 0,	3),
('ORD0000023', 'CUS0000001', 'EMP0000001', 2, '2017-11-12 10:01:28',	 289,		289,		289,	0		, 0,	0),
('ORD0000024', 'CUS0000001', 'EMP0000002', 5, '2017-11-12 10:05:28',	 387,		387,		400,	13		, 0,	0),
('ORD0000025', 'CUS0000001', 'EMP0000001', 1, '2017-11-12 15:24:28',	 762.5,		762.5,		762.5,	0		, 0,	0),
('ORD0000026', 'CUS0000001', 'EMP0000002', 4, '2017-11-12 18:10:28',	 404.5,		404.5,		404.5,	0		, 0,	0),
('ORD0000027', 'CUS0000001', 'EMP0000001', 10,'2017-11-12 20:05:28',	 479.5,		479.5,		479.5,	0		, 0,	0),
('ORD0000028', 'CUS0000001', 'EMP0000004', 1, '2017-11-13 08:20:28',	 335,		335,		350,	15		, 0,	0),
('ORD0000029', 'CUS0000001', 'EMP0000005', 2, '2017-11-13 11:25:28',	 450.5,		450.5,		450.5,	0		, 0,	4),
('ORD0000030', 'CUS0000001', 'EMP0000004', 3, '2017-11-13 12:30:28',	 381.5,		381.5,		381.5,	0		, 0,	4),
('ORD0000031', 'CUS0000001', 'EMP0000004', 4, '2017-11-13 12:35:28',	 479.5,		479.5,		479.5,	0		, 0,	4),
('ORD0000032', 'CUS0000001', 'EMP0000004', 1, '2017-11-13 15:00:28',	 358,		358,		358,	0		, 0,	0),
('ORD0000033', 'CUS0000001', 'EMP0000005', 8, '2017-11-13 15:05:28',	 543,		543,		543,	0		, 0,	4),
('ORD0000034', 'CUS0000001', 'EMP0000005', 8, '2017-11-13 18:15:28',	 543,		543,		543,	0		, 0,	3),
('ORD0000035', 'CUS0000001', 'EMP0000002', 8, '2017-11-14 10:40:28',	 364,		364,		364,	0		, 0,	4),
('ORD0000036', 'CUS0000001', 'EMP0000002', 8, '2017-11-14 11:20:28',	 508.5,		508.5,		508.5,	0		, 0,	3),
('ORD0000037', 'CUS0000001', 'EMP0000001', 1, '2017-11-15 08:57:28',	 202.5,		202.5,		202.5,	0		, 0,	3),
('ORD0000038', 'CUS0000001', 'EMP0000001', 2, '2017-11-15 09:10:28',	 583.5,		583.5,		583.5,	0		, 0,	3),
('ORD0000039', 'CUS0000001', 'EMP0000004', 3, '2017-11-15 10:00:28',	 647,		647,		647,	0		, 0,	0),
('ORD0000040', 'CUS0000002', 'EMP0000001', 1, '2017-11-18 13:08:28',	 358,		286.5,		300,	13.5	, 20,	0),
('ORD0000041', 'CUS0000002', 'EMP0000001', 2, '2017-11-18 17:00:28',	 479.5,		384,		400,	16		, 20,	0),
('ORD0000042', 'CUS0000003', 'EMP0000002', 3, '2017-11-20 11:30:28',	 941.5,		753,		753,	0		, 20,	3),
																											
('ORD0000043', 'CUS0000001', 'EMP0000001', 1, '2017-09-01 10:00:28',	 364,		364,		364,	0		, 0,	0),
('ORD0000044', 'CUS0000001', 'EMP0000002', 4, '2017-09-01 20:10:28',	 439,		439,		439,	0		, 0,	3),
('ORD0000045', 'CUS0000004', 'EMP0000001', 2, '2017-09-02 09:12:28',	 595,		476,		476,	0		, 20,	3),
('ORD0000046', 'CUS0000003', 'EMP0000004', 1, '2017-09-02 09:20:28',	 450.5,		360.5,		360.5,	0		, 20,	3),
('ORD0000047', 'CUS0000001', 'EMP0000005', 2, '2017-09-02 09:25:28',	 502.5,		502.5,		502.5,	0		, 0,	3),
('ORD0000048', 'CUS0000001', 'EMP0000004', 3, '2017-09-10 10:30:28',	 416,		416,		416,	0		, 0,	3),
('ORD0000049', 'CUS0000001', 'EMP0000004', 4, '2017-09-10 11:35:28',	 318,		318,		320,	2		, 0,	0),
('ORD0000050', 'CUS0000002', 'EMP0000004', 1, '2017-09-10 12:01:28',	 370,		296,		296,	0		, 20,	0),
('ORD0000051', 'CUS0000001', 'EMP0000005', 8, '2017-09-10 12:05:28',	 364,		364,		364,	0		, 0,	0),
('ORD0000052', 'CUS0000001', 'EMP0000005', 8, '2017-09-10 13:17:28',	 543,		543,		543,	0		, 0,	4),
('ORD0000053', 'CUS0000001', 'EMP0000002', 8, '2017-09-20 10:00:28',	 260,		260,		260,	0		, 0,	0),
('ORD0000054', 'CUS0000001', 'EMP0000002', 8, '2017-09-20 11:20:28',	 543,		543,		543,	0		, 0,	4)

insert into [OrderNoteDetails]
	([ordernote_id], [product_id], [quan], [discount])
values
('ORD0000001', 'P000000044', 1	,0),
('ORD0000001', 'P000000047', 1	,0),
('ORD0000001', 'P000000049', 1	,0),
('ORD0000002', 'P000000047', 2	,0),
('ORD0000002', 'P000000050', 1	,0),
('ORD0000002', 'P000000001', 3	,0),
('ORD0000002', 'P000000044', 1	,0),
('ORD0000002', 'P000000030', 1	,0),
('ORD0000003', 'P000000001', 1	,0),
('ORD0000003', 'P000000012', 1	,0),
('ORD0000003', 'P000000025', 2	,0),
('ORD0000003', 'P000000056', 1	,0),
('ORD0000003', 'P000000011', 1	,0),
('ORD0000004', 'P000000044', 1	,0),
('ORD0000004', 'P000000014', 1	,0),
('ORD0000004', 'P000000015', 2	,0),
('ORD0000004', 'P000000010', 3	,0),
('ORD0000004', 'P000000029', 1	,0),
('ORD0000004', 'P000000011', 1	,0),
('ORD0000004', 'P000000054', 4	,0),
('ORD0000005', 'P000000017', 1	,0),
('ORD0000005', 'P000000020', 2	,0),
('ORD0000005', 'P000000022', 1	,0),
('ORD0000005', 'P000000011', 2	,0),
('ORD0000006', 'P000000030', 3	,0),
('ORD0000006', 'P000000047', 1	,0),
('ORD0000006', 'P000000031', 1	,0),
('ORD0000006', 'P000000010', 1	,0),
('ORD0000006', 'P000000020', 1	,0),
('ORD0000007', 'P000000044', 1	,0),
('ORD0000007', 'P000000047', 1	,0),
('ORD0000007', 'P000000049', 1	,0),
('ORD0000008', 'P000000014', 1	,0),
('ORD0000008', 'P000000010', 2	,0),
('ORD0000008', 'P000000050', 2	,0),
('ORD0000008', 'P000000011', 1	,0),
('ORD0000008', 'P000000012', 3	,0),
('ORD0000008', 'P000000020', 1	,0),
('ORD0000008', 'P000000019', 2	,0),
('ORD0000009', 'P000000001', 3	,0),
('ORD0000009', 'P000000009', 2	,0),
('ORD0000009', 'P000000011', 1	,0),
('ORD0000009', 'P000000045', 1	,0),
('ORD0000009', 'P000000049', 1	,0),
('ORD0000010', 'P000000034', 3	,0),
('ORD0000010', 'P000000033', 2	,0),
('ORD0000011', 'P000000010', 1	,0),
('ORD0000011', 'P000000011', 2	,0),
('ORD0000011', 'P000000033', 2	,0),
('ORD0000011', 'P000000030', 2	,0),
('ORD0000011', 'P000000020', 1	,0),
('ORD0000011', 'P000000002', 1	,0),
('ORD0000012', 'P000000040', 1	,0),
('ORD0000012', 'P000000001', 1	,0),
('ORD0000012', 'P000000002', 2	,0),
('ORD0000012', 'P000000010', 2	,0),
('ORD0000012', 'P000000034', 3	,0),
('ORD0000013', 'P000000044', 1	,0),
('ORD0000013', 'P000000047', 2	,0),
('ORD0000013', 'P000000049', 1	,0),
('ORD0000013', 'P000000050', 1	,0),
('ORD0000013', 'P000000011', 2	,0),
('ORD0000014', 'P000000010', 2	,0),
('ORD0000014', 'P000000030', 3	,0),
('ORD0000014', 'P000000011', 1	,0),
('ORD0000014', 'P000000033', 1	,0),
('ORD0000014', 'P000000020', 2	,0),
('ORD0000014', 'P000000021', 1	,0),
('ORD0000014', 'P000000029', 1	,0),
('ORD0000015', 'P000000019', 2	,0),
('ORD0000015', 'P000000020', 1	,0),
('ORD0000015', 'P000000010', 1	,0),
('ORD0000015', 'P000000055', 4	,0),
('ORD0000016', 'P000000040', 2	,0),
('ORD0000016', 'P000000047', 1	,0),
('ORD0000016', 'P000000013', 1	,0),
('ORD0000016', 'P000000014', 2	,0),
('ORD0000016', 'P000000010', 1	,0),
('ORD0000017', 'P000000048', 2	,0),
('ORD0000017', 'P000000011', 1	,0),
('ORD0000017', 'P000000020', 1	,0),
('ORD0000017', 'P000000029', 1	,0),
('ORD0000018', 'P000000013', 2	,0),
('ORD0000018', 'P000000047', 2	,0),
('ORD0000018', 'P000000049', 1	,0),
('ORD0000018', 'P000000050', 1	,0),
('ORD0000018', 'P000000021', 1	,0)
insert into [OrderNoteDetails]
	([ordernote_id], [product_id], [quan] ,[discount])
values
('ORD0000019', 'P000000044', 1	,0),
('ORD0000019', 'P000000001', 1	,0),
('ORD0000019', 'P000000020', 1	,0),
('ORD0000019', 'P000000021', 2	,0),
('ORD0000019', 'P000000040', 1	,0),
('ORD0000020', 'P000000002', 1	,0),
('ORD0000020', 'P000000018', 1	,0),
('ORD0000020', 'P000000030', 1	,0),
('ORD0000020', 'P000000033', 2	,0),
('ORD0000020', 'P000000019', 3	,0),
('ORD0000020', 'P000000010', 1	,0),
('ORD0000021', 'P000000010', 1	,0),
('ORD0000021', 'P000000065', 4	,0),
('ORD0000021', 'P000000011', 2	,0),
('ORD0000021', 'P000000029', 3	,0),
('ORD0000022', 'P000000022', 1	,0),
('ORD0000022', 'P000000003', 2	,0),
('ORD0000022', 'P000000059', 2	,0),
('ORD0000022', 'P000000061', 1	,0),
('ORD0000022', 'P000000046', 1	,0),
('ORD0000022', 'P000000019', 2	,0),
('ORD0000023', 'P000000010', 2	,0),
('ORD0000023', 'P000000011', 2	,0),
('ORD0000023', 'P000000033', 3	,0),
('ORD0000024', 'P000000037', 1	,0),
('ORD0000024', 'P000000069', 2	,0),
('ORD0000024', 'P000000017', 2	,0),
('ORD0000024', 'P000000018', 1	,0),
('ORD0000024', 'P000000022', 1	,0),
('ORD0000025', 'P000000020', 1	,0),
('ORD0000025', 'P000000021', 2	,0),
('ORD0000025', 'P000000039', 2	,0),
('ORD0000025', 'P000000038', 1	,0),
('ORD0000025', 'P000000064', 1	,0),
('ORD0000025', 'P000000027', 2	,0),
('ORD0000025', 'P000000028', 3	,0),
('ORD0000026', 'P000000054', 3	,0),
('ORD0000026', 'P000000055', 1	,0),
('ORD0000026', 'P000000009', 2	,0),
('ORD0000026', 'P000000015', 2	,0),
('ORD0000027', 'P000000010', 1	,0),
('ORD0000027', 'P000000025', 1	,0),
('ORD0000027', 'P000000030', 2	,0),
('ORD0000027', 'P000000016', 2	,0),
('ORD0000027', 'P000000060', 5	,0),
('ORD0000028', 'P000000061', 1	,0),
('ORD0000028', 'P000000040', 1	,0),
('ORD0000028', 'P000000050', 1	,0),
('ORD0000028', 'P000000018', 2	,0),
('ORD0000028', 'P000000017', 1	,0),
('ORD0000029', 'P000000016', 2	,0),
('ORD0000029', 'P000000017', 1	,0),
('ORD0000029', 'P000000036', 1	,0),
('ORD0000029', 'P000000059', 1	,0),
('ORD0000029', 'P000000010', 1	,0),
('ORD0000029', 'P000000020', 2	,0),
('ORD0000029', 'P000000012', 1	,0),
('ORD0000030', 'P000000010', 3	,0),
('ORD0000030', 'P000000011', 2	,0),
('ORD0000030', 'P000000034', 1	,0),
('ORD0000030', 'P000000032', 2	,0),
('ORD0000030', 'P000000018', 1	,0),
('ORD0000031', 'P000000028', 1	,0),
('ORD0000031', 'P000000014', 2	,0),
('ORD0000031', 'P000000029', 1	,0),
('ORD0000031', 'P000000066', 1	,0),
('ORD0000031', 'P000000012', 2	,0),
('ORD0000031', 'P000000013', 2	,0),
('ORD0000032', 'P000000013', 2	,0),
('ORD0000032', 'P000000020', 1	,0),
('ORD0000032', 'P000000011', 1	,0),
('ORD0000032', 'P000000054', 1	,0),
('ORD0000032', 'P000000050', 2	,0),
('ORD0000033', 'P000000051', 1	,0),
('ORD0000033', 'P000000020', 3	,0),
('ORD0000033', 'P000000021', 1	,0),
('ORD0000033', 'P000000030', 1	,0),
('ORD0000033', 'P000000010', 2	,0),
('ORD0000033', 'P000000011', 2	,0),
('ORD0000034', 'P000000014', 2	,0),
('ORD0000034', 'P000000055', 2	,0),
('ORD0000034', 'P000000067', 1	,0),
('ORD0000034', 'P000000019', 1	,0),
('ORD0000034', 'P000000020', 2	,0),
('ORD0000034', 'P000000011', 2	,0),
('ORD0000035', 'P000000021', 2	,0),
('ORD0000035', 'P000000031', 2	,0),
('ORD0000035', 'P000000011', 2	,0),
('ORD0000035', 'P000000028', 1	,0),
('ORD0000036', 'P000000029', 1	,0),
('ORD0000036', 'P000000034', 1	,0),
('ORD0000036', 'P000000020', 2	,0),
('ORD0000036', 'P000000065', 2	,0),
('ORD0000036', 'P000000063', 2	,0),
('ORD0000036', 'P000000028', 1	,0),
('ORD0000037', 'P000000022', 1	,0),
('ORD0000037', 'P000000018', 1	,0),
('ORD0000037', 'P000000054', 1	,0),
('ORD0000037', 'P000000045', 1	,0),
('ORD0000038', 'P000000040', 1	,0),
('ORD0000038', 'P000000070', 2	,0),
('ORD0000038', 'P000000016', 1	,0),
('ORD0000038', 'P000000017', 2	,0),
('ORD0000038', 'P000000027', 3	,0),
('ORD0000038', 'P000000033', 2	,0),
('ORD0000039', 'P000000013', 2	,0),
('ORD0000039', 'P000000042', 1	,0),
('ORD0000039', 'P000000044', 2	,0),
('ORD0000039', 'P000000015', 3	,0),
('ORD0000039', 'P000000026', 2	,0),
('ORD0000039', 'P000000066', 1	,0),
('ORD0000040', 'P000000023', 1	,0),
('ORD0000040', 'P000000044', 1	,0),
('ORD0000040', 'P000000043', 2	,0),
('ORD0000040', 'P000000008', 2	,0),
('ORD0000040', 'P000000020', 1	,0),
('ORD0000041', 'P000000030', 1	,0),
('ORD0000041', 'P000000022', 2	,0),
('ORD0000041', 'P000000040', 2	,0),
('ORD0000041', 'P000000011', 1	,0),
('ORD0000041', 'P000000012', 3	,0),
('ORD0000042', 'P000000010', 2	,0),
('ORD0000042', 'P000000014', 3	,0),
('ORD0000042', 'P000000044', 1	,0),
('ORD0000042', 'P000000066', 2	,0),
('ORD0000042', 'P000000050', 2	,0),
('ORD0000042', 'P000000022', 4	,0),
('ORD0000042', 'P000000028', 3	,0)
insert into [OrderNoteDetails]
	([ordernote_id], [product_id], [quan] , [discount])
values
('ORD0000043', 'P000000037', 1	,0),
('ORD0000043', 'P000000038', 1	,0),
('ORD0000043', 'P000000040', 1	,0),
('ORD0000043', 'P000000022', 2	,0),
('ORD0000043', 'P000000021', 1	,0),
('ORD0000044', 'P000000011', 3	,0),
('ORD0000044', 'P000000031', 1	,0),
('ORD0000044', 'P000000033', 2	,0),
('ORD0000044', 'P000000056', 1	,0),
('ORD0000044', 'P000000029', 2	,0),
('ORD0000045', 'P000000039', 1	,0),
('ORD0000045', 'P000000020', 3	,0),
('ORD0000045', 'P000000021', 1	,0),
('ORD0000045', 'P000000065', 2	,0),
('ORD0000045', 'P000000066', 2	,0),
('ORD0000045', 'P000000067', 2	,0),
('ORD0000046', 'P000000017', 2	,0),
('ORD0000046', 'P000000027', 2	,0),
('ORD0000046', 'P000000030', 2	,0),
('ORD0000046', 'P000000045', 1	,0),
('ORD0000046', 'P000000040', 1	,0),
('ORD0000046', 'P000000058', 1	,0),
('ORD0000047', 'P000000040', 3	,0),
('ORD0000047', 'P000000019', 3	,0),
('ORD0000047', 'P000000012', 3	,0),
('ORD0000048', 'P000000013', 2	,0),
('ORD0000048', 'P000000037', 3	,0),
('ORD0000048', 'P000000012', 1	,0),
('ORD0000048', 'P000000023', 1	,0),
('ORD0000048', 'P000000056', 1	,0),
('ORD0000049', 'P000000050', 1	,0),
('ORD0000049', 'P000000024', 2	,0),
('ORD0000049', 'P000000012', 2	,0),
('ORD0000049', 'P000000011', 1	,0),
('ORD0000050', 'P000000021', 3	,0),
('ORD0000050', 'P000000041', 2	,0),
('ORD0000050', 'P000000055', 1	,0),
('ORD0000051', 'P000000050', 1	,0),
('ORD0000051', 'P000000013', 3	,0),
('ORD0000051', 'P000000033', 1	,0),
('ORD0000051', 'P000000054', 2	,0),
('ORD0000051', 'P000000019', 1	,0),
('ORD0000052', 'P000000020', 2	,0),
('ORD0000052', 'P000000021', 2	,0),
('ORD0000052', 'P000000045', 1	,0),
('ORD0000052', 'P000000040', 1	,0),
('ORD0000052', 'P000000041', 2	,0),
('ORD0000052', 'P000000019', 1	,0),
('ORD0000053', 'P000000029', 1	,0),
('ORD0000053', 'P000000010', 1	,0),
('ORD0000053', 'P000000020', 1	,0),
('ORD0000053', 'P000000030', 3	,0),
('ORD0000054', 'P000000030', 1	,0),
('ORD0000054', 'P000000040', 1	,0),
('ORD0000054', 'P000000010', 2	,0),
('ORD0000054', 'P000000020', 1	,0),
('ORD0000054', 'P000000033', 2	,0),
('ORD0000054', 'P000000015', 3	,0)




insert into [ReceiptNote]
	([rn_id], [emp_id], [inday], [total_amount])
values
('RN00000001', 'EMP0000001', '2017-10-08 17:57:28.533', 200),
('RN00000002', 'EMP0000002', '2017-10-08 17:57:28.533', 1200),
('RN00000003', 'EMP0000001', '2017-10-09 17:57:28.533', 400),
('RN00000004', 'EMP0000002', '2017-10-09 17:57:28.533', 600),
('RN00000005', 'EMP0000002', '2017-10-09 17:57:28.533', 100),
('RN00000006', 'EMP0000003', '2017-10-10 17:57:28.533', 1960),
('RN00000007', 'EMP0000004', '2017-10-12 17:57:28.533', 400),
('RN00000008', 'EMP0000005', '2017-10-13 17:57:28.533', 600),
('RN00000009', 'EMP0000003', '2017-10-16 17:57:28.533', 120),
('RN00000010', 'EMP0000003', '2017-10-20 17:57:28.533', 200),
('RN00000011', 'EMP0000002', '2017-11-08 17:57:28.533', 200),
('RN00000012', 'EMP0000002', '2017-11-09 17:57:28.533', 600),
('RN00000013', 'EMP0000003', '2017-11-10 17:57:28.533', 1100),
('RN00000014', 'EMP0000001', '2017-11-10 17:57:28.533', 3000),
('RN00000015', 'EMP0000001', '2017-11-14 17:57:28.533', 200),
('RN00000016', 'EMP0000001', '2017-11-15 17:57:28.533', 200),
('RN00000017', 'EMP0000001', '2017-11-15 17:57:28.533', 120),
('RN00000018', 'EMP0000001', '2017-11-15 17:57:28.533', 2500),
('RN00000019', 'EMP0000001', '2017-11-16 17:57:28.533', 100),
('RN00000020', 'EMP0000001', '2017-11-16 17:57:28.533', 500),
('RN00000021', 'EMP0000001', '2017-09-01 17:57:28.533', 400),
('RN00000022', 'EMP0000001', '2017-09-05 17:57:28.533', 100),
('RN00000023', 'EMP0000001', '2017-09-06 17:57:28.533', 150),
('RN00000024', 'EMP0000001', '2017-09-06 17:57:28.533', 220),
('RN00000025', 'EMP0000001', '2017-09-09 17:57:28.533', 260)

insert into [ReceiptNoteDetails]
	([rn_id], [igd_id], [quan], [item_price], [note])
values
('RN00000001', 'IGD0000001', 2, 100, ''),
('RN00000002', 'IGD0000047', 1, 1000, 'Tien nuoc'),
('RN00000002', 'IGD0000009', 4, 25, ''),
('RN00000002', 'IGD0000008', 2, 50, ''),
('RN00000003', 'IGD0000025', 8, 25, ''),
('RN00000003', 'IGD0000027', 10, 20, ''),
('RN00000004', 'IGD0000020', 2, 100, ''),
('RN00000004', 'IGD0000046', 8, 50, ''),
('RN00000005', 'IGD0000012', 2, 50, ''),
('RN00000006', 'IGD0000047', 1, 1800, 'Tien dien, tien rua may lanh'),
('RN00000006', 'IGD0000033', 5, 20, ''),
('RN00000006', 'IGD0000021', 3, 20, ''),
('RN00000007', 'IGD0000011', 8, 50, ''),
('RN00000008', 'IGD0000028', 10, 5, ''),
('RN00000008', 'IGD0000031', 5, 40, ''),
('RN00000008', 'IGD0000015', 3, 50, ''),
('RN00000008', 'IGD0000012', 4, 50, ''),
('RN00000009', 'IGD0000034', 1, 20, ''),
('RN00000009', 'IGD0000002', 1, 100, ''),
('RN00000010', 'IGD0000003', 2, 100, ''),
('RN00000011', 'IGD0000024', 5, 40, ''),
('RN00000012', 'IGD0000008', 6, 50, ''),
('RN00000012', 'IGD0000007', 6, 50, ''),
('RN00000013', 'IGD0000047', 1, 1100, 'Tien gas'),
('RN00000014', 'IGD0000047', 1, 3000, 'Mua bep gas, Mua lo vi song'),
('RN00000015', 'IGD0000005', 2, 100, ''),
('RN00000016', 'IGD0000003', 1, 100, ''),
('RN00000016', 'IGD0000006', 1, 100, ''),
('RN00000017', 'IGD0000030', 2, 50, ''),
('RN00000017', 'IGD0000028', 1, 10, ''),
('RN00000018', 'IGD0000047', 1, 2000, 'Tien gas'),
('RN00000018', 'IGD0000013', 2, 100, ''),
('RN00000018', 'IGD0000015', 1, 100, ''),
('RN00000018', 'IGD0000014', 2, 100, ''),
('RN00000019', 'IGD0000010', 1, 100, ''),
('RN00000020', 'IGD0000008', 5, 30, ''),
('RN00000020', 'IGD0000011', 7, 50, ''),
('RN00000021', 'IGD0000020', 2, 100, ''),
('RN00000021', 'IGD0000023', 4, 50, ''),
('RN00000022', 'IGD0000023', 2, 50, ''),
('RN00000023', 'IGD0000030', 3, 50, ''),
('RN00000024', 'IGD0000019', 10, 22, ''),
('RN00000025', 'IGD0000002', 2, 100, ''),
('RN00000025', 'IGD0000033', 3, 20, '')




insert into [APWareHouse]
	([apwarehouse_id], [name], [contain], [std_contain])
values
('APW0000001', 'AdPress Warehouse 1', 80, 10000)

-- 1: Cosmetics		2: SpaVoucher	3: GymVoucher	4: ResVoucher		5: TravVoucher		6: Food		7: Agricultural		8: Watch	9: TopTen
insert into [Stock]
	([sto_id], [apwarehouse_id], [name], [group], [barter_code], [barter_name], [unit_in], [unit_out], [standard_price], [info], [supplier], [deleted])
values
('STO0000001', 'APW0000001', 'Voucher Spa Lan Anh', 2, '1111111', 'sample_barter_name', 'pcs', 'pcs', 0, '', 'Spa Lan Anh', 0)

insert into [StockIn]
	([si_id], [ad_id], [intime], [total_amount])
values
('STI0000001', 'AD00000003', '2017-12-14 00:00:00', 20000)

insert into [StockInDetails]
	([si_id], [sto_id], [quan], [item_price], [note])
values
('STI0000001', 'STO0000001', 100, 200, 'voucher từ spa Lan Anh quận 7')

insert into [StockOut]
	([stockout_id], [ad_id], [outTime], [discount], [Vat], [total_amount])
values
('STO0000001', 'AD00000003', '2017-12-15 00:00:00', 0, 0, 2000)

insert into [StockOutDetails]
	([stockout_id], [stock_id], [quan], [discount])
values
('STO0000001', 'STO0000001', 20, 0)


select * from AdminRes
go
select * from Employee
go
select * from Customer
go
	

select *from ReceiptNote
go
select *from ReceiptNoteDetails
go


select * from WareHouse
go 
select * from Ingredient
go
select * from Product
go
select *  from ProductDetails
go
 
select * from SalaryNote
select * from WorkingHistory

select * from OrderNote
select * from OrderNoteDetails


select * from StockOutDetails
select * from StockOut
select * from StockInDetails
select * from StockIn
select * from Stock
select * from APWareHouse

--delete [WorkingHistory]
--delete [SalaryNote]

--delete [ReceiptNoteDetails]
--delete [ReceiptNote]

--delete [OrderNoteDetails]
--delete [OrderNote]

--delete [ProductDetails]
--delete [Product]

--delete [Ingredient]
--delete [WareHouse]

--delete [StockOutDetails]
--delete [StockOut]
--delete [StockInDetails]
--delete [StockIn]
--delete [Stock]
--delete [APWareHouse]
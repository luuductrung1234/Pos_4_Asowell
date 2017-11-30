USE DBAsowell
GO




insert into AdminRes values
('AD00000001', 'admin_username1', 'password2', N'Ton That Vinh'),
('AD00000002', 'admin_username2', 'password3', N'Luu Duc Trung')
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
('IGD0000001',	'WAH0000001',	N'pepsi', 					N'',		0,	N'dry',			N'litre',	    130,0),
('IGD0000002',	'WAH0000002',	N'aquafina', 				N'',		0,	N'dry',			N'litre',	    90,	0),
('IGD0000003',	'WAH0000003',	N'7up', 					N'',		0,	N'dry',			N'litre',	    150,0),
('IGD0000004',	'WAH0000004',	N'water bottle (big)', 		N'',		0,	N'dry',			N'litre',	    90,	0),
('IGD0000005',	'WAH0000005',	N'Coffee Bean', 			N'',		0,	N'dry',			N'kilogram',		0,	0),
('IGD0000006',	'WAH0000006',	N'Trung Nguyen coffee S', 	N'',		0,	N'dry',			N'kilogram',	    45,	0),
('IGD0000007',	'WAH0000007',	N'Dalat milk', 				N'',		0,	N'dairy',		N'litre',		0,	0),
('IGD0000008',	'WAH0000008',	N'Dutch Lady milk', 		N'',		0,	N'dairy',		N'litre',		0,	0),
('IGD0000009',	'WAH0000009',	N'Condense milk', 			N'',		0,	N'dairy',		N'litre',		0,	0),
('IGD0000010',	'WAH0000010',	N'Soda', 					N'',		0,	N'dairy',		N'litre',	0,	0),
('IGD0000011',	'WAH0000011',	N'Whipping cream', 			N'',		0,	N'dairy',		N'litre',		0,	0),
('IGD0000012',	'WAH0000012',	N'Cream cheese', 			N'',		0,	N'dairy',		N'litre',		0,	0),
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
('IGD0000029',	'WAH0000029',	N'Blue curacao syrup', 		N'',		0,	N'dry',			N'litre',	    0,	0),
('IGD0000030',	'WAH0000030',	N'Peach syrup', 			N'',		0,	N'dry',			N'litre',	    0,	0),
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
	([product_id], [name], [info], [price], [type], [deleted], [Discount], [std_stats])
values		-- đồ uống
('P000000030',	N'pepsi',					N'',		25,		0, 0, 0, 'Drink'),
('P000000031',	N'7up',						N'',		25,		0, 0, 0, 'Drink'),
('P000000032',	N'water',					N'',		25,		0, 0, 0, 'Drink'),
('P000000033',	N'black coffee',			N'',		30,		6, 0, 0, 'Drink'),
('P000000034',	N'coffee milk',				N'',		35,		6, 0, 0, 'Drink'),
('P000000035',	N'cream coffee',			N'',		40,		6, 0, 0, 'Drink'),
('P000000036',	N'americano',				N'',		40,		6, 0, 0, 'Drink'),
('P000000037',	N'durian coffee',			N'',		50,		6, 0, 0, 'Drink'),
('P000000038',	N'coffee latte',			N'',		50,		6, 0, 0, 'Drink'),
('P000000039',	N'cappucino',				N'',		50,		6, 0, 0, 'Drink'),
('P000000040',	N'orange coffee',			N'',		50,		6, 0, 0, 'Drink'),
('P000000041',	N'tiramisu coffee',			N'',		50,		6, 0, 0, 'Drink'),
('P000000042',	N'chocolate coffee',		N'',		60,		6, 0, 0, 'Drink'),
('P000000043',	N'caramel cofffee',			N'',		60,		6, 0, 0, 'Drink'),
('P000000044',	N'strawberry tea',			N'',		30,		0, 0, 0, 'Drink'),
('P000000045',	N'lemon tea',				N'',		30,		0, 0, 0, 'Drink'),
('P000000046',	N'apple tea',				N'',		30,		0, 0, 0, 'Drink'),
('P000000047',	N'milk tea',				N'',		40,		0, 0, 0, 'Drink'),
('P000000048',	N'peach tea',				N'',		50,		0, 0, 0, 'Drink'),
('P000000049',	N'matcha latte',			N'',		50,		0, 0, 0, 'Drink'),
('P000000050',	N'ginger honey latte',		N'',		50,		0, 0, 0, 'Drink'),
('P000000051',	N'hot choco',				N'',		60,		0, 0, 0, 'Drink'),
('P000000052',	N'ice choco',				N'',		60,		0, 0, 0, 'Drink'),
('P000000053',	N'orange juice (trái nhỏ)',	N'',		40,		0, 0, 0, 'Drink'),
('P000000054',	N'orange ade',				N'',		40,		0, 0, 0, 'Drink'),
('P000000055',	N'lemonade',				N'',		40,		0, 0, 0, 'Drink'),
('P000000056',	N'orange juice (trái lớn)',	N'',		40,		0, 0, 0, 'Drink'),
('P000000057',	N'orange juice (trái vừa)',	N'',		40,		0, 0, 0, 'Drink'),

('P000000058',	N'SACRED FIRE GOLDEN ALE',	N'',		40,		2, 0, 0, 'Drink'),
('P000000059',	N'PITILESS FOLLY PALE ALE',	N'',		40,		2, 0, 0, 'Drink'),
('P000000060',	N'KURTZ INSANE IPA',		N'',		40,		2, 0, 0, 'Drink'),
('P000000061',	N'PATIENT WILDERNESS WHEAT ALE',	N'',40,		2, 0, 0, 'Drink'),
('P000000062',	N'PRIMEVAL FOREST PILSNER',	N'',		40,		2, 0, 0, 'Drink'),
('P000000063',	N'Double IPA (Winter)',		N'',		50,		2, 0, 0, 'Drink'),
('P000000064',	N'Christmas Tripel IPA',	N'',		50,		2, 0, 0, 'Drink'),

('P000000065',	N'Bordeaux',				N'',		40,		3, 0, 0, 'Drink'),
('P000000066',	N'Champagne',				N'',		40,		3, 0, 0, 'Drink'),
('P000000067',	N'Chianti',					N'',		40,		3, 0, 0, 'Drink'),
('P000000068',	N'Rioja',					N'',		45,		3, 0, 0, 'Drink'),
('P000000069',	N'Priorat',					N'',		40,		3, 0, 0, 'Drink'),
('P000000070',	N'Port',					N'',		40,		3, 0, 0, 'Drink'),
('P000000071',	N'Meritage',				N'',		40,		3, 0, 0, 'Drink'),
('P000000072',	N'Côtes du Rhône',			N'',		40,		3, 0, 0, 'Drink'),
('P000000073',	N'Martinez',				N'',		45,		7, 0, 0, 'Drink'),
('P000000074',	N'Martini',					N'',		45,		7, 0, 0, 'Drink'),
('P000000075',	N'Mint Julep',				N'',		45,		7, 0, 0, 'Drink'),
('P000000076',	N'Last Word',				N'',		45,		7, 0, 0, 'Drink'),
('P000000077',	N'Jack Rose',				N'',		45,		7, 0, 0, 'Drink'),
('P000000078',	N'Bloody Mary',				N'',		50,		7, 0, 0, 'Drink'),
('P000000079',	N'Negroni',					N'',		50,		7, 0, 0, 'Drink'),
('P000000080',	N'Whiskey Sour',			N'',		50,		7, 0, 0, 'Drink')


insert into Product 
	([product_id], [name], [info], [price], [type], [deleted], [Discount], [std_stats])
values		-- thức ăn
('P000000081',	N'Fruit Platter',			N'',		50,		1, 0, 0, 'BreakFast'),
('P000000082',	N'Bread Basket',			N'',		50,		1, 0, 0, 'BreakFast'),
('P000000083',	N'Banana Bread',			N'',		50,		1, 0, 0, 'BreakFast'),
('P000000084',	N'Cinnamon Toast',			N'',		50,		1, 0, 0, 'BreakFast'),
('P000000085',	N'French Toast',			N'',		50,		1, 0, 0, 'BreakFast'),
('P000000086',	N'Lemon Pancake',			N'',		60,		1, 0, 0, 'BreakFast'),
('P000000087',	N'Braised Mix Mushroom',	N'',		70,		1, 0, 0, 'BreakFast'),
('P000000088',	N'Fried/Poach Eggs',		N'',		70,		1, 0, 0, 'BreakFast'),
('P000000089',	N'Bake Eggs with Chorizo',	N'',		70,		1, 0, 0, 'BreakFast'),
('P000000090',	N'Ham Cheese Omelet',		N'',		70,		1, 0, 0, 'BreakFast'),
																
('P000000091',	N'Cheese melt',				N'',		50,		1, 0, 0, 'KigBreakFast'),
('P000000092',	N'Honey Bter Bread',		N'',		70,		1, 0, 0, 'KigBreakFast'),
('P000000093',	N'Chocolate Fruits',		N'',		70,		1, 0, 0, 'KigBreakFast'),
('P000000094',	N'Fried Egg Toast',			N'',		80,		1, 0, 0, 'KigBreakFast'),

('P000000095',	N'Sweet Potatoes Fries',	N'',		50,		1, 0, 0, 'Starter'),
('P000000096',	N'Roasted Garlic Bread',	N'',		60,		1, 0, 0, 'Starter'),
('P000000097',	N'Mix Italian Olives',		N'',		60,		1, 0, 0, 'Starter'),
('P000000098',	N'Freshly shuck',			N'',		80,		1, 0, 0, 'Starter'),
('P000000099',	N'Warm Edamame',			N'',		80,		1, 0, 0, 'Starter'),
('P000000100',	N'Bocconcini Bruschetta',	N'',		60,		1, 0, 0, 'Starter'),
('P000000101',	N'Duck Liver Pate',			N'',		70,		1, 0, 0, 'Starter'),
('P000000102',	N'3 Cheese Board',			N'',		80,		1, 0, 0, 'Starter'),
('P000000103',	N'Dips Board',				N'',		60,		1, 0, 0, 'Starter'),
('P000000104',	N'Chorizos roll',			N'',		80,		1, 0, 0, 'Starter'),

('P000000105',	N'Garden Green',			N'',		80,		1, 0, 0, 'Starter'),
('P000000106',	N'Roasted Vegs',			N'',		80,		1, 0, 0, 'Starter'),
('P000000107',	N'Caesar Salad',			N'',		80,		1, 0, 0, 'Starter'),
('P000000108',	N'Cajun Chicken',			N'',		80,		1, 0, 0, 'Starter'),
('P000000109',	N'Minute Steak',			N'',		80,		1, 0, 0, 'Starter'),
('P000000110',	N'Chicken Melt',			N'',		80,		1, 0, 0, 'Starter'),
('P000000111',	N'House Burger',			N'',		80,		1, 0, 0, 'Starter'),
('P000000112',	N'Spaghetti Bolognese',		N'',		80,		1, 0, 0, 'Starter'),
('P000000113',	N'Salmon Tagliatelle',		N'',		80,		1, 0, 0, 'Starter'),
('P000000114',	N'Chicken Pesto',			N'',		80,		1, 0, 0, 'Starter'),
('P000000115',	N'Seafood Marinara',		N'',		80,		1, 0, 0, 'Starter'),

('P000000116',	N'Mussle Pot Blue Cheese',	N'',		100,	1, 0, 0, 'Main'),
('P000000117',	N'Mussel Pot Tomatoes',		N'',		100,	1, 0, 0, 'Main'),
('P000000118',	N'Mussel Pot Thai',			N'',		100,	1, 0, 0, 'Main'),
('P000000119',	N'Grilled Salmon',			N'',		120,	1, 0, 0, 'Main'),
('P000000120',	N'1/2 Chicken',				N'',		100,	1, 0, 0, 'Main'),
('P000000121',	N'Soak Duck',				N'',		120,	1, 0, 0, 'Main'),
('P000000122',	N'BBQ Ribs',				N'',		150,	1, 0, 0, 'Main'),
('P000000123',	N'Lamb Rack',				N'',		150,	1, 0, 0, 'Main'),
('P000000124',	N'Rib Eye',					N'',		150,	1, 0, 0, 'Main')


insert into Product 
	([product_id], [name], [info], [price], [type], [deleted], [Discount], [std_stats])
values		-- thức ăn
('P000000001',	N'plain yogurt',			N'',		25,		1, 0, 0, 'Dessert'),
('P000000002',	N'choco fondue',			N'',		70,		1, 0, 0, 'Dessert'),
('P000000003',	N'choco cloud',				N'',		55,		1, 0, 0, 'Dessert'),
('P000000004',	N'custard bread tower',		N'',		70,		1, 0, 0, 'Dessert'),
('P000000005',	N'choco muffin',			N'',		25,		1, 0, 0, 'Dessert'),
('P000000006',	N'apple muffin',			N'',		25,		1, 0, 0, 'Dessert'),
('P000000007',	N'greentea muffin',			N'',		25,		1, 0, 0, 'Dessert'),
('P000000008',	N'banana cake',				N'',		25,		1, 0, 0, 'Dessert'),
('P000000009',	N'carrot cake',				N'',		25,		1, 0, 0, 'Dessert'),
('P000000010',	N'french fries',			N'',		35,		1, 0, 0, 'Starter'),
('P000000011',	N'french toast',			N'',		45,		1, 0, 0, 'Main'),
('P000000012',	N'tiramisu cake',			N'',		45,		1, 0, 0, 'Dessert'),
('P000000013',	N'cheese hotdog',			N'',		35,		1, 0, 0, 'Starter'),
('P000000014',	N'cereal & milk',			N'',		50,		1, 0, 0, 'Main'),
('P000000015',	N'honey butter bread',		N'',		70,		1, 0, 0, 'Main'),
('P000000016',	N'pumpkin soup',			N'',		35,		1, 0, 0, 'Starter'),
('P000000017',	N'chilli fries',			N'',		50,		1, 0, 0, 'Starter'),
('P000000018',	N'tortillas nachos',		N'',		50,		1, 0, 0, 'Main'),
('P000000019',	N'chicken melt',			N'',		50,		1, 0, 0, 'Main'),
('P000000020',	N'comma club',				N'',		55,		1, 0, 0, 'Main'),
('P000000021',	N'gourmet berger',			N'',		60,		1, 0, 0, 'Main'),
('P000000022',	N'spaghetti bolognese',		N'',		55,		1, 0, 0, 'Main'),
('P000000023',	N'spaghetti carbonara',		N'',		55,		1, 0, 0, 'Main'),
('P000000024',	N'noodle eggs omelette',	N'',		45,		1, 0, 0, 'Main'),
('P000000025',	N'chicken burrito',			N'',		60,		1, 0, 0, 'Main'),
('P000000026',	N'hawaiian pizza',			N'',		60,		1, 0, 0, 'Main'),
('P000000027',	N'comma pizza',				N'',		60,		1, 0, 0, 'Main'),
('P000000028',	N'chicken cajun salad',		N'',		55,		1, 0, 0, 'Main'),
('P000000029',	N'bibimbob',				N'',		60,		1, 0, 0, 'Main')	


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




--SET IDENTITY_INSERT [Table] ON
--SET IDENTITY_INSERT [Chair] ON
--SET IDENTITY_INSERT [OrderTemp] ON
--SET IDENTITY_INSERT [OrderDetailsTemp] ON

insert into [Table]
	([table_number], [chair_amount], [pos_X], [pos_Y], [is_Locked], [is_Ordered], [is_Pinned], [is_Printed])
values
(1, 6, 200, 200, 0, 1, 1, 0),
(2, 4, 300, 300, 0, 1, 1, 0)


insert into [Chair]
	([chair_number], [table_owned])
values
(1, 1),
(2, 1),
(3, 1),
(4, 1),
(5, 1),
(6, 1),
(1, 2),
(2, 2),
(3, 2),
(4, 2)

insert into [OrderTemp]
	( [cus_id], [emp_id], [table_owned], [ordertime], [totalPrice_nonDisc], [total_price], [customer_pay], [pay_back], [discount])
values
( 'CUS0000001', 'EMP0000001', 1, '2017-10-08 17:57:28.533', 1317,	1317,	1500, 183, 0),
( 'CUS0000001', 'EMP0000001', 2, '2017-10-08 20:00:45.533', 635.5,	635.5,	635.5, 0, 0)


insert into [OrderDetailsTemp]
	([ordertemp_id], [product_id], [chair_id], [SelectedStats], [note], [is_printed], [quan], [discount])
values
('1', 'P000000030', 1, 'Drink',     ''              ,	0,	 1,	0),
('1', 'P000000030', 1, 'Drink',     'more ice'      ,	0,	 2,	0),
('1', 'P000000030', 2, 'Drink',     ''              ,	0,	 1,	0),
('1', 'P000000001', 2, 'Dessert',   ''              ,	0,	 1,	0),
('1', 'P000000002', 2, 'Dessert',   ''              ,	0,	 3,	0),
('1', 'P000000003', 2, 'Main',		''              ,	0,	 2,	0),
('1', 'P000000003', 3, 'Dessert',   'no hanigue'    ,	0,	 2,	0),
('1', 'P000000003', 3, 'Starter',   ''              ,	0,	 2,	0),
('1', 'P000000020', 3, 'Main',		''              ,	0,	 1,	0),
('1', 'P000000075', 3, 'Drink',		''              ,	0,	 1,	0),
('1', 'P000000001', 4, 'Dessert',   ''              ,	0,	 1,	0),
('1', 'P000000032', 4, 'Drink',		'lavie not aqua',	0,	 3,	0),
('1', 'P000000061', 4, 'Drink',		''              ,	0,	 1,	0),
('1', 'P000000021', 5, 'Main',		''              ,	0,	 1,	0),
('1', 'P000000020', 5, 'Main',		'no peper'      ,	0,	 1,	0),
('1', 'P000000001', 5, 'Dessert',   ''              ,	0,	 1,	0),
('1', 'P000000040', 5, 'Drink',		''              ,	0,	 1,	0),
('1', 'P000000068', 6, 'Drink',		''              ,	0,	 2,	0),

('2', 'P000000001', 7, 'Dessert',   ''              ,	0,	 1,	0),
('2', 'P000000001', 7, 'Starter',   ''              ,	0,	 1,	0),
('2', 'P000000045', 7, 'Drink',     'less sugar'    ,	0,	 1,	0),
('2', 'P000000044', 8, 'Drink',     ''              ,	0,	 1,	0),
('2', 'P000000019', 8, 'Main',		''              ,	0,	 1,	0),
('2', 'P000000019', 8, 'Main',		'more salad'    ,	0,	 1,	0),
('2', 'P000000022', 9, 'Main',		''              ,	0,	 2,	0),
('2', 'P000000029', 10, 'Main',		''              ,	0,	 1,	0),
('2', 'P000000040', 10, 'Drink',    ''              ,	0,	 1,	0),
('2', 'P000000005', 10, 'Dessert',  ''              ,	0,	 2,	0),
('2', 'P000000004', 10, 'Dessert',  ''              ,	0,	 1,	0)



select * from [Table]
select * from [Chair]
select * from [OrderTemp]
select * from [OrderDetailsTemp]



--delete [OrderDetailsTemp]
--delete [Chair]
--delete [OrderTemp]
--delete [Table]

--drop table [OrderDetailsTemp]
--drop table [Chair]
--drop table [OrderTemp]
--drop table [Table]
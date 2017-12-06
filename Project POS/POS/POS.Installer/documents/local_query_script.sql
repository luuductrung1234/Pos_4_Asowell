USE DBpos
GO



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
 

 --用户
 create table tb_user
 (
 账号 varchar(20) primary key,
 密码 varchar(20),
 用户名 varchar(30),
 性别 bit,
 年龄 int,
 会员等级 tinyint,
 积分 bigint,
 消费能力 smallint,
 头像 image, 
 注册日期 datetime,
 v int,--版本控制演示：使用int
 上次登录日期 smalldatetime
 )
  
 --产品分类
 --drop table tb_categories
 create table tb_categories
 (
	子ID varchar(100) primary key,--当前分类编码 
	父ID varchar(100),--上级分类编码
	分类名称 nvarchar(100),--当前分类名称 
	v uniqueidentifier,--版本控制演示：使用guid
	顺序 decimal 
 )
 --产品
 create table tb_product
 (
	产品ID uniqueidentifier primary key,
	产品名称  nchar(100), 
	分类编号 char(10),-- 
	进价 real,
	运费 numeric,
	税率 smallmoney,
	零售价 money,
	会员折扣 float,
    产品描述_中文 ntext,
	产品描述_英文 text,
	RSS xml,
	附件1 binary,
	附件2 varbinary, 
	版本 timestamp --版本控制 timestamp
 )
 --订单
 create table tb_order
 (
	订单ID uniqueidentifier primary key,
	订单名称 varchar(100), 
	账号 varchar(20),
	总金额 money, 
	订单时间 datetime
 )
 --订单明细
 --drop table tb_order_detail
 create table tb_order_detail
 (
	订单明细ID uniqueidentifier primary key,
	订单ID uniqueidentifier,
	产品ID uniqueidentifier,
	产品名称  nchar(100), 
	零售价 money,
	支付价 money,
	购买数量 int,
	序号 int
 )
 
 go

 create proc sp_add_order
 @订单号 uniqueidentifier,
 @订单名称 varchar(100), 
 @账号 varchar(20),
 @金额 money
 as
 begin
	insert into tb_order values(@订单号,@订单名称,@账号,@金额,getdate())
 end

 go
 
 
 create proc x_add_order
 @订单号 uniqueidentifier,
 @订单名称 varchar(100), 
 @账号 varchar(20),
 @金额 money
 as
 begin
	insert into tb_order values(@订单号,@订单名称,@账号,@金额,getdate())
	select @@ROWCOUNT
 end

 go
 create proc sp_get_order_all
 as
 begin
	select top 3 * from tb_order(nolock)
 end

 go

create view  v_user_order
as 
	select t1.订单名称,t1.订单ID,t2.购买数量,t1.账号 from tb_order t1
	join tb_order_detail t2
	on t1.订单ID = t2.订单ID
 
 go
 
create proc sp_multi_tb
as
begin 
	waitfor delay '00:00:00.100 ' 
	select * from tb_user
	select * from tb_categories
	select * from tb_product 
end
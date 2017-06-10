 
--远程服务器

 --用户
 create table tb_user
 (
 自增NO int identity(1,1),
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
  
 go
   
create table tb_order_info
(
	订单ID uniqueidentifier,
    账号 varchar(20),
	明细数量 int,
	总金额Max  money,
	总金额Min money,
	总金额Sum money,
	总金额Avg money,
	R金额 money
)
    

/* 本代码由kQL.orm.cmdTool工具自动生成  
   contact:chwmqq@126.com 
   created time:2017-06-09 23:58:48*/
using System;
using System.ComponentModel; 

namespace kQL.orm.demo.models
{
    public class tb_order
    {
	[Description]public Guid 订单ID {get;set;}
	public String 订单名称 {get;set;}
	public String 账号 {get;set;}
	public Decimal 总金额 {get;set;}
	public DateTime 订单时间 {get;set;}
 
    }

}

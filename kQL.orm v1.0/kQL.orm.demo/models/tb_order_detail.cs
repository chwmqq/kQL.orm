
/* 本代码由kQL.orm.cmdTool工具自动生成  
   contact:chwmqq@126.com 
   created time:2017-06-06 15:15:13*/
using System;
using System.ComponentModel; 

namespace kQL.orm.demo.models
{
    public class tb_order_detail
    {
	[Description]public Guid 订单明细ID {get;set;}
	public Guid 订单ID {get;set;}
	public Guid 产品ID {get;set;}
	public String 产品名称 {get;set;}
	public Decimal 零售价 {get;set;}
	public Decimal 支付价 {get;set;}
	public Int32 购买数量 {get;set;}
	public Int32 序号 {get;set;}
 
    }

}

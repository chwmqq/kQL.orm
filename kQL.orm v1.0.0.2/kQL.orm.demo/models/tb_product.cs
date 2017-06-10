
/* 本代码由kQL.orm.cmdTool工具自动生成  
   contact:chwmqq@126.com 
   created time:2017-06-09 23:58:48*/
using System;
using System.ComponentModel; 

namespace kQL.orm.demo.models
{
    public class tb_product
    {
	[Description]public Guid 产品ID {get;set;}
	public String 产品名称 {get;set;}
	public String 分类编号 {get;set;}
	public Single 进价 {get;set;}
	public Decimal 运费 {get;set;}
	public Decimal 税率 {get;set;}
	public Decimal 零售价 {get;set;}
	public Double 会员折扣 {get;set;}
	public String 产品描述_中文 {get;set;}
	public String 产品描述_英文 {get;set;}
	[Category("XML")]public String RSS {get;set;}
	public Byte[] 附件1 {get;set;}
	public Byte[] 附件2 {get;set;}
	[Category("TIMESTAMP")]public String 版本 {get;set;}
 
    }

}

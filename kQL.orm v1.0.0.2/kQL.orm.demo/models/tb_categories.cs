
/* 本代码由kQL.orm.cmdTool工具自动生成  
   contact:chwmqq@126.com 
   created time:2017-06-09 23:58:48*/
using System;
using System.ComponentModel; 

namespace kQL.orm.demo.models
{
    public class tb_categories
    {
	[Description]public String 子ID {get;set;}
	public String 父ID {get;set;}
	public String 分类名称 {get;set;}
	public Guid v {get;set;}
	public Decimal 顺序 {get;set;}
 
    }

}

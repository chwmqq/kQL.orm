
/* 本代码由kQL.orm.cmdTool工具自动生成  
   contact:chwmqq@126.com 
   created time:2017-06-09 23:58:48*/
using System;
using System.ComponentModel; 

namespace kQL.orm.demo.models
{
    public class tb_user
    {
	[Category("IDENTITY")]public Int32 自增NO {get;set;}
	[Description]public String 账号 {get;set;}
	public String 密码 {get;set;}
	public String 用户名 {get;set;}
	public Boolean 性别 {get;set;}
	public Int32 年龄 {get;set;}
	public Byte 会员等级 {get;set;}
	public Int64 积分 {get;set;}
	public Int16 消费能力 {get;set;}
	public Byte[] 头像 {get;set;}
	public DateTime 注册日期 {get;set;}
	public Int32 v {get;set;}
	public DateTime 上次登录日期 {get;set;}
 
    }

}

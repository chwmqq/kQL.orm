using kQL.orm.demo.models;
using kQL.orm.expr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace kQL.orm.demo
{
    class Program
    {
        static void Main(string[] args)
        { 
            SampleCode.测试_清空();
            SampleCode.测试_插入();
            SampleCode.测试_更新();
            SampleCode.测试_存储过程_视图();
            SampleCode.测试_查询();
            SampleCode.测试_扩展方法();
            SampleCode.测试_结果集();
            SampleCode.测试_导数据();
            SampleCode.测试_删除();
            Console.WriteLine("All Finished!!");
            Console.Read(); 
        }
    }
}

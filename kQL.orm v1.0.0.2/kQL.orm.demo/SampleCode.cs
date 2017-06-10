using kQL.orm.demo.models;
using kQL.orm.expr;
using kQL.orm.results;
using kQL.orm.ultility; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace kQL.orm.demo
{
    public class SampleCode
    {
        static Dy localDy = new Dy(); 

        public static byte[] 获取头像(int i)
        {
            var T_头像 = AppDomain.CurrentDomain.BaseDirectory + @"images\{0}.jpg";
            var 头像 = string.Format(T_头像, i % 6 == 0 ? 1 : i % 6);
            return System.IO.File.ReadAllBytes(头像);
        }

        public static void 测试_插入()
        {
            //单条插入、添加一个用户
            var user1 = new tb_user
            {
                自增NO = 1, //自增列 框架不会去插入
                账号 = string.Format("U{0:D4}", 1),
                密码 = "12345678",
                用户名 = string.Format("Tester{0:D3}", 1),
                性别 = true,
                年龄 = new Random().Next(20, 60),
                会员等级 = (byte)(new Random().Next(1, 255)),
                积分 = new Random().Next(1000, 10000),
                消费能力 = Math.Abs((short)new Random().Next(1, 100)),
                头像 = 获取头像(1),
                注册日期 = DateTime.Now
            };
            //var query1 = new DyQuery<tb_user>().Insert(user1).AsQuery();
            //var result = new Dy().Query(query1);
            ////result.RowCount;//影响的行数
            //Console.WriteLine(result.AsJson());
            //Thread.Sleep(50);

            //多条 插入
            var multi_user = new List<int> { 2, 3, 4, 5, 6, 7, 8, 9, 10 }.Select(i =>
            {
                Thread.Sleep(50);
                return new tb_user
                {
                    自增NO = i, //自增列 框架不会去插入
                    账号 = string.Format("U{0:D4}", i),
                    密码 = "12345678",
                    用户名 = string.Format("Tester{0:D3}", i),
                    性别 = i % 2 == 0,
                    年龄 = new Random().Next(20, 60),
                    会员等级 = (byte)(new Random().Next(1, 255)),
                    积分 = new Random().Next(1000, 10000),
                    消费能力 = Math.Abs((short)new Random().Next(1, 100)),
                    头像 = 获取头像(i),
                    注册日期 = DateTime.Now
                };
            }).ToList();
            //var dyQuery = new DyQuery<tb_user>()
            //    .Insert(multi).AsQuery();
            //string json = dy.Query(dyQuery).AsJson();
            //Console.WriteLine(json);
            //Thread.Sleep(50);


            //批量插入 
            var multi_user_batch = new List<int> { 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 }.Select(i =>
            {
                Thread.Sleep(50);
                return new tb_user
                {
                    自增NO = i, //自增列 框架不会去插入
                    账号 = string.Format("U{0:D4}", i),
                    密码 = "12345678",
                    用户名 = string.Format("Tester{0:D3}", i),
                    性别 = i % 2 == 0,
                    年龄 = new Random().Next(20, 60),
                    会员等级 = (byte)(new Random().Next(1, 255)),
                    积分 = new Random().Next(1000, 10000),
                    消费能力 = Math.Abs((short)new Random().Next(1, 100)),
                    头像 = 获取头像(i),
                    注册日期 = DateTime.Now, 
                };
            }).ToList();

            localDy.BulkInsert(multi_user_batch);


            //初始化 分类
            var multi_category = new List<tb_categories> {
                new tb_categories {
                    子ID = "100",
                    父ID = "",
                    分类名称="数码",
                    顺序=1
                }
                , new tb_categories {
                    子ID = "100100",
                    父ID = "100",
                    分类名称="电脑",
                    顺序=1
                }
                , new tb_categories {
                    子ID = "100101",
                    父ID = "100",
                    分类名称="手机",
                    顺序=1
                }
                , new tb_categories {
                    子ID = "100100100",
                    父ID = "100100",
                    分类名称="笔记本",
                    顺序=1
                }
                , new tb_categories {
                    子ID = "100100101",
                    父ID = "100100",
                    分类名称="台式机",
                    顺序=1
                }
                , new tb_categories {
                    子ID = "100101100",
                    父ID = "100101",
                    分类名称="苹果",
                    顺序=1
                }
                , new tb_categories {
                    子ID = "100101101",
                    父ID = "100101",
                    分类名称="小米",
                    顺序=1
                }
            };

            var multi_products = new List<int> { 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 }.Select(i =>
            {
                Thread.Sleep(50);
                return new tb_product
                {
                    产品ID = Guid.NewGuid(),
                    产品名称 = string.Format("Product{0:D6}", i),
                    分类编号 = multi_category[i % 7].子ID,
                    进价 = (float)new Random().NextDouble(),
                    运费 = (decimal)new Random().NextDouble(),
                    税率 = (decimal)new Random().NextDouble(),
                    零售价 = (decimal)new Random().NextDouble() * 100,
                    会员折扣 = new Random().NextDouble(),
                    产品描述_中文 = "中文",
                    产品描述_英文 = "English",
                    RSS = "<root><product></product></root>",
                    //附件1 = "", //参考[头像]
                    //附件2 = "",
                    //版本 timestamp //无须指定
                };
            }).ToList();
            var dyQuery = new DyQuery<tb_user>()
               .Insert(user1).Insert(multi_user).Insert(multi_category)
               .Insert(multi_products).AsQuery();
            var json = localDy.Done(dyQuery).AsJson();
            Console.WriteLine(json);
            Thread.Sleep(50);


            //添加订单
            List<tb_user> userList = new List<tb_user>();
            userList.Add(user1);
            userList.AddRange(multi_user);
            userList.AddRange(multi_user_batch);

            for (int i = 0; i < 100; i++)
            {
                var user = userList[new Random().Next(0, 19)];
                var order = new tb_order
                {
                    订单ID = Guid.NewGuid(),
                    订单名称 = DateTime.Now.ToShortDateString() + string.Format("{0:D4}", i),
                    订单时间 = DateTime.Now.AddDays(new Random().Next(1, 180)),
                    账号 = user.账号
                };

                List<tb_order_detail> details = new List<tb_order_detail>();
                var count = new Random().Next(2, 7); Thread.Sleep(50);
                for (int j = 0; j < count; j++)
                {
                    var product = multi_products[new Random().Next(0, 9)]; Thread.Sleep(50);
                    details.Add(new tb_order_detail
                    {
                        订单明细ID = Guid.NewGuid(),
                        订单ID = order.订单ID,
                        产品ID = product.产品ID,
                        产品名称 = product.产品名称,
                        序号 = j,
                        零售价 = product.零售价,
                        购买数量 = 100 - i,
                        支付价 = product.零售价 * (decimal)product.会员折扣 * (decimal)(1 - user.会员等级.ToString().Length * 0.1)
                    });
                }
                order.总金额 = details.Sum(t => t.支付价);

                var dyQuery2 = new DyQuery<tb_order>().Insert(order).Insert(details).AsQuery();
                var json2 = localDy.Done(dyQuery2).AsJson();
                Console.WriteLine(json2);
                Thread.Sleep(50);

            }

        }
        public static void 测试_删除()
        {
            //List<tb_user> userList = new List<tb_user>()
            //{
            //    new tb_user { 账号 = "U0002" },
            //    new tb_user { 账号 = "U0003" }
            //}; 
            //var query = new DyQuery<tb_user>().Delete(new tb_user { 账号 = "U0001" }).Delete(userList).AsQuery();
            //var rowCount = dy.Query(query).RowCount;
            //if (rowCount == 1 /*new tb_user { 账号 = "U0001" }*/+ userList.Count)
            //{
            //    //成功
            //}
            //else {
            //    //失败
            //}

            ////根据条件删除
            //var query = new DyQuery<tb_user>().Delete().Where(t1 => t1.用户名.Dy_EndsWith("3") && t1.年龄 > 20).AsQuery();
            //var rowCount = dy.Query(query).RowCount; 

            //删除指定用户
            //根据实例删除
            var userQuery = new DyQuery<tb_user>().Where(t1 => t1.用户名.Dy_EndsWith("2")).AsQuery();
            var userList = localDy.Done(userQuery).AsTList<tb_user>();
            var deleteUserQuery = new DyQuery<tb_user>().Delete(userList).AsQuery();
            var json1 = localDy.Done(deleteUserQuery).AsJson();
            Console.WriteLine(json1);

            //根据条件删除
            var userQuery2 = new DyQuery<tb_user>().Delete().Where(t1 => t1.用户名.Dy_EndsWith("3") && t1.年龄 > 20).AsQuery();
            var json2 = localDy.Done(userQuery2).AsJson();
            Console.WriteLine(json2);


            //产品实例删除 带timestamp版本号
            var productQuery = new DyQuery<tb_product>().Where(t1 => t1.产品名称.Dy_Contains("1"));
            var products = localDy.Done(productQuery).AsTList<tb_product>();
            var deleteProductQuery = new DyQuery<tb_product>().Delete(products).AsQuery();
            var json3 = localDy.Done(deleteProductQuery).AsJson();
            Console.WriteLine(json3);

            //用户删除 带v版本控制
            var query1 = new DyQuery<tb_user>().Where(t1 => t1.消费能力 >= 50).AsQuery();
            var ulist = localDy.Done(query1).AsTList<tb_user>();
            //Thread.Sleep(10000);//打开数据库，去修改对应表的v字段版本号
            var delQuery1 = new DyQuery<tb_user>().Delete(ulist).AsQuery();
            var result4 = localDy.Done(delQuery1);
            if (result4.RowCount == ulist.Count)
            {
                //内部开启事务
                Console.WriteLine("删除成功");
            }
            else {
                Console.WriteLine("删除失败");
            }
            Console.WriteLine(result4.AsJson());

        }
        public static void 测试_清空()
        {
            //清空操作
            localDy.Done(new DyQuery<tb_user>().Truncate().AsQuery());
            localDy.Done(new DyQuery<tb_categories>().Truncate());
            localDy.Done(new DyQuery<tb_order>().Truncate());
            localDy.Done(new DyQuery<tb_order_detail>().Truncate());
            localDy.Done(new DyQuery<tb_product>().Truncate());
        }
        public static void 测试_更新()
        {
 

            //更新指定用户 
            var query0 = new DyQuery<tb_user>().AsQuery();
            var userList = localDy.Done(query0).AsTList<tb_user>(); 
            var user1 = userList.FirstOrDefault(user => user.账号 == "U0012");
            user1.会员等级 = 100;
            user1.年龄 = 0;
            var query1 = new DyQuery<tb_user>().Update(user1).AsQuery();
            var json1 = localDy.Done(query1).AsJson();
            Console.WriteLine(json1);

            //多实例更新用户 tb_user.v 带版本
            userList.ForEach(user => user.会员等级 = 80);
            var query2 = new DyQuery<tb_user>().Update(userList).AsQuery();
            var result2 = localDy.Done(query2);
            if (result2.RowCount == userList.Count)
            {
                Console.WriteLine("更新成功");
            }
            else {
                Console.WriteLine("更新失败，因为 前一个更新将版本号已经更新了 本次更新不完整 事务回滚");
            }
            var json2 = result2.AsJson();
            Console.WriteLine(json2);

            //更新订单
            var query3 = new DyQuery<tb_order>();
            var orderList = localDy.Done(query3).AsTList<tb_order>();
            var order = orderList.FirstOrDefault();
            var orderId = order.订单ID;
            var query4 = new DyQuery<tb_order_detail>().Where(t1 => t1.订单ID == orderId);
            var details = localDy.Done(query4).AsTList<tb_order_detail>();

            details.ForEach(detail => detail.支付价 = detail.支付价 * 1.1m);
            order.总金额 = details.Sum(detail => detail.支付价);

            var query5 = new DyQuery<tb_order>().Update(order).Update(details).AsQuery();
            var result5 = localDy.Done(query5);
            if (result5.RowCount == 1 /*order数量*/ + details.Count)
            {
                Console.WriteLine("更新成功");
            }
            else {
                Console.WriteLine("更新失败");
            }
            Console.WriteLine(result5.AsJson());


            //update from 更新  [产品名称]变更需要将订单明细中[产品名称]同时变更
            var query6 = new DyQuery<tb_order_detail>().Update().Set<tb_product>((t1, t2) => t1.产品名称 == t2.产品名称.Dy_Substring(1, 4))
                .Join<tb_product>(JoinWay.InnerJoin, (t1, t2) => t1.产品ID == t2.产品ID)
                .Where(t1 => t1.支付价 > 30).AsQuery();
            var result6 = localDy.Done(query6);
            Console.WriteLine(result6.AsJson());

             
        }
        public static void 测试_存储过程_视图()
        {
            //无参数
            var query1 = new DyQuery<sp_get_order_all>().Proc(new sp_get_order_all { });
            var result1 = localDy.Done(query1);
            Console.WriteLine(result1.AsJson());

            //有参数
            var query2 = new DyQuery<sp_add_order>().Proc(new sp_add_order
            {
                订单号 = Guid.NewGuid(),
                订单名称 = "O" + DateTime.Now.ToString("yyyyMMddHHmm"),
                账号 = "U0005",
                金额 = 1200.00m
            });
            var result2 = localDy.Done(query2);
            Console.WriteLine(result2.AsJson());

            //有参数
            var query3 = new DyQuery<sp_add_order>().Proc(new x_add_order
            {
                订单号 = Guid.NewGuid(),
                订单名称 = "X" + DateTime.Now.ToString("yyyyMMddHHmm"),
                账号 = "U0005",
                金额 = 200.00m
            });
            var result3 = localDy.Done(query3);
            Console.WriteLine(result3.AsJson());

            var query4 = new DyQuery<sp_multi_tb>().Proc(new sp_multi_tb { }).AsQuery();
            var result4 = localDy.Done(query4);
            var userList = result4.AsTList<tb_user>(0);
            var categoryList = result4.AsTList<tb_categories>(1);
            var productList = result4.AsTList<tb_product>(2);
            Console.WriteLine("tb_user count:{0},tb_categories count:{1},tb_product count:{2}", userList.Count, categoryList.Count, productList.Count);


            var query5 = new DyQuery<v_user_order>().AsQuery();
            Console.WriteLine(localDy.Done(query5).AsJson());
        }
        public static void 测试_查询()
        { 
            //单表
            //获取全部数据
            var query1 = new DyQuery<tb_user>().AsQuery();
            var result1 = localDy.Done(query1);
            Console.WriteLine(result1.AsJson());
            var userList = result1.AsTList<tb_user>();//返回列表
            var userDynamicList = result1.AsDyTList();//返回动态对象列表

            //条件过滤
            var query2 =
                new DyQuery<tb_user>()
                .Where(t1 => t1.会员等级 > 100)
                //.Where(t1 => t1.会员等级 > 50 && t1.性别 == true && t1.注册日期 == null && t1.上次登录日期 !=null)
                .Select(t1 => new
                {
                    t1.账号,
                    t1.用户名,
                    上次登录日期 = t1.上次登录日期.Dy_IsNull("20170601"),
                    日期Cast = t1.上次登录日期.Dy_IsNull("20170601").Dy_Cast<string, string>("varchar"),
                    日期Convert = t1.上次登录日期.Dy_IsNull("20170601").Dy_Convert<string, string>(111, "varchar")
                }).AsQuery();
            var result2 = localDy.Done(query2);
            Console.WriteLine(result2.AsJson());

            //多表复杂查询
            //指定用户的订单及明细 调用自定义函数 GetUserId()
            //string userId = GetUserId();
            var query3 =
                //new DyQuery<tb_order_detail>()
                //or
                new DyQuery<tb_order_detail>(t1 => t1) //指定表别名
                .Join<tb_order>(JoinWay.InnerJoin, (t1, t2) => t1.订单ID == t2.订单ID)
                .Join<tb_product>(JoinWay.InnerJoin, (t1, t3) => t1.产品ID == t3.产品ID)
                .Join<tb_order, tb_user>(JoinWay.InnerJoin, (t2, t4) => t2.账号 == t4.账号)
                .Where<tb_user>(t4 => t4.账号 == GetUserId()) //=> GetUserId()
                .Where<tb_order>(t2 =>
                    t2.订单时间.Dy_DateDiff(DateTime.Now, "dd") >= 0
                    &&
                    t2.订单时间.Dy_DateAdd(-5, "mm").Dy_DateDiff(DateTime.Now, "dd") > 0
                )
                .Select<tb_order_detail, tb_order, tb_product, tb_user>
                (
                    (t1, t2, t3, t4) =>
                    new
                    {
                        t4.账号,
                        产品简称 = t3.产品名称.Dy_Substring(1, 2),
                        t2.订单名称,
                        支付价 = t1.支付价.Dy_M_Floor(),
                    }
                )
                .AsQuery();
            var result3 = localDy.Done(query3);
            Console.WriteLine(result3.AsJson());


          
            //IN 查询
            var query4 = new DyQuery<tb_user>() 
                .Where(t1 => t1.用户名.Dy_Substring(1, 6).Dy_Right(1).Dy_In(new List<string> { "1", "2", "3" }))
                .Where(t1 => t1.账号.Dy_In(new DyQuery<tb_order>(t2 => t2).Select(t2 => t2.账号)))
                .Select(t1 => t1.账号).AsQuery();
            Console.WriteLine(localDy.Done(query4).AsJson());


            //Exists 子查询   
            var query5 = new DyQuery<tb_user>()
                     .Exists(WhereWay.And
                        , new DyQuery<tb_order>(t2 => t2)
                        .Join<tb_order_detail>(JoinWay.InnerJoin, t3 => t3).On<tb_order, tb_order_detail>((t2, t3) => t2.订单ID == t3.订单ID)
                        //与下面等价
                        //.Join<tb_order_detail>(JoinWay.InnerJoin, (t2, t3) => t2.订单ID == t3.订单ID)
                        .Group(t2 => t2.订单ID).Group(t2 => t2.账号)
                        .Having<tb_order_detail>(WhereWay.And, t3 => t3.订单ID.Dy_Count() > 5)
                        //.Select<tb_order, tb_order_detail>((t2, t3) => new { t2.订单ID, t2.账号, 明细数量 = t3.订单ID.Dy_Count() })
                        .Where<tb_order, tb_user>((t2, t1) => t2.账号 == t1.账号)
                        .Select(t2 => 1).AsQuery()
                    )
                    .Select(t1 => new { t1.账号, t1.用户名 })
                .AsQuery();
            Console.WriteLine(localDy.Done(query5).AsJson());


            //Top N 查询
            var query6 = new DyQuery<tb_order>(t2 => t2)
                          .Join<tb_order_detail>(JoinWay.InnerJoin, (t2, t3) => t2.订单ID == t3.订单ID)
                          .Select<tb_order, tb_order_detail>((t2, t3) => new { t2.订单ID, t2.账号, t3.订单明细ID })
                          .AsQueryTopN(5);

            Console.WriteLine(localDy.Done(query6).AsJson());

            //分页  json字符串中会返回额外的几个字段  _totalRowCount（总记录数）, _pageIndex（分页索引）, _pageSize（页大小）, RN (序号) 
            var query7 = new DyQuery<tb_order>(t2 => t2)
                         .Join<tb_order_detail>(JoinWay.InnerJoin, (t2, t3) => t2.订单ID == t3.订单ID)
                         .Order<DateTime>(OrderWay.Asc, t2 => t2.订单时间)
                         .Select<tb_order, tb_order_detail>((t2, t3) => new { t2.订单ID, t2.账号, t3.订单明细ID })
                         .Select<tb_order_detail>(t3 => t3.订单ID)
                         .AsQuery();
            //.AsQueryPaged(1,10); 
            Console.WriteLine(localDy.Done(query7).AsJson());


            //组合WHERE
            var query8 = new DyQuery<tb_user>()
                .Where(t1 => (t1.用户名.Dy_EndsWith("1") || t1.账号.Dy_Right(1) == "2") && (t1.用户名.Dy_Contains("3") || t1.账号.Dy_Contains("3")))
                .Select(t1 => t1.账号).AsQuery();
            Console.WriteLine(localDy.Done(query8).AsJson());



        }
        #region 调用自定义函数
        private static string GetUserId()
        {
            return "U0005";
        }
        #endregion  
        public static void 测试_扩展方法()
        {
            //字符串函数 
            var query1 =
                new DyQuery<tb_user>()
                .Where(t1 => t1.会员等级 > 100)
                .Select(t1 => new
                {
                    Dy_Ascii = t1.账号.Dy_Ascii(),
                    Dy_Char = t1.消费能力.Dy_Char(),
                    Dy_NChar = t1.消费能力.Dy_NChar(),
                    Dy_Unicode = t1.用户名.Dy_Unicode(),
                    Dy_Quotename1 = t1.用户名.Dy_Quotename(),
                    Dy_Quotename2 = t1.用户名.Dy_Quotename("{}"),
                    Dy_Soundex = t1.用户名.Dy_Soundex(),
                    Dy_Replicate = t1.用户名.Dy_Replicate(3),
                    Dy_CharIndex1 = t1.用户名.Dy_CharIndex("001"),
                    Dy_CharIndex2 = t1.用户名.Dy_CharIndex("0", 3),
                    Dy_PatIndex = t1.用户名.Dy_PatIndex("0%1"),
                    Dy_Difference = t1.用户名.Dy_Difference("tester0x"),
                    Dy_Left = t1.用户名.Dy_Left(2),
                    Dy_Right = t1.用户名.Dy_Right(2),
                    Dy_Len = t1.用户名.Dy_Len(),
                    Dy_Low = t1.用户名.Dy_Lower(),
                    Dy_Upper = t1.用户名.Dy_Upper(),
                    Dy_LTrim = t1.用户名.Dy_LTrim(),
                    Dy_RTrim = t1.用户名.Dy_RTrim(),
                    Dy_Reverse = t1.用户名.Dy_Reverse(),
                    Dy_Space = t1.用户名 + DyExtFn.Dy_Space(3) + t1.用户名,
                    Dy_Str1 = 12.2321d.Dy_Str(),
                    Dy_Str2 = 12.2321d.Dy_Str(6, 2),
                    Dy_Stuff = t1.用户名.Dy_Stuff("x", 1, 0),
                    Dy_Substring = t1.用户名.Dy_Substring(2, 1),
                    Dy_Replace = t1.用户名.Dy_Replace("0", "o")
                }).AsQueryTopN(1);
            var result1 = localDy.Done(query1);
            Console.WriteLine(result1.AsJson());


            //日期和时间函数
            var query2 = new DyQuery<tb_user>()
                     .Select(t1 => new {
                         Dy_DateAdd_YY = t1.注册日期.Dy_DateAdd(2, "yy"),
                         Dy_DateAdd_DD = t1.注册日期.Dy_DateAdd(30, "dd"),
                         Dy_DateDiff_DD = t1.注册日期.Dy_DateDiff(DateTime.Now, "dd"),
                         Dy_DateDiff_YY = t1.注册日期.Dy_DateDiff(DateTime.Now, "yy"),
                         Dy_DateName_QQ = t1.注册日期.Dy_DateName("qq"),
                         Dy_DateName_WW = t1.注册日期.Dy_DateName("ww"),
                         Dy_DatePart_YY = t1.注册日期.Dy_DatePart("yy"),
                         Dy_Day = t1.注册日期.Dy_Day(),
                         Dy_Month = t1.注册日期.Dy_Month(),
                         Dy_Year = t1.注册日期.Dy_Year(),
                         Dy_GetDate = DyExtFn.Dy_GetDate(),
                         Dy_GetUtcDate = DyExtFn.Dy_GetUtcDate(),
                     })
                        .AsQueryTopN(1);
            var result2 = localDy.Done(query2);
            Console.WriteLine(result2.AsJson());


            //数学函数 
            var query3 = new DyQuery<tb_order_detail>()
                .Select(t1 => new {
                    M = -1,
                    PI = DyExtFn.Dy_M_PI(),
                    Dy_M_Abs = (-1).Dy_M_Abs(),
                    Dy_M_Cos = 1.0.Dy_M_Cos(),
                    Dy_M_Sin = 1.5.Dy_M_Sin(),
                    Dy_M_Cot = 1.5.Dy_M_Cot(),
                    Dy_M_Tan = 1.5.Dy_M_Tan(),
                    Dy_M_ACos = 0.5.Dy_M_ACos(),//[0,pi] 
                    Dy_M_ASin = (-0.5).Dy_M_ASin(),//[-pi/2,+pi/2]
                    Dy_M_ATan = 0.5.Dy_M_ATan(),//[-pi/2,+pi/2]
                    Dy_M_Degrees = 1.5.Dy_M_Degrees(),
                    Dy_M_Radians = 1.5.Dy_M_Radians(),
                    Dy_M_Exp = 1.5.Dy_M_Exp(),
                    Dy_M_Log1 = 1.5.Dy_M_Log(),
                    Dy_M_Log2 = 1.5.Dy_M_Log(1.2),
                    Dy_M_Log10 = 1.5.Dy_M_Log10(),
                    Dy_M_Ceiling = 1.5.Dy_M_Ceiling(),
                    Dy_M_Floor = 1.5.Dy_M_Floor(),
                    Dy_M_Power = 1.5.Dy_M_Power(2.0),
                    Dy_M_Sqrt = 4.0.Dy_M_Sqrt(),
                    Dy_M_Sign = (1.3).Dy_M_Sign(),
                    Dy_M_Rand1 = DyExtFn.Dy_M_Rand(),
                    Dy_M_Rand2 = (-1).Dy_M_Rand(),
                    Dy_M_Round1 = (-1.333).Dy_M_Round(2),
                    Dy_M_Round2 = (-1.336).Dy_M_Round(2, 2)
                })
                .AsQueryTopN(1);
            var result3 = localDy.Done(query3);
            Console.WriteLine(result3.AsJson());

            //类型转换函数
            var query4 = new DyQuery<tb_user>()
                     .Select(t1 => new {
                         注册日期 = t1.注册日期.Dy_IsNull<DateTime, string>("20170101").Dy_Convert<string, string>(120, "varchar(10)"),
                         消费能力 = t1.消费能力.Dy_Cast<short, string>("varchar(10)") + "AAA",
                         消费能力2 = t1.消费能力.Dy_Cast<short, double>("decimal(18,4)")
                     })
                     .AsQueryTopN(1);
            var result4 = localDy.Done(query4);
            Console.WriteLine(result4.AsJson());

            //系统函数
            var query5 = new DyQuery<tb_user>()
                        .Select(t1 => new {
                            Dy_IsNull = t1.注册日期.Dy_IsNull<DateTime, string>("20170101"),
                            Dy_IsNumeric = t1.消费能力.Dy_IsNumeric(),
                            Dy_IsDate = t1.消费能力.Dy_IsDate(),
                            Dy_IsDate2 = t1.注册日期.Dy_IsDate() 
                        })
                     .AsQueryTopN(1);
            var result5 = localDy.Done(query5);
            Console.WriteLine(result5.AsJson());



            //聚合函数 
            var query6 = new DyQuery<tb_order>(t2 => t2)
                        .Join<tb_order_detail>(JoinWay.InnerJoin, t3 => t3).On<tb_order, tb_order_detail>((t2, t3) => t2.订单ID == t3.订单ID)
                        //与下面等价
                        //.Join<tb_order_detail>(JoinWay.InnerJoin, (t2, t3) => t2.订单ID == t3.订单ID) 
                        .Group(t2 => t2.订单ID).Group(t2 => t2.账号)
                        .Having<tb_order_detail>(WhereWay.And, t3 => t3.订单ID.Dy_Count() > 5)
                        .Select<tb_order, tb_order_detail>(
                            (t2, t3) => new
                            {
                                t2.订单ID,
                                t2.账号,
                                明细数量 = t3.订单ID.Dy_Count(),
                                总金额Max = t3.支付价.Dy_Max(),
                                总金额Min = t3.支付价.Dy_Min(),
                                总金额Sum = t3.支付价.Dy_Sum(),
                                总金额Avg = t3.支付价.Dy_Avg(),
                                R金额 = ((t3.支付价.Dy_Max() + t3.支付价.Dy_Min() - t3.支付价.Dy_Sum() * t3.支付价.Dy_Avg()) / t3.支付价.Dy_Min()).Dy_Convert<decimal, decimal>("decimal(18,2)")
                            }
                            )
                        .AsQuery();
            Console.WriteLine(localDy.Done(query6).AsJson());


            //自定义扩展函数 
            //"Dy_TimeFrt","Dy_Count1"
            var query7 = new DyQuery<tb_user>()
                    .Where(t1 =>
                    t1.用户名.Dy_StartsWith("T") // like 'T%'
                    || t1.密码.Dy_EndsWith("8") // like '%8'
                    || t1.账号.Dy_Contains("003") // like '%003%'
                    || t1.用户名.Dy_StartsWithNot("T") // not like 'T%'
                    || t1.密码.Dy_EndsWithNot("8") // not like '%8'
                    || t1.账号.Dy_ContainsNot("003")// not like '%003%'
                    )
                    .Select(t1 => t1)
                    .AsQuery();
            var result7 = localDy.Done(query7);
            Console.WriteLine(result7.AsJson());

            query7 = new DyQuery<tb_user>()
                    .Where(t1 =>
                    t1.用户名.Dy_StrGT("678") // [字符串_字段] > '678'
                    || t1.用户名.Dy_StrGE("678")// [字符串_字段] >= '678'
                    || t1.用户名.Dy_StrLT("678")// [字符串_字段] < '678'
                    || t1.用户名.Dy_StrLE("678")// [字符串_字段] <= '678'
                    || t1.用户名.Dy_StrGT("678")
                    )
                    .Select(t1 => t1)
                    .AsQuery();
            result7 = localDy.Done(query7);
            Console.WriteLine(result7.AsJson());

            query7 = new DyQuery<tb_user>()
                  .Where(t1 =>
                  t1.账号.Dy_Right(1).Dy_In(new List<string> { "1", "2" })
                  ||
                  t1.账号.Dy_Right(1).Dy_InNot(new List<string> { "7", "8" })
                  ||
                  t1.账号.Dy_In(
                        new DyQuery<tb_order>(t2 => t2).Select(t2 => new { t2.账号 }).AsQuery()
                      )
                  ||
                  t1.注册日期 <= DateTime.Now.Dy_TimeFrt("yyyy-MM") // Dy_TimeFrt:将时间格式化后转化为时间类型 便于与数据库字段的比较
                  )
                  .Select(t1 => new { t1.账号 })
                  .AsQuery();
            result7 = localDy.Done(query7);
            Console.WriteLine(result7.AsJson());

            query7 = new DyQuery<tb_user>()
                 .Where(t1 =>
                    t1.账号.Dy_Right(1).Dy_In(new List<string> { "1", "2" })
                 )
                 .Select(t1 => new { 总计 = DyExtFn.Dy_CountN(1) }) //直接统计
                 .AsQuery();
            result7 = localDy.Done(query7);
            Console.WriteLine(result7.AsJson());

            query7 = new DyQuery<tb_user>()
                .Where(t1 =>
                   t1.账号.Dy_Right(1).Dy_In(new List<string> { "1", "2" })
                )
                .Select(t1 => new { 总计 = t1.账号.Dy_Count() }) //作用在字段上
                .AsQuery();
            result7 = localDy.Done(query7);
            Console.WriteLine(result7.AsJson());

        } 
        public static void 测试_结果集()
        {
            /*以下方法都是对已经查询后的结果集，做进一步处理*/
            //RowCount 返回影响的行数，只对 增、删、改 有效，查询返回结果永远是0
            List<int> idlist = new List<int>();
            for (int i = 0; i < 100; i++)
            {
                idlist.Add(i + 50);
            }
            var multi_user_batch = idlist.Select(i =>
            {
                Thread.Sleep(50);
                return new tb_user
                {
                    账号 = string.Format("U{0:D4}", i),
                    密码 = "12345678",
                    用户名 = string.Format("Tester{0:D3}", i),
                    性别 = i % 2 == 0,
                    年龄 = new Random().Next(20, 60),
                    会员等级 = (byte)(new Random().Next(1, 255)),
                    积分 = new Random().Next(1000, 10000),
                    消费能力 = Math.Abs((short)new Random().Next(1, 100)),
                    头像 = 获取头像(i),
                    注册日期 = DateTime.Now,
                };
            }).ToList();
            //var q0 = new DyQuery<tb_user>().Insert(multi_user_batch).AsQuery();
            //var result0 = dy.Query(q0);
            //Console.WriteLine("影响的行数：" + result0.RowCount);


            //Records返回结果集
            var q1 = new DyQuery<tb_user>().Where(t1 => t1.用户名.Dy_Contains("00")).Select(t1 => new { t1.账号, t1.用户名, t1.消费能力 }).AsQuery();
            var result = localDy.Done(q1);
            Console.WriteLine("影响的行数：" + result.RowCount);//返回0
            Console.WriteLine("结果集中表的数量：" + result.Records.Tables.Count);
             

            //AsT -> 如果查询有多条记录，返回记录的第一条
            var query1 = new DyQuery<tb_user>().Where(t1 => t1.用户名.Dy_Contains("00")).Select(t1 => new { t1.账号, t1.用户名, t1.消费能力 }).AsQuery(); 
            var user1 = localDy.Done(query1).AsDyT(); 
            Console.WriteLine("账号:{0},用户名:{1}", user1.账号,user1.用户名);

            //AsTList ->返回记录列表
            var query2 = new DyQuery<tb_user>().Where(t1 => t1.用户名.Dy_Contains("00")).Select(t1 => new { t1.账号, t1.用户名, t1.消费能力 }).AsQuery();
            var user2list = localDy.Done(query2).AsDyTList();
            foreach (var user2 in user2list)
            {
                Console.WriteLine("账号:{0},用户名:{1}", user2.账号, user2.用户名); 
            }
             

            //AsRC ->返回首行首列
            var query3 = new DyQuery<tb_user>().Where(t1 => t1.用户名.Dy_Contains("00")).Select(t1 => new { t1.账号, t1.用户名, t1.消费能力 }).AsQuery();
            var 首行首列= localDy.Done(query3).AsRC();
            Console.WriteLine(首行首列);

            //AsJson ->将Records结果集，直接转成标准json字符串格式  ajax请求的时候返回json格式很方便
            var query4 = new DyQuery<tb_user>().Where(t1 => t1.用户名.Dy_Contains("00")).Select(t1 => new { t1.账号, t1.用户名, t1.消费能力 }).AsQuery();
            Console.WriteLine(localDy.Done(query4).AsJson());

            //AsTPageList 
            int pIndex = 1;
            int pSize = 5;
            var criteria = new DyQuery<tb_user>()
                            .Order(OrderWay.Asc, t1 => t1.账号)
                            .Select(t1 => new { t1.账号, t1.用户名, t1.消费能力 });

            var query5 = criteria.AsQueryPaged(pIndex, pSize);
            var plist1 = localDy.Done(query5).AsTPageList<tb_user>();
            Console.WriteLine("当前页:{0},每页大小:{1},总记录数:{2},总页数:{3},是否有下一页:{4},是否有上一页:{5}", plist1.PageIndex, plist1.PageSize, plist1.TotalCount, plist1.TotalPages, plist1.HasNextPage, plist1.HasPreviousPage);
            pIndex = 3;
            var query6 = criteria.AsQueryPaged(pIndex, pSize);
            var plist2 = localDy.Done(query6).AsTPageList<tb_user>();
            Console.WriteLine("当前页:{0},每页大小:{1},总记录数:{2},总页数:{3},是否有下一页:{4},是否有上一页:{5}", plist2.PageIndex, plist2.PageSize, plist2.TotalCount, plist2.TotalPages, plist2.HasNextPage, plist2.HasPreviousPage);


            //AsKeyValues 转置输出，画图、统计时比较有用
            /*
            colName1 | colName2 | colName3
            ------------------------------
            1        | 2        | 3       
            4        | 5        | 6       
            7        | 8        | 9
            ==>
            colName1:[1,4,7]       
            colName2:[2,5,8]       
            colName3:[3,6,9]       
            */
            var query7 = new DyQuery<tb_user>().Where(t1 => t1.用户名.Dy_Contains("0")).Select(t1 => new { t1.账号, t1.用户名, t1.消费能力 }).AsQuery();
            var keyValues = localDy.Done(query7).AsKeyValues();
            foreach (var key in keyValues.Keys) {
                Console.WriteLine("Key:{0},Values:{1}", key, string.Join(",", keyValues[key].ToArray()));
            }

            //AsDyT 动态对象 与 AsT类似 返回第一行数据
            var query8 = new DyQuery<tb_user>().Where(t1 => t1.用户名.Dy_Contains("00")).Select(t1 => new { t1.账号, t1.用户名, t1.消费能力 }).AsQuery();
            var dynamicObj = localDy.Done(query8).AsDyT();
            Console.WriteLine("账号:{0},用户名:{1},消费能力:{2}", dynamicObj.账号, dynamicObj.用户名, dynamicObj.消费能力);

            //AsDyTList  与 AsTList类似 返回列表
            var query9 = new DyQuery<tb_user>().Where(t1 => t1.用户名.Dy_Contains("00")).Select(t1 => new { t1.账号, t1.用户名, t1.消费能力 }).AsQuery();
            var dynamicObjList = localDy.Done(query9).AsDyTList();
            foreach (var dyObj in dynamicObjList) {
                Console.WriteLine("账号:{0},用户名:{1},消费能力:{2}", dyObj.账号, dyObj.用户名, dyObj.消费能力);
            }

            //AsTree 树结构  分类处理、如省市区、商品的分类
            var query10 = new DyQuery<tb_categories>().AsQuery();
            var treeNodeRoot = localDy.Done(query10).AsTree<tb_categories>((t1, t2) => t1.父ID == t2.子ID);
            Console.WriteLine(treeNodeRoot.ToString());
            //树搜索 引入 kQL.orm.results 命名空间
            //ConvertTTreeToTList 树节点转成List列表
            //var treeToList = tree.ConvertTTreeToTList();//不输出跟节点
            var treeToList = treeNodeRoot.ConvertTTreeToTList(true); //输出跟节点
            Console.WriteLine("treeToList数量:{0}",treeToList.Count);
            //FindTTreeNode
            tb_categories category = new tb_categories {
                分类名称 = "台式机"
            };
            var findTreeNode = treeNodeRoot.FindTTreeNode(category, (treeNode, searchCategoryInstance) => treeNode.Current.分类名称 == searchCategoryInstance.分类名称);//其中searchCategoryInstance 就是 category
            Console.WriteLine(findTreeNode.ToString());
            //DFS_FlagTree 深度优先将路径上的[TreeNode->Flag]标记为1，应用场景 树型复选框，选中某节点，节点上级和下级节点都要选中
            treeNodeRoot.DFS_FlagTree(category, (treeNode, searchCategoryInstance) => treeNode.Current.分类名称 == searchCategoryInstance.分类名称);
            Console.WriteLine("---------DFS_FlagTree 节点标记------");
            Console.WriteLine(treeNodeRoot.ToString());
            //BFS_CutFlagTree 广度优先路径上的[TreeNode->Flag]进行剪枝，应用场景 树型复选框，选中某节点，节点上级和下级节点都要选中，将所有选中的节点返回
            treeNodeRoot.BFS_CutFlagTree();
            Console.WriteLine("---------BFS_CutFlagTree 节点剪枝------");
            Console.WriteLine(treeNodeRoot.ToString());
            /****** DFS_FlagTree 与 BFS_CutFlagTree 一般情况都是要配合使用，先对节点标记，再对节点剪枝 ******/



            //一对一、一对多 AsT_OneOne、AsTList_OneOne、AsT_OneMany、AsTList_OneMany 
            var query11 = new DyQuery<tb_order>(t2 => t2)
                         .Join<tb_order_detail>(JoinWay.InnerJoin, (t2, t3) => t2.订单ID == t3.订单ID)
                         .Order<DateTime>(OrderWay.Asc, t2 => t2.订单时间)
                         .Select<tb_order>(t2 => new { t2.订单ID, t2.账号 ,t2.订单名称})
                         .Select<tb_order_detail>(t3 => new { t3.订单ID, t3.订单明细ID, t3.产品名称, t3.支付价 })
                         .AsQuery();
            //AsT_OneOne 单条 
            var r0 = localDy.Done(query11).AsT_OneOne<tb_order, tb_order_detail>();
            Console.WriteLine("AsT_OneOne");
            Console.WriteLine("{0},{1}", r0.Item1.订单ID, r0.Item1.账号);
            Console.WriteLine("{0},{1},{2}", r0.Item2.订单ID, r0.Item2.产品名称, r0.Item2.支付价);

            //AsTList_OneOne 多条
            var r1 = localDy.Done(query11).AsTList_OneOne<tb_order, tb_order_detail>();
            Console.WriteLine("AsTList_OneOne");
            foreach (var r in r1) { 
                Console.WriteLine("{0},{1}", r.Item1.订单ID, r.Item1.账号);
                Console.WriteLine("{0},{1},{2},{3}", r.Item2.订单ID, r.Item2.订单明细ID, r.Item2.产品名称, r.Item2.支付价); 
            }

            //AsT_OneMany 返回单条,一般情况下无什么意义
            var r2 = localDy.Done(query11).AsT_OneMany<tb_order, tb_order_detail>();
            Console.WriteLine("{0},{1}", r2.Item1.订单ID, r2.Item1.账号);
            foreach (var r2_0 in r2.Item2) { 
                Console.WriteLine("--{0},{1},{2}", r0.Item2.订单ID, r0.Item2.产品名称, r0.Item2.支付价);
            }

            //AsTList_OneMany 返回多条  
            // 指定分组 one => new tb_order { 账号 = one.账号 }，即对结果集Table的，主表字段进行过滤 
            var r3 = localDy.Done(query11).AsTList_OneMany<tb_order, tb_order_detail>(one => new tb_order { 账号 = one.账号 });
            /** new 主表类型，这里 new tb_order , 只要属性字段，“one.账号”无实际意义  ，也可以写成以下形式，与上一条等价 **/
            /****即，只认对象类型中的属性名称 ****/
            var r4 = localDy.Done(query11).AsTList_OneMany<tb_order, tb_order_detail>(one => new tb_order { 账号 = "" });
            foreach (var oneMany in r4) {
                Console.WriteLine("账号:{0},订单数量:{1}", oneMany.Item1.账号, oneMany.Item2.Count);
            }

            //AsTList_OneMany 根据多个字段分组
            var r5 = localDy.Done(query11).AsTList_OneMany<tb_order, tb_order_detail>(one => new tb_order { 账号 = one.账号, 订单ID = one.订单ID });
            //以下等价
            var r6 = localDy.Done(query11).AsTList_OneMany<tb_order, tb_order_detail>(one => new tb_order { 账号 = "", 订单ID = Guid.Empty });
            foreach (var oneMany in r6)
            {
                Console.WriteLine("订单名称:{0},账号:{1},订单数量:{2}", oneMany.Item1.订单名称, oneMany.Item1.账号, oneMany.Item2.Count);
            }
            
        }

        public static void 测试_导数据() {

            //本地库数据初始化 插入10万条用户数据 
            int insertCount = 100000;
            Console.WriteLine("开始测试_导数据,本地库插入{0}条用户数据", insertCount);
            Console.WriteLine("开始生成数据");
            List<tb_user> userlist = new List<tb_user>();
            for (int i = 200; i < insertCount + 200; i++)
            {
                userlist.Add(
                new tb_user
                {
                    自增NO = i, //自增列 框架不会去插入
                    账号 = string.Format("U{0:D4}", i),
                    密码 = "12345678",
                    用户名 = string.Format("Tester{0:D3}", i),
                    性别 = i % 2 == 0,
                    年龄 = RNG.Next(20, 60),
                    会员等级 = (byte)(RNG.Next(1, 255)),
                    积分 = RNG.Next(1000, 10000),
                    消费能力 = Math.Abs((short)RNG.Next(1, 100)),
                    头像 = 获取头像(i),
                    注册日期 = DateTime.Now
                });
            }
            Console.WriteLine("完成生成数据，开始执行插入本地库");
            long ms = localDy.BulkInsert(userlist);
            Console.WriteLine("插入本地库{1}条数据执行:{0}毫秒", ms, insertCount);
            Console.WriteLine();


            //单表->导入远程服务器
            Console.WriteLine("开始执行导入远程服务器");
            var remoteDy = new Dy("remoteServer");
            var query1 = new DyQuery<tb_user>().AsQuery();
            var result = localDy.CopyToRemote<kQL.orm.demo.remotemodels.tb_user>(query1, remoteDy);
            Console.WriteLine("完成导入远程服务器，本次执行毫秒:{0}", result.Item1);
            Console.WriteLine();

            //聚合结果导入远程服务器
            var query2 = new DyQuery<tb_order>(t2 => t2)
                        .Join<tb_order_detail>(JoinWay.InnerJoin, t3 => t3).On<tb_order, tb_order_detail>((t2, t3) => t2.订单ID == t3.订单ID) 
                        .Group(t2 => t2.订单ID).Group(t2 => t2.账号)
                        .Having<tb_order_detail>(WhereWay.And, t3 => t3.订单ID.Dy_Count() > 5)
                        .Select<tb_order, tb_order_detail>(
                            (t2, t3) => new
                            {
                                t2.订单ID,
                                t2.账号,
                                明细数量 = t3.订单ID.Dy_Count(),
                                总金额Max = t3.支付价.Dy_Max(),
                                总金额Min = t3.支付价.Dy_Min(),
                                总金额Sum = t3.支付价.Dy_Sum(),
                                总金额Avg = t3.支付价.Dy_Avg(),
                                R金额 = ((t3.支付价.Dy_Max() + t3.支付价.Dy_Min() - t3.支付价.Dy_Sum() * t3.支付价.Dy_Avg()) / t3.支付价.Dy_Min()).Dy_Convert<decimal, decimal>("decimal(18,2)")
                            }
                            ).AsQuery();
            result = localDy.CopyToRemote<kQL.orm.demo.remotemodels.tb_order_info>(query2, remoteDy);
            Console.WriteLine("聚合结果导入远程服务器->本次执行毫秒:{0}", result.Item1);
        }
    }
}

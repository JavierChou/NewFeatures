namespace Csharp
{
    /// <summary> 2008（.NET Framework 3.x） </summary>
    internal class Ver3
    {
        public void Test()
        {
            Sample.Builder()
                .SetTitle(GetType().Name)
                .AddAction(TestVar)
                .AddAction(TestExtend)
                .AddAction(TestLamda)
                .AddAction(TestAnonymousType)
                .AddAction(TestAutoAttribute)
                .AddAction(TestSearchExpressions)
                .AddAction(TestExpressionsTree)
                .AddAction(TestDynamic)
                .AddAction(TestLinq)
                .Run();
        }

        void TestVar()
        {
            Console.WriteLine(@"//类型推断：var
int i = 123;
string s = "";

//等价于上面写法
var i = 123;    //申明变量的时候，可以不用直指定类型
var s = "";

//支持数组
var intArray = new[] { 1, 2, 3 };
var stringArray = new[] { """" };
");
            var i = 123;
            var s = "";

            var intArray = new[] { 1, 2, 3 };
            var stringArray = new[] { "" };
        }

        void TestExtend()
        {
            Console.Write(@"//扩展方法：this
public static class Extends
{
    public static int ToInt(this string s) { return Int32.Parse(s); }
}

Console.WriteLine(""122"".ToInt() + 1); //");
            Console.WriteLine("122".ToInt() + 1);
        }

        void TestLamda()
        {
            Console.WriteLine(@"//Lamda 表达式（匿名方法的一个语法上的简化）
var ls = new List<Lamda>
{
    new Lamda { ID = 1 },
    new Lamda { ID = 2 }
};  //对象和集合的初始化
");
            var ls = new List<Lamda>
            {
                new Lamda { ID = 1 },
                new Lamda { ID = 2 }
            };
        }

        void TestAnonymousType()
        {
            Console.Write(@"//匿名类型
var p = new { ID = 1, Num = 2 };
var p2 = new { ID = 1, Num = 2 };
Console.WriteLine(p == p2); //");
            var p = new { ID = 1, Num = 2 };
            var p2 = new { ID = 1, Num = 2 };
            Console.WriteLine(p == p2);
        }

        void TestAutoAttribute()
        {
            Console.WriteLine(@"//自动属性
class PointOld
{
    private int _x;
    private int _y;

    public int X { get { return _x; } set { _x = value; } }
    public int Y { get { return _y; } set { _y = value; } }
}

//等价于上面写法，自动生成一个后台的私有变量
class PointNew
{
    public int X { get; set; }
    public int Y { get; set; }
}");
        }

        void TestSearchExpressions()
        {
            Console.Write(@"//查询表达式
var ls = new List<Lamda> { new Lamda { ID = 2 }, new Lamda { ID = 1 }, new Lamda { ID = 3 } };
var asc = from g in
              from i in ls
              group i by i.ID
          select g.Key;
Console.WriteLine(asc.ToString()); //");
            var ls = new List<Lamda> { new Lamda { ID = 2 }, new Lamda { ID = 1 }, new Lamda { ID = 3 } };
            var asc = from g in
                          from i in ls
                          group i by i.ID
                      select g.Key;
            Console.WriteLine(asc.ToString());
        }

        void TestExpressionsTree()
        {
            Console.Write(@"//表达式树
Func<int, int> f = x => x + 1;
Console.WriteLine(f(1));    //");
            Func<int, int> f = x => x + 1;
            Console.WriteLine(f(1));
        }

        void TestDynamic()
        {
            Console.Write(@"//动态绑定（运行时）：dynamic
dynamic p = new PointNew { X = 1, Y = 2 };
Console.WriteLine(p.X); //");
            dynamic p = new PointNew { X = 1, Y = 2 };
            Console.WriteLine(p.X);
        }

        void TestLinq()
        {
            Console.WriteLine(@"//Linq

/*.NET3.5中比较核心的内容
* Linq To Object：提供对集合和对象的处理
* Linq To XML：应用于XML
* Linq To Sql：应用于SqlServer数据库
* Linq To DataSet： DataSet
* Linq To Entities：应用于SqlServer之外的关系数据库
*/

//Linq To Object
var ls = new List<PointNew>
{
    new PointNew{ X=1, Y=1 },
    new PointNew{ X=2, Y=2 },
    new PointNew{ X=3, Y=3 }
};
var selectPoint = from point in ls
                  where point.X > 1 && point.Y > 2
                  orderby point.X
                  select point;
foreach (var item in selectPoint)
{
    Console.WriteLine($""item.X：{item.X} item.Y：{item.Y}"");
}
");
            var ls = new List<PointNew>
            {
                new PointNew{ X=1, Y=1 },
                new PointNew{ X=2, Y=2 },
                new PointNew{ X=3, Y=3 }
            };
            var selectPoint = from point in ls
                              where point.X > 1 && point.Y > 2
                              orderby point.X
                              select point;
            foreach (var item in selectPoint)
            {
                Console.WriteLine($"item.X：{item.X} item.Y：{item.Y}");
            }
        }
    }

    #region >> 扩展方法
    public static class Extends
    {
        public static int ToInt(this string s) { return Int32.Parse(s); }
    }
    #endregion

    #region >> Lamda
    class Lamda
    {
        public int ID;
    }
    #endregion

    #region >> 自动属性
    class PointOld
    {
        private int _x;
        private int _y;

        public int X { get { return _x; } set { _x = value; } }
        public int Y { get { return _y; } set { _y = value; } }
    }

    class PointNew
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
    #endregion
}

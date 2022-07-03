namespace Csharp
{
    /// <summary> 2019（.NET Core 3.x）（.NET Standard 2.1） </summary>
    internal class Ver8
    {
        public void Test()
        {
            Sample.Builder()
                .SetTitle(GetType().Name)
                .AddAction(TestReadonly)
                .AddAction(TestInterface)
                .AddAction(TestUsing)
                .AddAction(TestStaticLocalFunction)
                .AddAction(TestNullable)
                .AddAction(TestNullMergeSet)
                .AddAction(TestIndexAndRange)
                .AddAction(TestUnmanagedTypes)
                .AddAction(TestStringInterpolations)
                .AddAction(TestAsync)
                .Run();
        }

        void TestReadonly() => Console.WriteLine("//Readonly 成员\npublic int X => 20; //只读属性");

        void TestInterface()
        {
            Console.WriteLine(@"//默认接口方法
interface IVer8
{
    void Init();
    //接口可以以类似虚函数的方式实现函数的初始化
    void Show() { Console.WriteLine(this.GetType().Name); } //接口默认实现方法
}

class Demo8 : IVer8
{
    public void Init() { }
}
");
            Console.Write(@"//使用接口定义才能调用默认接口方法
IVer8 demo = new Demo8();
demo.Show();    //");
            IVer8 demo = new Demo8();
            demo.Show();
        }

        void TestUsing()
        {
            Console.WriteLine(@"//using 改进
using (var fs = new FileStream("", FileMode.Open))
{
    using (var ms = new MemoryStream())
    { }
}

//等价于上面写法
using (var fs = new FileStream("", FileMode.Open))
using (var ms = new MemoryStream()) ;");
        }

        void TestStaticLocalFunction()
        {
            Console.WriteLine(@"//声明静态本地函数
int M()
{
    int y = 5;
    int x = 7;
    return Add(x, y);

    //支持静态
    static int Add(int left, int right) => left + right;
}
");
            Console.WriteLine("//调用静态本地函数\nM();\t//" + M());
        }

        int M()
        {
            int y = 5;
            int x = 7;
            return Add(x, y);

            static int Add(int left, int right) => left + right;
        }

        void TestNullable()
        {
            Console.WriteLine("//vs 2020默认开启该功能，需手动关闭配置");
            Console.WriteLine("#nullable enable");
            Console.WriteLine("string? str = \"\";");
            Console.WriteLine("#nullable disable");

#nullable enable
            string? str = "";
#nullable disable
        }

        void TestNullMergeSet()
        {
            Console.WriteLine(@"//Null 合并赋值：??=
string? str = "";
str = str == null ? "" : str;
str = str ?? "";

//等价于上面写法
str ??= "";");
            string? str = "";
            str ??= "";
        }

        void TestIndexAndRange()
        {
            Console.WriteLine("//索引和范围支持：数组、string、ReadOnlySpan<T>");
            var words = "hello world";
            Console.WriteLine("var words = \"hello world\";\t//" + words);
            Console.WriteLine("The last word is words[^1] => " + words[^1]);
            Console.WriteLine("The first 4 word is words[..4] => " + words[..4]);
            Console.WriteLine("The 6 to 8 word is words[6..8] => " + words[6..8]);
            Console.WriteLine("The 6 to last 1 is words[6..^1] => " + words[6..^1]);
            Console.WriteLine();
            var phrase = 0..5;
            Console.WriteLine("//将范围声明为变量\nRange phrase = 0..5;");
            Console.WriteLine("//使用范围\nvar text = words[phrase];\t//" + words[phrase]);
        }

        void TestUnmanagedTypes()
        {
            Console.WriteLine(@"//非托管构造类型：结构体支持泛型
public struct Coords<T>
{
    public T X;
    public T Y;
}

var coords = new Coords<int> { X = 0, Y = 0 };");
            var coords = new Coords<int> { X = 0, Y = 0 };
        }

        void TestStringInterpolations()
        {
            Console.WriteLine("//内插逐字字符串：字符串中 $ 和 @ 标记的顺序可以任意安排");
            Console.WriteLine("var str = @$\"..\";\t//早期 C# 版本中，$ 标记必须出现在 @ 标记之前");
            //var str = $@"...";
            var str = @$"...";
        }

        async void TestAsync()
        {
            Console.WriteLine(@"//异步流：async
static async IAsyncEnumerable<int> GenerateSequence()
{
    for (int i = 0; i < 20; i++)
    {
        await Task.Delay(100);  //等待 100 毫秒
        yield return i;
    }
}

//使用异步迭代访问
await foreach (var number in GenerateSequence())
{
    Console.WriteLine(number);
}");
            await foreach (var number in GenerateSequence())
            {
                Console.WriteLine(number);
            }
        }

        static async IAsyncEnumerable<int> GenerateSequence()
        {
            for (int i = 0; i < 20; i++)
            {
                await Task.Delay(100);
                yield return i;
            }
        }
    }

    #region >> Readonly 成员
    class ReadonlyMember
    {
        public int X => 20; //只读属性
    }
    #endregion

    #region >> 默认接口方法
    interface IVer8
    {
        void Init();
        void Show() { Console.WriteLine(this.GetType().Name); }
    }

    class Demo8 : IVer8
    {
        public void Init() { }
    }
    #endregion

    #region >> 非托管构造类型
    public struct Coords<T>
    {
        public T X;
        public T Y;
    }
    #endregion
}

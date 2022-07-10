namespace Csharp
{
    /// <summary> 2005 </summary>
    internal partial class Ver2
    {
        public void Test()
        {
            Sample.Builder()
                .SetTitle(GetType().Name)
                .AddAction(TestGenerics)
                .AddAction(TestDefault)
                .AddAction(TestNullableType)
                .AddAction(TestPartial)
                .AddAction(TestStaticClass)
                .AddAction(TestGlobal)
                .AddAction(TestExternAlias)
                .AddAction(TestFixed)
                .AddAction(TestVolatile)
                .AddAction(TestPragmaWarning)
                .Run();
        }

        void TestGenerics()
        {
            Console.WriteLine(@"//泛型：CLR 2.0中引入的最重要的新特性，使得可以在类、方法中对使用的类型进行参数化

/*泛型的好处
* 编译时就可以保证类型安全
* 不用做类型装换，获得一定的性能提升
*/
class MyCollection<T>
{
    ICollection<T> variable;

    public void Add(T item) { variable.Add(item); }
}

MyCollection<string> ls = new MyCollection<string>();
MyCollection<int> ls2 = new MyCollection<int>();
");
            MyCollection<string> ls = new MyCollection<string>();
            MyCollection<int> ls2 = new MyCollection<int>();

            Console.WriteLine(@"//泛型约束：给泛型参数加约束，要求类型满足一定条件

/*约束                           说明
* where T: struct                类型参数需是值类型
* where T : class                类型参数需是引用类型
* where T : new()                类型参数要有一个public的无参构造函数
* where T : <base class name>	 类型参数要派生自某个基类
* where T : <interface name>	 类型参数要实现了某个接口
* where T : U                    这里T和U都是类型参数，T必须是或者派生自U
*/

//这些约束，可以同时一起使用
class StudentList<T> where T : Student, IComparable<T>, new() { }");
        }

        void TestDefault()
        {
            Console.Write(@"//默认：default
class Default
{
    public static int ReturnIntDefault() { return default; }

    public static Student ReturnStudentDefault() { return default; }

    public static DefaultStruct ReturnDefaultStruct() { return default; }

    public static T ReturnDefalut<T>() { return default(T); }
}

struct DefaultStruct
{
    string Name;
    int Age;
}

Console.WriteLine(Default.ReturnIntDefault());  //");
            Console.WriteLine(Default.ReturnIntDefault());
            Console.Write("Console.WriteLine(Default.ReturnStudentDefault());   //");
            Console.WriteLine(Default.ReturnStudentDefault());
            Console.Write("Console.WriteLine(Default.ReturnDefaultStruct());    //");
            Console.WriteLine(Default.ReturnDefaultStruct());
            Console.Write("Console.WriteLine(Default.ReturnDefalut<string>());  //");
            Console.WriteLine(Default.ReturnDefalut<string>());
        }

        void TestNullableType()
        {
            Console.WriteLine(@"//可空类型：仅针对于值类型
int? num = null;
if (num.HasValue == true)
    Console.WriteLine(""num = "" + num.Value);
else
    Console.WriteLine(""num = Null"");");
            int? num = null;
            if (num.HasValue == true)
                Console.WriteLine("num = " + num.Value);
            else
                Console.WriteLine("num = Null");
            Console.WriteLine();

            Console.Write(@"//可空类型赋值
int number = num ?? -1; //");
            int number = num ?? -1;
            Console.WriteLine(number);
        }

        void TestStaticClass()
        {
            Console.WriteLine(@"//静态类：static

/*
* 静态类就一个只能有静态成员的类，用static关键字对类进行标示
* 静态类不能被实例化
* 静态类理论上相当于一个只有静态成员并且构造函数为私有的普通类
* 静态类相对来说的好处就是编译器能够保证静态类不会添加任何非静态成员
*/");
        }

        void TestGlobal()
        {
            global::System.Console.Write(@"//全局命名空间（最上层的命名空间）：global::
int Console = 123;
global::System.Console.WriteLine(Console);  //");
            int Console = 123;
            global::System.Console.WriteLine(Console);
        }

        void TestExternAlias()
        {
            Console.WriteLine(@"//扩展别名

/*
* 用来消除不同程序集中类名重复的冲突
* 可以引用同一个程序集的不同版本，就是在编译的时候，提供了一个将有冲突的程序集进行区分的手段
* 被引用的程序集的属性里面可以指定Alias的值，默认是 global
*/
using System;
using aliasName = System;");
        }

        void TestFixed()
        {
            Console.WriteLine(@"//固定：fixed
unsafe struct FixedExample
{
    public fixed byte Array[8]; //使用 fixed 关键字来创建固定长度的数组
}");
        }

        void TestVolatile()
        {
            Console.WriteLine(@"//不稳定的：volatile

//字段可能被多个线程同时访问，编译器不会对相应的值做针对单线程下的优化，保证相关的值在任何时候访问都是最新的。
");
            Worker worker = new Worker();
            Thread thread = new Thread(worker.DoWork);
            thread.Start();
            Console.WriteLine("TestVolatile Start!");
            while (!thread.IsAlive) ;
            Thread.Sleep(500);
            worker.RequestStop();
            thread.Join();
            Console.WriteLine("TestVolatile Stop!");
        }

        void TestPragmaWarning()
        {
            Console.WriteLine(@"//编译警告（用来取消或者添加编译时的警告信息）
#pragma warning disable 414, 3021   //CS414 和 CS3021 的警告信息将不会显示");

#pragma warning disable 414, 3021
        }
    }

    #region >> 泛型
    class MyCollection<T>
    {
        ICollection<T> variable;

        public void Add(T item) { variable.Add(item); }
    }

    class StudentList<T> where T : Student, IComparable<T>, new() { }
    #endregion

    #region >> Default
    class Default
    {
        public static int ReturnIntDefault() { return default; }

        public static Student ReturnStudentDefault() { return default; }

        public static DefaultStruct ReturnDefaultStruct() { return default; }

        public static T ReturnDefalut<T>() { return default(T); }
    }

    struct DefaultStruct
    {
        string Name;
        int Age;
    }
    #endregion

    #region >> 部分类
    internal partial class Ver2
    {
        void TestPartial()
        {
            Console.WriteLine(@"//部分类：partial

/*
* 在申明一个类、结构或者接口的时候，用 partial 关键字，可以让源代码分布在不同的文件中（代码分离）
* 部分类仅是编译器提供的功能，在编译的时候会把 partial 关键字定义的类和在一起去编译，和CRL没什么关系
*/");
        }
    }
    #endregion

    #region >> Fixed
    unsafe struct FixedExample
    {
        public fixed byte Array[8];
    }
    #endregion

    #region >> Volatile
    class Worker
    {
        volatile bool stop;

        public void DoWork()
        {
            bool work = false;
            while (!stop)
            {
                work = !work;
            }
            Console.WriteLine("DoWork");
        }

        public void RequestStop() { stop = true; }
    }
    #endregion
}

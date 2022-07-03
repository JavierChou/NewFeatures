namespace Csharp
{
    /// <summary> 2021（.NET 5.x） </summary>
    internal class Ver9
    {
        public void Test()
        {
            Sample.Builder()
                .SetTitle(GetType().Name)
                .AddAction(TestInit)
                .AddAction(TestRecord)
                .AddAction(TestTopLevelPrograms)
                .AddAction(TestMatch)
                .AddAction(TestNew)
                .AddAction(TestCovariant)
                .Run();
        }

        void TestInit()
        {
            Console.WriteLine(@"//init 关键字
class Person
{
    public string Name { get; init; }
    public int Age { get; init; }
}

//属性和索引器创建 init 访问器，调用方可使用属性初始化表达式语法在创建表达式中设置这些值，但构造完成后，这些属性将变为只读
var person = new Person { Name = ""Mads"", Age = 20 };  // OK
person.Age = 18;    // ERROR");
            var person = new Person { Name = "Mads", Age = 20 };
            //person.Age = 18;
        }

        void TestRecord()
        {
            Console.WriteLine(@"//记录：record
record Person_Record(string Name, int Age);   //等价于上面写法，记录类只读
");
            var record1 = new Person_Record("", 18);
            var record2 = new Person_Record("", 18);
            Console.WriteLine(@$"//值相等性：数据相同即认为是相等
var record1 = new Person_Record("", 18);
var record2 = new Person_Record("", 18);
Console.WriteLine(record1 == record2);  //{record1 == record2}
");
            var record = record1 with { Age = 20 };
            Console.WriteLine(@$"//非破坏性变化：修改record的值 使用 with 创建一个副本
var record = record1 with {{ Age = 20 }};
Console.WriteLine(record.Age);  //{record.Age}
");
            Console.WriteLine(@"//继承
record Student : Person_Record
{
    public int ID;

    public Student(string Name, int Age) : base(Name, Age) { }
}

//等价于上面写法
record Student_2(string Name, int Age, int ID) : Person_Record(Name, Age);

var student = new Student(""Mads"", 20) { ID = 129 };
var student2 = new Student_2(""Mads"", 20, 129);
");
            var student = new Student("Mads", 20) { ID = 129 };
            var student2 = new Student_2("Mads", 20, 129);
            Console.Write(@"//位置记录：初始化时位置构造函数 construction
var (n, a) = student;    // 位置解构函数 deconstruction
Console.WriteLine($""Name：{n} Age：{a}"");   //");
            var (n, a) = student;
            Console.WriteLine($"Name：{n} Age：{a}");
        }

        void TestTopLevelPrograms()
        {
            Console.WriteLine(@"//顶级语句：实际就是简写
using System;
Console.WriteLine(""Hello, World!"");   //在 Program.cs 内

//单行程序，使用完全限定的类型名称
System.Console.WriteLine(""Hello World!"");

//args一样也是可用的
Console.WriteLine(args[0]);");
        }

        void TestMatch()
        {
            Console.Write(@"//增强的模式匹配
var grade = 90 switch   //关系模式
{
    >= 60 => ""Good"",
    < 60 => ""Bad""
};
Console.WriteLine(grade);   //");
            var grade = 90 switch
            {
                >= 60 => "Good",
                < 60 => "Bad"
            };
            Console.WriteLine(grade);
            Console.Write(@"
/*逻辑模式
* 联合 and 模式要求两个模式都匹配
* 析取 or 模式要求任一模式匹配
* 否定 not 模式要求模式不匹配
*/
public static bool IsLetter(this char c) => c is >= 'a' and <= 'z' or >= 'A' and <= 'Z';

Console.WriteLine(""a is letter："" + 'a'.IsLetter());   //");
            Console.WriteLine("a is letter：" + 'a'.IsLetter());
            Console.Write("Console.WriteLine(\"0 is letter：\" + '0'.IsLetter());\t//");
            Console.WriteLine("0 is letter：" + '0'.IsLetter());
            Console.WriteLine(@"
public static bool IsLetterOrSeparator(this char c) => c is >= 'a' and <= 'z' or >= 'A' and <= 'Z' or '.' or ',';
");
            Console.WriteLine(". is letterOrSeparator：" + '.'.IsLetterOrSeparator());
            Console.WriteLine(", is letterOrSeparator：" + ','.IsLetterOrSeparator());
            Console.WriteLine("# is letterOrSeparator：" + '#'.IsLetterOrSeparator());
            Console.WriteLine(@"
public static bool IsNotNull(this object o) => o is not null;   // 用于 NULL 检查的新语法
");
            Console.WriteLine("0 is not null：" + 0.IsNotNull());
        }

        void TestNew()
        {
            Person demo = new();
            Console.WriteLine(@$"//类型推导new表达式：省略类型 new()
Point p = new (3, 5);   //忽略new关键字后面的类型

Person demo = new();    //调用默认构造实现 Name：{demo.Name} Age：{demo.Age}");
            demo = new(this.GetType().Name, 18);
            Console.WriteLine($"demo = new(this.GetType().Name, 18);    //调用带参构造实现 Name：{demo.Name} Age：{demo.Age}");
        }

        void TestCovariant()
        {
            Console.WriteLine(@"//返回值类型支持协变
interface Food { }

class Meat : Food { }

abstract class Animal
{
    public abstract Food GetFood();
}

class Tiger : Animal
{
    //返回值使用 Meat 而不是 Food，更为形象具体
    public override Meat GetFood() => new();
}");
        }
    }

    #region >> 记录类（适合用于访问参数、封装数据）
    class Person
    {
        public Person() { }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public string Name { get; init; }
        public int Age { get; init; }
    }

    record Person_Record(string Name, int Age);

    record Student : Person_Record
    {
        public int ID;

        public Student(string Name, int Age) : base(Name, Age) { }
    }

    record Student_2(string Name, int Age, int ID) : Person_Record(Name, Age);
    #endregion

    #region >> 模式匹配增强功能
    static class Match
    {
        public static bool IsLetter(this char c) => c is >= 'a' and <= 'z' or >= 'A' and <= 'Z';

        public static bool IsLetterOrSeparator(this char c) => c is >= 'a' and <= 'z' or >= 'A' and <= 'Z' or '.' or ',';

        public static bool IsNotNull(this object o) => o is not null;
    }
    #endregion

    #region >> 返回值类型支持协变
    interface Food { }

    class Meat : Food { }

    abstract class Animal
    {
        public abstract Food GetFood();
    }

    class Tiger : Animal
    {
        public override Meat GetFood() => new();
    }
    #endregion
}

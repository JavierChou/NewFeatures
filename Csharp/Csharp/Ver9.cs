namespace Csharp
{
    /// <summary> 2021（.NET 5.x） </summary>
    internal class Ver9
    {
        public void Test()
        {
            Sample.Builder()
                .SetTitle(GetType().Name)
                .AddAction(TestRecord)
                .AddAction(TestMatch)
                .AddAction(TestNew)
                .Run();
        }

        void TestRecord()
        {
            Console.WriteLine("记录类型,数据相同即认为是相等");
            var record1 = new Person_Record("", 18);
            var record2 = new Person_Record("", 18);
            Console.WriteLine("值相等性：" + (record1 == record2));
            Console.WriteLine();
            Console.WriteLine("非破坏性变化 => 修改record的值 使用 with 创建一个副本");
            var record = record1 with { Age = 20 };
            Console.WriteLine("副本值 Age：" + record.Age);
        }

        void TestMatch()
        {
            Console.WriteLine("a is letter：" + 'a'.IsLetter());
            Console.WriteLine("0 is letter：" + '0'.IsLetter());
            Console.WriteLine();
            Console.WriteLine(". is letterOrSeparator：" + '.'.IsLetterOrSeparator());
            Console.WriteLine(", is letterOrSeparator：" + ','.IsLetterOrSeparator());
            Console.WriteLine("# is letterOrSeparator：" + '#'.IsLetterOrSeparator());
            Console.WriteLine();
            Console.WriteLine("0 is not null：" + 0.IsNotNull());
        }

        void TestNew()
        {
            Console.WriteLine("省略类型 new()");
            Person demo = new();
            Console.WriteLine("调用默认构造实现 Name：" + demo.Name + " Age：" + demo.Age);
            demo = new(this.GetType().Name, 18);
            Console.WriteLine("调用带参构造实现 Name：" + demo.Name + " Age：" + demo.Age);
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

    record Person_Record(string Name, int Age);   //等价于上面写法，记录类只读
    #endregion

    #region >> 顶级语句（实际就是简写）
    //注：以下语句需在 Program.cs 内

    //using System;
    //Console.WriteLine("Hello, World!");

    //单行程序，使用完全限定的类型名称
    //System.Console.WriteLine("Hello World!");
    #endregion

    #region >> 模式匹配增强功能
    static class Match
    {
        //联合 and 模式要求两个模式都匹配
        //析取 or 模式要求任一模式匹配
        //否定 not 模式要求模式不匹配
        public static bool IsLetter(this char c) => c is >= 'a' and <= 'z' or >= 'A' and <= 'Z';

        public static bool IsLetterOrSeparator(this char c) => c is >= 'a' and <= 'z' or >= 'A' and <= 'Z' or '.' or ',';

        public static bool IsNotNull(this object o) => o is not null;   // 用于 NULL 检查的新语法
    }
    #endregion
}

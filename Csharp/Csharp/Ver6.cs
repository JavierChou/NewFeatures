namespace Csharp
{
    /// <summary> 2015（.NET Framework 4.6） </summary>
    internal class Ver6
    {
        public void Test()
        {
            Sample.Builder()
                .SetTitle(GetType().Name)
                .AddAction(TestAutoProperties)
                .AddAction(TestExpressionBodiedFunctionMembers)
                .AddAction(TestNull)
                .AddAction(TestStringSplicing)
                .AddAction(TestNameOf)
                .AddAction(TestStaticUsing)
                .Run();
        }

        void TestAutoProperties()
        {
            Console.WriteLine(@"//自动属性的增强
class Customer
{
    public string Name { get; }

    public Customer(string firstName, string lastName)
    {
        Name = firstName + lastName;
    }
}

//等价于上面写法
class CustomerNew
{
    public string FirstName { get; } = ""Javier"";
    public string LastName { get; } = ""Chou"";
}");
        }

        void TestExpressionBodiedFunctionMembers()
        {
            Console.WriteLine(@"//表达式体函数成员

void Print() => Console.WriteLine(""用Lambda作为函数体"");

public string Name => FirstName + LastName; //Lambda表达式用作属性");
        }

        void Print() => Console.WriteLine("用Lambda作为函数体");

        void TestNull()
        {
            Console.WriteLine(@"//Null条件运算符：?.
Customer customer = null;
Console.WriteLine(customer?.Name);");
            Customer customer = null;
            Console.WriteLine(customer?.Name);
        }

        void TestStringSplicing()
        {
            Console.Write(@"//内插字符串
var name = ""JavierChou"";
Console.WriteLine($""name：{name}"");    //");
            var name = "JavierChou";
            Console.WriteLine($"name：{name}");
        }

        void TestNameOf()
        {
            Console.Write(@"//获取变量名：nameof
var age = 18;
Console.WriteLine(nameof(age)); //");
            var age = 18;
            Console.WriteLine(nameof(age));
        }

        void TestStaticUsing()
        {
            Console.WriteLine(@"//引用静态类：Using Static

using static JavierChou;    //在Using中可以指定一个静态类，然后可以在随后的代码中直接使用静态的成员");
        }
    }

    #region >> 自动属性
    class Customer
    {
        public string Name { get; }

        public Customer(string firstName, string lastName)
        {
            Name = firstName + lastName;
        }
    }

    class CustomerNew
    {
        public string FirstName { get; } = "Javier";
        public string LastName { get; } = "Chou";
    }
    #endregion
}

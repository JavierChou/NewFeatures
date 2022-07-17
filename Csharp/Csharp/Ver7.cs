namespace Csharp
{
    /// <summary> 2017（.NET Core 2.x）（.NET Standard 2.0/1.x）（.NET FX） </summary>
    internal class Ver7
    {
        public void Test()
        {
            Sample.Builder()
                .SetTitle(GetType().Name)
                .AddAction(TestOut)
                .AddAction(TestValueTuple)
                .AddAction(TestMatch)
                .AddAction(TestLocalMethod)
                .AddAction(TestRefLocalsAndReturns)
                .AddAction(TestOtherAttribute)
                .Run();
        }

        void TestOut()
        {
            Console.Write(@"//out 变量
int result = 0;
int.TryParse(""20"", out result);   //低版本写法

//等价于上面写法
int.TryParse(""20"", out var result);   //不需要事先定义一个变量
Console.WriteLine(result);  //");
            int.TryParse("20", out var result);
            Console.WriteLine(result);
        }

        void TestValueTuple()
        {
            Console.Write(@"//元组和析构函数：ValueTuple

/*ValueTuple特点
* 支持语义上的字段命名
* ValueTuple 是值类型（Struct）
*/

//创建元组
var valueTuple = (""name"", 18);
var createValueTuple = ValueTuple.Create(""name"", 18);
var newValueTuple = new ValueTuple<string, int>(""name"", 18);
Console.WriteLine($""first：{valueTuple.Item1} second：{valueTuple.Item2}"");   //");
            var valueTuple = ("name", 18);
            var createValueTuple = ValueTuple.Create("name", 18);
            var newValueTuple = new ValueTuple<string, int>("name", 18);
            Console.WriteLine($"first：{valueTuple.Item1} second：{valueTuple.Item2}\n");

            Console.Write(@"//创建给字段命名的元组
(string name, int age) nameValueTuple = (""name"", 18); //左边指定字段名称
Console.WriteLine($""first：{nameValueTuple.name} second：{nameValueTuple.age}"");    //");
            (string name, int age) nameValueTuple = ("name", 18);
            Console.WriteLine($"first：{nameValueTuple.name} second：{nameValueTuple.age}\n");
            Console.Write(@"var nameValueTuple2 = (name: ""name"", age: 18);    //右边指定字段名称
Console.WriteLine($""first：{nameValueTuple2.name} second：{nameValueTuple2.age}"");    //");
            var nameValueTuple2 = (name: "name", age: 18);
            Console.WriteLine($"first：{nameValueTuple2.name} second：{nameValueTuple2.age}\n");
            Console.Write(@"(string name, int age) nameValueTuple3 = (name2: ""name"", age2: 18);   //左右两边指定名称，此处有警告：由于目标类型指定了其他名称，因此元组元素名称 name2 被忽略
Console.WriteLine($""first：{nameValueTuple3.name} second：{nameValueTuple3.age}"");    //");
            (string name, int age) nameValueTuple3 = (name2: "name", age2: 18);
            Console.WriteLine($"first：{nameValueTuple3.name} second：{nameValueTuple3.age}\n");

            Console.Write(@"//解构元组：将元组中的字段值赋值给声明的局部变量（编译后可查看）
(string, int) GetTuple()
{
    return (""name"", 18);
}

var (name, age) = GetTuple();
Console.WriteLine($""first：{name} second：{age}"");  //");
            var (name, age) = GetTuple();
            Console.WriteLine($"first：{name} second：{age}\n");
            Console.Write(@"//解构可以应用于 .Net 的任意类型，但需要编写 Deconstruct 方法成员（实例或扩展）
class StudentDeconstruct
{
    public string Name { get; set; }
    public int Age { get; set; }

    public StudentDeconstruct(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public void Deconstruct(out string name, out int age)
    {
        name = Name;
        age = Age;
    }
}

var (name1, age1) = new StudentDeconstruct(""name"", 18);   //编译后就是由其实例调用 Deconstruct 方法，然后给局部变量赋值
Console.WriteLine($""first：{name1} second：{age1}"");  //");
            var (name1, age1) = new StudentDeconstruct("name", 18);
            Console.WriteLine($"first：{name1} second：{age1}\n");

            Console.Write(@"//弃元：_
var (name3, _) = GetTuple();
Console.WriteLine($""name3：{name3}"");  //");
            var (name3, _) = GetTuple();
            Console.WriteLine($"name3：{name3}");
        }

        (string, int) GetTuple()
        {
            return ("name", 18);
        }

        void TestMatch()
        {
            Console.WriteLine(@"//模式匹配：is 表达式（其实是 as 和 if 的组合）
class Man : Person { }

class Woman : Person { }

var persons = new List<Person>
{
    new Man(),
    new Woman()
};
persons.ForEach(x =>
{
    if (x is Man man) Console.WriteLine($""This is {nameof(man)}"");
    else if (x is Woman woman) Console.WriteLine($""This is {nameof(woman)}"");
});");
            var persons = new List<Person>
            {
                new Man(),
                new Woman()
            };
            persons.ForEach(x =>
            {
                if (x is Man man) Console.WriteLine($"This is {nameof(man)}");
                else if (x is Woman woman) Console.WriteLine($"This is {nameof(woman)}");
            });
            Console.WriteLine();

            Console.WriteLine(@"//switch语句更新：编译后就是 as 、if 、goto 语句的组合体
var obj = new object();
switch (obj)
{
    case 0: break;  //常量
    case int ival: break;   //类型
    case "": break;
    case List<object> ls when ls.Any(): break;  //条件
    case string str when int.TryParse(str, out var result): break;  //类型+条件
    default: break;
}

var name = obj switch  //一定有返回值
{
    1 => throw new NotImplementedException(),
    2 => throw new NotImplementedException(),
    _ => obj.ToString()  //默认
};

_ = 1 switch
{
    (> 32) and (< 212) => ""liquid"",
    < 32 => ""solid"",
    > 212 => ""gas"",
    32 => ""solid /liquid transition"",
    212 => ""liquid / gas transition""
};

_ = new StudentDeconstruct("""", 18) switch
{
    (name: """", age: > 18) => 18,
    (name: """", age: < 18) => 18,
    StudentDeconstruct { Age: > 18 } => 18,
    null => throw new ArgumentNullException(nameof(StudentDeconstruct), """"),
    var someObject => 0,
};");
            var obj = new object();
            switch (obj)
            {
                case 0: break;
                case int ival: break;
                case "": break;
                case List<object> ls when ls.Any(): break;
                case string str when int.TryParse(str, out var result): break;
                default: break;
            }

            var name = obj switch
            {
                1 => throw new NotImplementedException(),
                2 => throw new NotImplementedException(),
                _ => obj.ToString()
            };

            _ = 1 switch
            {
                (> 32) and (< 212) => "liquid",
                < 32 => "solid",
                > 212 => "gas",
                32 => "solid/liquid transition",
                212 => "liquid / gas transition"
            };

            _ = new StudentDeconstruct("", 18) switch
            {
                (name: "", age: > 18) => 18,
                (name: "", age: < 18) => 18,
                StudentDeconstruct { Age: > 18 } => 18,
                null => throw new ArgumentNullException(nameof(StudentDeconstruct), ""),
                var someObject => 0,
            };
        }

        void TestLocalMethod()
        {
            Write();

            void Write()
            {
                Console.WriteLine(@"//本地函数：方法中写内部方法
void TestLocalMethod()
{
    Write();

    void Write() { ... }
}");
            }
        }

        void TestRefLocalsAndReturns()
        {
            Console.Write(@"//局部引用和引用返回（防止值类型大对象在Copy过程中损失更多的性能）
ref int GetRef(int[,] arr, Func<int, bool> func)
{
    for (int i = 0; i < arr.GetLength(0); i++)
    {
        for (int j = 0; j < arr.GetLength(1); j++)
        {
            if (func(arr[i, j]))
                return ref arr[i, j];
        }
    }
    throw new InvalidOperationException(""Not Found."");
}

int[,] arr = { { 10, 15 }, { 20, 25 } };
ref var num = ref GetRef(arr, c => c == 20);
num = 60;
Console.WriteLine(arr[1, 0]);   //");
            int[,] arr = { { 10, 15 }, { 20, 25 } };
            ref var num = ref GetRef(arr, c => c == 20);
            num = 60;
            Console.WriteLine(arr[1, 0]);
        }

        ref int GetRef(int[,] arr, Func<int, bool> func)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (func(arr[i, j]))
                        return ref arr[i, j];
                }
            }
            throw new InvalidOperationException("Not Found.");
        }

        void TestOtherAttribute()
        {
            Console.Write(@"//数值文字中的前导下划线（改善可读性）：数字分隔符,支持 decimal、float、double
var age = 123_456_789;	//");
            var age = 123_456_789;
            Console.WriteLine(age);
            Console.WriteLine();

            Console.Write(@"//二进制字面量可以使用 0b 前缀进行标识
var b = 0b0111_1011;    //");
            var b = 0b0111_1011;
            Console.WriteLine(b);

            Console.WriteLine(@"
//private protected 限制对同一程序集中声明的派生类的访问（与）
//protected internal 允许通过同一程序集中的类或派生类进行访问（或）");
        }
    }

    #region >> 解构
    class StudentDeconstruct
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public StudentDeconstruct(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public void Deconstruct(out string name, out int age)
        {
            name = Name;
            age = Age;
        }
    }
    #endregion

    #region >> Is Expressions
    class Man : Person { }

    class Woman : Person { }
    #endregion
}

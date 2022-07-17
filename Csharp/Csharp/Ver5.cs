using System.Runtime.CompilerServices;

namespace Csharp
{
    /// <summary> 2012（.NET Framework 4.5） </summary>
    internal class Ver5
    {
        public void Test()
        {
            Sample.Builder()
                .SetTitle(GetType().Name)
                .AddAction(TestAsync)
                .AddAction(TestCallAttribute)
                .Run();
        }

        void TestAsync()
        {
            Console.WriteLine(@"//异步编程（async await）
详见：https://docs.microsoft.com/zh-cn/dotnet/csharp/async");
        }

        void TestCallAttribute()
        {
            Console.WriteLine(@"//调用者信息特性
void ShowInfo([CallerFilePath] string file = null, [CallerLineNumber] int number = 0, [CallerMemberName] string name = null)
{
    Console.WriteLine($""FilePath：{file}"");    //当前编译器的执行文件名
    Console.WriteLine($""LineNumber：{number}"");    //所在行数
    Console.WriteLine($""MemberName：{name}"");  //方法或属性名称
}

ShowInfo();
");
            ShowInfo();
        }

        void ShowInfo([CallerFilePath] string file = null, [CallerLineNumber] int number = 0, [CallerMemberName] string name = null)
        {
            Console.WriteLine($"FilePath：{file}");
            Console.WriteLine($"LineNumber：{number}");
            Console.WriteLine($"MemberName：{name}");
        }
    }
}

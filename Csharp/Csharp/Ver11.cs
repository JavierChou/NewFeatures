namespace Csharp;

/// <summary> 2022（.NET 6.x preview） </summary>
internal class Ver11
{
    public void Test()
    {
        Sample.Builder()
            .SetTitle(GetType().Name)
            .AddAction(TestNewLinesInStringInterpolations)
            .AddAction(TestListPatterns)
            .AddAction(TestArgumentNull)
            .Run();
    }

    void TestNewLinesInStringInterpolations()
    {
        //从字符串文本扩散到了文本之外的 插值表达式
        Console.WriteLine($"This is NewLinesInStringInterpolations：{
            new NewLinesInStringInterpolations()
            {
                Strings = new List<SealedToString>()
            }
            .Strings
            .DefaultIfEmpty(new SealedToString(123456, 0))
            .First()
            .X
            .ToString("###,###")
            }");
    }

    void TestListPatterns()
    {
        Console.WriteLine("CheckSwitch：");
        Console.WriteLine(ListPatterns.CheckSwitch(new[] { 1, 2, 10 }));          //1
        Console.WriteLine(ListPatterns.CheckSwitch(new[] { 1, 2, 7, 3, 3, 10 })); //1
        Console.WriteLine(ListPatterns.CheckSwitch(new[] { 1, 2 }));              //2
        Console.WriteLine(ListPatterns.CheckSwitch(new[] { 1, 3 }));              //3
        Console.WriteLine(ListPatterns.CheckSwitch(new[] { 1, 3, 5 }));           //4
        Console.WriteLine(ListPatterns.CheckSwitch(new[] { 2, 5, 6, 7 }));        //50
        Console.WriteLine();
        Console.WriteLine("范围模式 (..) 匹配零个或多个元素的任何序列");
        Console.WriteLine("{ 1, 2, 10 } is [1, 2, .., 10]：" + (new int[] { 1, 2, 10 } is [1, 2, .., 10]));
        Console.WriteLine("{ 1, 2, 5, 10  } is [1, 2, .., 10]：" + (new int[] { 1, 2, 5, 10 } is [1, 2, .., 10]));
        Console.WriteLine("{ 1, 2, 5, 6, 7, 8, 9, 10 } is [1, 2, .., 10]：" + (new int[] { 1, 2, 5, 6, 7, 8, 9, 10 } is [1, 2, .., 10]));
    }

    void TestArgumentNull()
    {
        Console.WriteLine("添加!!到参数名，自动执行空值检查，空值检查代码将在方法主体的代码之前执行");
        try
        {
            ArgumentNull.Check(null);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}

#region >> 允许在内插字符串的 “插值表达式” 中换行
class NewLinesInStringInterpolations
{
    public List<SealedToString> Strings;
}
#endregion

#region >> 列表模式（类似JS的语法）
class ListPatterns
{
    public static int CheckSwitch(int[] values) => values switch
    {
        [1, 2, .., 10] => 1,
        [1, 2] => 2,
        [1, _] => 3,
        [1, ..] => 4,
        [..] => 50
    };

    //切片模式后面可以跟着另一个列表模式，比如 var 模式来捕获切片内容
    public static string CaptureSlice(int[] values) => values switch
    {
        [1, .. var middle, _] => $"Middle {String.Join(", ", middle)}",
        [.. var all] => $"All {String.Join(", ", all)}"
    };
}
#endregion

#region >> 参数空值检查
class ArgumentNull
{
    public static void CheckArgument(string s)
    {
        if (s is null)
            throw new ArgumentNullException(nameof(s));
        Console.WriteLine(s);
    }

    public static void Check(string s!!) => Console.WriteLine(s);   //等价于上面写法
}
#endregion

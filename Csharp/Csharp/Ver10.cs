#region >> 全局引入（适用于编译中的所有源文件）
global using Csharp;
#endregion

#region >> 文件范围的命名空间声明（节省了水平和垂直空间）
namespace Csharp;
#endregion

/// <summary> 2021（.NET 6.x） </summary>
internal class Ver10
{
    public void Test()
    {
        Sample.Builder()
            .SetTitle(GetType().Name)
            .AddAction(TestStructRecord)
            .AddAction(TestSealedToString)
            .Run();
    }

    void TestStructRecord()
    {
        var record = new Point(1, 2, 3);
        Console.WriteLine("结构体 X：" + record.X + " Y：" + record.Y + " Z：" + record.Z);
    }

    void TestSealedToString()
    {
        Console.WriteLine("ToString：" + new SealedToString(1, 1));
    }
}

#region >> 值类型记录
readonly record struct Point(double X, double Y, double Z);
#endregion

#region >> 记录类型可以密封 ToString
record class SealedToString(int X, int Y)
{
    public sealed override string ToString() => $"({X},{Y})";
}
#endregion

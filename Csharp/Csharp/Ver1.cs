namespace Csharp
{
    /// <summary> 2002（.NET Framework 1.x） </summary>
    internal class Ver1
    {
        public void Test()
        {
            Sample.Builder()
                .SetTitle(GetType().Name)
                .AddAction(TestRemark)
                .Run();
        }

        void TestRemark()
        {
            Console.WriteLine(@"/*基本的面向对象的语法
* using：在程序中包含命名空间
* class：声明一个类
* 注释    //单行    /*多行*/
* 成员变量
* 成员函数
* 实例化类
* 标识符：标识类，变量，函数或任何其他用户定义项目的名称
* 关键字：关键字不能用作标识符（可以使用 @ 字符将关键字前缀来表示某一标识符）
*/");
        }
    }
}

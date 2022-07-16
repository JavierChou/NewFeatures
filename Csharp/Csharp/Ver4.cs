namespace Csharp
{
    /// <summary> 2010（.NET Framework 4.x） </summary>
    internal class Ver4
    {
        public void Test()
        {
            Sample.Builder()
                .SetTitle(GetType().Name)
                .AddAction(TestOptionalParameters)
                .Run();
        }

        void TestOptionalParameters()
        {
            Console.Write(@"//可选参数（命名参数）
void Write(string name = """", int age = 18)
{
    Console.WriteLine($""name：{name} age：{age}"");
}

Write();    //");
            Write();
            Console.Write(@"Write(""javier"");  //");
            Write("javier");
            Console.Write(@"Write(""javier"", 20);  //");
            Write("javier", 20);
            Console.WriteLine();

            Console.Write(@"//命名实参（可以在调用时改变参数的顺序）
Write(age: 20, name: ""javier"");   //");
            Write(age: 20, name: "javier");
        }

        void Write(string name = "", int age = 18)
        {
            Console.WriteLine($"name：{name} age：{age}");
        }
    }
}

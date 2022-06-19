namespace Csharp;

internal class Sample
{
    readonly Queue<Action> _actions;
    string _title = string.Empty;

    private Sample() => _actions = new Queue<Action>();

    public static Sample Builder() => new();

    public Sample SetTitle(string title)
    {
        this._title = title;
        return this;
    }

    public Sample AddAction(Action action)
    {
        if (action is not null)
            this._actions.Enqueue(action);
        return this;
    }

    public void Run()
    {
        new Thread(ActionTask).Start();
    }

    void ActionTask()
    {
        Console.WriteLine($"------ {this._title} Begin ------\n");
        Thread.Sleep(100);
        while (this._actions.Count > 0)
        {
            var action = this._actions.Dequeue();
            Console.WriteLine(">> " + action.Method.Name);
            action();
            Console.WriteLine();
            Thread.Sleep(100);
        }
        Console.WriteLine($"------ {this._title} End ------");
    }
}

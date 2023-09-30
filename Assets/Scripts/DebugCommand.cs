using System;

public class DebugCommandBase
{
    private string _commandId;
    private string _commandDesc;
    private string _commandFormat;

    public string CommandId { get { return _commandId; } }
    public string CommandDesc { get { return _commandDesc; } }
    public string CommandFormat { get { return _commandFormat; } }

    public DebugCommandBase(string commandId, string commandDesc, string commandFormat)
    {
        _commandId = commandId;
        _commandDesc = commandDesc;
        _commandFormat = commandFormat;
    }
}

public class DebugCommand : DebugCommandBase
{
    Action command;
    public DebugCommand(string id, string desc, string format, Action command) : base (id, desc, format)
    {
        this.command = command;
    }
    public void Invoke()
    {
        command.Invoke();
    }
}
public class DebugCommand<T1, T2, T3> : DebugCommandBase
{
    Action<T1, T2, T3> command;
    public DebugCommand(string id, string desc, string format, Action<T1, T2, T3> command) : base(id, desc, format)
    {
        this.command = command;
    }
    public void Invoke(T1 value1, T2 value2 , T3 value3)
    {
        command.Invoke(value1, value2, value3);
    }
}

using System;

namespace CommandsSystem
{
    public interface ICommandExecutor
    {   
        void StopCommand(Command command);
        bool TryExecuteCommand(Command command, Action callback = null);
        bool TryUndoLastCommand(Action callback = null);
        void ResetStack();
    }
}

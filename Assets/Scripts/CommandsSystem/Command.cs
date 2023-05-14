using System;

namespace CommandsSystem
{
    public abstract class Command
    {
        public bool IsReady { get; protected set; }

        protected Command() { }

        public abstract void Stop();
        public abstract void Execute();
        public abstract void Undo();
    }
}
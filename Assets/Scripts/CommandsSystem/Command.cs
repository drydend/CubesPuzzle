using System;

namespace CommandsSystem
{
    public abstract class Command
    {
        public bool IsDone { get; protected set; }

        protected Command() { }

        public abstract void Execute();
        public abstract void Undo();
    }
}
using System.Collections.Generic;

namespace StateMachines
{
    public abstract class BaseState
    {
        public abstract void Enter();
        public abstract void Exit();
    }
}

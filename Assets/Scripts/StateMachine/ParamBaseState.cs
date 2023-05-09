using System.Collections.Generic;

namespace StateMachines
{
    public abstract class ParamBaseState<ParamT> : BaseState
    {
        public abstract void SetArgs(ParamT args);
    }
}

using System;
using System.Collections.Generic;

namespace StateMachines
{
    public class StateMachine
    {
        private Dictionary<Type, BaseState> _states;
        
        private BaseState _currentState;

        public StateMachine(Dictionary<Type, BaseState> states)
        {
            _states = states;
        }

        public void SwitchState<StateType>() where StateType : BaseState
        {   
            if(!_states.ContainsKey(typeof(StateType))) 
            {
                return;
            }

            _currentState?.Exit();
            _currentState = _states[typeof(StateType)];
            _currentState.Enter();
        }

        public void SwithcStateWithParam<StateType, ArgsType>(ArgsType args) where StateType : ParamBaseState<ArgsType>
        {
            if (!_states.ContainsKey(typeof(StateType)))
            {
                return;
            }

            var newState = (ParamBaseState<ArgsType>) _states[typeof(StateType)];

            _currentState?.Exit();

            _currentState = newState;
            newState.SetArgs(args);
            newState.Enter();
        }

        public void ExitAllStates()
        {
            _currentState?.Exit();
        }
    }
}

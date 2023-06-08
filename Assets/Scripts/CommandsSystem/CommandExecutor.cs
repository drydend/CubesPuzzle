using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace CommandsSystem
{
    public class CommandExecutor : ICommandExecutor
    {
        private Stack<Command> _commands = new Stack<Command>();

        private ICoroutinePlayer _coroutinePlayer;
        private bool _isInProgress;

        private Coroutine _currentCommand;

        public CommandExecutor(ICoroutinePlayer coroutinePlayer) 
        {
            _coroutinePlayer = coroutinePlayer;
            _isInProgress = false;
        }

        public void StopCommand(Command command)
        {
            if( _commands.Count < 0 ) 
            {
                return;
            }

            if(_commands.Peek() == command && !command.IsReady) 
            {
                command.Stop();
            }
        }

        public bool TryExecuteCommand(Command command, Action callback = null)
        {
            if (_isInProgress)
            {
                return false;
            }
            
            _commands.Push(command);
            _currentCommand = _coroutinePlayer.StartRoutine(ExecuteCommandRoutine(command, callback));

            return true;
        }

        public bool TryUndoLastCommand(Action callback = null)
        {
            if (_isInProgress)
            {
                return false;
            }

            _currentCommand = _coroutinePlayer.StartRoutine(UndoLastCommandRoutine(_commands.Pop()));

            return true;
        }

        public void ResetCommandExecutor()
        {
            _isInProgress = false;

            if (_commands.Count > 0)
            {
                StopCommand(_commands.Peek());
            }

            _commands.Clear();
        }

        private IEnumerator UndoLastCommandRoutine(Command command, Action callback = null)
        {
            _isInProgress = true;
            command.Undo();
            yield return new WaitUntil(() => command.IsReady);
            callback?.Invoke();
            OnCommandUndo();
        }

        private IEnumerator ExecuteCommandRoutine(Command command, Action callback = null)
        {
            _isInProgress = true;
            command.Execute();
            yield return new WaitUntil(() => command.IsReady);
            callback?.Invoke();
            OnCommandExecuted();
        }

        private void OnCommandUndo()
        {
            _isInProgress = false;
        }

        private void OnCommandExecuted() 
        {
            _isInProgress = false;
        }
    }
}

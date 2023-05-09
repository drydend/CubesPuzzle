using CommandsSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.TextCore.Text;
using UnityEngine;
using Utils;

namespace Assets.Scripts.Walls
{
    public class CommandExecutor : ICommandExecutor
    {
        private Stack<Command> _commands = new Stack<Command>();

        private ICoroutinePlayer _coroutinePlayer;
        private bool _isInProgress;

        public CommandExecutor(ICoroutinePlayer coroutinePlayer) 
        {
            _coroutinePlayer = coroutinePlayer;
            _isInProgress = false;
        }

        public bool TryExecuteCommand(Command command, Action callback = null)
        {
            if (_isInProgress)
            {
                return false;
            }
            
            _commands.Push(command);
            _coroutinePlayer.StartRoutine(ExecuteCommandRoutine(command));

            return true;
        }

        public bool TryUndoLastCommand(Action callback = null)
        {
            if (_isInProgress)
            {
                return false;
            }

            _coroutinePlayer.StartRoutine(UndoLastCommandRoutine(_commands.Pop()));

            return true;
        }

        public void ResetStack()
        {
            _commands.Clear();
        }

        private IEnumerator UndoLastCommandRoutine(Command command, Action callback = null)
        {
            _isInProgress = true;
            command.Undo();
            yield return new WaitUntil(() => command.IsDone);
            callback?.Invoke();
            OnCommandUndo();
        }

        private IEnumerator ExecuteCommandRoutine(Command command, Action callback = null)
        {
            _isInProgress = true;
            command.Execute();
            yield return new WaitUntil(() => command.IsDone);
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

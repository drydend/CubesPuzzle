using Assets.Scripts.Walls;
using CommandsSystem;
using Input;
using UnityEngine;
using Utils;

namespace LevelSystem
{
    public class LevelFactory
    {   
        private PlayerInput _playerInput;
        private ICoroutinePlayer _coroutinePlayer;

        public LevelFactory(PlayerInput playerInput , ICoroutinePlayer coroutinePlayer)
        {
            _playerInput = playerInput;
            _coroutinePlayer = coroutinePlayer;
        }

        public Level CreateLevel(LevelConfig levelConfig)
        {
            var levelPreset = Object.Instantiate(levelConfig.Preset);
            ICommandExecutor commandExecutor = new CommandExecutor(_coroutinePlayer);

            var level = new Level(_playerInput, levelPreset, commandExecutor);
            level.InitializeStateMachine();
            return level;
        }
    }
}

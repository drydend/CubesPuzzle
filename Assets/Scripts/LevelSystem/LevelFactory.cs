using CommandsSystem;
using GameUI;
using Input;
using UnityEngine;
using Utils;

namespace LevelSystem
{
    public class LevelFactory
    {
        private PlayerInput _playerInput;
        private ICoroutinePlayer _coroutinePlayer;
        private UIMenusHolder _UIMenusHolder;
        private LevelUI _levelUI;

        public Level LastCreateLevel { get; private set; }

        public LevelFactory(PlayerInput playerInput, ICoroutinePlayer coroutinePlayer
            , UIMenusHolder UIMenusHolder, LevelUI levelUI)
        {
            _playerInput = playerInput;
            _coroutinePlayer = coroutinePlayer;
            _UIMenusHolder = UIMenusHolder;
            _levelUI = levelUI;
        }

        public Level CreateLevel(LevelConfig levelConfig)
        {
            var levelPreset = Object.Instantiate(levelConfig.Preset);
            ICommandExecutor commandExecutor = new CommandExecutor(_coroutinePlayer);
            ILevelCompleteChecker levelWinChecker = new LevelCompleteChecker();
            
            levelWinChecker.SetLevel(levelPreset);

            var level = new Level(_playerInput, levelPreset, commandExecutor, levelWinChecker, _UIMenusHolder, _levelUI);
            level.InitializeStateMachine();

            LastCreateLevel = level;
            return level;
        }
    }
}

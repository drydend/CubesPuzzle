using CommandsSystem;
using GameUI;
using Input;
using Tutorial;
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
        private TutorialVisualizer _tutorialVisualizer;
        private TutorialPath _tutorialPath;
        private readonly TutorialCompleteTrigger _tutorialCompleteTrigger;

        public Level LastCreateLevel { get; private set; }

        public LevelFactory(PlayerInput playerInput, ICoroutinePlayer coroutinePlayer
            , UIMenusHolder UIMenusHolder, LevelUI levelUI, TutorialVisualizer tutorialVisualizer, 
            TutorialPath tutorialPath, TutorialCompleteTrigger tutorialCompleteTrigger)
        {
            _playerInput = playerInput;
            _coroutinePlayer = coroutinePlayer;
            _UIMenusHolder = UIMenusHolder;
            _levelUI = levelUI;
            _tutorialVisualizer = tutorialVisualizer;
            _tutorialPath = tutorialPath;
            _tutorialCompleteTrigger = tutorialCompleteTrigger;
        }

        public Level CreateLevel(LevelConfig levelConfig)
        {
            var levelPreset = CreatePreset(levelConfig);

            ICommandExecutor commandExecutor = new CommandExecutor(_coroutinePlayer);
            ILevelCompleteChecker levelWinChecker = new LevelCompleteChecker();
            
            levelWinChecker.SetLevel(levelPreset);

            var level = new Level(_playerInput, levelPreset, commandExecutor, levelWinChecker, _UIMenusHolder, _levelUI);
            level.InitializeStateMachine();

            LastCreateLevel = level;
            return level;
        }

        public Level CreateFinalLevel(LevelConfig levelConfig)
        {
            LevelPreset levelPreset = CreatePreset(levelConfig);

            ICommandExecutor commandExecutor = new CommandExecutor(_coroutinePlayer);
            ILevelCompleteChecker levelWinChecker = new LevelCompleteChecker();

            levelWinChecker.SetLevel(levelPreset);

            var level = new Level(_playerInput, levelPreset, commandExecutor, levelWinChecker, _UIMenusHolder, _levelUI);
            level.InitializeStateMachine();

            LastCreateLevel = level;
            return level;
        }

        public TutorialLevel CreateTutorialLevel(LevelConfig levelConfig)
        {
            var levelPreset = CreatePreset(levelConfig);

            ICommandExecutor commandExecutor = new CommandExecutor(_coroutinePlayer);
            ILevelCompleteChecker levelWinChecker = new LevelCompleteChecker();

            levelWinChecker.SetLevel(levelPreset);

            _tutorialVisualizer.Initialize(levelPreset);
            var level = new TutorialLevel(_playerInput, levelPreset, commandExecutor, levelWinChecker, 
                _UIMenusHolder, _levelUI, _tutorialPath, _tutorialVisualizer, _tutorialCompleteTrigger);
            level.InitializeStateMachine();

            LastCreateLevel = level;
            return level;
        }

        private static LevelPreset CreatePreset(LevelConfig levelConfig)
        {
            var preset = Object.Instantiate(levelConfig.Preset);
            preset.GenerateWater(Mathf.CeilToInt(levelConfig.InitialCameraSize) * 2);

            return preset;
        }

    }
}

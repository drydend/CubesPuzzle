using GameUI;
using StateMachines;

namespace LevelSystem
{
    public class LevelCompleteState : BaseState
    {
        private UIMenu _completeScreen;
        private UIMenusHolder _menuHolder;
        private Level _level;

        public LevelCompleteState(UIMenu completeScreen, UIMenusHolder UIMenusHolder, Level level) 
        {
            _completeScreen = completeScreen;
            _menuHolder = UIMenusHolder;
            _level = level;
        }

        public override void Enter()
        {
            _menuHolder.OpenMenu(_completeScreen);
            _level.OnCompleated();
        }

        public override void Exit()
        {
            _menuHolder.CloseCurrentMenu();
        }
    }
}
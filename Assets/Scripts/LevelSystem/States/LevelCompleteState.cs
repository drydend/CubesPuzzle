using GameUI;
using StateMachines;

namespace LevelSystem
{
    public class LevelCompleteState : BaseState
    {
        private UIMenu _completeScreen;
        private UIMenusHolder _menuHolder;

        public LevelCompleteState(UIMenu completeScreen, UIMenusHolder UIMenusHolder) 
        {
            _completeScreen = completeScreen;
            _menuHolder = UIMenusHolder;
        }

        public override void Enter()
        {
            _menuHolder.OpenMenu(_completeScreen);
        }

        public override void Exit()
        {
            _menuHolder.CloseCurrentMenu();
        }
    }
}
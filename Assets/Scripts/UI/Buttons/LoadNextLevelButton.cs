using UnityEngine.UI;
using Zenject;

namespace GameUI.Buttons
{
    public class LoadNextLevelButton : InteractableUIButton
    {
        private Game _game;

        [Inject]
        public void Construct(Game game)
        {
            _game = game;
            onClick.AddListener(LoadNextLevel);
        }

        private void LoadNextLevel()
        {
            _game.LoadNextLevel();
        }
    }
}

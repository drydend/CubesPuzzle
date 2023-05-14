using GameUI.Buttons;
using Zenject;

namespace GameUI.Buttons
{
    public class RestartButton : InteractableUIButton
    {
        private Game _game;

        [Inject]
        public void Construct(Game game)
        {
            _game = game;
            onClick.AddListener(RestartLevel);
        }

        public void RestartLevel()
        {
            _game.RestartLevel();
        }
    }
}

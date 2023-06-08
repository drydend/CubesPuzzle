using UnityEngine;
using Zenject;

public class GameLauncher : MonoBehaviour
{
    private Game _game;

    [Inject]
    public void Construct(Game game)
    {
        _game = game;
        _game.Initialize();
    }

    public void Start()
    {
        _game.LoadLastUnlockedLevel();
    }
}

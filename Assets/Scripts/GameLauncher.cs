using UnityEngine;
using Zenject;

public class GameLauncher : MonoBehaviour
{
    private Game _game;

    [Inject]
    public void Construct(Game game)
    {
        _game = game;
    }

    public void Start()
    {
        _game.StartLevel(1);
    }
}

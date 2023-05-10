using SceneLoading;
using UnityEngine;
using Zenject;

public class EntryPoint : MonoBehaviour
{
    private SceneLoader _sceneLoader;

    [Inject]
    public void Construct(SceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }

    private void Start()
    {
        _sceneLoader.LoadGameScene();
    }
}

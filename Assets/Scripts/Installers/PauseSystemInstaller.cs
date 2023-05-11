using PauseSystem;
using UnityEngine;
using Zenject;

public class PauseSystemInstaller : MonoInstaller
{
    [SerializeField]
    private PauseTriggers _pauseTriggers;
    [SerializeField]
    private UnpauseTriggers _unpauseTriggers;

    private LevelPauser _levelPauser;

    public override void InstallBindings()
    {
        BindLevelPauser(); 
        BindUnpauseButton();
        BindUnpauseTrigger();
    }

    private void BindLevelPauser()
    {
        Container
            .Bind<LevelPauser>()
            .AsSingle();
    }

    private void BindUnpauseButton()
    {
        Container
            .Bind<IPauseTrigger>()
            .To<PauseTriggers>()
            .FromInstance(_pauseTriggers)
            .AsSingle();
    }

    private void BindUnpauseTrigger()
    {
        Container
            .Bind<IUnpauseTrigger>()
            .To<UnpauseTriggers>()
            .FromInstance(_unpauseTriggers)
            .AsSingle();
    }
}

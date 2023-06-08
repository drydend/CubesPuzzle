using LevelSystem;
using SavingSystem;
using SceneLoading;
using UnityEngine;
using Utils;
using Zenject;

namespace Installers
{
    public class ProjectBootstrap : MonoInstaller
    {
        [SerializeField]
        private LevelsConfigs _levelConfigs;
        [SerializeField]
        private CoroutinePlayer _coroutinePlayerPrefab;

        public override void InstallBindings()
        {
            Application.targetFrameRate = Screen.currentResolution.refreshRate + 10;

            InstallCoroutinePlayer();
            InstallLevelConfigs();
            InstallSceneLoader();
            InstallSaveSystem();
        }

        private void InstallSaveSystem()
        {
            Container
                .Bind<ILevelSaveDataLoader>()
                .To<JsonSaveDataLoader>()
                .AsSingle();
        }

        private void InstallSceneLoader()
        {
            Container
                .Bind<SceneLoader>()
                .AsSingle();
        }

        private void InstallLevelConfigs()
        {
            Container.Bind<LevelsConfigs>()
                .FromInstance(_levelConfigs)
                .AsSingle();
        }

        private void InstallCoroutinePlayer()
        {
            var coroutinePlayerInctance = Instantiate(_coroutinePlayerPrefab);
            DontDestroyOnLoad(coroutinePlayerInctance);

            Container
                .Bind<ICoroutinePlayer>()
                .To<CoroutinePlayer>()
                .FromInstance(coroutinePlayerInctance)
                .AsSingle();
        }
    }
}
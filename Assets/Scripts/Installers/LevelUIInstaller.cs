using GameUI;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class LevelUIInstaller : MonoInstaller
    {
        [SerializeField]
        private LevelUI _levelUI;

        public override void InstallBindings()
        {
            InstallUIMenusHolder();
            InstallLevelUI();
        }

        private void InstallUIMenusHolder()
        {
            Container
                .Bind<UIMenusHolder>()
                .AsSingle();
        }

        private void InstallLevelUI()
        {
            Container
                .Bind<LevelUI>()
                .FromInstance(_levelUI)
                .AsSingle();
        }
    }
}

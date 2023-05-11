using Input;
using LevelSystem;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class PlayerInputInstaller : MonoInstaller
    {
        [SerializeField]   
        private PlayerSwipeConfig _swipeConfig;
        [SerializeField]
        private Camera _camera;

        private PlayerInput _input;

        public override void InstallBindings()
        {
            InstallPlayerInput();
            InstallLevelStartTrigger();
        }

        private void InstallLevelStartTrigger()
        {
            Container
                .Bind<ILevelStartTrigger>()
                .To<PlayerInput>()
                .FromInstance(_input)
                .AsSingle();
        }

        private void InstallPlayerInput()
        {
            _input = new PlayerInput(_camera, _swipeConfig);
            _input.Initialize();

            Container
                .Bind<PlayerInput>()
                .FromInstance(_input)
                .AsSingle();
        }
    }
}

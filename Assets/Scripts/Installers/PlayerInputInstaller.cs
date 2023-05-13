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
        private GameInputField _gameInputField;
        [SerializeField]
        private Camera _camera;

        private PlayerInput _input;

        public override void InstallBindings()
        {
            InstallPlayerInput();
        }

        private void InstallPlayerInput()
        {
            _input = new PlayerInput(_camera, _swipeConfig, _gameInputField);
            _input.Initialize();

            Container
                .Bind<PlayerInput>()
                .FromInstance(_input)
                .AsSingle();
        }
    }
}

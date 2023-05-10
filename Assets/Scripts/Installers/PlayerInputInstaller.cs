using Input;
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
            _input = new PlayerInput(_camera, _swipeConfig);
            _input.Initialize();

            Container
                .Bind<PlayerInput>()
                .FromInstance(_input)
                .AsSingle();
        }
    }
}

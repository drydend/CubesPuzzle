using UnityEngine;
using Zenject;

namespace Installers
{

    public class CameraMoverInstaller : MonoInstaller
    {
        [SerializeField]
        private CameraSizeFilter _cameraMover;

        public override void InstallBindings()
        {
            Container
                .Bind<CameraSizeFilter>()
                .FromInstance(_cameraMover)
                .AsSingle();
        }
    }
}

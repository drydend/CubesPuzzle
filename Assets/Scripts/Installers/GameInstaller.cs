﻿using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<Game>()
                .AsSingle();
        }
    }
}

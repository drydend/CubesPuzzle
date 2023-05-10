using LevelSystem;
using Zenject;

namespace Installers
{
    public class LevelFactoryIntaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<LevelFactory>()
                .AsSingle();
        }
    }
}

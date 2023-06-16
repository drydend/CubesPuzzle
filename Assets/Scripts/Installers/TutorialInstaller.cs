using Tutorial;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class TutorialInstaller : MonoInstaller
    {
        [SerializeField]
        private TutorialPath _tutorialPath;
        [SerializeField]
        private TutorialVisualizer _tutorialVisualizer;

        public override void InstallBindings()
        {
            Container
                .Bind<TutorialPath>()
                .FromInstance(_tutorialPath)
                .AsSingle();

            Container
                .Bind<TutorialVisualizer>()
                .FromInstance(_tutorialVisualizer)
                .AsSingle();

            Container
                .Bind<TutorialCompleteTrigger>()
                .AsSingle();
        }
    }
}

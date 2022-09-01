using UnityEngine;
using Zenject;

namespace FPS
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private LevelsProvider _levelsProvider;
        public override void InstallBindings()
        {
            BindLevelsProvider(); 
            BindSceneProvider();
        }
        private void BindSceneProvider()
        {
            Container.Bind<SceneProvider>().
              FromNew().
              AsSingle().
              NonLazy();
        }
        private void BindLevelsProvider()
        {
            Container.Bind<LevelsProvider>().
              FromInstance(_levelsProvider).
              AsSingle().
              NonLazy();
        }
    }
}

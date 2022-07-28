using UnityEngine;
using Zenject;

namespace FPS
{
    public class FactoriesInstaller : MonoInstaller, IInitializable
    {
        [SerializeField] private DefaulZombieFactory _defaulZombieFactory;

        public void Initialize() => Container.Resolve<DefaulZombieFactory>();

        public override void InstallBindings()
        {
            BindInstallerInterfaces();
            BindEnemyFactory();
        }

        private void BindInstallerInterfaces()
        {
            Container.BindInterfacesTo<FactoriesInstaller>().
                FromInstance(this).
                AsSingle();
        }
        private void BindEnemyFactory()
        {
            Container.Bind<DefaulZombieFactory>().
                To<DefaulZombieFactory>().FromInstance(_defaulZombieFactory).
                AsSingle();
        }
    }
}

using Zenject;

namespace FPS
{
    public class FactoriesInstaller : MonoInstaller, IInitializable
    {      
        public void Initialize() => Container.Resolve<DefaultZombieFactory>();

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
            Container.Bind<DefaultZombieFactory>().
                FromNew().AsSingle();
        }
    }
}

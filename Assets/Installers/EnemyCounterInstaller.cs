using Zenject;

namespace FPS
{
    public class EnemyCounterInstaller : MonoInstaller
    {       
        public override void InstallBindings()
        {
            BindCounter();
        }
        private void BindCounter()
        {
            Container.Bind<KilledEnemiesDisplayer>().
                FromNew().
                AsSingle().
                NonLazy();
        }
    }
}
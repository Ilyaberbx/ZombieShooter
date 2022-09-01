using Zenject;

namespace FPS
{
    public class DefaultZombieFactory : EnemyFactory<DefaultZombie>
    {
        public DefaultZombieFactory(DiContainer diContainer) : base(diContainer)
        {
        }
    }
}

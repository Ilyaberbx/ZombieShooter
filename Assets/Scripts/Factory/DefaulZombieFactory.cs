using Zenject;

namespace FPS
{
    public class DefaulZombieFactory : EnemyFactory<DefaultZombie>
    {
        public DefaulZombieFactory(DiContainer diContainer) : base(diContainer)
        {
        }
    }
}

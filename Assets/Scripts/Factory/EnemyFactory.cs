using Zenject;

namespace FPS
{
    public abstract class EnemyFactory<T> : ObjectFactory<T> where T : BaseEnemy
    {
        public EnemyFactory(DiContainer diContainer) : base(diContainer)
        {
        }
    }
}

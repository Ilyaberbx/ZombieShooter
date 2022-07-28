namespace FPS
{
    public interface IEnemy : IUnit
    {
        int TimeBeforeChase { get; }

        void Attack();   
        
        IWeapon Weapon { get; }

        NavMeshEnemyChaser TargetChaser { get; }

        EnemyAnimator Animator { get; }

    }
}
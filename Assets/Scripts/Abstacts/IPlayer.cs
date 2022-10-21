namespace FPS
{
    public interface IPlayer : IUnit
    {
        void Attack();
        WeaponLauncher WeaponLauncher { get; }
    }
}
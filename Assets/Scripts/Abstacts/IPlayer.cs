namespace FPS
{
    public interface IPlayer : IUnit
    {
        void Attack();
        PlayerWeaponLauncher WeaponLauncher { get; }
    }
}
namespace FPS
{
    public interface IWeaponVisitor
    {
        void Visit(Pistol weapon);
        void Visit(Rifle weapon);
    }
}

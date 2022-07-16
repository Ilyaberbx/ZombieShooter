using Zenject;

namespace FPS
{
    public class InputInstaller : MonoInstaller
    {
        public override void InstallBindings() => BindInputs();
        private void BindInputs()
        {
            DefaultMovementInput _movementInput = new DefaultMovementInput();
            Container.Bind<DefaultMovementInput>().
                FromInstance(_movementInput).
                AsSingle().
                NonLazy();
            WeaponInput _weaponInput = new WeaponInput();
            Container.Bind<WeaponInput>().
                FromInstance(_weaponInput).
                AsSingle().
                NonLazy();
        }
    }
}

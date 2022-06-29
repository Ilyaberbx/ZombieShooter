using Zenject;

namespace FPS
{
    public class InputInstaller : MonoInstaller
    {
        public override void InstallBindings() => BindMovementInput();
        private void BindMovementInput()
        {
            DefaultMovementInput _movementInput = new DefaultMovementInput();
            Container.Bind<DefaultMovementInput>().
                FromInstance(_movementInput).
                AsSingle().
                NonLazy();
        }
    }
}

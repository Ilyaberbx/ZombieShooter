using Zenject;

namespace FPS
{
    public class InputInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindMovementInput();
        }
        private void BindMovementInput()
        {
            DefaulMovementInput _movementInput = new DefaulMovementInput();
            Container.Bind<DefaulMovementInput>().
                FromInstance(_movementInput).
                AsSingle().
                NonLazy();
        }
    }
}

using Zenject;

namespace FPS
{
    public class InputInstaller : MonoInstaller
    {
        public override void InstallBindings() => BindMovementInput();
        private void BindMovementInput()
        {
            BasicFPSInput _movementInput = new BasicFPSInput();
            Container.Bind<BasicFPSInput>().
                FromInstance(_movementInput).
                AsSingle().
                NonLazy();
        }
    }
}

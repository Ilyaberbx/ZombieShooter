using Zenject;
using UnityEngine;

namespace FPS
{
    public class InputInstaller : MonoInstaller
    {
        public override void InstallBindings() => BindInputs();
        private void BindInputs()
        {
            Container.Bind<DefaultMovementInput>().
                FromNew().
                AsSingle().
                NonLazy();
            Container.Bind<WeaponInput>().
                FromNew().
                AsSingle().
                NonLazy();
        }
    }
}

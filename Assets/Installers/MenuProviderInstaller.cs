using UnityEngine;
using Zenject;

namespace FPS
{
    public class MenuProviderInstaller : MonoInstaller
    {
        [SerializeField] private MenuProvider _menuProvider;
        public override void InstallBindings()
        {
            BindMenuProvider();
        }
        private void BindMenuProvider()
        {
            Container.Bind<MenuProvider>().
                FromInstance(_menuProvider).
                AsTransient().
                NonLazy();
        }
    }
}

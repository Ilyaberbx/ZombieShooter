using UnityEngine;
using Zenject;

namespace FPS
{
    public class GameExodusControllerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindController();
        }
        private void BindController()
        {
            Container.Bind<GameExodusController>().
                FromNew().
                AsSingle().
                NonLazy();
        }
    }
}
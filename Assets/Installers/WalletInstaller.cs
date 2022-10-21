using UnityEngine;
using Zenject;

namespace FPS
{
    public class WalletInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindWallet();
        }
        private void BindWallet()
        {
            Container.Bind<Wallet>().
                FromNew().
                AsSingle().
                NonLazy();
        }
    }
}
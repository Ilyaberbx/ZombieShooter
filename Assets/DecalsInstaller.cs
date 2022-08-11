using Zenject;
using UnityEngine;

namespace FPS
{
    public class DecalsInstaller : MonoInstaller
    {
        [SerializeField] private DecalPoolsProvider _decalPools;
        [SerializeField] private DecalPreset _decalPreset;

        public override void InstallBindings()
        {
            BindPools();
            BindPreset();
        }
        private void BindPools()
        {
            Container.Bind<DecalPoolsProvider>().
                FromInstance(_decalPools).
                AsSingle();
        }
        private void BindPreset()
        {
            Container.Bind<DecalPreset>().
                FromInstance(_decalPreset).
                AsSingle();
        }
    }
}

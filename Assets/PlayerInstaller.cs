using UnityEngine;
using Zenject;


namespace FPS

{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Player _player;

        public override void InstallBindings()
        {
            BindPlayer();
        }

        private void BindPlayer()
        {
            Container.Bind<Player>().
                FromInstance(_player).
                AsSingle();
        }
    }
}

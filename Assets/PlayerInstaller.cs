using UnityEngine;
using Zenject;


namespace FPS

{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Player _player;
        [SerializeField] private UnitSpawnPoint _at;
        public override void InstallBindings()
        {
            BindPlayer();
        }

        private void BindPlayer()
        {
            Player player = Container.InstantiatePrefabForComponent<Player>(_player, _at.transform.position,Quaternion.identity,null);

            Container.Bind<Player>().
                FromInstance(player)
                .AsSingle()
                .NonLazy();
        }
    }
}

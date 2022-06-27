using UnityEngine;
using Zenject;

namespace FPS
{
    public class GameStateControllerInstaller : MonoInstaller
    {
        [SerializeField] private GameStateController _gameStateController;

        public override void InstallBindings()
        {
            BindGameStateController();
        }
        private void BindGameStateController()
        {
            Container.Bind<GameStateController>().
                FromInstance(_gameStateController).
                AsSingle().
                NonLazy();
        }

    }
}

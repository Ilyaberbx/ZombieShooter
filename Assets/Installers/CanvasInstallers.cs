using Zenject;
using UnityEngine;

namespace FPS
{
    public class CanvasInstallers : MonoInstaller
    {
        [SerializeField] private GamePlayCanvas _gamePlayCanvas;

        public override void InstallBindings()
        {
            BindGamePlayCanvas();
        }
        private void BindGamePlayCanvas()
        {
            Container.Bind<GamePlayCanvas>().
                FromInstance(_gamePlayCanvas).
                AsSingle().
                NonLazy();
        }
    }
}

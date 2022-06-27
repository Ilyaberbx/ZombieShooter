using UnityEngine;
using Zenject;

namespace FPS
{
    public abstract class GamePlayObjectMono : MonoBehaviour
    {
        [Inject] protected GameStateController GameStateController { get; private set; }
        protected void OnGameStateChanged(GameState newState) => enabled = newState == GameState.GamePlay;
    }
}

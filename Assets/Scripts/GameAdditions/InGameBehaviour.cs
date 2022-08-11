using UnityEngine;
using Zenject;

namespace FPS
{
    public abstract class InGameBehaviour : MonoBehaviour
    {
        [Inject] protected GameStateController GameStateController { get; private set; }
        protected virtual void OnGameStateChanged(GameState newState) => enabled = newState == GameState.GamePlay;
    }
}

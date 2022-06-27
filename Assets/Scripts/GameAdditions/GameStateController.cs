using UnityEngine;

namespace FPS
{
    public class GameStateController : MonoBehaviour
    {
        public GameState CurrentState { get; private set; }

        public delegate void GameStateChangedfHandler(GameState newState);
        public event GameStateChangedfHandler OnGameStateChanged;
        public void SetState(GameState newState)
        {
            if (CurrentState == newState) return;

            CurrentState = newState;

            OnGameStateChanged?.Invoke(newState);
        }

    }
}

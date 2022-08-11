using UnityEngine;

namespace FPS
{
    public class KilledEnemiesDisplayer 
    {
        private GamePlayCanvas _gamePlayCanvas;
        public KilledEnemiesDisplayer(GamePlayCanvas canvas)
        {
            _gamePlayCanvas = canvas;
        }
        private int _count;
        public void Add()
        {
            _count++;
            _gamePlayCanvas.DisplayKilledEnemies(_count);
        }
    }
}

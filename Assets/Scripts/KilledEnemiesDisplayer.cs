namespace FPS
{
    public class KilledEnemiesDisplayer 
    {
        private GamePlayCanvas _gamePlayCanvas;

        private int _count;

        public KilledEnemiesDisplayer(GamePlayCanvas canvas)
        {
            _gamePlayCanvas = canvas;
        }      
        public void Add()
        {
            _count++;
            _gamePlayCanvas.DisplayKilledEnemies(_count);
        }
    }
}

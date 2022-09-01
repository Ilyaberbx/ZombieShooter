using UnityEngine;
using TMPro;
using Zenject;

namespace FPS
{
    public class GamePlayCanvas : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _killedEnemyText;
        [SerializeField] private TextMeshProUGUI _ammoCount;
        [SerializeField] private LevelProgress _levelProgress;

        private void Awake() => DisplayKilledEnemies(0);

        public void DisplayAmmoCount(int count) => _ammoCount.text = "Ammo: " + count.ToString();
        public void DisplayKilledEnemies(int count) => _killedEnemyText.text = count.ToString() + "/" + _levelProgress.NeedEnemyCount;

    }
}

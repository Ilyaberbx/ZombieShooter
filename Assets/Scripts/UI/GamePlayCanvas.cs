using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace FPS
{
    public class GamePlayCanvas : InGameBehaviour
    {
        [SerializeField] private TextMeshProUGUI _killedEnemyText;
        [SerializeField] private TextMeshProUGUI _ammoCount;

        public void DisplayAmmoCount(int count) => _ammoCount.text = "Ammo: " + count.ToString();
        public void DisplayKilledEnemies(int count) => _killedEnemyText.text = count.ToString();
    }
}

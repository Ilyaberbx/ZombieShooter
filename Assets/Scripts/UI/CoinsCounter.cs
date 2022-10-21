using UnityEngine;
using Zenject;
using TMPro;

namespace FPS
{

    public class CoinsCounter : MonoBehaviour
    {

        [Inject] private Wallet _wallet;
        private TextMeshProUGUI _coinsText;
        private void Awake() => _coinsText = GetComponent<TextMeshProUGUI>();
        private void OnEnable()
        {
            SetCoinsCount(_wallet.Coins);
            _wallet.OnCoinsCountChange += SetCoinsCount;
        }
        private void OnDisable() => _wallet.OnCoinsCountChange += SetCoinsCount;
        private void SetCoinsCount(int coins) => _coinsText.text =  "Coins: " + coins.ToString();
    }
}

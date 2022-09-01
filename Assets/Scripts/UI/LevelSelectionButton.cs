using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace FPS
{
    public class LevelSelectionButton : MonoBehaviour, IButton
    {
        [Inject] private SceneProvider _sceneProvider;
        [SerializeField] protected Button _button;
        [SerializeField] protected int _levelId;

        public Button Button => _button;

        private void Awake() => _button.onClick.AddListener(Execute);
        public void Execute() => _sceneProvider.OpenLevel(_levelId);
    }
}

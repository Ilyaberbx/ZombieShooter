using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FPS
{
    public class SettingsEnable : MonoBehaviour
    {
        [SerializeField] private Settings _settings;
        [SerializeField] private Slider _sensetivitySlider;
        [SerializeField] private Slider _soundSlider;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Toggle _fullscreenToogle;
        [SerializeField] private TMP_Dropdown _QualityDropDown; 

        private void OnEnable()
        {
            _sensetivitySlider.value = _settings.ViewYSensetivity;
            _musicSlider.value = _settings.MusicVolume;
            _soundSlider.value = _settings.SoundVolume;
            _fullscreenToogle.isOn = Screen.fullScreen;
            _QualityDropDown.value = QualitySettings.GetQualityLevel();
        }
    }
}

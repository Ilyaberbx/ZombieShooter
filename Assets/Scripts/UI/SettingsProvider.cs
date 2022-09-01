using UnityEngine;

namespace FPS
{
    public class SettingsProvider : MonoBehaviour
    {
        [SerializeField] private Settings _settings;

        public void SetMusicVolume(float value) => _settings.MusicVolume = value;

        public void SetSoundVolume(float value) => _settings.SoundVolume = value;

        public void SetSensetivity(float value)
        {
            _settings.ViewXSensetivity = value;
            _settings.ViewYSensetivity = value;
        }
        public void SetQuality(int qualityIndex) => QualitySettings.SetQualityLevel(qualityIndex);
        public void SetFullscreen(bool isFullscreen) => Screen.fullScreen = isFullscreen;

        public void SetResolution(int resolutionIndex) => Screen.SetResolution(Screen.resolutions[resolutionIndex].width, 
            Screen.resolutions[resolutionIndex].
            height,Screen.fullScreen);
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace FPS
{
    public class ResolutionDropDown : MonoBehaviour
    {
        [SerializeField] private Settings _settings;
        private TMP_Dropdown _dropDown;

        private void Start()
        {
            _dropDown = GetComponent<TMP_Dropdown>();
            InitializeResolutions();
        }

        private void InitializeResolutions()
        {
            _dropDown.ClearOptions();

            List<string> options = new List<string>();

            for (int i = 0; i < Screen.resolutions.Length; i++)
            {
                string option = Screen.resolutions[i].width + " x " + Screen.resolutions[i].height;
                options.Add(option);

                if (Screen.resolutions[i].width == Screen.currentResolution.width &&
                    Screen.resolutions[i].height == Screen.currentResolution.height)
                    _settings.CurrentResolutionIndex = i;

            }

            _dropDown.AddOptions(options);
            _dropDown.value = _settings.CurrentResolutionIndex;
            _dropDown.RefreshShownValue();
        }
    }
}

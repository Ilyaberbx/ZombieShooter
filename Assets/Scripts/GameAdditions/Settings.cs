using UnityEngine;

namespace FPS
{
    [CreateAssetMenu]
    public class Settings : ScriptableObject
    {
        private readonly string Sound = "Sound";
        private readonly string Music = "Music";
        private readonly string Sensetivity = "Sensetivity";


        [Header("GravitySettings")]
        [Range(1, 5)] [SerializeField] private float _gravityCoefficient;

        private bool _viewInverted;

        private int _currentResolutionIndex;

        public int ViewInverted
        {
            get
            {
                if (_viewInverted)
                    return -1;

                return 1;
            }
        }
        public float ViewYSensetivity
        {
            get => PlayerPrefs.GetFloat(Sensetivity);

            set
            {
                if (value >= 0)
                    PlayerPrefs.SetFloat(Sensetivity,value);
            }
        }
        public float ViewXSensetivity
        {
            get => PlayerPrefs.GetFloat(Sensetivity);

            set
            {
                if (value >= 0)
                    PlayerPrefs.SetFloat(Sensetivity, value);
            }
        }
        public float GravityCoefficient => _gravityCoefficient;

        public float MusicVolume
        {
            get => PlayerPrefs.GetFloat(Music);

            set
            {
                if (value <= 1 && value >= 0)
                    PlayerPrefs.SetFloat(Music, value);
            }
        }
        public float SoundVolume
        {
            get => PlayerPrefs.GetFloat(Sound);

            set
            {
                if (value <= 1 && value >= 0)
                    PlayerPrefs.SetFloat(Sound, value);
            }
        }

        public int CurrentResolutionIndex
        {
            get => _currentResolutionIndex;
            set
            {
                if (value >= 0)
                    _currentResolutionIndex = value;
            }
        }
    }
}

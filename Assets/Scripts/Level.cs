using UnityEngine;



namespace FPS
{
    [System.Serializable]
    public class Level
    {
        [SerializeField] private int _id = 0;
        public int ID
        {
            get => _id;
            set
            {
                if (value > 0)
                    _id = value;
            }
        }
        public bool IsCompleted => 1 == PlayerPrefs.GetInt(ID.ToString(), 0);

    }
}

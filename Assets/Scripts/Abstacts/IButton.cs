using UnityEngine.UI;

namespace FPS
{
    public interface IButton 
    {
        Button Button { get; }
        void Execute();
    }
}

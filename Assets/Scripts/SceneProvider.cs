using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneProvider : MonoBehaviour
{
    private readonly string MainMenu = "MainMenu";
    public void Restart() => SceneManager.LoadSceneAsync(Application.loadedLevel);
    public void Menu() => SceneManager.LoadSceneAsync(MainMenu);
}

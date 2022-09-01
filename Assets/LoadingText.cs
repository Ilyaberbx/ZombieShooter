using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingText : MonoBehaviour
{
    [SerializeField] private float _animatingSpeed;
    [SerializeField] private TextMeshProUGUI _loadingText;

    IEnumerator Start()
    {
        while (SceneManager.GetActiveScene().isLoaded)
        {
            _loadingText.text = "Loading...";
            yield return new WaitForSeconds(_animatingSpeed);
            _loadingText.text = "Loading..";
            yield return new WaitForSeconds(_animatingSpeed);
            _loadingText.text = "Loading.";
            yield return new WaitForSeconds(_animatingSpeed);
        }
        Debug.Log("StopTyping");
    }
}

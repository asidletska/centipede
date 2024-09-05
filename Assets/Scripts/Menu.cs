using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void OnPlayHandler()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1.0f;
    }
}

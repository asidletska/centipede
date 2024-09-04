using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    public GameObject pausePanel;

    public void OnPauseButtonHandler()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }

    public void OnContinueHandler()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OnMenuHandler()
    {
        SceneManager.LoadScene(0);
    }
}

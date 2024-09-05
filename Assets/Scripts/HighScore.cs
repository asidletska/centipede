using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    public TextMeshProUGUI highScoreLabel;

    void Start()
    {
        highScoreLabel = GetComponent<TextMeshProUGUI>();
        highScoreLabel.text = "HighScore: " + PlayerPrefs.GetInt("highscore", ScoreManager.highScore).ToString();
    }

    void Update()
    {
        highScoreLabel.text = "HighScore: " + PlayerPrefs.GetInt("highscore", ScoreManager.highScore).ToString();
    }
}

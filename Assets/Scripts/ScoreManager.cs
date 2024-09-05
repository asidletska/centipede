using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public Text highScoreText;
    public static ScoreManager instance;
    private int score { get; set; }
    public static int highScore;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }
    private void Start()
    {
        scoreText.text = "" + score.ToString();
        scoreText.text = PlayerPrefs.GetString("0" + score.ToString());
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateScoreText();
        UpdateHighScoreText();
    }
    public void AddScore(int points)
    {
        score += points;
        scoreText.text = "" + score.ToString();
        UpdateScoreText();
        CheckHighScore();
    }

    public void UpdateScoreText()
    {
        scoreText.text = "" + score.ToString();
    }

    public void UpdateHighScoreText()
    {
        highScoreText.text = "High Score: " + highScore.ToString();

    }

    public void CheckHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }
}

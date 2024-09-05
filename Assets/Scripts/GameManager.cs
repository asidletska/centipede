using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }
    private Blaster blaster;
    private Centipede centipede;
    private MushroomField mushroomField;
    public UnityEvent _gameOver;

    private int lives;

    public TextMeshProUGUI livesText;
    public GameObject gameOver;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        blaster = FindObjectOfType<Blaster>();
        centipede = FindObjectOfType<Centipede>();
        mushroomField = FindObjectOfType<MushroomField>();

        NewGame();
    }
    private void Update()
    {
        if (lives <= 0 && Input.anyKeyDown)
        {
            SceneManager.LoadScene(1);
            NewGame();
        }
    }

    private void NewGame()
    {
        SetLives(3);

        centipede.Respawn();
        blaster.Respawn();
        mushroomField.Clear();
        mushroomField.Generate();
        gameOver.SetActive(false);

    }

    private void GameOver()
    {
        _gameOver.Invoke();
        blaster.gameObject.SetActive(false);
        gameOver.SetActive(true);

    }
    public void ResetRound()
    {
        SetLives(lives - 1);

        if (lives <= 0)
        {
            GameOver();
            return;
        }
        centipede.Respawn();
        blaster.Respawn();
        mushroomField.Heal();
    }
    public void NextLevel()
    {
        centipede.speed *= 1.1f;
        centipede.Respawn();

    }

    private void SetLives(int value)
    {
        lives = value;
        livesText.text = lives.ToString();
    }

}

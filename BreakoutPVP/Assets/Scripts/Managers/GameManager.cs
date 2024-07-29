using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public enum PowerUpType { Shield = 1, Double = 2 }

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }
    public BrickSpawner BrickHandler { get; set; }

    [Header("Score Variables")]
    [SerializeField] int P1points = 0, P2points = 0;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject p1ScoreImage;
    [SerializeField] GameObject p2ScoreImage;

    [Header("ToDelete")]
    [SerializeField] TextMeshProUGUI P1Score, P2Score;
    [SerializeField] GameObject player1WinText; //Hidden until testing is done
    [SerializeField] GameObject player2WinText; //Hidden until testing is done

    void Awake() {
        if (Instance == null) {
            Instance = this;
            BrickHandler = FindObjectOfType<BrickSpawner>();
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    void Start() {
        BrickHandler.SpawnBrick();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Backspace)) // basic restart code
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        CheckPoints();
        PrintScore();
        ActivePauseMenu();
    }

    void CheckPoints() {
        if (P1points >= 3) {
            Debug.Log("P1 WINS");
            BrickHandler.SpawnBrick();
            DoGameOver();
            // SoundManager.Instance.Play();
        }
        if (P2points >= 3) {
            Debug.Log("P2 WINS");
            BrickHandler.SpawnBrick();
            DoGameOver();
            // SoundManager.Instance.Play();
        }
    }

    public void P1Scored() {
        P1points++;
        p1ScoreImage.GetComponent<ScoreManager>().UpdateScoreText(P1points);
        Debug.Log("P1 Score: " + P1points);
        // SoundManager.Instance.Play();
    }
    public void P2Scored() {
        P2points++;
        p2ScoreImage.GetComponent<ScoreManager>().UpdateScoreText(P2points);
        Debug.Log("P2 Score: " + P2points);
        // SoundManager.Instance.Play();
    }
    public void PrintScore() {
        P1Score.text = P1points.ToString();
        P2Score.text = P2points.ToString();
    }

    public void AssignPowerUp(Brick brick) {
        int powerUpDie = Random.Range(1, 3); // 1 or 2
        PowerUpType powerUpType = (PowerUpType)powerUpDie;

        if (brick != null) {
            switch (powerUpType) {
                case PowerUpType.Shield: 
                    brick.ActivateShield();
                    // SoundManager.Instance.Play();
                    break;
                case PowerUpType.Double: 
                    brick.ActivateDouble();
                    // SoundManager.Instance.Play();
                    break;
                default: break;
            }
        }
    }

    public void ActivePauseMenu()
    {
        if(Input.GetKey(KeyCode.Escape)) 
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void QuitMatch()
    {
    #if UNITY_STANDALONE
        Application.Quit();
    #endif
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
    }

    public void Rematch()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void DoGameOver()
    {
        gameOverScreen.SetActive(true);

        if(P1points>= 3)
        {
            player1WinText.SetActive(true);
        }
        else if(P2points>= 3) 
        { 
            player2WinText.SetActive(true);
        }
    }
}


using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public enum PowerUpType { Shield = 1, Double = 2 }

public class GameManager : MonoBehaviour {

    public static GameManager Instance;
    public BrickSpawner BrickHandler { get; set; }

    [Header("Score/Round Variables")]
    [SerializeField] int P1points = 0;
    [SerializeField] int P2points = 0;
    [SerializeField] int currentRound = 0;
    [SerializeField] GameObject pauseMenu, gameOverScreen, p1ScoreImage, p2ScoreImage;
    [SerializeField] GameObject roundImage;

    [Header("ToDelete")]
    [SerializeField] TextMeshProUGUI P1Score;
    [SerializeField] TextMeshProUGUI P2Score;
    [SerializeField] GameObject player1WinText, player2WinText; // Hidden until testing is done

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
            SceneManager.LoadScene("Cafeteria Showdown");


        CheckPoints();
        //PrintScore();
        ActivePauseMenu();
    }

    void CheckPoints() {
        if (P1points >= 3) {
            Debug.Log("P1 WINS");
            BrickHandler.SpawnBrick();
            DoGameOver();
            SoundManager.Instance.Play("game win", 1);
        }
        if (P2points >= 3) {
            Debug.Log("P2 WINS");
            BrickHandler.SpawnBrick();
            DoGameOver();
            SoundManager.Instance.Play("game win", 1);
        }
    }

    public void P1Scored() {
        P1points++;
        p1ScoreImage.GetComponent<ScoreManager>().UpdateScoreText(P1points);
        NextRoundText();
        Debug.Log("P1 Score: " + P1points);
    }
    public void P2Scored() {
        P2points++;
        p2ScoreImage.GetComponent<ScoreManager>().UpdateScoreText(P2points);
        NextRoundText();
        Debug.Log("P2 Score: " + P2points);
    }

    /*
    public void PrintScore() {
        P1Score.text = P1points.ToString();
        P2Score.text = P2points.ToString();
    }
    */

    void NextRoundText()
    {
        currentRound++; //To increase round
        roundImage.GetComponent<RoundManager>().UpdateRoundText(currentRound - 1);
    }

   
    public void AssignPowerUp(Brick brick) {
        int powerUpDie = Random.Range(1, 3); // 1 or 2
        PowerUpType powerUpType = (PowerUpType)powerUpDie;

        if (brick != null) {
            SoundManager.Instance.Play("collect skill", 0);

            switch (powerUpType) {
                case PowerUpType.Shield: brick.ActivateShield(); break;
                case PowerUpType.Double: brick.ActivateDouble(); break;
                default: break;
            }
        }
    }

    public void ActivePauseMenu() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            pauseMenu.SetActive(true);
            SoundManager.Instance.ToggleMute();
            Time.timeScale = 0;
        }
    }
    public void Resume() {
        pauseMenu.SetActive(false);
        SoundManager.Instance.ToggleMute();
        Time.timeScale = 1;
    }

    public void QuitMatch() {
    #if UNITY_STANDALONE
        Application.Quit();
    #endif
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
    }

    public void Rematch() {
        SceneManager.LoadScene("Cafeteria Showdown");
    }

    public void DoGameOver() {
        gameOverScreen.SetActive(true);

        if      (P1points >= 3) player1WinText.SetActive(true);
        else if (P2points >= 3) player2WinText.SetActive(true);

        SoundManager.Instance.soundSource.Stop();
        SoundManager.Instance.Play("game win", 1);
    }
}


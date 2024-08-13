using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public enum PowerUpType { Shield = 1, Double = 2 }

public class GameManager : MonoBehaviour {

    public static GameManager Instance;
    public BrickSpawner BrickHandler { get; set; }

    [Header("Player Scores")]
    [SerializeField] int currentRound = 0;
    [SerializeField] int P1points = 0, P2points = 0;

    [Header("UI Panels")]
    [SerializeField] GameObject roundImage;
    [SerializeField] GameObject pauseMenu, gameOverScreen, p1ScoreImage, p2ScoreImage;
    [SerializeField] GameObject p1WinImage, p2WinImage;


    [Header("ToDelete")]
    //[SerializeField] GameObject player1WinText, player2WinText; // Hidden until testing is done


    bool isPaused, canPause;

    private void Awake()
    {
        Instance = this;
        BrickHandler = FindObjectOfType<BrickSpawner>();
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    /*
    void Awake() {
        if (Instance == null) {
            Instance = this;
            BrickHandler = FindObjectOfType<BrickSpawner>();

            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
    */
    

    void Start() {
        Time.timeScale = 1.0f;
        BrickHandler.SpawnBrick();
        SoundManager.Instance.Play("battle", 2);

        isPaused = false;
        canPause = true;
    }

    void Update() {
        CheckPoints();
        if (Input.GetKeyDown(KeyCode.Backspace)) // basic restart code
            SceneManager.LoadScene("Game Screen");

        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePause();
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

    void NextRoundText() {
        currentRound++; //To increase round
        roundImage.GetComponent<RoundManager>().UpdateRoundText(currentRound - 1);
    }

    public void AssignPowerUp(Brick brick) 
    {
        int powerUpDie = Random.Range(1, 3); // 1 or 2
        PowerUpType powerUpType = (PowerUpType)powerUpDie;
       
        if (brick != null) {
            SoundManager.Instance.Play("collect skill", 0);
            switch (powerUpType) {
                case PowerUpType.Shield: brick.ActivateShield();
                    Debug.Log("ShieldON");
                    break;
                case PowerUpType.Double: brick.ActivateDouble();
                    Debug.Log("DmgON"); 
                    break;
                default:
                    Debug.Log("Unexpected powerUpType: " + powerUpType);
                    break;
            }
        }
    }
    
    public void TogglePause() {
        if (!canPause) return;

        isPaused = !isPaused;

        if (isPaused) Time.timeScale = 0f;
        else          Time.timeScale = 1f;

        pauseMenu.SetActive(!pauseMenu.activeSelf);
        SoundManager.Instance.ToggleMute();
    }

    public void Rematch() 
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game Screen"); 
    }

    public void DoGameOver() {
        gameOverScreen.SetActive(true);
        canPause = false;

        if (P1points >= 3)
        {
            p1WinImage.SetActive(true);
            p2WinImage.SetActive(false);
        }
        else if (P2points >= 3)
        {
            p1WinImage.SetActive(false);
            p2WinImage.SetActive(true); 
        }

        SoundManager.Instance.soundSource.Stop(); // stops bgm to play the win sfx
        SoundManager.Instance.Play("game win", 1);
        Time.timeScale = 0f;
    }
    public void QuitMatch() {
    #if UNITY_STANDALONE
        Application.Quit();
    #endif
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
    }
}
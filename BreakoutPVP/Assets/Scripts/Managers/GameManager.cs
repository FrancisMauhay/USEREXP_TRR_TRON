using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public enum PowerUpType { Shield = 1, Double = 2 }

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }
    public BrickSpawner BrickHandler { get; set; }

    [SerializeField] int P1points = 0, P2points = 0;
    [SerializeField] TextMeshProUGUI P1Score, P2Score;

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
    }

    void CheckPoints() {
        if (P1points >= 3) {
            Debug.Log("P1 WINS");
            BrickHandler.SpawnBrick();
            // SoundManager.Instance.Play();
        }
        if (P2points >= 3) {
            Debug.Log("P2 WINS");
            BrickHandler.SpawnBrick();
            // SoundManager.Instance.Play();
        }
    }

    public void P1Scored() {
        P1points++;
        Debug.Log("P1 Score: " + P1points);
        // SoundManager.Instance.Play();
    }
    public void P2Scored() {
        P2points++;
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
}


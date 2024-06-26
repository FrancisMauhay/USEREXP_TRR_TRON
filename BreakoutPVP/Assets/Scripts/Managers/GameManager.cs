using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour {

    public static GameManager instance { get; private set; }
    public BrickSpawner BrickHandler { get; set; }

    [SerializeField] int P1points = 0, P2points = 0;
    [SerializeField] TextMeshProUGUI P1Score, P2Score;

    void Awake()  {
        if (instance == null) {
            instance = this;
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
        if (P1points >= 3) Debug.Log("P1 WINS");
        if (P2points >= 3) Debug.Log("P2 WINS");
    }

    public void P1Scored() {
        P1points++;
        Debug.Log("P1 Score: " + P1points);
    }

    public void P2Scored() {
        P2points++;
        Debug.Log("P2 Score: " + P2points);
    }

    public void PrintScore() {
        P1Score.text = P1points.ToString();
        P2Score.text = P2points.ToString();
    }
}

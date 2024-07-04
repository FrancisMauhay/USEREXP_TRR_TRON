using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour {

    public static GameManager instance { get; private set; }
    public BrickSpawner BrickHandler { get; set; }

    [SerializeField] int P1points = 0, P2points = 0;
    private int ChanceDie;
    private int PowerUpDie;
    [SerializeField] TextMeshProUGUI P1Score, P2Score;

    void Awake()  
    {
        if (instance == null) {
            instance = this;
            BrickHandler = FindObjectOfType<BrickSpawner>();
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        ChanceDie = Random.Range(1,6);
        PowerUpDie = Random.Range(1,3);
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

    public void PowerUpRoll() {
        if(ChanceDie == 1)
        {
            Debug.Log("You get a Power Up");
            if(PowerUpDie == 1)
            {
                Debug.Log("1");
                //PowerUp1
            }
            else if(PowerUpDie == 2)
            {
                Debug.Log("2");
                //PowerUp2
            }
            else if (PowerUpDie == 3)
            {
                Debug.Log("3");
                // PowerUp3
            }
            ChanceDie = Random.Range(1,6);
            PowerUpDie = Random.Range(1,3);
        }
        else
        {
            Debug.Log("No PowerUp");
            ChanceDie = Random.Range(1, 6);
        }
    }
}

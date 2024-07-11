using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {
    [SerializeField] bool player2;

    public BrickSpawner BrickHandler { get; set; }
    public BallSpawner BallHandler { get; set; }

    void Awake() {
        BrickHandler = FindObjectOfType<BrickSpawner>();
        BallHandler = FindObjectOfType<BallSpawner>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<Ball>() != null) {

            if (player2 == true) {
                BallHandler.DestroyBall(other.gameObject);
                BrickHandler.rightBrickActive = false;
                GameManager.Instance.P1Scored();
                BrickHandler.SpawnBrick();
            }
            else {
                BallHandler.DestroyBall(other.gameObject);
                BrickHandler.leftBrickActive = false;
                GameManager.Instance.P2Scored();
                BrickHandler.SpawnBrick();
            }
        }
    }
}

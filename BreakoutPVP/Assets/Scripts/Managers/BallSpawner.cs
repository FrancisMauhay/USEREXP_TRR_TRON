using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour {
    [SerializeField] GameObject ballPrefab;
    
    private int ballAmt = 0;

    void Update() {
        if (ballAmt < 1)
            SpawnBall();
    }

    public void SpawnBall() {
        GameObject ball = Instantiate(ballPrefab) as GameObject;
        ball.transform.position = new Vector2(0, 0);

        ballAmt++;
    }

    public void DestroyBall(GameObject ballObj) {
        Destroy(ballObj);
        ballAmt = 0;
    }
}

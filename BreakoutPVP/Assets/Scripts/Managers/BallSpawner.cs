using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour {
    [SerializeField] GameObject ballPrefab;
    private GameObject ballInstance;
    
    private int ballAmt = 0;

    float defaultTimer = 4.0f;

    void Update() {
        if (ballAmt < 1)
            SpawnBall();

       CheckBallVelocity();
    }

    public void SpawnBall() {
        ballInstance = Instantiate(ballPrefab) as GameObject;
        ballInstance.transform.position = new Vector2(0, 0);

        ballAmt++;
    }

    public void DestroyBall(GameObject ballObj) {
        Destroy(ballObj);
        ballAmt = 0;
    }

    public void CheckBallVelocity() { 
        Ball ball = GetComponent<Ball>();
        if(ball.rb.velocity.magnitude <= 0.1f)
        {
            defaultTimer -= Time.deltaTime;
            
            if(defaultTimer <= 0)
            {
                ball.ResetBall();
                defaultTimer = 4.0f;
            }
        }
        else
        {
            defaultTimer = 4.0f;
        }
    }
}

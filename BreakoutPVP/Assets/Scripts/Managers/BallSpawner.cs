using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour {
    [SerializeField] GameObject ballPrefab;
    
    private int ballAmt = 0;

    float defaultTimer = 10.0f;

    void Update() {
        if (ballAmt < 1)
            SpawnBall();

       // ResetBall();
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

/*    private void ResetBall()
    {
        GameObject ball = ballPrefab.GetComponent<GameObject>();
        if (ball.GetComponent<Ball>().rb.velocity.x <= 0 &&
            ball.GetComponent<Rigidbody>().velocity.y <= 0)
        {
            defaultTimer -= Time.deltaTime;
            Debug.Log(defaultTimer);

            if (defaultTimer <= 0)
            {
                DestroyBall(ball);
            }
        }
    }*/
}

using UnityEngine;

public class BallSpawner : MonoBehaviour {

    [SerializeField] GameObject[] ballPrefabs;
    private GameObject ballInstance;

    private int ballAmt = 0;
    float defaultTimer = 4.0f;

    void Update() {
        if (ballAmt < 1)
            SpawnBall();

        CheckBallVelocity();
    }

    public void SpawnBall() {
        int randNum = Random.Range(0, ballPrefabs.Length);

        ballInstance = Instantiate(ballPrefabs[randNum]);
        ballInstance.transform.position = new Vector2(0, 0);

        Ball b = ballInstance.GetComponent<Ball>();

        switch (randNum) {
            case 0: b.food = Food.DONUT;   break;
            case 1: b.food = Food.EGG;     break;
            case 2: b.food = Food.PANCAKE; break;
            case 3: b.food = Food.PIZZA;   break;
            default:                       break;
        }
        ballAmt++;
    }

    public void DestroyBall(GameObject ballObj) {
        Destroy(ballObj);
        ballAmt = 0;
    }

    public void CheckBallVelocity() {
        if (ballInstance != null) {
            Ball ball = ballInstance.GetComponent<Ball>();
            
            if (ball != null &&  ball.rb.velocity.magnitude <= 0.1f )  {
                defaultTimer -= Time.deltaTime;

                if (defaultTimer <= 0) {
                    ball.ResetBall();
                    defaultTimer = 4.0f;
                }
            }
            else defaultTimer = 4.0f;
        }
    }
}

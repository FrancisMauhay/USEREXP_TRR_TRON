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
                BrickHandler.SpawnBrick();
                
                GameManager.Instance.P1Scored();
                SoundManager.Instance.Play("death2", 1);
            }
            else {
                BallHandler.DestroyBall(other.gameObject);
                BrickHandler.leftBrickActive = false;
                BrickHandler.SpawnBrick();

                GameManager.Instance.P2Scored();
                SoundManager.Instance.Play("death1", 1);
            }
        }
    }
}

using UnityEngine;

public enum Food { DEFAULT, PIZZA, EGG, PANCAKE, DONUT }

public class Ball : MonoBehaviour {

    [SerializeField] float moveSpeed;
    //[SerializeField] Paddle paddle; //need to find way to get the script of each player
    [SerializeField] GameObject player1Prefab;
    [SerializeField] GameObject player2Prefab;
    [SerializeField] static float resetBoolTimer = 1f;
    public Rigidbody2D rb;
    float initMoveSpeed;
    bool player2 = false;

    public Food food;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        player1Prefab = GameObject.Find("P1");
        player2Prefab = GameObject.Find("P2");
     
        Launch();

        initMoveSpeed = moveSpeed;
    }

    void Update() {
        // Debug.Log("Ball Velocity "+rb.velocity);
        //player1Prefab.GetComponent<Paddle>().didBallHit = false;
        //player2Prefab.GetComponent<Paddle>().didBallHit = false;
    }

    void Launch() {
        if (rb != null) {

            if (!player2) rb.velocity = Vector2.left * moveSpeed;
            else          rb.velocity = Vector2.right * moveSpeed;
        }
    }
    
    void OnCollisionEnter2D (Collision2D collision) { 
        
        // ball hits paddle
        if (collision.gameObject.GetComponent<Paddle>() != null) {
            Vector2 hitFactor = CalculateHitFactor(transform.position, collision.transform.position, collision.collider.bounds.size);
            Vector2 newDirection = new Vector2(hitFactor.x, hitFactor.y).normalized;
            rb.velocity = newDirection * moveSpeed;
            moveSpeed++;
             
            if (collision.gameObject.tag == "Player1") {
                player1Prefab.GetComponent<Paddle>().didBallHit = true;
                StartCoroutine(player1Prefab.GetComponent<Paddle>().SwitchBoolean1(resetBoolTimer));
                SoundManager.Instance.Play("hit1", 1);
            }
            else if(collision.gameObject.tag == "Player2") {
                player2Prefab.GetComponent<Paddle>().didBallHitP2 = true;
                StartCoroutine(player2Prefab.GetComponent<Paddle>().SwitchBoolean2(resetBoolTimer));
                SoundManager.Instance.Play("hit2", 1);
            }
        }

        // ball crashes at the brick
        if (collision.gameObject.GetComponent<Brick>() != null) { 
            Vector2 hitFactor = CalculateHitFactor(transform.position, collision.transform.position, collision.collider.bounds.size);
            Vector2 newDirection = new Vector2(hitFactor.x, hitFactor.y).normalized;

            rb.velocity = newDirection * moveSpeed;
            collision.gameObject.GetComponent<Brick>().HitWall();

            // resets speed when it hits a wall
            moveSpeed = initMoveSpeed;

            switch (food) {
                case Food.DONUT:   SoundManager.Instance.Play("donut hit", 0);   break;
                case Food.EGG:     SoundManager.Instance.Play("egg hit", 0);     break;
                case Food.PANCAKE: SoundManager.Instance.Play("pancake hit", 0); break;
                case Food.PIZZA:   SoundManager.Instance.Play("pizza hit", 0);   break;
                default:                                                         break;
            }
        }

        // ball bounces off a wall  
        if (collision.gameObject.GetComponent<Brick>() != null) {
            Vector2 hitFactor = CalculateHitFactor(transform.position, collision.transform.position, collision.collider.bounds.size);
            Vector2 newDirection = new Vector2(rb.velocity.x, hitFactor.y).normalized;

            rb.velocity = newDirection * moveSpeed;

            switch (collision.gameObject.GetComponent<Brick>().currHP) {
                case 0: SoundManager.Instance.Play("crack3", 0); break;
                case 1: SoundManager.Instance.Play("crack2", 0); break;
                case 2: SoundManager.Instance.Play("crack1", 0); break;
                default: break;
            }
        }
    }
    public Vector2 CalculateHitFactor(Vector2 ballPos, Vector2 paddlePos, Vector2 paddleSize) {
        float x = (ballPos.x - paddlePos.x) / paddleSize.x;
        float y = (ballPos.y - paddlePos.y) / paddleSize.y;

        return new Vector2(x, y);
    }
    public void ResetBall() { 
        transform.position = Vector2.zero;

        if (Random.Range(0f, 1f) > 0.5f) rb.velocity = Vector2.left;
        else                             rb.velocity = Vector2.right;
    }
}

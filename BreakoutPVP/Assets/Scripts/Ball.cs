using UnityEngine;

public class Ball : MonoBehaviour {

    [SerializeField] float moveSpeed;
    public Rigidbody2D rb;
    float initMoveSpeed;
    bool player2 = false;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        Launch();

        initMoveSpeed = moveSpeed;
    }

    void Update() {
        // Debug.Log("Ball Velocity "+rb.velocity);
    }

    void Launch() {
        if (rb != null) {

            if (!player2) rb.velocity = Vector2.left * moveSpeed;
            else          rb.velocity = Vector2.right * moveSpeed;

            // SoundManager.Instance.Play();
        }
    }
    
    void OnCollisionEnter2D (Collision2D collision) { 
        
        // ball hits paddle
        if (collision.gameObject.GetComponent<Paddle>() != null) {
            Vector2 hitFactor = CalculateHitFactor(transform.position, collision.transform.position, collision.collider.bounds.size);
            Vector2 newDirection = new Vector2(hitFactor.x, hitFactor.y).normalized;

            rb.velocity = newDirection * moveSpeed;
            moveSpeed++;
            // SoundManager.Instance.Play();
        }

        // ball crashes at the brick
        if (collision.gameObject.GetComponent<Brick>() != null) { 
            Vector2 hitFactor = CalculateHitFactor(transform.position, collision.transform.position, collision.collider.bounds.size);
            Vector2 newDirection = new Vector2(hitFactor.x, hitFactor.y).normalized;

            rb.velocity = newDirection * moveSpeed;
            collision.gameObject.GetComponent<Brick>().HitWall();

            // resets speed when it hits a wall
            moveSpeed = initMoveSpeed;
            // SoundManager.Instance.Play();
        }

        // ball bounces off a wall
        if (collision.gameObject.GetComponent<Wall>() != null) {
            Vector2 hitFactor = CalculateHitFactor(transform.position, collision.transform.position, collision.collider.bounds.size);
            Vector2 newDirection = new Vector2(rb.velocity.x, hitFactor.y).normalized;

            rb.velocity = newDirection * moveSpeed;
            // SoundManager.Instance.Play();
        }
    }
    public Vector2 CalculateHitFactor(Vector2 ballPos, Vector2 paddlePos, Vector2 paddleSize) {
        float x = (ballPos.x - paddlePos.x) / paddleSize.x;
        float y = (ballPos.y - paddlePos.y) / paddleSize.y;

        return new Vector2(x, y);
    }
    public void ResetBall() { 
        transform.position = Vector2.zero;
        rb.velocity = Vector2.left;
    }
}

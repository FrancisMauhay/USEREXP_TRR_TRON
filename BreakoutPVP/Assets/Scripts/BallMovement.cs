using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    private bool player2 = false;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Launch();   
    }

    void Launch()
    {
        if (rb != null)
        {
            if (player2 == false)
            {
                rb.velocity = Vector2.left * moveSpeed;
            }
            else if(player2 == true)
            {
                rb.velocity = Vector2.right * moveSpeed;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            Vector2 hitFactor = CalculateHitFactor(transform.position, collision.transform.position, collision.collider.bounds.size);
            Vector2 newDirection = new Vector2(hitFactor.x, hitFactor.y).normalized;
            rb.velocity = newDirection * moveSpeed;
            moveSpeed++;
        }
    }

    Vector2 CalculateHitFactor(Vector2 ballPos, Vector2 paddlePos, Vector2 paddleSize)
    {
        float x = (ballPos.x - paddlePos.x) / paddleSize.x;
        float y = (ballPos.y - paddlePos.y) / paddleSize.y;
        return new Vector2(x, y);
    }
}

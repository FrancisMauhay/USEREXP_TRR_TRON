using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Paddle : MonoBehaviour {
    PlayerInput playerInput;
    InputAction move;
    [SerializeField] Animator animator;
    [SerializeField] float MovementSpeed;
    [SerializeField] bool Player1;

    Vector2 direction;
    Vector2 movement;
    public bool didBallHit = false;
    public bool didBallHitP2 = false;

    void Start() {
        didBallHit = false;
        didBallHitP2 = false;

        playerInput = GetComponent<PlayerInput>();

        if (playerInput != null) {

            if (Player1 == true)
                move = playerInput.actions.FindAction("Player 1");

            else if (Player1 == false)
                move = playerInput.actions.FindAction("Player 2");
        }
    }

    void Update() {
        animator.SetFloat("Speed", Mathf.Abs(movement.magnitude));
        animator.SetBool("IsBallHit", didBallHit); //problem same Bool for 2 different instances
        animator.SetBool("IsBallHitP2", didBallHitP2);

        PlayerMove();
    }

    void PlayerMove() {
        direction = move.ReadValue<Vector2>();
        movement = new Vector2(0, direction.y * MovementSpeed * Time.deltaTime);
        // Debug.Log(movement.magnitude);
        transform.Translate(movement);
    }

    public IEnumerator SwitchBoolean1(float resetTimer) {
        yield return new WaitForSeconds(resetTimer);
        didBallHit = false;
    }

    public IEnumerator SwitchBoolean2(float resetTimer) {
        yield return new WaitForSeconds(resetTimer);
        didBallHitP2 = false;
    }


}

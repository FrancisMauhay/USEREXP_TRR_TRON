using UnityEngine;
using UnityEngine.InputSystem;

public class Paddle : MonoBehaviour {
    PlayerInput playerInput;
    InputAction move;
    [SerializeField] float MovementSpeed;
    [SerializeField] bool Player1;

    void Start() {
        playerInput = GetComponent<PlayerInput>();

        if (playerInput != null) {

            if (Player1 == true)
                move = playerInput.actions.FindAction("Player 1");

            else if (Player1 == false)
                move = playerInput.actions.FindAction("Player 2");
        }
    }

    void Update() {
        PlayerMove();
    }

    void PlayerMove() {
        Vector2 direction = move.ReadValue<Vector2>();
        Vector2 movement = new Vector2(0, direction.y * MovementSpeed * Time.deltaTime);

        transform.Translate(movement);
    }
}

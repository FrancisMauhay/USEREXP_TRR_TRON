using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    PlayerInput playerInput;
    InputAction move;
    [SerializeField] float MovementSpeed;
    [SerializeField] bool Player1;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        if(playerInput != null)
        {
            if (Player1 == true)
            {
                move = playerInput.actions.FindAction("Player 1");
            }
            else if (Player1 == false)
            {
                move = playerInput.actions.FindAction("Player 2");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        Vector2 direction = move.ReadValue<Vector2>();
        Vector2 movement = new Vector2 (direction.x * MovementSpeed * Time.deltaTime, 0);
        transform.Translate(movement);
    }
}

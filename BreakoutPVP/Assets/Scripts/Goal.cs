using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] bool player2;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<BallMovement>() != null)
        {
            if(player2 == true)
            {
                GameManager.instance.P1Scored();
            }
            else
            {
                GameManager.instance.P2Scored();
            }
        }
    }
}

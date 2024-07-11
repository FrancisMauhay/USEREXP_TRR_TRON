using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    enum PowerUpType { Shield, Double };

    private PowerUpType PwrUp;

    void ApplyPowerUp(GameObject target)
    {
        Brick brick = target.GetComponent<Brick>();
        if (brick != null)
        {
            switch (PwrUp)
            { 
                case PowerUpType.Shield:
                    break;
                case PowerUpType.Double:
                    break;
            }

        }
    }
}

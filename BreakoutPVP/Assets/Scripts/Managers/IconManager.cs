using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconManager : MonoBehaviour
{
    public static IconManager instance;

    [Header("PowerUps")]
    [SerializeField] private GameObject shieldIconP1; // 0 for P1, 1 for P2
    [SerializeField] private GameObject doubleDmgIconP1; // 0 for P1, 1 for P2
    [SerializeField] private GameObject shieldIconP2; // 0 for P1, 1 for P2
    [SerializeField] private GameObject doubleDmgIconP2; // 0 for P1, 1 for P2

    private void Awake()
    {
        instance = this;
       
    }

    private void OnDestroy()
    {
        instance = null;
    }

    public void ToggleShieldIconP1(bool trueFalse)
    {
        shieldIconP1.SetActive(trueFalse);
    }

    public void ToggleShieldIconP2(bool trueFalse)
    {
        shieldIconP2.SetActive(trueFalse);
    }
    public void ToggleDamageIconP1(bool trueFalse)
    {
        doubleDmgIconP1.SetActive(trueFalse);
    }
    public void ToggleDamageIconP2(bool trueFalse)
    {
        doubleDmgIconP2.SetActive(trueFalse);
    }
  
}

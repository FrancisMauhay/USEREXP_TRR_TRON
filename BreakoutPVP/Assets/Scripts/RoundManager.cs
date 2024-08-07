using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    [Header("Round Variables")]
    [SerializeField] Image[] currentRoundSpriteArray;
    [SerializeField] Image currentRoundSprite;

    // Start is called before the first frame update
    void Start()
    {
        currentRoundSprite.sprite = currentRoundSpriteArray[0].sprite; //setting to Round 1
    }

    void UpdateRoundText(int round)
    {
        currentRoundSprite.sprite = currentRoundSpriteArray[round - 1].sprite;
    }

}

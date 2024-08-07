using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    [Header("Round Variables")]
    [SerializeField] Image[] currentRoundSpriteArray;
    [SerializeField] Image roundImageToDisplay;

    // Start is called before the first frame update
    void Start()
    {
        roundImageToDisplay.sprite = currentRoundSpriteArray[0].sprite; //setting to Round 1
    }

    public void UpdateRoundText(int round)
    {
        roundImageToDisplay.sprite = currentRoundSpriteArray[round].sprite;
    }

}

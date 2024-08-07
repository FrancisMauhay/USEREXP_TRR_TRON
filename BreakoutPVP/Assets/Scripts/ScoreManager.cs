using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
    
    public Image[] scoreSprites;
    public Image imageToDisplay;

    void Start() { 
        imageToDisplay.sprite = scoreSprites[0].sprite; 
    }

    public void UpdateScoreText(int score) { 
        imageToDisplay.sprite = scoreSprites[score].sprite;
    }
}

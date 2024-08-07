using System.Collections;
using UnityEngine;

public class Brick : MonoBehaviour {

    public int currHP;
    //[SerializeField] Material mat;
    [SerializeField] SpriteRenderer tableSpriteRender;
    [SerializeField] Sprite[] tableSprites;

    public bool P2Brick = false;

    int damage = 1;
    bool ShieldActive = false, DoubleActive = false;

    public BrickSpawner BrickHandler { get; set; }

    void Awake() {
        BrickHandler = FindObjectOfType<BrickSpawner>();
        
    }

    void Start()  {
        /* the player hitting the brick is their corresponding brick
            - P1 (left) hits P1 wall (right)
            - P2 (right) hits P2 wall (left)
        */

        if (P2Brick) gameObject.name = "Right Wall";
        else         gameObject.name = "Left Wall";

        currHP = 3;
        //mat.color = Color.green;
    }

    void Update() {
        //test(); // for small HP values

        /* actual color changer code (make sure the HP is set beforehand)
            if (currHP >= 10) mat.color = Color.green;
            else if (currHP >= 5) mat.color = Color.yellow;
            else mat.color = Color.red;
        */
    }

    public void HitWall() {
        currHP -= damage;

        // Clamp the index to prevent array out of bounds
        int spriteIndex = Mathf.Clamp(currHP - 1, 0, tableSprites.Length - 1);
        tableSpriteRender.sprite = tableSprites[spriteIndex];

        if (currHP <= 0) 
        {
            BrickHandler.BrickDestroyed(gameObject);
            // SoundManager.Instance.Play();
        }
        else 
        {
            GameManager.Instance.AssignPowerUp(this);
            // SoundManager.Instance.Play();
        }
        // Debug.LogWarning(gameObject.name + " has been hit");
    }

    /*
    void test() {
        switch (currHP) {
            case 0: break;
            case 1: mat.color = Color.red; break;
            case 2: mat.color = Color.yellow; break;
            case 3: mat.color = Color.green; break;
            default: break;
        }

        // Debug.LogWarning(name + "'s HP: " + currHP);
    }
    */

    public void ActivateShield() { 
        if(!ShieldActive) {
            ShieldActive = true;
            damage = 0;
            // Debug.Log("Shield Activated");
            StartCoroutine(ShieldDuration());
            // SoundManager.Instance.Play();
        }
    }
    public void ActivateDouble() {
        if (!DoubleActive) {
            DoubleActive = true;
            damage = 2;
            // Debug.Log("Double Damage Activated");
            StartCoroutine(DoubleDuration());
            // SoundManager.Instance.Play();
        }
    }
    IEnumerator ShieldDuration() {
        yield return new WaitForSeconds(10);
        ShieldActive = false;
        damage = 1;
        // Debug.Log("Shield Deactivated");
        // SoundManager.Instance.Play();
    }
    IEnumerator DoubleDuration() {
        yield return new WaitForSeconds(10);
        DoubleActive = false;
        damage = 1;
        // Debug.Log("Double Damage Over");
        // SoundManager.Instance.Play();
    }
}

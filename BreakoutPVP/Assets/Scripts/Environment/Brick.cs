using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {

    [SerializeField] int currHP;
    [SerializeField] Material mat;

    public bool P2Brick = false;

    private int damage = 1;
    private bool ShieldActive = false;
    private bool DoubleActive = false;

    public BrickSpawner BrickHandler { get; set; }

    void Awake() {
        BrickHandler = FindObjectOfType<BrickSpawner>();
    }

    void Start()  {
        currHP = 3;
        mat.color = Color.green;
    }

    void Update() {
        test();

        /* actual color changer code (make sure the HP is set beforehand)
            if (currHP >= 10) mat.color = Color.green;
            else if (currHP >= 5) mat.color = Color.yellow;
            else mat.color = Color.red;
        */
    }

    public void HitWall() {
        currHP -= damage;

        if (currHP <= 0) {
            BrickHandler.BrickDestroyed(gameObject);
        }
        else {
            GameManager.instance.AssignPowerUp(this);
        }
        // Debug.LogWarning(gameObject.name + " has been hit");
    }

    void test() {
        switch (currHP) {
            case 0: break;
            case 1: mat.color = Color.red; break;
            case 2: mat.color = Color.yellow; break;
            case 3: mat.color = Color.green; break;
            default: break;
        }
    }

    public void ActivateShield() { 
        if(!ShieldActive) {
            ShieldActive = true;
            damage = 0;
            Debug.Log("Shield Activated");
            StartCoroutine(ShieldDuration());
        }
    }

    public void ActivateDouble() {
        if (!DoubleActive) {
            DoubleActive = true;
            damage = 2;
            Debug.Log("Double Damage Activated");
            StartCoroutine(DoubleDuration());
        }
    }

    IEnumerator ShieldDuration() {
        yield return new WaitForSeconds(10);
        ShieldActive = false;
        damage = 1;
        Debug.Log("Shield Deactivated");
    }

    IEnumerator DoubleDuration() {
        yield return new WaitForSeconds(10);
        DoubleActive = false;
        damage = 1;
        Debug.Log("Double Damage Over");
    }
}

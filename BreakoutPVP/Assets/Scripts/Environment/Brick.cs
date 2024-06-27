using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {

    [SerializeField] int currHP;
    [SerializeField] Material mat;

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
        currHP--;

        if (currHP <= 0)
            BrickHandler.BrickDestroyed(gameObject);
        
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
}

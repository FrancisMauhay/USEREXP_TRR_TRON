using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {

    [SerializeField] int currHP;
    [SerializeField] bool isBreakable;
    [SerializeField] Material mat;
 
    void Start() {
        if (isBreakable) {
            mat.color = Color.green;
            currHP = 3;
        }
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
        if (!isBreakable) return;
        
        currHP--;

        if (currHP <= 0) 
            Destroy(gameObject);
    }
    
    void test() {
        Debug.Log(gameObject.name + " = " + currHP);

        switch (currHP) {
            case 0: break;
            case 1: mat.color = Color.red; break;
            case 2: mat.color = Color.yellow; break;
            case 3: mat.color = Color.green; break;
            default: break;
        }
    }
}

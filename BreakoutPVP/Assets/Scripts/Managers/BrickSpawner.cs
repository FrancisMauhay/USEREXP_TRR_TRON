using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawner : MonoBehaviour {

    [SerializeField] GameObject BrickPrefab;
    [SerializeField] Material mat1, mat2;

    public bool rightBrickActive, leftBrickActive;

    void Awake() {
        rightBrickActive = false;
        leftBrickActive = false;

        mat1.color = Color.green; // left brick
        mat2.color = Color.green; // right brick
    }

    public void SpawnBrick() {
        if (!rightBrickActive) {
            // Debug.Log("Right Brick is active");
            rightBrickActive = true;

            GameObject brick = Instantiate(BrickPrefab) as GameObject;
            brick.transform.position = new Vector2(9, 0);
            brick.GetComponent<Renderer>().material = mat2;
            // Debug.LogWarning("Spawned right brick");
        }
        if (!leftBrickActive) {
            // Debug.Log("Left Brick is Active");
            leftBrickActive = true;

            GameObject brick = Instantiate(BrickPrefab) as GameObject;
            brick.transform.position = new Vector2(-9, 0);
            brick.GetComponent<Renderer>().material = mat1;
            // Debug.LogWarning("Spawned left brick");
        }
    }

    public void BrickDestroyed(GameObject brickObj) {
        Destroy(brickObj);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    [SerializeField] public GameObject BrickPrefab;

    public bool rightBrickActive = false;
    public bool leftBrickActive = false;

    public void SpawnBrick()
    {
        if (rightBrickActive == false)
        {
            rightBrickActive = true;
            Debug.Log("Right Brick is active");

            GameObject brick = Instantiate(BrickPrefab) as GameObject;
            brick.transform.position = new Vector2(9, 0);
        }
        if (leftBrickActive == false)
        {
            leftBrickActive = true;
            Debug.Log("Left Brick is Active");

            GameObject brick = Instantiate(BrickPrefab) as GameObject;
            brick.transform.position = new Vector2(-9, 0);
        }
    }

    public void BrickDestroyed(GameObject brickObj)
    {
        Destroy(brickObj);
    }
}

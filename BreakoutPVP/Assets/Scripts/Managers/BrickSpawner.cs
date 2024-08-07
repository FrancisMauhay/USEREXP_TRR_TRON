using UnityEngine;

public class BrickSpawner : MonoBehaviour {

    [SerializeField] GameObject BrickPrefab;
    [SerializeField] Material mat1, mat2;

    public bool rightBrickActive, leftBrickActive;

    private GameObject leftBrick;
    private GameObject rightBrick;

    void Awake() {
        rightBrickActive = false;
        leftBrickActive = false;

        mat1.color = Color.green; // left brick
        mat2.color = Color.green; // right brick
    }

    public void SpawnBrick() {
        if (!rightBrickActive) { // Debug.Log("Right Brick is active");
            rightBrickActive = true;

            rightBrick = Instantiate(BrickPrefab) as GameObject;
            rightBrick.transform.position = new Vector2(9, 0);
            rightBrick.GetComponent<Brick>().P2Brick = true;
            rightBrick.GetComponent<Renderer>().material = mat2;
            // Debug.LogWarning("Spawned right brick");
        }
        if (!leftBrickActive) { // Debug.Log("Left Brick is Active");
            leftBrickActive = true;

            leftBrick = Instantiate(BrickPrefab) as GameObject;
            leftBrick.transform.position = new Vector2(-9, 0);
            leftBrick.GetComponent<Renderer>().material = mat1;
            // Debug.LogWarning("Spawned left brick");
        }
    }

    public void BrickDestroyed(GameObject brickObj) {
        Destroy(brickObj);
    }
}

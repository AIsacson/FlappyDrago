using UnityEngine;

public class OctupusMovement : MonoBehaviour {

    public GameObject octupusPrefab;
    public Rigidbody2D rb;
    Vector2 octupusPosition = new Vector2(-10, 0);
    GameObject [] enemies;
    float minY = -3f;
    float maxY = 3f;
    float minX = 10f;
    float maxX = 15f;
    float minSpawnRate = 5f;
    float maxSpawnRate = 15f;
    float spawnXPosition;
    float spawnYPosition;
    float spawnRate;
    float timePassed;
    int numberOfEnemies = 2;

    void Start() {
        spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
        enemies = new GameObject[numberOfEnemies];
        for (int i = 0; i < numberOfEnemies; i++) {
            enemies[i] = Instantiate(octupusPrefab, octupusPosition, Quaternion.identity);
        }
    }

    void Update () {
        timePassed += Time.deltaTime;
        if(!GameController.instance.gameOver && timePassed >= spawnRate) {
            spawnXPosition = Random.Range(minX, maxX);
            spawnYPosition = Random.Range(minY, maxY);
            enemies[0].transform.position = new Vector2(spawnXPosition, spawnYPosition);
            spawnXPosition = Random.Range(minX, maxX);
            spawnYPosition = Random.Range(minY, maxY);
            enemies[1].transform.position = new Vector2(spawnXPosition, spawnYPosition);
            timePassed = 0;
        }
        if(!GameController.instance.gameOver){
            enemies[0].transform.Translate(Vector2.left * 2.5f * Time.deltaTime);
            enemies[1].transform.Translate(Vector2.left * 2.5f * Time.deltaTime);
        }
    }
}

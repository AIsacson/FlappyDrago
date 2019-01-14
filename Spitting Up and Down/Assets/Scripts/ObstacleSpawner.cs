using UnityEngine;

// Some of this code in this script was taken from one of Unity's tutorials available on their website.

public class ObstacleSpawner : MonoBehaviour {

    public GameObject obstaclePrefab;
    public GameObject shieldPrefab;
    public int numberOfObstacles = 5;
    public int numberOfShields = 2;

    Vector2 obstaclePosition = new Vector2(-10, 0);
    Vector2 shieldPosition = new Vector2(-10, 0);
    GameObject[] obstacles;
    GameObject shield;
    float timeSinceSpawnedObstacle;
    float timeSinceSpawnedShield;
    float maxTime = 25f;
    float minTime = 15f;
    float minY = -3.2f;
    float maxY = 4.3f;
    float minX = 3.5f;
    float maxX = 6f;
    float spawnRateObstacle = 4f;
    float spawnRateShield;
    float spawnXPosition = 6f;
    float spawnYPosition;
    int currentObstacle;
    int currentShield;

    void Start () {
        obstacles = new GameObject[numberOfObstacles];

        for (int i = 0; i < numberOfObstacles; i++) {
            obstacles[i] = Instantiate(obstaclePrefab, obstaclePosition, Quaternion.identity);
        }
        shield = Instantiate(shieldPrefab, shieldPosition, Quaternion.identity);
        spawnRateShield = Random.Range(minTime, maxTime);
    }

	void Update () {
        timeSinceSpawnedObstacle += Time.deltaTime;
        timeSinceSpawnedShield += Time.deltaTime;
        if(!GameController.instance.gameOver && timeSinceSpawnedObstacle >= spawnRateObstacle) {
            SpawnObstacle();
            timeSinceSpawnedObstacle = 0f;
        }
        if (!GameController.instance.gameOver && timeSinceSpawnedShield >= spawnRateShield) {
            SpawnShield();
            spawnRateShield = Random.Range(minTime, maxTime);
            timeSinceSpawnedShield = 0f;
        }
    }

    void SpawnObstacle() {
        spawnYPosition = Random.Range(minY, maxY);
        spawnXPosition = Random.Range(minX, maxX);
        obstacles[currentObstacle].transform.position = new Vector2(spawnXPosition, spawnYPosition);
        currentObstacle++;

        if (currentObstacle >= numberOfObstacles) {
            currentObstacle = 0;
        }
    }

    void SpawnShield() {
        shield.transform.position = new Vector2(4f, 0);
    }


}

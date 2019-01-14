using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePowerUpSpawner : MonoBehaviour {
    public GameObject powerupPrefab;
    public int numberOfPowerups = 3;

    Vector2 powerupPosition = new Vector2(-10, 0);
    GameObject[] powerups;
    float timeSinceSpawned;
    float maxTime = 10f;
    float minTime = 5f;
    float minY = -2f;
    float maxY = 2f;
    float spawnRate;
    float spawnXPosition = 8f;
    float spawnYPosition;
    int current;

    void Start() {
        powerups = new GameObject[numberOfPowerups];

        for (int i = 0; i < numberOfPowerups; i++) {
            powerups[i] = Instantiate(powerupPrefab, powerupPosition, Quaternion.identity);
        }
        spawnRate = Random.Range(minTime, maxTime);
    }

    void Update() {
        timeSinceSpawned += Time.deltaTime;
        if (!GameController.instance.gameOver && timeSinceSpawned >= spawnRate) {
            spawnYPosition = Random.Range(minY, maxY);
            timeSinceSpawned = 0f;
            powerups[current].transform.position = new Vector2(spawnXPosition, spawnYPosition);
            current++;

            if (current >= numberOfPowerups) {
                current = 0;
            }
        }
    }
}

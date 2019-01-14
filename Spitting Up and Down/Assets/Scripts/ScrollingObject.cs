using UnityEngine;

// Some of this code in this script was taken from one of Unity's tutorials available on their website.

public class ScrollingObject : MonoBehaviour {

    Rigidbody2D rb;
    float timeSinceLastSpeedUp;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(GameController.instance.scrollSpeed, 0);
	}

	void Update () {
        timeSinceLastSpeedUp += Time.deltaTime;
        if (!GameController.instance.gameOver && timeSinceLastSpeedUp >= GameController.instance.speedUpTime) {
            rb.velocity = new Vector2(GameController.instance.scrollSpeed, 0);
            timeSinceLastSpeedUp = 0;
        }
        if (GameController.instance.gameOver) {
            rb.velocity = Vector2.zero;
        }
	}
}

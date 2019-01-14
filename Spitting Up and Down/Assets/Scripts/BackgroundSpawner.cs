using UnityEngine;

// Some of this code in this script was taken from one of Unity's tutorials available on their website.

public class BackgroundSpawner : MonoBehaviour {
    float backgroundLength = 64.0f;
    bool nextGround;

	void Update () {
        if(transform.position.x < -backgroundLength) {
            MoveBackground();
        }
	}

    void MoveBackground() {
        Vector2 groundoffset = new Vector2(backgroundLength * 2.0f, 0);
        transform.position = (Vector2)transform.position + groundoffset;
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DragonMovement : MonoBehaviour {
    public float upSpeed;
    public GameObject player;
    public GameObject tilemapGameObject;
    public Slider shieldBar;
    public AudioClip daggerHit;
    public AudioClip shieldTake;
    public AudioClip deadSound;
    public AudioClip takeCoin;

    bool extraLife;
    bool shieldTaken;
    Animator anim;
    Rigidbody2D rb;
    float timer;
    float shieldDuration = 1f;
    AudioSource audioSource;

    void Start () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        shieldBar.gameObject.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    void LateUpdate() {
        if(Input.GetMouseButton(0) && !GameController.instance.gameOver) {
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(0, upSpeed));
        }
    }

    void Update() {
        timer += Time.deltaTime;
        if(shieldTaken && timer >= 0.8f) {
            shieldDuration -= 0.05f;
            shieldBar.value = shieldDuration;
            timer = 0;
            if(shieldDuration <= 0) {
                shieldBar.gameObject.SetActive(false);
                shieldTaken = false;
                extraLife = false;
                anim.SetBool("gotDagger", false);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {

        if(extraLife && collision.rigidbody.tag == "Obstacle") {
            collision.gameObject.SetActive(false);
            shieldBar.gameObject.SetActive(false);
            shieldDuration = 1f;
            audioSource.PlayOneShot(daggerHit, 0.8f);
            StartCoroutine("SmallDelay");
            StartCoroutine("SetObstaclesBack", collision.gameObject);
        }
        if(extraLife && collision.rigidbody.tag == "Ground") {
            shieldBar.gameObject.SetActive(false);
            shieldDuration = 1f;
            StartCoroutine("SmallDelay");
        }
    
        if (collision.rigidbody.tag == "Obstacle" && !extraLife) {
            anim.SetTrigger("dead");
            audioSource.PlayOneShot(deadSound, 0.5f);
            GameController.instance.GameOver();
        }

        if (collision.rigidbody.tag == "Ground" && !extraLife) {
            anim.SetTrigger("dead");
            audioSource.PlayOneShot(deadSound, 0.5f);
            GameController.instance.GameOver();
        }
        if (collision.rigidbody.tag == "Shield") {
            extraLife = true;
            anim.SetBool("gotDagger", true);
            audioSource.PlayOneShot(shieldTake, 0.8f);
            collision.gameObject.SetActive(false);
            shieldBar.gameObject.SetActive(true);
            shieldTaken = true;
            shieldDuration = 1f;
            StartCoroutine("SetObstaclesBack", collision.gameObject);
        }

        if (collision.rigidbody.tag == "Powerup") {
            collision.gameObject.SetActive(false);
            audioSource.PlayOneShot(takeCoin, 1f);
            GameController.instance.bonusPoint += 10;
            StartCoroutine("SetObstaclesBack", collision.gameObject);
        }
    }

    IEnumerator SmallDelay() {
        anim.SetBool("gotDagger", false);
        yield return new WaitForSeconds(0.5f);
        extraLife = false;
    }

    IEnumerator SetObstaclesBack(GameObject collidedObject) {
        collidedObject.transform.position = new Vector2(-10, 0);
        yield return new WaitForSeconds(4f);
        collidedObject.SetActive(true);
    }
}
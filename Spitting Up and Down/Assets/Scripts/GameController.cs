using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

/*
* Restart and home image made by: http://pancakebobapps.wix.com/apps
* 
* Music credit to composer Andrea Baroni: https://www.patreon.com/andreabaroni
*/

public class GameController : MonoBehaviour {

    public static GameController instance;
    public event System.Action<int> OnAddedScore;
    public float scrollSpeed = -1.5f;
    public int speedUpTime = 15;
    public bool gameOver;
    public bool shieldTaken;
    public Button restartBtn;
    public GameObject panel;
    public Text myScore;
    public Text bestScore;
    public Text bonusText;
    public Text coinBonus;
    public int bonusPoint;

    int score;
    int newScore;
    float timer;
    float scoreTimer;

    void Awake() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != null) {
            Destroy(gameObject);
        }
    }

    void Start() {
        panel.SetActive(false);
        coinBonus.enabled = true;
    }

    void Update() {
        timer += Time.deltaTime;
        scoreTimer += Time.deltaTime;
        if (!gameOver && timer >= speedUpTime) {
            scrollSpeed -= 0.2f;
            timer = 0;
        }
        if(!gameOver && scoreTimer >= 0.5f) {
            AddScore();
            scoreTimer = 0;
        }
        coinBonus.text = bonusPoint.ToString();
    }

    void AddScore() {
        score++;
        OnAddedScore(score);
    }

    public void GameOver() {
        gameOver = true;
        newScore = score + bonusPoint;
        if (score > PlayerPrefs.GetInt("BestScore", 0)) {
            PlayerPrefs.SetInt("BestScore", score);
        }
        bonusText.text = bonusPoint.ToString();
        bestScore.text = PlayerPrefs.GetInt("BestScore", 0).ToString();
        myScore.text = score.ToString();
        if (bonusPoint > 0) {
            StartCoroutine("NewScore", newScore);
        }
        panel.SetActive(true);
        coinBonus.enabled = false;
    }

    IEnumerator NewScore(int scoreAfter) {
        yield return new WaitForSeconds(2f);
        if (scoreAfter > PlayerPrefs.GetInt("BestScore", 0)) {
            PlayerPrefs.SetInt("BestScore", scoreAfter);
        }
        myScore.text = scoreAfter.ToString();
        bestScore.text = PlayerPrefs.GetInt("BestScore", 0).ToString();
        bonusText.text = "0";
    }

    public void Restart() {
        SceneManager.LoadScene("Level 1");
    }

    public void StartLevel() {
        SceneManager.LoadScene("Level 1");
    }

    public void GoBackHome() {
        SceneManager.LoadScene("Home");
    }
}

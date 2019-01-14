using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    public Text scoreBoard;
    public Image coin;
    public GameController gameController;

    private void Start()
    {
        scoreBoard.enabled = true;
        coin.enabled = true;
    }

    private void OnEnable()
    {
        gameController.OnAddedScore += ScoreChanged;
    }

    private void OnDisable()
    {
        gameController.OnAddedScore -= ScoreChanged;
    }

    void ScoreChanged(int score)
    {
        scoreBoard.text = score.ToString();
    }

    private void Update()
    {
        if(GameController.instance.gameOver){
            scoreBoard.enabled = false;
            coin.enabled = false;
        } 
    }
}

using System.Collections;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestText;
    private int score;
    public TileBoard board;
    public CanvasGroup gameOver;
    public CanvasGroup gameWin;
    public TileState[] TileState;
    private void Start()
    {
        NewGame();
    }
    public void NewGame()
    {
        SetScore(0);
        bestText.text = LoadHighScore().ToString();
        gameOver.alpha = 0;
        gameWin.alpha = 0;
        gameWin.gameObject.SetActive(false);
        gameOver.gameObject.SetActive(false);
        board.ClearBoared();
        board.CreateTile();
        board.CreateTile();
        board.enabled = true;
    }
    public void ResumeGame()
    {
        gameWin.alpha = 0;
        gameWin.gameObject.SetActive(false);
        board.enabled = true;
    }
    public void GameOver()
    {
        board.enabled=false;
        StartCoroutine(Fade(gameOver, 1f, 1f));
        gameOver.gameObject.SetActive(true);

    }
    private IEnumerator Fade(CanvasGroup canvasGroup, float to, float delay)
    {
        yield return new WaitForSeconds(delay);

        float elapsed = 0f;
        float duration = 0.1f;
        float from = canvasGroup.alpha;

        while(elapsed < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed/duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = to;
    }

    public void IncreaseScore(int points)
    {
        SetScore(score + points);
    }
    private void SetScore(int score)
    {
        this.score = score; 
        scoreText.text = score.ToString();

        SaveBestScore();
    }

    private void SaveBestScore()
    {
        int hScore = LoadHighScore();
        if (score > hScore) 
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }
    private int LoadHighScore()
    {
        return PlayerPrefs.GetInt("HighScore",0); 
    }

    public void YouWin()
    {
        board.enabled = false;
        StartCoroutine(Fade(gameWin, 1f, 1f));
        gameWin.gameObject.SetActive(true);
    }
}

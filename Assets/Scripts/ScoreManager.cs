using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int Score { get; private set; }
    public int HighScore { get; private set; }

    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Load the saved high score
        HighScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    public void AddScore()
    {
        // Do not update the score after Game Over
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver)
        {
            return;
        }

        Score++;

        // Update high score
        if (Score > HighScore)
        {
            HighScore = Score;

            PlayerPrefs.SetInt("HighScore", HighScore);
            PlayerPrefs.Save();
        }

        if (scoreText != null)
        {
            scoreText.text = "Score: " + Score;
        }

        Debug.Log("Score: " + Score);
        Debug.Log("High Score: " + HighScore);
    }
}
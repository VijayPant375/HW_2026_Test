using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int Score { get; private set; }
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
    }

    public void AddScore()
    {
        // Do not update the score after Game Over
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver)
        {
            return;
        }

        Score++;

        if (scoreText != null)
        {
            scoreText.text = "Score: " + Score;
        }

        Debug.Log("Score: " + Score);
    }
}
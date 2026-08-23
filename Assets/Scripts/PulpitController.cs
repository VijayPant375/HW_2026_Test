using UnityEngine;
using TMPro;

public class PulpitController : MonoBehaviour
{
    [SerializeField] private TextAsset configFile;
    [SerializeField] private TextMeshProUGUI timerText;

    private float destroyTime;
    private float remainingTime;

    private void Start()
    {
        GameConfig config = JsonUtility.FromJson<GameConfig>(configFile.text);

        destroyTime = Random.Range(
            config.pulpit_data.min_pulpit_destroy_time,
            config.pulpit_data.max_pulpit_destroy_time
        );

        remainingTime = destroyTime;

        if (timerText != null)
        {
            timerText.gameObject.SetActive(false);
        }

        UpdateTimerText();
    }

    private void Update()
    {
        // Stop and hide the timer after Game Over
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver)
        {
            if (timerText != null)
            {
                timerText.gameObject.SetActive(false);
            }

            return;
        }

        remainingTime -= Time.deltaTime;

        if (remainingTime <= 0f)
        {
            remainingTime = 0f;

            UpdateTimerText();

            Destroy(gameObject);

            return;
        }

        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        if (timerText != null)
        {
            timerText.text = remainingTime.ToString("F1");
        }
    }

    public void SetTimerVisible(bool visible)
    {
        if (timerText != null)
        {
            timerText.gameObject.SetActive(visible);
        }
    }

    [System.Serializable]
    private class GameConfig
    {
        public PulpitData pulpit_data;
    }

    [System.Serializable]
    private class PulpitData
    {
        public float min_pulpit_destroy_time;
        public float max_pulpit_destroy_time;
        public float pulpit_spawn_time;
    }
}
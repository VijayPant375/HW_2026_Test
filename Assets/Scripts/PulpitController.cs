using UnityEngine;
using TMPro;

public class PulpitController : MonoBehaviour
{
    [SerializeField] private TextAsset configFile;
    [SerializeField] private TextMeshProUGUI timerText;

    private float destroyTime;
    private float remainingTime;

    private Renderer pulpitRenderer;

    // Fixed color palette for pulpits
    private static readonly Color[] pulpitColors =
    {
        new Color(0.25f, 0.55f, 0.95f), // Blue
        new Color(0.30f, 0.75f, 0.40f), // Green
        new Color(0.95f, 0.65f, 0.20f), // Orange
        new Color(0.65f, 0.40f, 0.85f), // Purple
        new Color(0.90f, 0.35f, 0.35f)  // Red
    };

    private static int nextColorIndex = 0;

    private void Start()
    {
        GameConfig config =
            JsonUtility.FromJson<GameConfig>(configFile.text);

        destroyTime = Random.Range(
            config.pulpit_data.min_pulpit_destroy_time,
            config.pulpit_data.max_pulpit_destroy_time
        );

        remainingTime = destroyTime;

        // Get the pulpit's renderer
        pulpitRenderer = GetComponent<Renderer>();

        // Give this pulpit its own color
        if (pulpitRenderer != null)
        {
            pulpitRenderer.material.color =
                pulpitColors[nextColorIndex];

            nextColorIndex =
                (nextColorIndex + 1) % pulpitColors.Length;
        }

        // Hide timer until Doofus is standing on this pulpit
        if (timerText != null)
        {
            timerText.gameObject.SetActive(false);
        }

        UpdateTimerText();
    }

    private void Update()
    {
        // Stop and hide timer after Game Over
        if (GameManager.Instance != null &&
            GameManager.Instance.IsGameOver)
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
            timerText.text =
                remainingTime.ToString("F1");
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
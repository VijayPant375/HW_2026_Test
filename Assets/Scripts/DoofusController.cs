using UnityEngine;
using UnityEngine.InputSystem;

public class DoofusController : MonoBehaviour
{
    [SerializeField] private TextAsset configFile;

    private float speed;
    private GameObject currentPulpit;
    private bool fallDetected = false;

    private void Start()
    {
        GameConfig config = JsonUtility.FromJson<GameConfig>(configFile.text);
        speed = config.player_data.speed;
    }

    private void Update()
    {
        // Stop movement after a fall has been detected
        if (fallDetected)
            return;

        Vector2 input = Vector2.zero;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
                input.x -= 1;

            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
                input.x += 1;

            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
                input.y -= 1;

            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
                input.y += 1;
        }

        Vector3 movement = new Vector3(input.x, 0f, input.y).normalized;

        transform.position += movement * speed * Time.deltaTime;

        CheckPulpit();
    }

    private void CheckPulpit()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.4f);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Pulpit"))
            {
                GameObject pulpit = collider.gameObject;

                // Player has moved onto a new pulpit
                if (pulpit != currentPulpit)
                {
                    // Hide timer on the previous pulpit
                    if (currentPulpit != null)
                    {
                        PulpitController previousController =
                            currentPulpit.GetComponent<PulpitController>();

                        if (previousController != null)
                        {
                            previousController.SetTimerVisible(false);
                        }
                    }

                    // Update current pulpit
                    currentPulpit = pulpit;

                    // Show timer on the pulpit Doofus is standing on
                    PulpitController currentController =
                        currentPulpit.GetComponent<PulpitController>();

                    if (currentController != null)
                    {
                        currentController.SetTimerVisible(true);
                    }

                    // Increase score when landing on a new pulpit
                    if (ScoreManager.Instance != null)
                    {
                        ScoreManager.Instance.AddScore();
                    }
                }

                return;
            }
        }

        // Doofus is not standing on any pulpit
        fallDetected = true;

        Debug.Log("Doofus fell!");

        if (GameManager.Instance != null)
        {
            GameManager.Instance.GameOver();
        }
    }

    [System.Serializable]
    private class GameConfig
    {
        public PlayerData player_data;
    }

    [System.Serializable]
    private class PlayerData
    {
        public float speed;
    }
}
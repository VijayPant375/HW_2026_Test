using UnityEngine;
using UnityEngine.InputSystem;

public class DoofusController : MonoBehaviour
{
    [SerializeField] private TextAsset configFile;

    private float speed;
    private GameObject currentPulpit;

    private void Start()
    {
        GameConfig config = JsonUtility.FromJson<GameConfig>(configFile.text);
        speed = config.player_data.speed;
    }

    private void Update()
    {
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

    private void CheckPulpit()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.4f);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Pulpit"))
            {
                GameObject pulpit = collider.gameObject;

                if (pulpit != currentPulpit)
                {
                    currentPulpit = pulpit;

                    if (ScoreManager.Instance != null)
                    {
                        ScoreManager.Instance.AddScore();
                    }
                }

                return;
            }
        }
    }
}
using System.Collections;
using UnityEngine;

public class PulpitSpawner : MonoBehaviour
{
    [System.Serializable]
    public class PulpitData
    {
        public float min_pulpit_destroy_time;
        public float max_pulpit_destroy_time;
        public float pulpit_spawn_time;
    }

    [System.Serializable]
    public class DiaryData
    {
        public PulpitData pulpit_data;
    }

    public GameObject pulpitPrefab;
    public TextAsset configFile;

    private float spawnTime;
    private Transform lastPulpit;

    private void Start()
    {
        LoadConfig();

        GameObject existingPulpit =
            GameObject.FindGameObjectWithTag("Pulpit");

        if (existingPulpit != null)
        {
            lastPulpit = existingPulpit.transform;
        }

        StartCoroutine(SpawnPulpits());
    }

    private void LoadConfig()
    {
        DiaryData diary =
            JsonUtility.FromJson<DiaryData>(configFile.text);

        spawnTime = diary.pulpit_data.pulpit_spawn_time;
    }

    private IEnumerator SpawnPulpits()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);

            // Stop spawning when the game is over
            if (GameManager.Instance != null &&
                GameManager.Instance.IsGameOver)
            {
                yield break;
            }

            // Only allow two pulpits to exist at once
            if (GameObject.FindGameObjectsWithTag("Pulpit").Length < 2)
            {
                SpawnNextPulpit();
            }
        }
    }

    private void SpawnNextPulpit()
    {
        Vector3 spawnPosition;

        if (lastPulpit == null)
        {
            spawnPosition = Vector3.zero;
        }
        else
        {
            Vector3 direction = GetDoofusMovementDirection();

            // Pulpits are 9x9, so place the next one adjacent.
            spawnPosition =
                lastPulpit.position + direction * 9f;
        }

        GameObject newPulpit = Instantiate(
            pulpitPrefab,
            spawnPosition,
            Quaternion.identity
        );

        newPulpit.tag = "Pulpit";

        lastPulpit = newPulpit.transform;
    }

    private Vector3 GetDoofusMovementDirection()
    {
        GameObject doofus = GameObject.FindGameObjectWithTag("Player");

        if (doofus != null)
        {
            DoofusController controller =
                doofus.GetComponent<DoofusController>();

            if (controller != null)
            {
                Vector3 movementDirection =
                    controller.CurrentMovementDirection;

                // Convert the movement direction to one of
                // the four valid pulpit directions.
                if (Mathf.Abs(movementDirection.x) >
                    Mathf.Abs(movementDirection.z))
                {
                    return movementDirection.x > 0
                        ? Vector3.right
                        : Vector3.left;
                }
                else
                {
                    return movementDirection.z > 0
                        ? Vector3.forward
                        : Vector3.back;
                }
            }
        }

        // Safe fallback if Doofus cannot be found.
        return Vector3.forward;
    }
}
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
            Vector3[] directions =
            {
                Vector3.forward,
                Vector3.back,
                Vector3.left,
                Vector3.right
            };

            Vector3 direction =
                directions[Random.Range(0, directions.Length)];

            // Pulpits are 9x9, so place the next one adjacent
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
}
using UnityEngine;

public class PulpitController : MonoBehaviour
{
    [SerializeField] private TextAsset configFile;

    private float destroyTime;

    private void Start()
    {
        GameConfig config = JsonUtility.FromJson<GameConfig>(configFile.text);

        destroyTime = Random.Range(
            config.pulpit_data.min_pulpit_destroy_time,
            config.pulpit_data.max_pulpit_destroy_time
        );

        Destroy(gameObject, destroyTime);
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
using UnityEngine;
using UnityEngine.AI;

namespace BP.Almighty
{
    public class AIPlayer : MonoBehaviour
    {
        [SerializeField] private Vector2 arenaDims = new Vector2(30f, 30f);
        private Player player;

        //testing stuff until AI is sorted
        [SerializeField] private float spawnInterval = 1f;
        private float timeToNextSpawn;

        private void Awake()
        {
            player = GetComponent<Player>();
        }

        private void Start()
        {
            UpdateSpawnTime();
        }

        private void Update()
        {
            if(Time.time > timeToNextSpawn)
            {
                AttemptClickAction();
                UpdateSpawnTime();
            }
        }

        private void UpdateSpawnTime()
        {
            timeToNextSpawn = Time.time + spawnInterval;
        }

        private void AttemptClickAction()
        {
            float rndx = Random.Range(-0.5f * arenaDims.x, 0.5f * arenaDims.x);
            float rndz = Random.Range(-0.5f * arenaDims.y, 0.5f * arenaDims.y);
            Vector3 pos = new Vector3(rndx, 0f, rndz);

            player.TryClickAction(pos);
        }
    }
}



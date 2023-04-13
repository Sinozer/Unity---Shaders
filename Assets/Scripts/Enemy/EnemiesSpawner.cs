using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemiesSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab;

        private float _spawnDelay;
        private float _countTime;

        private int _enemiesSpawned;

        private void Start()
        {
            DontDestroyOnLoad(this);
            _spawnDelay = 2f;
        }

        private void Update()
        {
            _countTime += Time.deltaTime;
            if (!(_countTime >= _spawnDelay)) return;

            NavMesh.SamplePosition(GeneratePosition(), out var hit, Mathf.Infinity, 1);
            Instantiate(enemyPrefab, hit.position, Quaternion.identity);
            _countTime = 0;
            _enemiesSpawned++;
        }

        private static Vector3 GeneratePosition() => new(Random.Range(5, 45), Random.Range(5, 7), Random.Range(5, 45));
    }
}
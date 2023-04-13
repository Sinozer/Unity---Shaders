using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Enemy {
	public class EnemiesSpawner : MonoBehaviour {
		[SerializeField] private GameObject enemyPrefab;

		private float _spawnDelay;
		private float _countTime;

		private int _enemiesSpawned;
	
		// Start is called before the first frame update
		void Start() {
			DontDestroyOnLoad(this);
			_spawnDelay = 2f;
		}

		// Update is called once per frame
		void Update() {
			_countTime += Time.deltaTime;
			if (_countTime >= _spawnDelay) {
				NavMeshHit hit;
				NavMesh.SamplePosition(GeneratePosition(),out hit, Mathf.Infinity,1);
				Instantiate(enemyPrefab, hit.position, Quaternion.identity);
				_countTime = 0;
				_enemiesSpawned++;
			}
		}

		private Vector3 GeneratePosition() {
			return new Vector3(Random.Range(5, 45), Random.Range(5, 7), Random.Range(5, 45));
		}
	}
}

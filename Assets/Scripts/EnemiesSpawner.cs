using UnityEngine;

public class EnemiesSpawner : MonoBehaviour {
	public Transform player;
	private GameObject _enemyPrefab;
	
	// Start is called before the first frame update
	void Start() {

	}

	// Update is called once per frame
	void Update() {

	}

	private Vector3 GeneratePosition() {
		return new Vector3(Random.Range(0, 75), Random.Range(5, 10), Random.Range(0, 75));
	}
}

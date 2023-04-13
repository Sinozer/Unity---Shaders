using UnityEngine;

namespace Player {
	public class PlayerManager : MonoBehaviour {
		[SerializeField] private int maxHealth;

		struct Levels {
			public int _currentLevel;
			public int _currentXp;
			public int _xpToNextLvl;
		}

		private Levels _levels;

		private int _currentHealth;
	
		// Start is called before the first frame update
		void Start() {
			_currentHealth = maxHealth;
			_levels._currentLevel = 1;
			_levels._currentXp = 0;
			_levels._xpToNextLvl = 1 * 10;
		}

		// Update is called once per frame
		void Update() {
			if (_currentHealth <= 0) {
				Debug.Log("Dead");
				GameMan.Instance.EndGame();
			}
		}

		private void OnTriggerEnter(Collider other) {
			Debug.Log("Col");
		
			if (other.gameObject.CompareTag("Enemy")) {
				_currentHealth -= 10;
			}
		}
	}
}

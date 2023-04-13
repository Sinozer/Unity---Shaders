using System;
using UnityEngine;

namespace Player {
	public class PlayerManager : MonoBehaviour {
        public static PlayerManager Instance;

        [SerializeField] private int maxHealth;

		public int BloodGauge { get; set; }

		struct Levels {
			public int _currentLevel;
			public int _currentXp;
			public int _xpToNextLvl;
		}

		private Levels _levels;

		private int _currentHealth;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

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
			//Debug.Log("Col");
			bool isElite = Convert.ToBoolean(other.GetComponent<Renderer>().materials[1].GetInteger("_Elite"));
		
			if (other.gameObject.CompareTag("Enemy")) {
				_currentHealth -= isElite ? 20 : 10;
			}
		}
	}
}

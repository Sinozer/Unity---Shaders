using UnityEngine;
using UnityEngine.AI;

namespace Enemy {
    public class Enemy : MonoBehaviour {
        public Transform player;

        private NavMeshAgent _agent;
        private bool _isInvisible;

        private int _currentHealth;

        private void Start() {
            _currentHealth = 10;
            _agent = GetComponent<NavMeshAgent>();
            player = GameObject.Find("PlayerCorps").transform;

            //InvisibilitySpell.OnInvisibility += SetInvisiblity;
        }

        private void Update() {
            if (!_isInvisible) {
               // _agent.SetDestination(player.position);
            }

            if (_currentHealth <= 0) {
                Destroy(gameObject);                
            }
        }

        private void SetInvisiblity(bool value) {
            _isInvisible = value;
        }

        private void OnCollisionEnter(Collision other) {
            if (other.gameObject.CompareTag("Bullet")) {
                _currentHealth -= 5;
            }
        }
    }
}
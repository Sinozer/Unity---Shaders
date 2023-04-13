using System;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy {
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private SkinnedMeshRenderer _mesh;
        public Transform player;

        private NavMeshAgent _agent;
        private bool _isInvisible;
        private bool _isElite;
        private int _currentHealth;

        private void Start() {
            _agent = GetComponent<NavMeshAgent>();
            player = GameObject.Find("PlayerArmature").transform;

            //InvisibilitySpell.OnInvisibility += SetInvisiblity;

            int random = UnityEngine.Random.Range(1, 100);
            _isElite = random <= 5;

            _mesh.materials[1].SetInteger("_Elite", Convert.ToInt32(_isElite));

            _currentHealth = _isElite ? 20 : 10;
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
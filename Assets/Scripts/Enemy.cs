using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
    public Transform player;
    
    private NavMeshAgent _agent;

    private int _currentHealth;

    private void Start() {
        _currentHealth = 5;
        _agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("PlayerArmature").transform;
    }

    private void Update() {
        _agent.SetDestination(player.position);
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Touch");
		
        if (other.gameObject.CompareTag("Spell")) {
            _currentHealth -= 5;
        }
    }
}

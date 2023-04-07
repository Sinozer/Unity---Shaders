using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour {
    public Transform player;

    private NavMeshAgent _agent;

    private void Start() {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update() {
        _agent.SetDestination(player.position);
    }
}

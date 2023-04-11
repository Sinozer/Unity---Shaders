using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour {
    public Transform player;

    private NavMeshAgent _agent;
    private bool _isInvisible;

    private void Start() {
        _agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("PlayerArmature").transform;

        InvisibilitySpell.OnInvisibility += SetInvisiblity;
    }

    private void Update() {
        if (!_isInvisible)
        {
            _agent.SetDestination(player.position);
        }
    }

    private void SetInvisiblity(bool value)
    {
        _isInvisible = value;
    }
}

using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private Material _frozenMaterial;

    public enum EnemyState
    {
        IDLE,
        CHASING,
        FROZEN
    }
    public EnemyState State { get => _state; }
    private EnemyState _state = EnemyState.IDLE;
    
    public void SetState(EnemyState state)
    {
        _state = state;
        UpdateState();
    }

    private void UpdateState()
    {
        switch (State)
        {
            case EnemyState.IDLE:
                break;
            case EnemyState.CHASING:
                _agent.enabled = true;
                _animator.enabled = true;
                _mesh.materials[2] = null;
                break;
            case EnemyState.FROZEN:
                _agent.enabled = false;
                _animator.enabled = false;
                _mesh.materials[2] = _frozenMaterial;
                break;
        }
    }

    [SerializeField] private SkinnedMeshRenderer _mesh;
    public Transform player;
    
    private NavMeshAgent _agent;
    private bool _isInvisible;
    public bool IsElite { get => _isElite; }
    private bool _isElite;
    private int _currentHealth;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("PlayerArmature").transform;

        //InvisibilitySpell.OnInvisibility += SetInvisiblity;

        int random = UnityEngine.Random.Range(1, 100);
        _isElite = random <= 5;

        _mesh.materials[1].SetInteger("_Elite", Convert.ToInt32(_isElite));

        _currentHealth = _isElite ? 20 : 10;
    }

    private void Update()
    {
        switch (State)
        {
            case EnemyState.IDLE:
                break;
            case EnemyState.CHASING:
                _agent.SetDestination(player.position);
                break;
            case EnemyState.FROZEN:
                break;
        }

        // TODO Rajouter le state pour invisible

        //if (!_isInvisible)
        //{
        //    _agent.SetDestination(player.position);
        //}
    }

    private void SetInvisiblity(bool value)
    {
        _isInvisible = value;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            _currentHealth -= 5;
        }
    }
}

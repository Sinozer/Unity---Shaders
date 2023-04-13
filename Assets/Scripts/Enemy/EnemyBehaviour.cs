using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private Material _dissolveMaterial;
    [SerializeField] private Material _frozenMaterial;

    private Material _material;

    private float _dieTimer = 3f;
    private readonly float _frozenTimerRef = 3f;
    private float _frozenTimer = 3f;

    public enum EnemyState
    {
        IDLE,
        CHASING,
        FROZEN,
        DIE
    }
    public EnemyState State { get => _state; }
    private EnemyState _state = EnemyState.CHASING;

    public void SetState(EnemyState state)
    {
        if (_state == state) return;
        _state = state;
        UpdateState();
    }

    private void UpdateState()
    {
        switch (State)
        {
            case EnemyState.IDLE:
                _agent.enabled = false;
                _animator.enabled = false;
                break;
            case EnemyState.CHASING:
                _agent.enabled = true;
                _animator.enabled = true;

                if (_mesh.materials.Length < 3) break;

                List<Material> temp = _mesh.materials.ToList();
                temp.RemoveAt(temp.Count - 1);
                _mesh.materials = temp.ToArray();

                break;
            case EnemyState.FROZEN:
                _agent.enabled = false;
                _animator.enabled = false;

                List<Material> mats = _mesh.materials.ToList();
                mats.Add(_frozenMaterial);
                _mesh.materials = mats.ToArray();

                _frozenTimer = _frozenTimerRef;
                break;
            case EnemyState.DIE:
                _agent.enabled = false;
                _animator.enabled = false;
                GetComponent<CapsuleCollider>().enabled = false;

                List<Material> tempDie = new()
                {
                    _dissolveMaterial
                };
                _mesh.materials = tempDie.ToArray();
                break;
        }
    }

    [SerializeField] private SkinnedMeshRenderer _mesh;
    public Transform player;

    private NavMeshAgent _agent;
    public bool IsElite { get => _isElite; }
    private bool _isElite;
    private int _currentHealth;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("PlayerArmature").transform;

        int random = UnityEngine.Random.Range(1, 100);
        _isElite = random <= 5;

        _mesh.materials[1].SetInteger("_Elite", Convert.ToInt32(_isElite));

        _currentHealth = _isElite ? 20 : 10;

        UpdateState();
    }

    private void OnDestroy()
    {
        PlayerManager.Instance.BloodGauge += 5;
    }

    private void Update()
    {
        if (_currentHealth <= 0f)
            SetState(EnemyState.DIE);

        if (PlayerManager.Instance.IsInvisible)
            SetState(EnemyState.IDLE);
        else if (!PlayerManager.Instance.IsInvisible)
            SetState(EnemyState.CHASING);

        switch (State)
        {
            case EnemyState.IDLE:
                break;
            case EnemyState.CHASING:
                _agent.SetDestination(player.position);
                break;
            case EnemyState.FROZEN:
                _frozenTimer -= Time.deltaTime;

                if (_frozenTimer <= 0f)
                    SetState(EnemyState.CHASING);
                break;
            case EnemyState.DIE:
                _dieTimer -= Time.deltaTime;

                _mesh.material.SetFloat("_Dissolve_Amount", (3f - _dieTimer) / 3f);

                if (_dieTimer <= 0f)
                    Destroy(gameObject);
                break;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            _currentHealth -= 5;
        }
    }
}

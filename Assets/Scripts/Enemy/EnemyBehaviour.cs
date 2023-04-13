using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public enum EnemyState
    {
        IDLE,
        CHASING,
        FROZEN,
        DIE
    }

    public bool IsElite { get; private set; }
    public EnemyState State { get; private set; } = EnemyState.CHASING;
    public Transform player;
    
    public void SetState(EnemyState state)
    {
        if (State == state) return;
        State = state;
        UpdateState();
    }

    [SerializeField] private Material _dissolveMaterial;
    [SerializeField] private Material _frozenMaterial;

    [SerializeField] private SkinnedMeshRenderer _mesh;

    private Animator _animator;
    private Material _material;

    private float _dieTimer = 2f;
    private const float FROZEN_TIMER_REF = 3f;
    private float _frozenTimer = 3f;

    private NavMeshAgent _agent;

    private int _currentHealth;
    private static readonly int DissolveAmount = Shader.PropertyToID("_Dissolve_Amount");
    private static readonly int Elite = Shader.PropertyToID("_Elite");
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("PlayerArmature").transform;

        int random = UnityEngine.Random.Range(1, 100);
        IsElite = random <= 5;

        _mesh.materials[1].SetInteger(Elite, Convert.ToInt32(IsElite));

        _currentHealth = IsElite ? 20 : 10;

        UpdateState();
    }
    
    private void Update()
    {
        if (_currentHealth <= 0f) SetState(EnemyState.DIE);

        switch (State)
        {
            case EnemyState.IDLE:
                CheckInvisibility();

                break;
            case EnemyState.CHASING:
                if (!CheckInvisibility()) _agent.SetDestination(player.position);
                break;
            case EnemyState.FROZEN:
                _frozenTimer -= Time.deltaTime;

                if (_frozenTimer <= 0f) SetState(EnemyState.CHASING);
                break;
            case EnemyState.DIE:
                _dieTimer -= Time.deltaTime;

                _mesh.material.SetFloat(DissolveAmount, (2f - _dieTimer) / 2f);

                if (_dieTimer <= 0f) Destroy(gameObject);
                break;
        }
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
                HandleChasingState();
                break;

            case EnemyState.FROZEN:
                HandleFrozenState();
                break;

            case EnemyState.DIE:
                HandleDieState();
                break;
        }
    }

    private void HandleDieState()
    {
        _agent.enabled = false;
        _animator.enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;

        List<Material> tempDie = new()
        {
            _dissolveMaterial
        };
        _mesh.materials = tempDie.ToArray();
    }

    private void HandleFrozenState()
    {
        _agent.enabled = false;
        _animator.enabled = false;

        List<Material> mats = _mesh.materials.ToList();
        mats.Add(_frozenMaterial);
        _mesh.materials = mats.ToArray();

        _frozenTimer = FROZEN_TIMER_REF;
    }

    private void HandleChasingState()
    {
        _agent.enabled = true;
        _animator.enabled = true;

        if (_mesh.materials.Length < 3) return;

        List<Material> temp = _mesh.materials.ToList();
        temp.RemoveAt(temp.Count - 1);
        _mesh.materials = temp.ToArray();
    }
    
    private void OnDestroy() => PlayerManager.Instance.BloodGauge += 5;
    
    private bool CheckInvisibility()
    {
        if (PlayerManager.Instance.IsInvisible)
        {
            SetState(EnemyState.IDLE);
            return true;
        }

        SetState(EnemyState.CHASING);
        return false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet")) _currentHealth -= 5;
    }
}
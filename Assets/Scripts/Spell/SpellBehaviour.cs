using System;
using UnityEngine;

[Serializable]
public abstract class SpellBehaviour : MonoBehaviour
{
    public enum SpellState
    {
        IDLE,
        COOLDOWN
    }

    public SpellState State => _state;
    public GameObject Player => _player;
    public int Blood => _blood;
    public float Cooldown => _cooldown;

    public Spell SpellRef
    {
        get => _spellRef;
        set => _spellRef = value;
    }

    public void SetState(SpellState state)
    {
        switch (state)
        {
            case SpellState.IDLE:
                _cooldown = 0f;
                break;
            case SpellState.COOLDOWN:
                _cooldown = SpellRef.Cooldown;
                break;
        }

        _state = state;
    }

    public void UpdateState()
    {
        switch (State)
        {
            case SpellState.IDLE:
                break;
            case SpellState.COOLDOWN:
                if (Cooldown > 0) _cooldown -= Mathf.Clamp(Time.deltaTime, 0f, SpellRef.Cooldown);
                break;
        }

        CheckState();
    }

    public virtual bool Use()
    {
        if (Cooldown > 0) return false;
        _cooldown = SpellRef.Cooldown;

        if (_state == SpellState.COOLDOWN) return false;
        SetState(SpellState.COOLDOWN);

        _blood = SpellRef.Blood;

        if (_blood > 0 && PlayerManager.Instance.BloodGauge < _blood) return false;

        PlayerManager.Instance.BloodGauge -= _blood;

        return true;
    }

    [SerializeField] protected Spell _spellRef;

    protected SpellState _state = SpellState.IDLE;

    protected GameObject _player;

    protected void CheckState()
    {
        switch (State)
        {
            case SpellState.IDLE:
                break;
            case SpellState.COOLDOWN:
                if (Cooldown <= 0) SetState(SpellState.IDLE);
                break;
        }
    }

    protected float _cooldown;

    protected int _blood;

    private void Awake()
    {
        SpellRef.Init();
        _player = GameObject.Find("PlayerArmature");
    }

    private void Update()
    {
        UpdateState();
    }
}
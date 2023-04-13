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

    public SpellState State { get => _state; }
    protected SpellState _state = SpellState.IDLE;

    public GameObject Player { get => _player; }
    protected GameObject _player;

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

    public Spell SpellRef
    {
        get => _spellRef;
        set => _spellRef = value;
    }
    [SerializeField] protected Spell _spellRef;

    public float Cooldown { get => _cooldown; }
    protected float _cooldown;

    public int Blood { get => _blood; }
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

    virtual public bool Use()
    {
        if (Cooldown > 0) return false;
        _cooldown = SpellRef.Cooldown;

        if (_state == SpellState.COOLDOWN) return false;
        SetState(SpellState.COOLDOWN);

        _blood = SpellRef.Blood;

        if (_blood > 0 && PlayerManager.Instance.BloodGauge < _blood)
        {
            return false;
        }
        else
        {
            PlayerManager.Instance.BloodGauge -= _blood;
        }

        return true;
    }
}

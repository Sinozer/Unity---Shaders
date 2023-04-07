using System;
using UnityEngine;

[Serializable]
public abstract class SpellBehaviour : MonoBehaviour
{
    public Spell SpellRef
    {
        get => _spellRef;
        set => _spellRef = value;
    }
     [SerializeField] private Spell _spellRef;

    public float Cooldown { get => _cooldown; }
    protected float _cooldown;

    private void Update()
    {
        if (Cooldown > 0) _cooldown -= Mathf.Clamp(Time.deltaTime, 0f, SpellRef.Cooldown);
    }

    virtual public bool Use()
    {
        if (Cooldown > 0) return false;
        _cooldown = SpellRef.Cooldown;
        return true;
    }
}

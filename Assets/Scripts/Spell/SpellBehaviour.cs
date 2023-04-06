using UnityEngine;

abstract public class SpellBehaviour : MonoBehaviour
{
    public Spell SpellRef
    {
        get => _spellRef;
        set => _spellRef = value;
    }
     [SerializeField] private Spell _spellRef;

    public float Cooldown { get => _cooldown; }
    private float _cooldown;

    public abstract void Use();
}

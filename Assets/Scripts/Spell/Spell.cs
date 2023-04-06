using UnityEngine;

[CreateAssetMenu(fileName = "New Spell", menuName = "Spell")]
public class Spell : ScriptableObject
{
    public Sprite Icon { get => _icon; }
    [SerializeField] private Sprite _icon;

    public string Name { get => _name; }
    [SerializeField] private string _name;

    public string Description { get => _description; }
    [SerializeField] private string _description;

    public float Cooldown { get => _cooldown; }
    [SerializeField] private float _cooldown;

    public float Damage { get => _damage; }
    [SerializeField] private float _damage;

    public float Range { get => _range; }
    [SerializeField] private float _range;

    public KeyCode Key { get => _key; }
    [SerializeField] private KeyCode _key;
}
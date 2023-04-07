using UnityEngine;

[CreateAssetMenu(fileName = "New Spell", menuName = "Spell")]
public class Spell : ScriptableObject
{
    public Sprite Icon { get => _icon; }
    [SerializeField] private Sprite _icon;

    public string Name { get => _name; }
    [SerializeField] private string _name = "Default Name";

    public string Description { get => _description; }
    [SerializeField] private string _description = "Default Description";

    public float Cooldown { get => _cooldown; }
    [SerializeField] private float _cooldown = 15f;

    public float Damage { get => _damage; }
    [SerializeField] private float _damage = 20f;

    public float Range { get => _range; }
    [SerializeField] private float _range = 10f;

    // /!\ QWERTY KEYBOARD /!\
    public KeyCode Key { get => _key; }
    [SerializeField] private KeyCode _key = KeyCode.X;
}
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spell", menuName = "Spell")]
public class Spell : ScriptableObject
{
    public Sprite Icon => _icon;
    public string Name => _name;
    public string Description => _description;
    public float Cooldown => _cooldown;
    public float LastDuration => _lastDuration;
    public float Damage => _damage;
    public float Range => _range;
    public int Blood => _blood;

    public Dictionary<string, GameObject> GameObjects = new();

    // /!\ QWERTY KEYBOARD /!\
    public KeyCode Key => _key;

    public void Init()
    {
        foreach (var gameObject in _gameObjects)
            GameObjects[gameObject.key] = gameObject.value;
    }

    [Serializable]
    private class SGameObject
    {
        public string key;
        public GameObject value;
    }

    [SerializeField] private string _name = "Default Name";
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _description = "Default Description";

    [SerializeField] private float _cooldown = 15f;
    [SerializeField] private float _lastDuration = 5f;
    [SerializeField] private float _damage = 20f;
    [SerializeField] private float _range = 10f;
    [SerializeField] private int _blood = 0;

    [SerializeField] private List<SGameObject> _gameObjects = new();

    [SerializeField] private KeyCode _key = KeyCode.X;
}
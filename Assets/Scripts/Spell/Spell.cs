using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "New Spell", menuName = "Spell")]
public class Spell : ScriptableObject
{
    [Serializable]
    private class SGameObject
    {
        public string key;
        public GameObject value;
    }

    public Sprite Icon { get => _icon; }
    [SerializeField] private Sprite _icon;

    public string Name { get => _name; }
    [SerializeField] private string _name = "Default Name";

    public string Description { get => _description; }
    [SerializeField] private string _description = "Default Description";

    public float Cooldown { get => _cooldown; }
    [SerializeField] private float _cooldown = 15f;

    public float LastDuration { get => _lastDuration; }
    [SerializeField] private float _lastDuration = 5f;

    public float Damage { get => _damage; }
    [SerializeField] private float _damage = 20f;

    public float Range { get => _range; }
    [SerializeField] private float _range = 10f;

    public int Blood { get => _blood; }
    [SerializeField] private int _blood = 0;

    [SerializeField] private List<SGameObject> _gameObjects = new();
    public Dictionary<string, GameObject> GameObjects = new();

    // /!\ QWERTY KEYBOARD /!\
    public KeyCode Key { get => _key; }
    [SerializeField] private KeyCode _key = KeyCode.X;

    public void Init()
    {
        foreach (var gameObject in _gameObjects)
            GameObjects[gameObject.key] = gameObject.value;
    }
}
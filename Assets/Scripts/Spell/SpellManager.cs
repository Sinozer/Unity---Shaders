using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    [SerializeField] private GameObject _spellsHolder;
    [SerializeField] private List<SpellBehaviour> _spells = new();
    [SerializeReference] private SpellBehaviour _currentSpell;

    private void Awake()
    {
        if (!_spellsHolder) return;
        SpellBehaviour[] temp = _spellsHolder.GetComponents<SpellBehaviour>();
        _spells.AddRange(temp);
    }

    private void Update()
    {
        foreach (SpellBehaviour spell in _spells)
        {
            _currentSpell = spell;
            if (Input.GetKeyDown(_currentSpell.SpellRef.Key)) Cast();
        }
    }

    private void Cast() => _currentSpell.Use();
}
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    [SerializeReference] private List<SpellBehaviour> _spells = new();
    [SerializeReference] private SpellBehaviour _currentSpell;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(_currentSpell.SpellRef.Key)) Cast();
    }
    
    public void Cast()
    {
        _currentSpell.Use();
    }
}

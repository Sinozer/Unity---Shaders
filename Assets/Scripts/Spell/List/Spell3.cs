using System;
using UnityEngine;

[Serializable]
public class Spell3 : SpellBehaviour
{
    public void Start()
    {
        
    }

    public override bool Use()
    {
        if (!base.Use()) return false;

        // Do your things
        Debug.LogWarning("Spell 3");

        return true;
    }
}

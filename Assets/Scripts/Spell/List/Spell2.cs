using System;
using UnityEngine;

[Serializable]
public class Spell2 : SpellBehaviour
{
    public void Start()
    {
        
    }

    public override bool Use()
    {
        if (!base.Use()) return false;

        // Do your things

        return true;
    }
}

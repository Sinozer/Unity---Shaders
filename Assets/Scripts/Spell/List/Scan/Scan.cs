using System;
using UnityEngine.VFX;
using UnityEngine;

[Serializable]
public class Scan : SpellBehaviour
{
    public void Start()
    {
        
    }

    public override bool Use()
    {
        if (!base.Use()) return false;

        // Do your things
        GameObject vfx = Instantiate(SpellRef.GameObjects["Vfx"]);
        vfx.transform.position = Player.transform.position;
        Destroy(vfx, SpellRef.LastDuration);

        return true;
    }
}

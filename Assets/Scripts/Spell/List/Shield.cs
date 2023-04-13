using System;
using UnityEngine;
using UnityEngine.VFX;

[Serializable]
public class Shield : SpellBehaviour
{
    public void Start()
    {
        
    }

    public override bool Use()
    {
        if (!base.Use()) return false;

        // Do your things
        GameObject vfx = Instantiate(SpellRef.GameObjects["Vfx"], Player.transform);
        vfx.GetComponent<VisualEffect>().SetFloat("LifeTime", SpellRef.LastDuration);
        Destroy(vfx, SpellRef.LastDuration);

        return true;
    }
}

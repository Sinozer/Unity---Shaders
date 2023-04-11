using System;
using UnityEngine;

public class InvisibilitySpell : SpellBehaviour
{
    public static event Action OnInvisibility;

    [SerializeField] private SkinnedMeshRenderer _mesh;
    [SerializeField] private Material _invisibilityMaterial;

    public override bool Use()
    {
        if (!base.Use()) return false;

        // Do your things
        Debug.LogWarning("Invisibility");

        OnInvisibility?.Invoke();

        Material[] materials = new Material[3];

        for (int i = 0; i < 3; i++)
        {
            materials[i] = _invisibilityMaterial;
        }

        _mesh.materials = materials;

        return true;
    }
}

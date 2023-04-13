using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InvisibilitySpell : SpellBehaviour
{
    [SerializeField] private Material _invisibilityMaterial;

    private SkinnedMeshRenderer _mesh;
    private List<Material> _mats;

    private void Start()
    {
        _mesh = Player.GetComponentInChildren<SkinnedMeshRenderer>();
        _mats = _mesh.materials.ToList();
    }

    private void OnDestroy()
    {
        PlayerManager.Instance.IsInvisible = false;
        _mats.RemoveAt(_mats.Count - 1);
        _mesh.materials = _mats.ToArray();
    }

    public override bool Use()
    {
        if (!base.Use()) return false;

        PlayerManager.Instance.IsInvisible = true;
        _mats.Add(_invisibilityMaterial);
        _mesh.materials = _mats.ToArray();
        Destroy(this, SpellRef.LastDuration);

        return true;
    }
}

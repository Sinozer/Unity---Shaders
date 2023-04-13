using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class InvisibilitySpell : SpellBehaviour
{
    [SerializeField] private Material invisibilityMaterial;

    private MeshRenderer[] _mesh;
    private List<Material>[] _mats;

    private void Start()
    {
        _mesh = Player.GetComponentsInChildren<MeshRenderer>();
        _mats = new List<Material>[_mesh.Length];

        for (int i = 0; i < _mesh.Length; i++)
        {
            _mats[i] = _mesh[i].materials.ToList();
        }
    }

    public override bool Use()
    {
        if (!base.Use()) return false;

        PlayerManager.Instance.IsInvisible = true;

        for (int i = 0; i < _mesh.Length; i++)
        {
            _mats[i].Add(invisibilityMaterial);
            _mesh[i].materials = _mats[i].ToArray();
        }

        StartCoroutine(SpellCooldown());

        return true;
    }

    private IEnumerator SpellCooldown()
    {
        yield return new WaitForSeconds(SpellRef.LastDuration);

        PlayerManager.Instance.IsInvisible = false;

        for (int i = 0; i < _mesh.Length; i++)
        {
            _mats[i].RemoveAt(_mats[i].Count - 1);
            _mesh[i].materials = _mats[i].ToArray();
        }

        yield return null;
    }
}
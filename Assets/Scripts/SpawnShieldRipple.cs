using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SpawnShieldRipple : MonoBehaviour
{
    public GameObject shieldRipples;

    private VisualEffect _shieldRipplesVFX;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Bullet")) return;
        
        GameObject ripples = Instantiate(shieldRipples,transform);
        _shieldRipplesVFX = ripples.GetComponent<VisualEffect>();
        _shieldRipplesVFX.SetVector3("SphereCenter", collision.contacts[0].point);

        Destroy(ripples, 2);
    }
}

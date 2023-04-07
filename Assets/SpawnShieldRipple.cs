using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SpawnShieldRipple : MonoBehaviour
{
    public GameObject shieldRipples;

    private VisualEffect shieldRipplesVFX;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Debug.Log("lul");
            var ripples = Instantiate(shieldRipples,transform) as GameObject;
            shieldRipplesVFX = ripples.GetComponent<VisualEffect>();
            shieldRipplesVFX.SetVector3("SphereCenter", collision.contacts[0].point);

            Destroy(ripples, 2);
        }
    }
}

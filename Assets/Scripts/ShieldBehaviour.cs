using UnityEngine;

public class ShieldBehaviour : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision) => Destroy(collision.gameObject);
}

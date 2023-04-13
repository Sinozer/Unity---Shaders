using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    [SerializeField] GameObject FXToSpawn;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (FXToSpawn != null) Instantiate(FXToSpawn, gameObject.transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
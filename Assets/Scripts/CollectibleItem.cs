using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    [SerializeField] GameObject FXToSpawn;

    private void OnTriggerEnter(Collider other)
    {
        // S'assurer que le player a bien le tag "Player"
        if (other.CompareTag("Player"))
        {
            if (FXToSpawn != null)
                Instantiate(FXToSpawn, gameObject.transform.position, Quaternion.identity);

            // Apply a visual effect on a player 
                // => Level up effect / activate shield 
            // Héritage à utiliser si besoin

            // Play a sound (?)

            Destroy(gameObject);
        }
    }

}

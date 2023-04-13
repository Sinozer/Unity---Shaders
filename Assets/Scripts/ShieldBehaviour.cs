using UnityEngine;

public class ShieldBehaviour : MonoBehaviour
{
    // Ca fait 1 ligne !
    private void OnCollisionEnter(Collision collision) => collision.gameObject.GetComponent<EnemyBehaviour>().SetState(EnemyBehaviour.EnemyState.DIE);
}

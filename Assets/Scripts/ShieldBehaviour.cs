using UnityEngine;

public class ShieldBehaviour : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision) => collision.gameObject.GetComponent<EnemyBehaviour>().SetState(EnemyBehaviour.EnemyState.DIE);
}

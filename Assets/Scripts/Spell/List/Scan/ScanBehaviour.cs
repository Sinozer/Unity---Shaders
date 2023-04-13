using UnityEngine;

public class ScanBehaviour : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;

    private void Update()
    {
        float step = _speed * Time.deltaTime;
        transform.localScale = new Vector3(transform.localScale.x + step, transform.localScale.y + step, transform.localScale.z + step);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.LogWarning("Collision");
        if (other.gameObject.layer == 10)
        {
            Debug.LogWarning("ENTITY");
            other.transform.GetComponent<EnemyBehaviour>().SetState(EnemyBehaviour.EnemyState.FROZEN);
        }
    }
}

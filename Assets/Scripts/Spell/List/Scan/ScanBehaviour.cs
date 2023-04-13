using UnityEngine;

public class ScanBehaviour : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;

    private void Update()
    {
        float step = _speed * Time.deltaTime;
        Vector3 localScale = transform.localScale;
        
        localScale = new Vector3(localScale.x + step, localScale.y + step,
            localScale.z + step);
        transform.localScale = localScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 10) return;
        other.transform.GetComponent<EnemyBehaviour>().SetState(EnemyBehaviour.EnemyState.FROZEN);
    }
}
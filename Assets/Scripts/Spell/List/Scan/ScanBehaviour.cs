using UnityEngine;

public class ScanBehaviour : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;

    private void Update()
    {
        float step = _speed * Time.deltaTime;
        transform.localScale = new Vector3(transform.localScale.x + step, transform.localScale.y + step, transform.localScale.z + step);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Make something else than destroy the object
        if (collision.gameObject.layer == 10)
            Destroy(collision.gameObject);
    }
}

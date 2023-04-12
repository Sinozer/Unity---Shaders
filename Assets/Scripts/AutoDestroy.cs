using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField] [Range(0.1f, 10f)] float _lifeDuration = 2f;

    void Start()
    {
        Destroy(gameObject, _lifeDuration);
    }
}

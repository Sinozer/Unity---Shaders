using UnityEngine;

namespace Player
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] [Range(0.1f, 10f)] private float lifeDuration = 2f;

        private void Start() => Destroy(gameObject, lifeDuration);

        public void OnCollisionEnter(Collision other) => Destroy(gameObject);
    }
}
using UnityEngine;

namespace Player {
	public class Bullet : MonoBehaviour {
		[SerializeField] [Range(0.1f, 10f)] float lifeDuration = 2f;

		void Start() {
			Destroy(gameObject, lifeDuration);
		}
		
		public void OnCollisionEnter(Collision other) {
			Destroy(gameObject);
		}
	}
}
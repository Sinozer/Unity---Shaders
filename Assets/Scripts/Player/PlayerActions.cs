using UnityEngine;
using UnityEngine.InputSystem;

namespace Player {
	public class PlayerActions : MonoBehaviour {
		public GameObject bullet;
	
		private PlayerInputs _inputs;
		private GameObject _target;
		private Camera _mainCamera;
		private Vector3 _offset;
		
	
		// Start is called before the first frame update
		void Start() {
			_mainCamera = Camera.main;
			_inputs = GetComponent<PlayerInputs>();
			_offset = new Vector3(1, 0, 0);
		}

		// Update is called once per frame
		void Update() {
			if(_inputs.shoot)
				Shoot();
			CheckTarget();
			_inputs.shoot = false;
		}

		private void Shoot() {
			if (_target == null) {
				GameObject clone = Instantiate(bullet, transform.position + _offset, Quaternion.identity);
				//clone.transform.forward = transform.forward;
				clone.GetComponent<Rigidbody>().velocity += new Vector3(25,0,0);
			}
			else {
				Vector3 dir = (_target.transform.position - transform.position).normalized;
				
				GameObject clone = Instantiate(bullet, transform.position + dir, Quaternion.identity);
				//clone.transform.forward = transform.forward;
				clone.GetComponent<Rigidbody>().velocity += dir * 25;
			}
			
		}

		private void CheckTarget() {
			if (_inputs.targeting) {
				Ray cameraRay = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

				if (Physics.Raycast(cameraRay, out RaycastHit hitInfo) && hitInfo.collider) {
					if (hitInfo.collider.gameObject.CompareTag("Enemy")) {
						//Debug.Log("Target");
						_target = hitInfo.collider.gameObject;
						//TODO : set une variable aux enemis pour qu'ils sachent s'ils sont target, afin de changer leur shader
					}
				}
			}
		}
	}
}

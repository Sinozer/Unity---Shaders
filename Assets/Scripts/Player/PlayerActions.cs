using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerActions : MonoBehaviour
    {
        public GameObject bullet;

        private PlayerInputs _inputs;
        private GameObject _target;
        private Camera _mainCamera;
        private Vector3 _offset;
        private static readonly int Targeted = Shader.PropertyToID("_Targeted");

        private void Start()
        {
            _mainCamera = Camera.main;
            _inputs = GetComponent<PlayerInputs>();
            _offset = new Vector3(1, 0, 0);
        }

        private void Update()
        {
            if (_inputs.shoot)
                Shoot();
            CheckTarget();
            _inputs.shoot = false;
        }

        private void Shoot()
        {
            if (_target == null)
            {
                GameObject clone = Instantiate(bullet, transform.position + _offset, Quaternion.identity);
                clone.GetComponent<Rigidbody>().velocity += new Vector3(25, 0, 0);
            }
            else
            {
                Transform transform1 = transform;
                Vector3 position = transform1.position;
                Vector3 dir = (_target.transform.position - position).normalized;
                GameObject clone = Instantiate(bullet, position + dir, Quaternion.identity);
                clone.GetComponent<Rigidbody>().velocity += dir * 25;
            }
        }

        private void CheckTarget()
        {
            if (!_inputs.targeting) return;
            Ray cameraRay = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (!Physics.Raycast(cameraRay, out RaycastHit hitInfo) || !hitInfo.collider) return;
            if (!hitInfo.collider.gameObject.CompareTag("Enemy")) return;
            _target = hitInfo.collider.gameObject;
            _target.GetComponentInChildren<SkinnedMeshRenderer>().materials[1].SetFloat(Targeted, 5f);
        }
    }
}
using System.Collections;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets {
	public class StarterAssetsInputs : MonoBehaviour {
		[Header("Character Input Values")] public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		
		[SerializeField] private float playerSpeed = 10f;

		[Header("Movement Settings")] public bool analogMovement;

		[Header("Mouse Cursor Settings")] public bool cursorLocked = true;
		public bool cursorInputForLook = true;
		
		private Camera _mainCamera;
		private Coroutine _coroutine;
		private CharacterController _characterController;

		private void Awake() {
			_mainCamera = Camera.main;
			_characterController = GetComponent<CharacterController>();
		}

#if ENABLE_INPUT_SYSTEM
		public void OnMove(InputValue value) {
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value) {
			if (cursorInputForLook) {
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value) {
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value) {
			SprintInput(value.isPressed);
		}

		public void OnClick(InputValue value) {
			Ray cameraRay = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
			
			if (Physics.Raycast(cameraRay, out RaycastHit hitInfo) && hitInfo.collider) {
				//MoveInput(hitInfo.point);
				if(_coroutine != null) StopCoroutine(_coroutine);
				_coroutine = StartCoroutine(PlayerMoveTowards(hitInfo.point));
			}
		}
		
		/*private void Move(InputAction.CallbackContext context) {
			Ray cameraRay = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

			if (Physics.Raycast(cameraRay, out RaycastHit hitInfo) && hitInfo.collider) {
				if(_coroutine != null) StopCoroutine(_coroutine);
				_coroutine = StartCoroutine(PlayerMoveTowards(hitInfo.point));
			}
		}*/

		private IEnumerator PlayerMoveTowards(Vector3 target) {
			/*float distanceToFloor = transform.position.y - target.y;
			target.y += distanceToFloor;*/
			//Vector3 destination = Vector3.MoveTowards(transform.position, target, playerSpeed * Time.deltaTime);
			//transform.position = destination;
			Vector3 direction = target - transform.position;
			Vector3 movement = direction.normalized * (playerSpeed * Time.deltaTime);
			_characterController.Move(movement);
				
			yield return null;
			
		}
		
#endif


		public void MoveInput(Vector2 newMoveDirection) {
			move = newMoveDirection;
		}

		public void LookInput(Vector2 newLookDirection) {
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState) {
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState) {
			sprint = newSprintState;
		}

		private void OnApplicationFocus(bool hasFocus) {
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState) {
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
}
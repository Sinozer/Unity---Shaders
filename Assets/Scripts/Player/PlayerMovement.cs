using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player {
	[RequireComponent(typeof(CharacterController))]
	public class PlayerMovement : MonoBehaviour {

		// player
		private float _playerSpeed = 2f;
		private float _sprintSpeed = 5.335f;
		private float _rotationSpeed = 3f;
		private float _rotationSmoothTime = 0.12f;
		private float _speed, _animationBlend;
		
		private float _speedChangeRate = 10.0f;
		private float _targetRotation = 0.0f;
		private float _terminalVelocity = 53.0f;
		private float _rotationVelocity, _verticalVelocity;

		// timeout deltatime
		private float _jumpTimeoutDelta, _fallTimeoutDelta;

		// animation IDs
		private int _animIDSpeed, _animIDGrounded, _animIDJump, _animIDFreeFall, _animIDMotionSpeed;
		private bool _hasAnimator;

		//Audio
		public AudioClip landingAudioClip;
		public AudioClip[] footstepAudioClips;
		[Range(0, 1)] public float footstepAudioVolume = 0.5f;

		//Physic
		public float gravity = -15.0f;
		public float jumpTimeout = 0.50f;
		public float jumpHeight = 1.2f;
		public float fallTimeout = 0.15f;
		public bool grounded = true;
		public float groundedRadius = 0.28f;
		public float groundedOffset = -0.14f;
		public LayerMask groundLayers;

		private Animator _animator;

		private CharacterController _characterController;

		private PlayerInputs _inputs;
		private Camera _mainCamera;
		private Coroutine _coroutine;
		private Vector3 _targetPos;

		//private const float _threshold = 0.01f;
		
		void Awake() {
			_mainCamera = Camera.main;
			_characterController = GetComponent<CharacterController>();
		}

		private void Start() {
			_hasAnimator = TryGetComponent(out _animator);
			_characterController = GetComponent<CharacterController>();
			_inputs = GetComponent<PlayerInputs>();

			AssignAnimationIDs();

			// reset our timeouts on start
			_jumpTimeoutDelta = jumpTimeout;
			_fallTimeoutDelta = fallTimeout;
		}

		private void AssignAnimationIDs() {
			_animIDSpeed = Animator.StringToHash("Speed");
			_animIDGrounded = Animator.StringToHash("Grounded");
			_animIDJump = Animator.StringToHash("Jump");
			_animIDFreeFall = Animator.StringToHash("FreeFall");
			_animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
		}

		private void Update() {
			_hasAnimator = TryGetComponent(out _animator);

			JumpAndGravity();
			GroundedCheck();
			Move();
		}

		private void GroundedCheck() {
			// set sphere position, with offset
			Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset,
				transform.position.z);
			grounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayers,
				QueryTriggerInteraction.Ignore);

			// update animator if using character
			if (_hasAnimator) {
				_animator.SetBool(_animIDGrounded, grounded);
			}
		}

		private void JumpAndGravity() {
			if (grounded) {
				// reset the fall timeout timer
				_fallTimeoutDelta = fallTimeout;

				// update animator if using character
				if (_hasAnimator) {
					_animator.SetBool(_animIDJump, false);
					_animator.SetBool(_animIDFreeFall, false);
				}

				// stop our velocity dropping infinitely when grounded
				if (_verticalVelocity < 0.0f) {
					_verticalVelocity = -2f;
				}

				// Jump
				if (_inputs.jump && _jumpTimeoutDelta <= 0.0f) {
					// the square root of H * -2 * G = how much velocity needed to reach desired height
					_verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);

					// update animator if using character
					if (_hasAnimator) {
						_animator.SetBool(_animIDJump, true);
					}
				}

				// jump timeout
				if (_jumpTimeoutDelta >= 0.0f) {
					_jumpTimeoutDelta -= Time.deltaTime;
				}
			}
			else {
				// reset the jump timeout timer
				_jumpTimeoutDelta = jumpTimeout;

				// fall timeout
				if (_fallTimeoutDelta >= 0.0f) {
					_fallTimeoutDelta -= Time.deltaTime;
				}
				else {
					// update animator if using character
					if (_hasAnimator) {
						_animator.SetBool(_animIDFreeFall, true);
					}
				}

				// if we are not grounded, do not jump
				_inputs.jump = false;
			}

			// apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
			if (_verticalVelocity < _terminalVelocity) {
				_verticalVelocity += gravity * Time.deltaTime;
			}
		}

		/*private void MoveAnimation(Vector3 inputDir) {

			// a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon
			float targetSpeed = _inputs.sprint ? _sprintSpeed : _playerSpeed;

			// note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
			// if there is no input, set the target speed to 0
			if (_inputs.mouseClick) targetSpeed = 0.0f;

			// a reference to the players current horizontal velocity
			float currentHorizontalSpeed =
				new Vector3(_characterController.velocity.x * 5, 0.0f, _characterController.velocity.z * 5).magnitude;

			float speedOffset = 0.1f;
			float inputMagnitude = 1f;

			// accelerate or decelerate to target speed
			if (currentHorizontalSpeed < targetSpeed - speedOffset ||
			    currentHorizontalSpeed > targetSpeed + speedOffset) {
				// creates curved result rather than a linear one giving a more organic speed change
				// note T in Lerp is clamped, so we don't need to clamp our speed
				_speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
					Time.deltaTime * _speedChangeRate);

				// round speed to 3 decimal places
				_speed = Mathf.Round(_speed * 1000f) / 1000f;
			}
			else {
				_speed = targetSpeed;
			}

			_animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * _speedChangeRate);
			if (_animationBlend < 0.01f) _animationBlend = 0f;

			// normalise input direction

			// note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
			// if there is a move input rotate player when the player is moving
			_targetRotation = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg +
			                  _mainCamera.transform.eulerAngles.y;
			float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
					_rotationSmoothTime);

			// rotate to face input direction relative to camera position
			transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);

			// update animator if using character
			if (_hasAnimator) {
				_animator.SetFloat(_animIDSpeed, _animationBlend);
				_animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
			}
		}*/


		private void Move() {
			if(!_inputs.mouseClick) return;
			Ray cameraRay = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

			if (Physics.Raycast(cameraRay, out RaycastHit hitInfo) && hitInfo.collider && hitInfo.collider.CompareTag("Terrain")) {
				if (_coroutine != null) StopCoroutine(_coroutine);
				_coroutine = StartCoroutine(PlayerMoveTowards(hitInfo.point));
				_targetPos = hitInfo.point;
			}
			_inputs.mouseClick = false;
			//MoveAnimation((hitInfo.point - transform.position).normalized);
		}

		private IEnumerator PlayerMoveTowards(Vector3 target) {
			float distanceToFloor = transform.position.y - target.y;
			target.y += distanceToFloor;

			while (Vector3.Distance(transform.position, target) > 0.1f) {
				//Vector3 destination = Vector3.MoveTowards(transform.position, target, playerSpeed * Time.deltaTime);
				//transform.position = destination;
				float targetSpeed = _inputs.sprint ? _sprintSpeed : _playerSpeed;

				Vector3 direction = target - transform.position;
				Vector3 movement = direction.normalized * (targetSpeed * Time.deltaTime) +
				                   new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime;
				_characterController.Move(movement);
				direction.y = 0.0f;

				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction.normalized), _rotationSpeed * Time.deltaTime);

				yield return null;
			}
		}
		
		private void OnFootstep(AnimationEvent animationEvent) {
			if (animationEvent.animatorClipInfo.weight > 0.5f) {
				if (footstepAudioClips.Length > 0) {
					var index = Random.Range(0, footstepAudioClips.Length);
					AudioSource.PlayClipAtPoint(footstepAudioClips[index],
						transform.TransformPoint(_characterController.center), footstepAudioVolume);
				}
			}
		}

		private void OnLand(AnimationEvent animationEvent) {
			if (animationEvent.animatorClipInfo.weight > 0.5f) {
				AudioSource.PlayClipAtPoint(landingAudioClip, transform.TransformPoint(_characterController.center),
					footstepAudioVolume);
			}
		}

		private void OnDrawGizmos() {
			Gizmos.color = Color.red;
			Gizmos.DrawSphere(_targetPos, 1);
		}
	}
}
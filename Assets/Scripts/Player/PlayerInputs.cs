using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInputs : MonoBehaviour
    {
        public bool jump;
        public bool sprint;
        public bool mouseClick;
        public bool shoot;
        public bool targeting;

#if ENABLE_INPUT_SYSTEM

        public void OnMove(InputValue value) => MoveInput(value.isPressed);

        public void OnJump(InputValue value) => JumpInput(value.isPressed);

        public void OnSprint(InputValue value) => SprintInput(value.isPressed);

        public void OnTarget(InputValue value) => TargetInput(value.isPressed);

        public void OnShoot(InputValue value) => ShootInput(value.isPressed);

#endif

        private void MoveInput(bool click) => mouseClick = click;

        private void JumpInput(bool newJumpState) => jump = newJumpState;

        private void SprintInput(bool newSprintState) => sprint = newSprintState;

        private void TargetInput(bool targeter) => targeting = targeter;

        private void ShootInput(bool sh) => shoot = sh;
    }
}
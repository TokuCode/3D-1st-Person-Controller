using System;
using Code.Helpers.Singleton;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Gameplay.Controls
{
    public class InputReader : Singleton<InputReader>, PlayerControls.IPlayerActions, IControls
    {
        PlayerControls _playerControls;
        
        public event Action JumpPressed;
        
        public Vector2 MouseDelta { get; private set; }
        public Vector2 MoveDirection { get; private set; }
        public bool Jump { get; private set; }
        public bool Sprint { get; private set; }
        public bool Crouch { get; private set; }

        private void OnEnable()
        {
            _playerControls = new PlayerControls();
            _playerControls.Enable();
            _playerControls.Player.SetCallbacks(this);
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MoveDirection = context.ReadValue<Vector2>();
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            MouseDelta = context.ReadValue<Vector2>();
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            // Not implemented
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            // Not implemented
        }

        public void OnCrouch(InputAction.CallbackContext context)
        {
            if (context.performed) Crouch = true;
            else if(context.canceled) Crouch = false;
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Jump = true;
                JumpPressed?.Invoke();
            }
            else if(context.canceled) Jump = false;
        }

        public void OnPrevious(InputAction.CallbackContext context)
        {
            // Not implemented
        }

        public void OnNext(InputAction.CallbackContext context)
        {
            // Not implemented
        }

        public void OnSprint(InputAction.CallbackContext context)
        {
            if (context.performed) Sprint = true;
            else if(context.canceled) Sprint = false;
        }
    }
}
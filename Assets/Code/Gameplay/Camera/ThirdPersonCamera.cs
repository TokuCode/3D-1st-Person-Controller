using Code.Gameplay.Character.Features;
using Code.Gameplay.Character.Framework;
using Code.Gameplay.Controls;
using UnityEngine;

namespace Code.Gameplay.Camera
{
    public class ThirdPersonCamera : Feature
    {
        private PhysicsCheck physics;
        private Transform _cameraTransform;
        
        [Header("Cursor")]
        [SerializeField] private bool cursorVisible;
        [SerializeField] private CursorLockMode cursorLockMode;

        private Vector2 _moveDirection;
        
        [Header("Movement Adjustment")]
        [SerializeField] private float rotationSmoothing;
        [SerializeField] private float velocityThreshold;

        public override void InitializeFeature(Controller controller)
        {
            base.InitializeFeature(controller);
            
            _dependencies.TryGet(out physics);
            
            UnityEngine.Camera main = UnityEngine.Camera.main;
            if(main == null) return;
            _cameraTransform = main.transform;
        }

        public override void UpdateFeature()
        {
            if(!enabled) return;
            
            SetCursor();
            UpdateRotations();
        }

        public override void Apply(ref InputPayload @event)
        {
            if(!enabled) return;
            
            if(@event.Context != UpdateContext.Update) return;
            
            _moveDirection = @event.MoveDirection;
        }

        private void SetCursor()
        {
            Cursor.visible = cursorVisible;
            Cursor.lockState = cursorLockMode;
        }
        
        private void UpdateRotations()
        {
            _invoker.OrientationRotation.Execute(_cameraTransform.rotation.eulerAngles.y);
            
            Vector3 velocity = _invoker.Velocity.Get();
            if(!physics.OnSlope)
                velocity.y = 0;

            if (velocity.magnitude > velocityThreshold)
            {
                Quaternion actualPlayerRotation = Quaternion.Euler(0, _invoker.PlayerRotation.Get(), 0);
                
                Vector3 moveDirectionWorld = _invoker.Forward.Get() * _moveDirection.y + _invoker.Right.Get() * _moveDirection.x;
                moveDirectionWorld.Normalize();

                if (moveDirectionWorld != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(moveDirectionWorld, Vector3.up);
                    Quaternion smoothedRotation = Quaternion.Slerp(actualPlayerRotation, targetRotation, rotationSmoothing);
                    _invoker.PlayerRotation.Execute(smoothedRotation.eulerAngles.y);
                }
            }
        }
    }
}

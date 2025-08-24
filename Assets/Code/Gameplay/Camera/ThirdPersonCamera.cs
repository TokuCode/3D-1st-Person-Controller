using Code.Gameplay.Character.Framework;
using UnityEngine;

namespace Code.Gameplay.Camera
{
    public class ThirdPersonCamera : Feature
    {
        private Transform _cameraTransform;
        
        [Header("Cursor")]
        [SerializeField] private bool cursorVisible;
        [SerializeField] private CursorLockMode cursorLockMode;

        public override void InitializeFeature(Controller controller)
        {
            base.InitializeFeature(controller);
            
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
        
        private void SetCursor()
        {
            Cursor.visible = cursorVisible;
            Cursor.lockState = cursorLockMode;
        }
        
        private void UpdateRotations()
        {
            _invoker.PlayerRotation.Execute(_invoker.LookAtRotation.Get());
            _invoker.OrientationRotation.Execute(_cameraTransform.rotation.eulerAngles.y);
        }
    }
}

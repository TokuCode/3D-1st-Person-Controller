using UnityEngine;

namespace Code.Gameplay.Character.Framework
{
    public class Invoker
    {
        public RequestCenterPosition CenterPosition { get; }
        public LocalScaleHandler LocalScale { get; }
        public HeightHandler Height { get; }
        public RadiusHanlder Radius { get; }
        public VelocityHandlder Velocity { get; }
        public AddForceCommand AddForce { get; }
        public AddRigidbodyPosition AddRigidbodyPositionAdd { get; }
        public ForwardHandler Forward { get; }
        public RightHandler Right { get; }
        public LatitudeHandler OrientationRotation { get; }
        public UseGravityHandler UseGravity { get; }
        public PositionHandler PlayerPosition { get; }
        public PositionHandler CameraPosition { get; }
        public Invoker(Controller controller)
        {
            if(controller is not PlayerController playerController) return;
            
            var _transform = playerController.transform;
            var _orientation = playerController.Orientation;
            var _playerRender = playerController.PlayerRender;
            var _cameraPosition = playerController.CameraPosition;
            var _rigidbody = playerController.GetComponent<Rigidbody>();
            var _collider = playerController.GetComponent<CapsuleCollider>();

            CenterPosition = new(_transform, _collider);
            LocalScale = new(_transform);
            Height = new(_collider);
            Radius = new(_collider);
            Velocity = new(_rigidbody);
            AddForce = new(_rigidbody);
            AddRigidbodyPositionAdd = new(_rigidbody);
            Forward = new(_orientation);
            Right = new(_orientation);
            OrientationRotation = new(_orientation);
            UseGravity = new(_rigidbody);
            PlayerPosition = new(_playerRender);
            CameraPosition = new(_cameraPosition);
        }
    }
}
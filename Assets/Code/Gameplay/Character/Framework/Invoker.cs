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
        public LatitudeHandler PlayerRotation { get; }
        
        public Invoker(Controller controller)
        {
            if(controller is not PlayerController playerController) return;
            
            var transform = playerController.transform;
            var orientation = playerController.Orientation;
            var playerRender = playerController.PlayerRender;
            var rigidbody = playerController.GetComponent<Rigidbody>();
            var collider = playerController.GetComponent<CapsuleCollider>();

            CenterPosition = new(transform, collider);
            LocalScale = new(transform);
            Height = new(collider);
            Radius = new(collider);
            Velocity = new(rigidbody);
            AddForce = new(rigidbody);
            AddRigidbodyPositionAdd = new(rigidbody);
            Forward = new(orientation);
            Right = new(orientation);
            OrientationRotation = new(orientation);
            UseGravity = new(rigidbody);
            PlayerPosition = new(playerRender);
            PlayerRotation = new(playerRender);
        }
    }
}
using UnityEngine;

namespace Code.Gameplay.Controls
{
    public interface IControls
    {
        Vector2 MouseDelta { get; }
        Vector2 MoveDirection { get; }
        bool Jump { get; }
        bool Sprint { get; }
        bool Crouch { get; }
    }
    
    public struct InputPayload
    {
        public Vector2 MouseDelta;
        public Vector2 MoveDirection;
        public bool Jump;
        public bool Sprint;
        public bool Crouch;
        public UpdateContext Context;
    }
    
    public enum UpdateContext
    {
        Update,
        FixedUpdate
    }
}

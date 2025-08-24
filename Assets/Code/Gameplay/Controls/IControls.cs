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
}

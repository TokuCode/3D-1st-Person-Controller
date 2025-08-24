using Code.Gameplay.Character.Framework;
using Code.Gameplay.Controls;
using UnityEngine;

public class FirstPersonCamera : Feature
{
    private Transform _cameraParent;

    [Header("Camera Rig")]
    [SerializeField] private float distanceToPlayer;

    [Header("Camera Coordinates")]
    [SerializeField] private float minPolar;
    [SerializeField] private float maxPolar;
    private float _latitude;
    private float _polar;

    [Header("Camera Movement")]
    [SerializeField] private Vector2 sensibility;
    [SerializeField, Range(0f, 1f)] private float smoothing;
    [SerializeField, Range(0f, 1f)] private float positionSmoothing;
    [SerializeField] private bool invertPolar;

    private Vector2 _mouseDelta;

    [Header("Cursor")]
    [SerializeField] private bool cursorVisible;
    [SerializeField] private CursorLockMode cursorLockMode;

    public override void InitializeFeature(Controller controller)
    {
        base.InitializeFeature(controller);
        
        Camera main = Camera.main;
        if (main != null)
        {
            _cameraParent = main.transform.parent;
        }
    }
    
    public override void UpdateFeature()
    {
        if(!enabled) return;
        
        SetCursor();
    }

    public override void Apply(ref InputPayload @event)
    {
        if(!enabled) return;
        
        if (@event.Context != UpdateContext.Update) return;

        _mouseDelta = @event.MouseDelta;
        
        MoveCamera();
        PlaceCamera();
    }

    private void MoveCamera()
    {
        if(_mouseDelta == Vector2.zero) return;
        
        _latitude = (_latitude + _mouseDelta.x * sensibility.x) % 360f;
        _polar -= _mouseDelta.y * sensibility.y * (invertPolar ? -1 : 1);
        _polar = Mathf.Clamp(_polar, minPolar, maxPolar);
    }
    
    private void PlaceCamera()
    {
        if(_cameraParent == null) return;
        
        Quaternion actualRotation = _cameraParent.rotation;
        Quaternion targetRotation = Quaternion.Euler(_polar, _latitude, 0f);
        Quaternion finalRotation = Quaternion.Slerp(actualRotation, targetRotation, smoothing);
        
        Vector3 newCameraPosition = _invoker.CameraPosition.Get() + finalRotation * Vector3.forward * distanceToPlayer;
        _cameraParent.position = Vector3.Lerp(_invoker.CameraPosition.Get(), newCameraPosition, positionSmoothing);
        _cameraParent.rotation = finalRotation;
        
        _invoker.OrientationRotation.Execute(finalRotation.eulerAngles.y);
    }
    
    private void SetCursor()
    {
        Cursor.visible = cursorVisible;
        Cursor.lockState = cursorLockMode;
    }
}

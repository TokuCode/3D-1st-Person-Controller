using Code.Gameplay.Controls;
using Helpers.Pipeline;
using UnityEngine;

namespace Code.Gameplay.Character.Framework
{
    public class PlayerController : Controller
    {
        [SerializeField] private Transform _orientation;
        public Transform Orientation => _orientation;
        [SerializeField] private Transform _lookAt;
        public Transform LookAt => _lookAt;
        [SerializeField] private Transform _playerRender;
        public Transform PlayerRender => _playerRender;
        
        public static PlayerController Singleton { get; private set; }
        private IControls _controls;
        public Pipeline<InputPayload> InputPipeline { get; private set; } = new();
        public Invoker Invoker { get; private set; }

        protected override void Awake()
        {
            SetSingleton();
            _controls = InputReader.Instance;
            Invoker = new Invoker(this);
            
            base.Awake();
        }
        
        private void SetSingleton()
        {
            if (Singleton != null && Singleton != this)
            {
                Destroy(gameObject);
            }
            else Singleton = this;
        }

        protected override void Update()
        {
            base.Update();
            ReadInput(UpdateContext.Update);
        }
        
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            ReadInput(UpdateContext.FixedUpdate);
        }
        
        private void ReadInput(UpdateContext context)
        {
            InputPayload input = new()
            {
                MouseDelta = _controls.MouseDelta,
                MoveDirection = _controls.MoveDirection,
                Jump = _controls.Jump,
                Sprint = _controls.Sprint,
                Crouch = _controls.Crouch,
                Context = context
            };
            InputPipeline.Process(ref input);
        }
    }
}
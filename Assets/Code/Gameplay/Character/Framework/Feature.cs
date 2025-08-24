using Code.Gameplay.Controls;
using Helpers.Pipeline;
using UnityEngine;

namespace Code.Gameplay.Character.Framework
{
    public abstract class Feature : MonoBehaviour, IFeature, IProcess<InputPayload>
    {
        protected Invoker _invoker;
        protected IDependencyManager _dependencies;
        
        public virtual void InitializeFeature(Controller controller)
        {
            if(controller is PlayerController playerController)
            {
                _invoker = playerController.Invoker;
                _dependencies = playerController.Dependencies;
                playerController.InputPipeline.Register(this);
            }
        }

        public virtual void UpdateFeature(){}

        public virtual void FixedUpdateFeature(){}
        public virtual void Apply(ref InputPayload @event){}
    }
}
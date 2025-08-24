using System.Collections.Generic;

namespace Helpers.Pipeline
{
    public class Pipeline<T>
    {
        private readonly List<IProcess<T>> _bindings = new ();
        
        public void Register(IProcess<T> process) => _bindings.Add(process);
        public void Deregister(IProcess<T> process) => _bindings.Remove(process);
        
        public void Process(ref T @event)
        {
            foreach (var process in _bindings)
                process.Apply(ref @event);
        }
        
        public void Clear() => _bindings.Clear();
    }
}
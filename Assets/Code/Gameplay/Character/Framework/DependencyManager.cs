using System;
using System.Collections.Generic;

namespace Code.Gameplay.Character.Framework
{
    public interface IDependencyManager
    {
        void TryAdd(IFeature feature);
        bool TryGet<T>(out T feature) where T : IFeature;
    }
    
    public class DependencyManager : IDependencyManager
    {
        protected Dictionary<Type, IFeature> _features;

        public void TryAdd(IFeature feature)
        {
            if (_features.ContainsKey(feature.GetType()))
            {
                _features.Add(feature.GetType(), feature);
            }
            else 
                throw new ArgumentException("There is already a feature with the same type");
        }
        
        public bool TryGet<T>(out T feature) where T : IFeature
        {
            if(_features.TryGetValue(typeof(T), out var rawFeature))
            {
                feature = (T)rawFeature;
                return true;
            }
            feature = default;
            return false;
        }
    }
}
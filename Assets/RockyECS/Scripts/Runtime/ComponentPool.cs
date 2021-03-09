using System;
using System.Collections.Generic;
using AillieoUtils;

namespace RockyECS
{

    internal static class ComponentPool
    {
        private static readonly Dictionary<Type,object> pools = new Dictionary<Type, object>();
        internal static T Get<T>() where T : class, IComponent, new()
        {
            object pool;
            if (!pools.TryGetValue(typeof(T), out pool))
            {
                pool = Pool<IComponent>.Create()
                    .SetSizeMax(256)
                    .SetCreateFunc(() => new T())
                    .SetOnRecycle(c => c.Reset())
                    .SetNameForProfiler($"CP: {typeof(T).Name}")
                    .AsPool();
                pools.Add(typeof(T), pool);
            }
            T component = (pool as Pool<IComponent>).Get() as T;
            return component;
        }

        internal static void Recycle<T>(T component) where T : IComponent
        {
            object pool;
            if (pools.TryGetValue(component.GetType(), out pool))
            {
                (pool as Pool<IComponent>).Recycle(component);
            }
        }
    }
}

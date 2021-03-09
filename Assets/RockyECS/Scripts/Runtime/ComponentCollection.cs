using System;
using System.Collections.Generic;
using AillieoUtils.TypeExt;

namespace RockyECS
{
    internal class ComponentCollection
    {
        private readonly Entity owner;
        internal ComponentCollection(Entity entity)
        {
            owner = entity;
        }

        private readonly Dictionary<Type, IComponent> components = new Dictionary<Type, IComponent>();

        internal T Add<T>() where T : class, IComponent, new()
        {
            T component = ComponentPool.Get<T>();
            components.Add(typeof(T), component);

            Container.Instance.NotifyAddOrRemoveEventForType(component.GetType(), owner);

            return component;
        }

        internal T Get<T>() where T : class, IComponent
        {
            return components.GetOrDefault(typeof(T)) as T;
        }

        internal void Clear()
        {
            foreach (var pair in components)
            {
                ComponentPool.Recycle(pair.Value);
                Container.Instance.NotifyAddOrRemoveEventForType(pair.Key, owner);
            }
            components.Clear();
        }

        internal bool Remove<T>(T component) where T : IComponent
        {
            if(components.Remove(typeof(T)))
            {
                ComponentPool.Recycle(component);
                Container.Instance.NotifyAddOrRemoveEventForType(typeof(T), owner);
                return true;
            }
            return false;
        }

    }
}

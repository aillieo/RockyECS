using AillieoUtils;
using AillieoUtils.TypeExt;
using System;
using System.Collections.Generic;

namespace RockyECS
{
    public sealed class Entity
    {
        public static readonly int invalid = -1;

        public int id { get; private set; }

        private readonly Context context;

        public void CleanUp()
        {
            foreach (var pair in components)
            {
                ComponentPool.Recycle(pair.Value);
                context.NotifyAddOrRemoveEventForType(pair.Key, this);
            }
            components.Clear();
        }

        private Entity(Context context)
        {
            this.context = context;
        }

        private readonly Dictionary<Type, IComponent> components = new Dictionary<Type, IComponent>();

        public bool HasComp<T>() where T : class, IComponent
        {
            return components.ContainsKey(typeof(T));
        }

        public T AddComp<T>() where T : class, IComponent, new()
        {
            T component = ComponentPool.Get<T>();
            components.Add(typeof(T), component);

            context.NotifyAddOrRemoveEventForType(component.GetType(), this);

            return component;
        }

        public T GetComp<T>() where T : class, IComponent
        {
            return components.GetOrDefault(typeof(T)) as T;
        }

        public bool RemoveComp<T>(T component) where T : IComponent
        {
            if (components.Remove(typeof(T)))
            {
                ComponentPool.Recycle(component);
                context.NotifyAddOrRemoveEventForType(typeof(T), this);
                return true;
            }
            return false;
        }

        public bool RemoveComp<T>() where T : class, IComponent
        {
            IComponent component;
            if(components.TryGetValue(typeof(T), out component))
            {
                return RemoveComp(component as T);                
            }
            return false;
        }

        public static bool IsNullOrInvalid(Entity entity)
        {
            return entity == null || entity.id == invalid;
        }

        internal static Pool<Entity> CreatePool(Context context)
        {
            int sid = 0;
            return Pool<Entity>.Create()
                .SetSizeMax(512)
                .SetCreateFunc(() => new Entity(context))
                .SetOnRecycle(entity => {
                    entity.id = Entity.invalid;
                })
                .SetOnGet(entity => entity.id = ++sid)
                .AsPool();
        }

        public override string ToString()
        {
            // debug: all coms fields by reflection
            return base.ToString();
        }
    }
}

using AillieoUtils;
using UnityEngine.Assertions;

namespace RockyECS
{
    public sealed class Entity
    {
        public static readonly int invalid = -1;

        public int id { get; private set; }

        public void CleanUp()
        {
            components.Clear();
        }

        private Entity()
        {
            components = new ComponentCollection(this);
        }

        private static int sid = 0;

        private readonly ComponentCollection components;

        public bool HasComp<T>() where T : class, IComponent
        {
            return components.Get<T>() != null;
        }

        public T AddComp<T>() where T : class, IComponent, new()
        {
            return components.Add<T>();
        }

        public T GetComp<T>() where T : class, IComponent
        {
            return components.Get<T>();
        }

        public bool RemoveComp<T>(T component) where T : IComponent
        {
            return components.Remove(component);
        }

        public static bool IsNullOrInvalid(Entity entity)
        {
            return entity == null || entity.id == invalid;
        }

        internal static readonly Pool<Entity> pool = Pool<Entity>.Create()
                .SetSizeMax(512)
                .SetCreateFunc(()=>new Entity())
                .SetOnRecycle(entity => {
                    entity.id = Entity.invalid;
                })
                .SetOnGet(entity => entity.id = ++sid)
                .AsPool();

        public override string ToString()
        {
            // debug: all coms fields by reflection
            return base.ToString();
        }
    }
}

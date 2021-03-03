using AillieoUtils;
using UnityEngine.Assertions;

namespace RockyECS
{
    public sealed class Entity
    {
        public static readonly int invalid = -1;
        private bool raw = true;

        public int id { get; private set; }

        public void Init()
        {
            Assert.IsTrue(raw);
            raw = false;
        }

        public void CleanUp()
        {
            Assert.IsFalse(raw);
            raw = true;

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
            T component = components.Add<T>();
            if (!raw)
            {
            }
            return component;
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

        public static readonly Pool<Entity> pool = Pool<Entity>.Create()
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

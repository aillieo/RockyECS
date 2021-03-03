using System;
using System.Collections.Generic;
using System.Linq;
using AillieoUtils;
using AillieoUtils.TypeExt;

namespace RockyECS
{
    public class Container : Singleton<Container>
    {
        private class EntityComparer : IComparer<Entity>
        {
            public int Compare(Entity x, Entity y)
            {
                return x.id.CompareTo(y.id);
            }
        }
        private readonly EntityComparer entityComparer = new EntityComparer();

        private readonly Dictionary<int, Entity> entities = new Dictionary<int, Entity>();

        private readonly Dictionary<Type, Event<Entity>> onAddOrRemoveComp = new Dictionary<Type, Event<Entity>>();

        internal Handle<Entity> ListenAddOrRemoveEventForType(Type compType, Action<Entity> action)
        {
            return onAddOrRemoveComp.GetOrAdd(compType, _ => new Event<Entity>()).AddListener(action);
        }

        internal void NotifyAddOrRemoveEventForType(Type compType, Entity entity)
        {
            if (onAddOrRemoveComp.TryGetValue(compType, out Event<Entity> evt))
            {
                evt.SafeInvoke(entity);
            }
        }

        public void Add(Entity entity)
        {
            if (!entities.ContainsKey(entity.id))
            {
                entity.Init();

                entities.Add(entity.id, entity);
            }
        }

        public bool Remove(int id)
        {
            Entity entity = entities.GetOrDefault(id);
            if (entity != null)
            {
                entity.CleanUp();

                entities.Remove(id);
                Entity.pool.Recycle(entity);

                return true;
            }

            return false;
        }

        public Entity Get(int id)
        {
            return entities.GetOrDefault(id);
        }

        public void CleanUp()
        {
            foreach (var entity in entities)
            {
                entity.Value.CleanUp();
                Entity.pool.Recycle(entity.Value);
            }
            entities.Clear();
        }

        public void Find<T>(Predicate<Entity> filter, List<Entity> result, int count = int.MaxValue) where T : class, IComponent
        {
            result.Clear();
            foreach(var e in entities)
            {
                if(e.Value.HasComp<T>())
                {
                    if(filter == null || filter(e.Value))
                    {
                        result.Add(e.Value);
                        if (result.Count >= count)
                        {
                            return;
                        }
                    }
                }
            }
        }

        public void Fill(Selection selection)
        {
            selection.ReSelect(entities.Select(p => p.Value));
        }

        public Entity SelectOne<T>() where T : class, IComponent
        {
            Selection selection = new Selection(new Filter<T>());
            selection.ReSelect(entities.Select(p => p.Value));
            return selection.FirstOrDefault();
        }

    }
}

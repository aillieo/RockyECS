using System;
using System.Collections.Generic;
using System.Linq;
using AillieoUtils;
using AillieoUtils.TypeExt;

namespace RockyECS
{
    public class Context
    {
        private readonly Dictionary<int, Entity> entities = new Dictionary<int, Entity>();

        private readonly Dictionary<Type, Event<Entity>> onAddOrRemoveComp = new Dictionary<Type, Event<Entity>>();

        private readonly Pool<Entity> pool;

        internal Context()
        {
            pool = Entity.CreatePool(this);
        }

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

        internal Selection CreateSelection(Filter filter)
        {
            Selection selection = new Selection(this, filter);
            foreach (var t in selection.filter.AssociatedCompTypes())
            {
                ListenAddOrRemoveEventForType(t, e => selection.MarkDirtyEntity(e));
            }
            selection.ReSelect(entities.Select(p => p.Value));
            return selection;
        }

        internal void DisposeSelection(Selection selection)
        {
            foreach (var t in selection.filter.AssociatedCompTypes())
            {
                // todo remove listeners
            }
        }

        public Entity Add()
        {
            Entity entity = pool.Get();
            entities.Add(entity.id, entity);
            return entity;
        }

        public bool Remove(int id)
        {
            Entity entity = entities.GetOrDefault(id);
            if (entity != null)
            {
                entity.CleanUp();

                entities.Remove(id);
                pool.Recycle(entity);

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
                pool.Recycle(entity.Value);
            }
            entities.Clear();
        }

        // todo 不通过selection来取
        public Entity SelectOne<T>() where T : class, IComponent
        {
            Selection selection = CreateSelection(new Filter<T>());
            Entity entity = selection.FirstOrDefault();
            DisposeSelection(selection);
            return entity;
        }

    }
}

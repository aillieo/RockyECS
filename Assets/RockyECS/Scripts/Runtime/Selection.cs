using AillieoUtils.TypeExt;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RockyECS
{
    public class Selection : IEnumerable<Entity>
    {
        internal Selection(Context context, Filter filter)
        {
            this.context = context;
            this.filter = filter;
        }

        public readonly Context context;
        internal readonly Filter filter;

        private readonly List<Entity> cachedEntities = new List<Entity>();
        private readonly HashSet<Entity> dirtyEntities = new HashSet<Entity>();

        internal void MarkDirtyEntity(Entity entity)
        {
            dirtyEntities.Add(entity);
        }

        internal IEnumerable<Entity> Select()
        {
            if (dirtyEntities.Count > 0)
            {
                // remove exist
                for(int i = cachedEntities.Count - 1; i >= 0; --i)
                {
                    Entity entity = cachedEntities[i];
                    if(dirtyEntities.Contains(entity))
                    {
                        if(!filter.Valid(entity))
                        {
                            cachedEntities.RemoveAtSwapBack(i);
                        }
                        dirtyEntities.Remove(entity);
                    }
                }

                // add new
                foreach (var entity in dirtyEntities)
                {
                    if(filter.Valid(entity))
                    {
                        cachedEntities.Add(entity);
                    }
                }

                dirtyEntities.Clear();
            }
            return cachedEntities;
        }

        internal void ReSelect(IEnumerable<Entity> source)
        {
            cachedEntities.Clear();
            dirtyEntities.Clear();
            cachedEntities.AddRange(source.Where(filter.Valid));
        }

        public IEnumerator<Entity> GetEnumerator()
        {
            return Select().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

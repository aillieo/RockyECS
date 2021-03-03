using System;
using System.Collections.Generic;

namespace RockyECS
{
    public abstract class Filter
    {
        public Filter And(Filter filter)
        {
            return new FilterAnd(this, filter);
        }

        public Filter Or(Filter filter)
        {
            return new FilterOr(this, filter);
        }

        public Filter Not()
        {
            return new FilterNot(this);
        }

        public static Filter operator &(Filter filter1, Filter filter2)
        {
            return filter1.And(filter2);
        }

        public static Filter operator |(Filter filter1, Filter filter2)
        {
            return filter1.Or(filter2);
        }

        public static Filter operator ~(Filter filter)
        {
            return filter.Not();
        }

        public abstract bool Valid(Entity entity);

        public abstract IEnumerable<Type> AssociatedCompTypes();
    }

    public class Filter<T> : Filter where T : class, IComponent
    {
        public override bool Valid(Entity entity)
        {
            return entity.HasComp<T>();
        }

        public override IEnumerable<Type> AssociatedCompTypes()
        {
            yield return typeof(T);
        }
    }

    internal class FilterAnd : Filter
    {
        private readonly Filter filter1;
        private readonly Filter filter2;
        internal FilterAnd(Filter lh, Filter rh)
        {
            filter1 = lh;
            filter2 = rh;
        }

        public override bool Valid(Entity entity)
        {
            return filter1.Valid(entity) && filter2.Valid(entity);
        }

        public override IEnumerable<Type> AssociatedCompTypes()
        {
            foreach(var t in filter1.AssociatedCompTypes())
            {
                yield return t;
            }
            foreach (var t in filter2.AssociatedCompTypes())
            {
                yield return t;
            }
        }
    }

    internal class FilterOr : Filter
    {
        private readonly Filter filter1;
        private readonly Filter filter2;
        internal FilterOr(Filter lh, Filter rh)
        {
            filter1 = lh;
            filter2 = rh;
        }

        public override bool Valid(Entity entity)
        {
            return filter1.Valid(entity) || filter2.Valid(entity);
        }

        public override IEnumerable<Type> AssociatedCompTypes()
        {
            foreach (var t in filter1.AssociatedCompTypes())
            {
                yield return t;
            }
            foreach (var t in filter2.AssociatedCompTypes())
            {
                yield return t;
            }
        }
    }

    internal class FilterNot : Filter
    {
        private readonly Filter filterSource;
        internal FilterNot(Filter source)
        {
            filterSource = source;
        }
     
        public override bool Valid(Entity entity)
        {
            return !filterSource.Valid(entity);
        }

        public override IEnumerable<Type> AssociatedCompTypes()
        {
            foreach (var t in filterSource.AssociatedCompTypes())
            {
                yield return t;
            }
        }
    }
}

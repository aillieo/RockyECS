using System.Collections.Generic;

namespace RockyECS
{
    public interface ISystemBootstrap
    {
        void InitContext();
    }

    public interface IFilteredUpdatingSystem
    {
        Filter[] CreateFilters();
        void Update(int filterIndex, Selection selection, float deltaTime);
    }

    public interface ICompositeSystem
    {
        IEnumerable<BaseSystem> GetSystems();
    }
}

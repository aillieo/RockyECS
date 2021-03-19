using System.Collections.Generic;

namespace RockyECS
{
    public interface ISystemBootstrap
    {
        void InitContext();
    }

    public interface ISelectionProvider
    {
        Filter CreateFilter();
    }

    //public interface ISelectionProvider
    //{
    //    Filter[] CreateFilters();
    //}

    public interface IFilteredUpdatingSystem : ISelectionProvider
    {
        void Update(Selection selection, float deltaTime);
    }

    //public interface IFilteredUpdatingSystem : ISelectionProvider
    //{
    //    void Update(IReadOnlyList<Selection> selections, float deltaTime);
    //}

    public interface IFilteredFrameUpdatingSystem : ISelectionProvider
    {
        void FrameUpdate(Selection selection, float deltaTime);
    }

    public interface ICompositeSystem
    {
        IEnumerable<BaseSystem> GetSystems();
    }
}

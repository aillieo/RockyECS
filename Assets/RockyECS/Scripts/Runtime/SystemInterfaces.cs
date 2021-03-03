using System.Collections.Generic;

namespace RockyECS
{
    public interface ISystem
    {
    }

    public interface ISystemOneShot
    {

    }

    public interface ISelectionProvider
    {
        Filter CreateFilter();
    }

    public interface IUpdatingSystem : ISystem
    {
        void Update(float deltaTime);
    }

    public interface IFrameUpdatingSystem : ISystem
    {
        void FrameUpdate(float deltaTime);
    }

    public interface IFilteredUpdatingSystem : ISystem, ISelectionProvider
    {
        void Update(Selection selection, float deltaTime);
    }

    public interface IFilteredFrameUpdatingSystem : ISystem, ISelectionProvider
    {
        void FrameUpdate(Selection selection, float deltaTime);
    }

    public interface ICompositeSystem : ISystem
    {
        IEnumerable<ISystem> GetSystems();
    }
}

using RockyECS;

namespace Sample
{
    public class S_MoveByDirection : BaseSystem, IFilteredUpdatingSystem
    {
        public Filter[] CreateFilters()
        {
            return new Filter[]
            {
                new Filter<C_MoveByDirection>()
            };
        }

        public void Update(int filterIndex, Selection selection, float deltaTime)
        {
        }
    }

}

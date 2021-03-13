using RockyECS;

namespace Sample
{
    public class S_LevelCleanUp : BaseSystem, IFilteredUpdatingSystem
    {
        public Filter[] CreateFilters()
        {
            return new Filter[]
            {
                new Filter<C_LevelCleanUp>()
            };
        }

        public void Update(int filterIndex, Selection selection, float deltaTime)
        {

        }
    }
}

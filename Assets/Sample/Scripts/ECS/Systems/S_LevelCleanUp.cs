using RockyECS;

namespace Sample
{
    public class S_LevelCleanUp : BaseSystem, IFilteredUpdatingSystem
    {
        public Filter CreateFilter()
        {
            return new Filter<C_LevelCleanUp>();
        }

        public void Update(Selection selection, float deltaTime)
        {
           
        }
    }
}

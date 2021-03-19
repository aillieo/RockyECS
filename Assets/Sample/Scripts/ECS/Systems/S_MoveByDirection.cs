using RockyECS;

namespace Sample
{
    public class S_MoveByDirection : BaseSystem, IFilteredUpdatingSystem
    {
        public Filter CreateFilter()
        {
            return new Filter<C_MoveByDirection>();
        }

        public void Update(Selection selection, float deltaTime)
        {
        }
    }

}

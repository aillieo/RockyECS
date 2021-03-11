using RockyECS;

namespace Sample
{
    public class S_ClickCleanUp : BaseSystem, IFilteredFrameUpdatingSystem
    {
        public Filter CreateFilter()
        {
            return new Filter<C_ClickEvent>();
        }

        public void FrameUpdate(Selection selection, float deltaTime)
        {
            foreach (var s in selection)
            {
                s.RemoveComp<C_ClickEvent>();
            }
        }
    }
}

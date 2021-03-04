using RockyECS;

namespace Sample
{
    public class S_ClickHandler : BaseSystem, IFilteredFrameUpdatingSystem
    {
        public Filter CreateFilter()
        {
            return new Filter<C_ClickEvent>();
        }

        public void FrameUpdate(Selection selection, float deltaTime)
        {
            foreach (var s in selection)
            {
                C_ClickEvent c = s.GetComp<C_ClickEvent>();
                if (c == null)
                {
                    continue;
                }

                s.GetComp<C_Collider>().onClick?.Invoke();
                s.RemoveComp(c);
            }
        }
    }

}

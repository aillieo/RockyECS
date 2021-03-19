using System.Linq;
using RockyECS;

namespace Sample
{
    public class S_ClickCleanUp : BaseSystem, IFilteredUpdatingSystem
    {

        private bool isNewFrame = false;

        public Filter[] CreateFilters()
        {
            return new Filter[]
            {
                new Filter<C_FrameIndex>(),
                new Filter<C_ClickEvent>()
            };
        }

        public void Update(int filterIndex, Selection selection, float deltaTime)
        {
            switch (filterIndex)
            {
                case 0:
                    isNewFrame = selection.First().GetComp<C_FrameIndex>().newFrame;
                    break;
                case 1:
                    if (!isNewFrame)
                    {
                        return;
                    }

                    foreach (var s in selection)
                    {
                        s.RemoveComp<C_ClickEvent>();
                    }

                    break;
            }
        }
    }
}

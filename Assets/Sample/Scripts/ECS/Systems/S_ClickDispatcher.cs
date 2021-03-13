using System.Linq;
using AillieoUtils;
using RockyECS;
using UnityEngine;

namespace Sample
{
    public class S_ClickDispatcher : BaseSystem, IFilteredUpdatingSystem
    {
        public Filter[] CreateFilters()
        {
            return new Filter[]
            {
                new Filter<C_FrameIndex>(),
                new Filter<C_Collider>()
            };
        }

        private bool isNewFrame = false;
        public Handle<Ray> screenRayEventHandle;
        private Ray? lastRay;

        public S_ClickDispatcher()
        {
            screenRayEventHandle = InputManager.Instance.clickEvent.AddListener(OnClickEvent);
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

                    if (lastRay == null)
                    {
                        return;
                    }

                    Ray ray = lastRay.Value;

                    foreach (var s in selection)
                    {
                        C_Collider c = s.GetComp<C_Collider>();
                        if (c == null)
                        {
                            continue;
                        }
                        if (Vector3.ProjectOnPlane(s.GetPosition().ToVec3() - ray.origin, ray.direction).sqrMagnitude <= c.threshold * c.threshold)
                        {
                            s.AddComp<C_ClickEvent>();
                        }
                    }

                    lastRay = null;

                    break;
            }
        }

        private void OnClickEvent(Ray screenRay)
        {
            lastRay = screenRay;
        }
    }

}

using AillieoUtils;
using RockyECS;
using UnityEngine;

namespace Sample
{
    public class S_ClickDispatcher : BaseSystem, IFilteredFrameUpdatingSystem
    {
        public Filter CreateFilter()
        {
            return new Filter<C_Collider>();
        }

        public Handle<Ray> screenRayEventHandle;
        private Ray? lastRay;

        public S_ClickDispatcher()
        {
            screenRayEventHandle = InputManager.Instance.clickEvent.AddListener(OnClickEvent);
        }

        public void FrameUpdate(Selection selection, float deltaTime)
        {
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
        }

        private void OnClickEvent(Ray screenRay)
        {
            lastRay = screenRay;
        }
    }

}

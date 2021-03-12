using System.Collections.Generic;
using System.Linq;
using AillieoUtils;
using RockyECS;
using UnityEngine;

namespace Sample
{
    public class S_FrameCounter : BaseSystem, IFilteredUpdatingSystem
    {
        public Filter CreateFilter()
        {
            return new Filter<C_FrameIndex>();
        }

        public void Update(Selection selection, float deltaTime)
        {
            C_FrameIndex c = selection.First().GetComp<C_FrameIndex>();
            if(c.frame != Time.frameCount)
            {
                c.frame = Time.frameCount;
                c.newFrame = true;
            }
            else
            {
                c.newFrame = false;
            }
        }
    }
}

using RockyECS;

namespace Sample
{
    public class C_FrameIndex : IComponent
    {
        public int frame;
        public bool newFrame;

        public void Reset()
        {
            frame = -1;
            newFrame = false;
        }
    }
}

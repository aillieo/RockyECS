using RockyECS;
using System;

namespace Sample
{
    public class C_Collider: IComponent
    {
        public Action onClick;
        public float threshold = 3f;


        public void Reset()
        {
            onClick = null;
        }
    }
}

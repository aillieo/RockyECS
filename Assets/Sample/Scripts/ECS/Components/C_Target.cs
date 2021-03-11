using RockyECS;

namespace Sample
{
    public class C_Target : IComponent
    {
        public int target;

        public Entity TargetAsEntity(Context context)
        {
            return context.Get(target);
        }

        public void Reset()
        {
        }
    }
}

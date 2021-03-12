using RockyECS;

namespace Sample
{
    public class S_Initialize : BaseSystem, ISystemBootstrap
    {
        public void InitContext()
        {
            Entity entity = context.Add();
            entity.AddComp<C_FrameIndex>();
            entity.AddComp<C_LevelData>();
        }
    }
}

using RockyECS;

namespace Sample
{
    public interface IRecipe
    {
        void Setup(Entity e, Context context);
    }
}

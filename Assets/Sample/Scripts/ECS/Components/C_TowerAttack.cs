using RockyECS;

namespace Sample
{
    public class C_TowerAttack: IComponent
    {
        public float postAttack = 1f;
        public float preAttack = 1f;

        public float timer = 0;
        public Recipe bulletRecipe = Recipe.Get<BulletRecipe>();
        public void Reset()
        {
        }
    }
}

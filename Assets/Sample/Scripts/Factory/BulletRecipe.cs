using RockyECS;

namespace Sample
{
    public class BulletRecipe : Recipe
    {
        public override void Setup(Entity e, Context context)
        {
            e.AddComp<C_Position>();
            e.AddComp<C_Rotation>();


            e.AddComp<C_TargetPos>();
            e.AddComp<C_Target>();

            var hitAlways = e.AddComp<C_BulletHitTargetAlways>();

            var moveToPos = e.AddComp<C_MoveToPos>();
            moveToPos.speed = 8f;


            e.AddComp<C_Renderer>();

            C_Asset asset = e.AddComp<C_Asset>();
            asset.mesh = "Mesh/Bullet";
            asset.material = "Materials/Default";
        }
    }
}

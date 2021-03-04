using RockyECS;
using UnityEngine;

namespace Sample
{
    public class BR_Simple : BulletRecipe
    {
        public override void SetupBullet(Entity bullet)
        {
            
            C_GizmosDrawer gizmosDrawer = bullet.AddComp<C_GizmosDrawer>();
            gizmosDrawer.color = Color.blue;
            gizmosDrawer.size = 0.04f;
            
            bullet.AddComp<C_TargetPos>();
            bullet.AddComp<C_Target>();

            var hitAlways = bullet.AddComp<C_BulletHitTargetAlways>();

            var moveToPos = bullet.AddComp<C_MoveToPos>();
            moveToPos.speed = 8f;
        }
    }

}


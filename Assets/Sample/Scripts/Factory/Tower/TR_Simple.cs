using RockyECS;

namespace Sample
{
    public class TR_Simple : TowerRecipe
    {
        public override void SetupTower(Entity tower)
        {
            TowerEntry metaTower = tower.GetComp<C_TowerConfig>().cfg;

            tower.AddComp<C_GameObject>();
            tower.AddComp<C_Asset>().asset = metaTower.asset;

            tower.AddComp<C_Renderer>();


            var findTarget = tower.AddComp<C_TowerFindTarget>();
            findTarget.range = metaTower.range / 100f;
            tower.AddComp<C_TowerFindTargetGizmos>();


            C_TowerAttack attack = tower.AddComp<C_TowerAttack>();
            attack.timer = 0;
            attack.preAttack = metaTower.postAttack;
            attack.postAttack = metaTower.postAttack;

            tower.AddComp<C_Target>();
        }
    }

}



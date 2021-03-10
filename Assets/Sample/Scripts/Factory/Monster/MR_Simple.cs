using RockyECS;
using System.Collections.Generic;

namespace Sample
{
    public class MR_Simple : MonsterRecipe
    {
        public override void SetupMonster(Entity monster)
        {
            MonsterEntry monsterEntry = monster.GetComp<C_MonsterConfig>().cfg;

            C_MonsterFindPath monsterFindPath = monster.AddComp<C_MonsterFindPath>();
            Entity e = RockyECS.Container.Instance.SelectOne<C_LevelData>();
            LinkedListNode<LevelEntry.TileData> first = e.GetComp<C_LevelData>().paths.First;
            monsterFindPath.target = first;
            monsterFindPath.rotating = 0;


            monster.AddComp<C_TargetPos>();
            monster.AddComp<C_MoveToPos>().speed = monsterEntry.speed / 100f;
            monster.AddComp<C_MoveStart>();

            C_MonsterHp hp = monster.AddComp<C_MonsterHp>();
            int h = monsterEntry.hp / 100;
            hp.max = h;
            hp.rest = h;

            C_Asset asset = monster.AddComp<C_Asset>();
            asset.mesh = "Mesh/MonsterTest"; // monsterEntry.asset;
            asset.material = "Materials/Red";

            monster.AddComp<C_Renderer>();
        }
    }
}


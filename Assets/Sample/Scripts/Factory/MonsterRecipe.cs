using RockyECS;
using System.Collections.Generic;

namespace Sample
{
    public class MonsterRecipe : Recipe
    {
        public override void Setup(Entity e, Context context)
        {
            e.AddComp<C_IdentifyMonster>();
            C_Position position = e.AddComp<C_Position>();
            Entity e1 = context.SelectOne<C_LevelData>();
            LinkedListNode<LevelEntry.TileData> first = e1.GetComp<C_LevelData>().paths.First;
            position.position = first.Value.position;
            e.AddComp<C_Rotation>();


            MonsterEntry monsterEntry = e.GetComp<C_MonsterConfig>().cfg;

            C_MonsterFindPath monsterFindPath = e.AddComp<C_MonsterFindPath>();
            Entity level = context.SelectOne<C_LevelData>();
            monsterFindPath.target = first;
            monsterFindPath.rotating = 0;


            e.AddComp<C_TargetPos>();
            e.AddComp<C_MoveToPos>().speed = monsterEntry.speed / 100f;
            e.AddComp<C_MoveStart>();

            C_MonsterHp hp = e.AddComp<C_MonsterHp>();
            int h = monsterEntry.hp / 100;
            hp.max = h;
            hp.rest = h;

            C_Asset asset = e.AddComp<C_Asset>();
            asset.mesh = "Mesh/MonsterTest"; // monsterEntry.asset;
            asset.material = "Materials/Red";

            e.AddComp<C_Renderer>();
        }
    }
}

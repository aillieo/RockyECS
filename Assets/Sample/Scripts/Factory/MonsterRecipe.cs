using RockyECS;
using System.Collections.Generic;

namespace Sample
{
    public abstract class MonsterRecipe : Recipe
    {
        public override void SetupUnit(Entity e)
        {
            e.AddComp<C_IdentifyMonster>();
            C_Position position = e.AddComp<C_Position>();
            Entity e1 = RockyECS.Container.Instance.SelectOne<C_LevelData>();
            LinkedListNode<LevelEntry.TileData> first = e1.GetComp<C_LevelData>().paths.First;
            position.position = first.Value.position;
            e.AddComp<C_Rotation>();
            SetupMonster(e);
        }
        
        public abstract void SetupMonster(Entity monster);
    }
}

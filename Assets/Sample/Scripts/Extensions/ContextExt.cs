using RockyECS;
using System;

namespace Sample
{
    public static class ContextExt
    {
        public static Entity Create(this Context context, IRecipe recipe)
        {
            Entity entity = context.Add();
            recipe.Setup(entity, context);
            return entity;
        }

        public static Entity CreateTower(this Context context, TowerEntry towerEntry)
        {
            Entity tower = context.Add();
            IRecipe recipe = Recipes.Get<TowerRecipe>();
            if (recipe == null)
            {
                throw new Exception($"获取 {nameof(TowerRecipe)}失败 {towerEntry.recipe}");
            }

            tower.AddComp<C_TowerConfig>().cfg = towerEntry;
            recipe.Setup(tower, context);
            return tower;
        }

        public static Entity CreateMonster(this Context context, MonsterEntry monsterEntry)
        {
            Entity monster = context.Add();
            IRecipe recipe = Recipes.Get<MonsterRecipe>();
            if (recipe == null)
            {
                throw new Exception($"获取 {nameof(MonsterRecipe)}失败 {monsterEntry.recipe}");
            }

            monster.AddComp<C_MonsterConfig>().cfg = monsterEntry;
            recipe.Setup(monster, context);
            return monster;
        }
    }
}

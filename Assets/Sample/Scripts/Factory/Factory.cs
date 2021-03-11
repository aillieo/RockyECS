using RockyECS;
using System;

namespace Sample
{
    public static class Factory
    {
        public static Entity Create(Recipe recipe, Context context)
        {
            Entity entity = context.Add();
            recipe.Setup(entity, context);
            return entity;
        }

        public static Entity CreateTower(TowerEntry towerEntry, Context context)
        {
            Entity tower = context.Add();
            Recipe recipe = Recipe.Get<TowerRecipe>();
            if (recipe == null)
            {
                throw new Exception($"获取 {nameof(TowerRecipe)}失败 {towerEntry.recipe}");
            }

            tower.AddComp<C_TowerConfig>().cfg = towerEntry;
            recipe.Setup(tower, context);
            return tower;
        }

        public static Entity CreateMonster(MonsterEntry monsterEntry, Context context)
        {
            Entity monster = context.Add();
            Recipe recipe = Recipe.Get<MonsterRecipe>();
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

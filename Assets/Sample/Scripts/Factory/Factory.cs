using RockyECS;
using System;

namespace Sample
{
    public static class Factory
    {
        public static Entity CreateTower(TowerEntry metaTower)
        {
            Entity tower = Entity.pool.Get();
            TowerRecipe recipe = Recipe.Get(metaTower.recipe) as TowerRecipe;
            if (recipe == null)
            {
                throw new Exception($"获取 {nameof(TowerRecipe)}失败 {metaTower.recipe}");
            }

            tower.AddComp<C_TowerConfig>().cfg = metaTower;
            recipe.SetupUnit(tower);
            return tower;
        }

        public static Entity CreateMonster(MonsterEntry metaMonster)
        {
            Entity monster = Entity.pool.Get();
            MonsterRecipe recipe = Recipe.Get(metaMonster.recipe) as MonsterRecipe;
            if (recipe == null)
            {
                throw new Exception($"获取 {nameof(MonsterRecipe)}失败 {metaMonster.recipe}");
            }

            monster.AddComp<C_MonsterConfig>().cfg = metaMonster;
            recipe.SetupUnit(monster);
            return monster;
        }
        
        public static Entity CreateBullet(BulletRecipe recipe)
        {
            Entity bullet = Entity.pool.Get();
            recipe.SetupUnit(bullet);
            return bullet;
        }
        
        public static Entity CreateSlot(SlotRecipe recipe)
        {
            Entity slot = Entity.pool.Get();
            recipe.SetupUnit(slot);
            return slot;
        }
    }
}
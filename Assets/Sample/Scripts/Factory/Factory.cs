using RockyECS;
using System;

namespace Sample
{
    public static class Factory
    {
        public static Entity CreateTower(TowerEntry towerEntry)
        {
            Entity tower = Container.Instance.Add();
            TowerRecipe recipe = Recipe.Get(towerEntry.recipe) as TowerRecipe;
            if (recipe == null)
            {
                throw new Exception($"获取 {nameof(TowerRecipe)}失败 {towerEntry.recipe}");
            }

            tower.AddComp<C_TowerConfig>().cfg = towerEntry;
            recipe.SetupUnit(tower);
            return tower;
        }

        public static Entity CreateMonster(MonsterEntry monsterEntry)
        {
            Entity monster = Container.Instance.Add();
            MonsterRecipe recipe = Recipe.Get(monsterEntry.recipe) as MonsterRecipe;
            if (recipe == null)
            {
                throw new Exception($"获取 {nameof(MonsterRecipe)}失败 {monsterEntry.recipe}");
            }

            monster.AddComp<C_MonsterConfig>().cfg = monsterEntry;
            recipe.SetupUnit(monster);
            return monster;
        }
        
        public static Entity CreateBullet(BulletRecipe recipe)
        {
            Entity bullet = Container.Instance.Add();
            recipe.SetupUnit(bullet);
            return bullet;
        }
        
        public static Entity CreateSlot(SlotRecipe recipe)
        {
            Entity slot = Container.Instance.Add();
            recipe.SetupUnit(slot);
            return slot;
        }
    }
}

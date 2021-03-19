using System.Collections.Generic;
using System.Linq;
using AillieoUtils;
using RockyECS;

namespace Sample
{
    public class S_MonsterGenerator : BaseSystem, IFilteredUpdatingSystem
    {
        public Filter CreateFilter()
        {
            return new Filter<C_MonsterGenerator>();
        }

        public void Update(Selection selection, float deltaTime)
        {
            foreach (var s in selection)
            {
            }

            Entity e = selection.First();
            C_MonsterGenerator c = e.GetComp<C_MonsterGenerator>();

            if (c.current == null)
            {
                return;
            }

            if (c.timeForNextWave > 0)
            {
                c.timeForNextWave -= deltaTime;
                if (c.timeForNextWave > 0)
                {
                    return;
                }
            }

            if (c.timeForNextWave < 0)
            {
                float fixTime = -c.timeForNextWave;
                c.timeForNextWave = 0;
                c.timer = fixTime;
                return;
            }

            c.timer += deltaTime;

            while (c.timer > c.current.interval)
            {
                c.timer -= c.current.interval;

                MonsterEntry mm = CfgProxy.Instance.Get<MonsterEntry>(c.current.monsterID);
                Entity monster = context.CreateMonster(mm);

                c.counter++;

                if (c.counter == c.current.count)
                {
                    if (c.currentWave < c.monsterSequences.Count - 1)
                    {
                        c.currentWave++;
                        c.current = c.monsterSequences[c.currentWave];
                        c.ResetCounter();
                    }
                    else
                    {
                        c.current = null;
                    }

                    break;
                }
            }
        }
    }
}

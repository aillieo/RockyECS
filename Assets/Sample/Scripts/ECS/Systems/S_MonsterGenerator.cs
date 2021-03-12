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
            return new Filter<C_LevelData>();
        }

        public void Update(Selection selection, float deltaTime)
        {
            foreach (var s in selection)
            {
            }

            Entity e = selection.First();
            C_LevelData c = e.GetComp<C_LevelData>();

            if(c.loadingFlag < 50)
            {
                if(c.loadingFlag == 49)
                {
                    InitMonsters(c.monsterSequences);
                }

                return;
            }

            Update(deltaTime);
        }

        public int totalWaves { get; private set; }

        public int currentWave { get; private set; }

        public float timeForNextWave { get; private set; }

        private float timer;
        private int counter;

        private List<LevelEntry.Wave> monsterSequences;
        private LevelEntry.Wave current;

        public void InitMonsters(List<LevelEntry.Wave> monsterSequences)
        {
            this.monsterSequences = monsterSequences;
            totalWaves = monsterSequences.Count;
            currentWave = 0;
            current = monsterSequences[currentWave];
        }


        private void ResetCounter()
        {
            timeForNextWave = current.delay;
            timer = 0;
            counter = 0;
        }

        private void Update(float deltaTime)
        {
            if (current == null)
            {
                return;
            }

            if (timeForNextWave > 0)
            {
                timeForNextWave -= deltaTime;
                if (timeForNextWave > 0)
                {
                    return;
                }
            }

            if (timeForNextWave < 0)
            {
                float fixTime = -timeForNextWave;
                timeForNextWave = 0;
                timer = fixTime;
                return;
            }

            timer += deltaTime;

            while (timer > current.interval)
            {
                timer -= current.interval;

                MonsterEntry mm = CfgProxy.Instance.Get<MonsterEntry>(current.monsterID);
                Entity monster = context.CreateMonster(mm);

                counter++;

                if (counter == current.count)
                {
                    if (currentWave < monsterSequences.Count - 1)
                    {
                        currentWave++;
                        current = monsterSequences[currentWave];
                        ResetCounter();
                    }
                    else
                    {
                        current = null;
                    }

                    break;
                }
            }
        }
    }
}

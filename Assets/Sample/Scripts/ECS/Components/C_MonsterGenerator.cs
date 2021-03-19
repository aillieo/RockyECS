using System.Collections.Generic;
using System.Linq;
using AillieoUtils;
using RockyECS;

namespace Sample
{
    public class C_MonsterGenerator : IComponent
    {
        public void Reset()
        {
            ResetCounter();
            current = null;
            currentWave = 0;
            timeForNextWave = 0;
            timer = 0;
            counter = 0;
            monsterSequences.Clear();
        }

        public int currentWave;

        public float timeForNextWave;

        public float timer;
        public int counter;

        public List<LevelEntry.Wave> monsterSequences;
        public LevelEntry.Wave current;

        public void InitMonsters(List<LevelEntry.Wave> monsterSequences)
        {
            this.monsterSequences = monsterSequences;
            currentWave = 0;
            current = monsterSequences[currentWave];
        }


        public void ResetCounter()
        {
            timeForNextWave = current.delay;
            timer = 0;
            counter = 0;
        }
    }
}

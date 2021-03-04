using RockyECS;
using UnityEngine;

namespace Sample
{
    public class StartUp : MonoBehaviour
    {
        private void Start()
        {
            SystemScheduler.Instance
                .AddSystem<S_LevelLoad>()
                .AddSystem<S_MonsterGenerator>()
                .AddSystem<S_MonsterFindPath>()
                .AddSystem<S_MoveToPos>()
                .AddSystem<S_MoveByDirection>()
                .AddSystem<S_MoveToTarget>()
                .AddSystem<S_TowerFindTarget>()
                .AddSystem<S_TowerAttack>()
                .AddSystem<S_GameObject>()
                .AddSystem<S_ClickDispatcher>()
                .AddSystem<S_ClickHandler>()
                .AddSystem<S_MonsterGUIDrawer>()
                .AddSystem<S_GizmosDrawer>()
                .AddSystem<S_TowerFindTargetGizmos>()
                .AddSystem<S_BulletHit>()
                ;

            Destroy(this);
        }
    }
}

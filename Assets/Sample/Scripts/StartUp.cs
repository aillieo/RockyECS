using RockyECS;
using UnityEngine;

namespace Sample
{
    public class StartUp : MonoBehaviour
    {
        private void Start()
        {
            new Engine()
                .AddSystem<S_Initialize>()

                .AddSystem<S_LevelLoad>()
                .AddSystem<S_MonsterGenerator>()
                .AddSystem<S_MonsterFindPath>()
                .AddSystem<S_MoveToPos>()
                .AddSystem<S_MoveByDirection>()
                .AddSystem<S_MoveToTarget>()
                .AddSystem<S_TowerFindTarget>()
                .AddSystem<S_TowerAttack>()
                .AddSystem<S_ResLoad>()
                .AddSystem<S_ClickDispatcher>()
                .AddSystem<S_ClickHandler>()
                .AddSystem<S_Rendering>()
                .AddSystem<S_MonsterGUIDrawer>()
                .AddSystem<S_MainGUIDrawer>()
                .AddSystem<S_GizmosDrawer>()
                .AddSystem<S_TowerFindTargetGizmos>()
                .AddSystem<S_BulletHit>()

                .AddSystem<S_ClickCleanUp>()
                .AddSystem<S_FrameCounter>()
                ;

            Destroy(this);
        }
    }
}

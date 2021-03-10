using AillieoUtils;
using RockyECS;
using System.Collections.Generic;
using UnityEngine;
using Event = AillieoUtils.Event;

namespace Sample
{
    public class GizmosDrawer : SingletonMonoBehaviour<GizmosDrawer>
    {
        public readonly Event gizmosEvent = new Event();
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void EnsureInstance()
        {
            CreateInstance();
        }
        
        public void OnDrawGizmos()
        {
            DrawPath();
            gizmosEvent?.SafeInvoke();
        }

        private void DrawPath()
        {
            Entity e = Container.Instance.SelectOne<C_LevelData>();
            LinkedList<LevelEntry.TileData> paths = e.GetComp<C_LevelData>().paths;

            Color c = Gizmos.color;
            Gizmos.color = Color.black;

            LinkedListNode<LevelEntry.TileData> node = paths.First;
            while(node!= null && node.Next != null)
            {
                Gizmos.DrawLine(node.Value.position.ToVec3(), node.Next.Value.position.ToVec3());
                node = node.Next;
            }

            Gizmos.color = c;
        }
    }
}

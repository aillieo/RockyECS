using AillieoUtils;
using RockyECS;
using UnityEngine;
using Event = AillieoUtils.Event;

namespace Sample
{
    public class IMGUIDrawer : SingletonMonoBehaviour<IMGUIDrawer>
    {
        public Camera cam 
        {
            get
            {
                if (mCam == null) 
                {
                    mCam = Camera.main; 
                }
                return mCam;
            }
        }

        private Camera mCam;

        public readonly Event guiEvent = new Event();
        public void OnGUI()
        {
            guiEvent?.SafeInvoke();
        }

        public Vector2 GetUnitScreenPosForIMGUI(Entity e)
        {
            Vector3 worldPos = e.GetPosition().ToVec3();
            Vector3 screen = Camera.main.WorldToScreenPoint(worldPos);
            Vector2 screenGUI = new Vector2(
                screen.x,
                Screen.height - screen.y);
            return screenGUI;
        }
    }
}

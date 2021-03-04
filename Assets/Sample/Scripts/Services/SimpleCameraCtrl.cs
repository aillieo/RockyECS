using AillieoUtils;
using UnityEngine;

namespace Sample
{
    public class SimpleCameraCtrl : SingletonMonoBehaviour<SimpleCameraCtrl>
    {

        [RuntimeInitializeOnLoadMethod]
        private static void OnLoad()
        {
            CreateInstance();
        }
        
        public float yCut = 0f;
        public Camera current
        {
            get
            {
                if (mCurrentCam == null)
                {
                    mCurrentCam = Camera.main;
                }

                return mCurrentCam;
            }
        }

        private Camera mCurrentCam;


        private Vector3 worldSpace;
        private Vector3 worldSpace2;
        
        private Vector3 ScreenSpace;
        private Vector3 curScreenSpace;
        private Vector3 offset;
        
        private void LateUpdate()
        {
            if (Input.GetMouseButtonDown(0))
            {
                worldSpace = IntersectionOnPlane();
            }

            if (Input.GetMouseButton(0))
            {
                worldSpace2 = IntersectionOnPlane();
                current.transform.position += (worldSpace - worldSpace2);
                
                worldSpace = IntersectionOnPlane();
            }


            float scrollValue = Input.GetAxis("Mouse ScrollWheel");
            if (scrollValue != 0)
            {
                Vector3 pos = current.transform.position;
                pos += 2.2f * scrollValue * current.transform.forward;
                current.transform.position = pos;
            }
        }

        private Vector3 IntersectionOnPlane()
        {
            Ray r = current.ScreenPointToRay(Input.mousePosition);
            float k = (yCut - r.origin.y) / r.direction.y;
            return r.origin + k * r.direction;
        }
        
    }

}

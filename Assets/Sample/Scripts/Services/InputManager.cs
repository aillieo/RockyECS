using AillieoUtils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sample
{
    [DefaultExecutionOrder(-1)]
    public class InputManager : SingletonMonoBehaviour<InputManager>
    {
        public AillieoUtils.Event<Ray> clickEvent = new AillieoUtils.Event<Ray>();

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current && EventSystem.current.IsPointerOverGameObject())
                    return;

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                clickEvent?.Invoke(ray);
            }
        }
    }

}

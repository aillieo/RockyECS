using System;
using System.Collections.Generic;
using AillieoUtils;
using RockyECS;

namespace Sample
{
    public class S_MainGUIDrawer : BaseSystem, IFilteredFrameUpdatingSystem
    {
        private Handle handle;
        private List<Action> guiActions = new List<Action>();

        public S_MainGUIDrawer()
        {
            handle = IMGUIDrawer.Instance.guiEvent.AddListener(OnGUI);
        }

        public Filter CreateFilter()
        {
            return new Filter<C_PlayerProperties>() | new Filter<C_LevelData>();
        }

        public void FrameUpdate(Selection selection, float deltaTime)
        {
            guiActions.Clear();

            foreach (var e in selection)
            {
                guiActions.Add(() => {

                });
            }
        }

        private void OnGUI()
        {
            foreach(var a in guiActions)
            {
                a?.Invoke();
            }
        }
    }
}

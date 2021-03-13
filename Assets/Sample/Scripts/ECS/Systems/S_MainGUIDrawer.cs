using System;
using System.Collections.Generic;
using System.Linq;
using AillieoUtils;
using RockyECS;

namespace Sample
{
    public class S_MainGUIDrawer : BaseSystem, IFilteredUpdatingSystem
    {
        private Handle handle;
        private List<Action> guiActions = new List<Action>();
        private bool isNewFrame = false;

        public S_MainGUIDrawer()
        {
            handle = IMGUIDrawer.Instance.guiEvent.AddListener(OnGUI);
        }

        public Filter[] CreateFilters()
        {
            return new[]
            {
                new Filter<C_FrameIndex>(),
                new Filter<C_PlayerProperties>() | new Filter<C_LevelData>()
            };
        }

        public void Update(int filterIndex, Selection selection, float deltaTime)
        {
            switch (filterIndex)
            {
                case 0:
                    isNewFrame = selection.First().GetComp<C_FrameIndex>().newFrame;
                    break;
                case 1:
                    if (!isNewFrame)
                    {
                        return;
                    }

                    guiActions.Clear();

                    foreach (var e in selection)
                    {
                        guiActions.Add(() => {

                        });
                    }

                    break;
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

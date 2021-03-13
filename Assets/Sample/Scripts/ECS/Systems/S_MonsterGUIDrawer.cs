using System;
using System.Collections.Generic;
using System.Linq;
using AillieoUtils;
using RockyECS;
using UnityEngine;

namespace Sample
{
    public class S_MonsterGUIDrawer : BaseSystem, IFilteredUpdatingSystem
    {
        private Handle handle;
        private List<Action> guiActions = new List<Action>();
        private bool isNewFrame = false;

        public S_MonsterGUIDrawer()
        {
            handle = IMGUIDrawer.Instance.guiEvent.AddListener(OnGUI);
        }

        public Filter[] CreateFilters()
        {
            return new Filter[]
            {
                new Filter<C_FrameIndex>(),
                new Filter<C_Position>() & new Filter<C_MonsterHp>()
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
                        C_MonsterHp hp = e.GetComp<C_MonsterHp>();
                        Vector2 screen = IMGUIDrawer.Instance.GetUnitScreenPosForIMGUI(e);
                        Rect rect = new Rect(screen, new Vector2(400, 400));
                        string text = $"{hp.rest}/{hp.max}";
                        guiActions.Add(() => {
                            GUI.Label(rect, text, new GUIStyle(GUI.skin.label) { fontSize = 20 });
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

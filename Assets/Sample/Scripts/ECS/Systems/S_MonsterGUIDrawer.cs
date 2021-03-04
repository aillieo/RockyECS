using System;
using System.Collections.Generic;
using AillieoUtils;
using RockyECS;
using UnityEngine;

namespace Sample
{
    public class S_MonsterGUIDrawer : BaseSystem, IFilteredFrameUpdatingSystem
    {
        private Handle handle;
        private List<Action> guiActions = new List<Action>();

        public S_MonsterGUIDrawer()
        {
            handle = IMGUIDrawer.Instance.guiEvent.AddListener(OnGUI);
        }

        public Filter CreateFilter()
        {
            return new Filter<C_Position>() & new Filter<C_MonsterHp>();
        }

        public void FrameUpdate(Selection selection, float deltaTime)
        {
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

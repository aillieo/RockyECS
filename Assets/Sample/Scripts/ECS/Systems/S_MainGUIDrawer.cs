using System;
using System.Collections.Generic;
using System.Linq;
using AillieoUtils;
using RockyECS;
using UnityEngine;

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
                new Filter<C_PlayerProperties>() & new Filter<C_LevelData>() & new Filter<C_MonsterGenerator>()
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

                    Entity e = selection.First();

                    C_LevelData data = e.GetComp<C_LevelData>();
                    C_PlayerProperties player = e.GetComp<C_PlayerProperties>();
                    C_MonsterGenerator m = e.GetComp<C_MonsterGenerator>();

                    Rect rect = new Rect(50, 100, 400, 100);
                    string text = $"wave:{m.currentWave} coins:{player.coins} hp:{player.hpRest}/{player.hpMax}";
                    guiActions.Clear();

                    guiActions.Add(() => {
                        GUI.Label(rect, text, new GUIStyle(GUI.skin.label) { fontSize = 20 });
                    });

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

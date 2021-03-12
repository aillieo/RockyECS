using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RockyECS.Editor
{
    public class DebuggerWindow : EditorWindow
    {
        [MenuItem("RockyECS/DebugWindow")]
        public static void Open()
        {
            GetWindow<DebuggerWindow>("RockyECS Debugger Window");
        }

        private void OnEnable()
        {
            EditorApplication.update += Repaint;
        }

        private void OnDisable()
        {
            EditorApplication.update -= Repaint;
        }

        private void OnGUI()
        {
            DrawSpeedCtrl();
            DrawEntities();
        }

        private void DrawSpeedCtrl()
        {
            //EditorGUILayout.BeginHorizontal();
            //GUILayout.Label("Playing");
            //bool playing = Engine.Instance.isPlaying;
            //Engine.Instance.isPlaying = GUILayout.Toggle(playing, GUIContent.none);

            //GUILayout.Label("Playback speed");
            //float timeScale = Engine.Instance.timeScale;
            //Engine.Instance.timeScale = EditorGUILayout.Slider(timeScale, 0.001f, 10);
            //EditorGUILayout.EndHorizontal();
        }

        private void DrawEntities()
        {
            //List<Entity> list1 = ListPool<Entity>.Get();
            //List<Entity> list2 = ListPool<Entity>.Get();

            //GUILayout.Label($"Managed entities: total {list2.Count}");
            //if (list2.Count > 0)
            //{
            //    GUILayout.BeginVertical("box");
            //    foreach (var u in list2)
            //    {
            //        GUILayout.Label(u.ToString());
            //    }
            //    GUILayout.EndVertical();
            //}

            //ListPool<Entity>.Recycle(list1);
            //ListPool<Entity>.Recycle(list2);
        }
    }

}



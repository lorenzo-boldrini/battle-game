using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Soldier_editor_Window : EditorWindow
{
   [MenuItem("Window/Soldier Editor")]

   static void OpenWindow()
    {
        Soldier_editor_Window window = (Soldier_editor_Window)GetWindow(typeof(Soldier_editor_Window));
        window.minSize = new Vector2(600, 300);
        window.Show();
    }
}



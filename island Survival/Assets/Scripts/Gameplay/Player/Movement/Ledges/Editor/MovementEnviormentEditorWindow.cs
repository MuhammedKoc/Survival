using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MovementEnviormentEditorWindow : EditorWindow
{
    [MenuItem("Window/Movement Enviorment Editor")]
    static void OpenWindow()
    {
        MovementEnviormentEditorWindow window = (MovementEnviormentEditorWindow)GetWindow(typeof(MovementEnviormentEditorWindow));
        window.minSize = new Vector2(200, 200);
        window.Show();
    }

    private void OnGUI()
    {
        DrawLayouts();
    }

    void DrawLayouts()
    {
        GUI.Label(new Rect(10f,20f,150,20),"Categories");
        GUI.Button(new Rect(30f, 40f, 50, 30), GUIContent.none);
        
    }
}

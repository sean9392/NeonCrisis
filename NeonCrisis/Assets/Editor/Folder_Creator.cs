using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Folder_Creator : EditorWindow {

    string folder_name;

    [MenuItem("Tools/Michael/Make Folder")]
    public static void Show_Window()
    {
        EditorWindow.GetWindow(typeof(Folder_Creator));
    }

    private void OnGUI()
    {
        folder_name = EditorGUILayout.TextField("Folder Name", folder_name);
        if(GUILayout.Button("Create Folder"))
        {
            AssetDatabase.CreateFolder("Assets", folder_name);
        }
    }
}

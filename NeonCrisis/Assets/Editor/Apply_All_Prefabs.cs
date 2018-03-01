using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Apply_All_Prefabs : MonoBehaviour {

    [MenuItem("Tools/Prefabs/Replace Prefab")]
    public static void Apply_Prefabs()
    {
        GameObject[] all_objects = Selection.gameObjects;
        for(int i = 0; i < all_objects.Length; i++)
        {
            Object prefab = PrefabUtility.GetPrefabParent((Object)all_objects[i]);
            PrefabUtility.ReplacePrefab(all_objects[i], prefab);
        }
    }
}

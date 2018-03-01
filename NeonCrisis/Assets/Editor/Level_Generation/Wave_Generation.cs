using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Wave_Generation : MonoBehaviour {

    //needs positioning stuff doing
    public static void Generate_Wave(string _name, GameObject[] _wave_objects)
    {
        GameObject wave_holder = new GameObject(_name);
        for(int i = 0; i < _wave_objects.Length; i++)
        {
            GameObject wave_object = Instantiate(_wave_objects[i]);
            wave_object.transform.SetParent(wave_holder.transform);
        }
        PrefabUtility.CreatePrefab("Assets/Resources/Prefabs/Enemies/Waves/" + _name + ".prefab", wave_holder);
        DestroyImmediate(wave_holder);
    }
}

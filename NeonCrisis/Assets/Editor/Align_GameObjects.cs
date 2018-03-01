using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Align_GameObjects : MonoBehaviour {

    [MenuItem("Tools/Align/Align_X")]
	public static void Align_X()
    {
        float highest, lowest, final;
        GameObject[] all_objects = Selection.gameObjects;
        highest = all_objects[0].transform.position.x;
        lowest = all_objects[0].transform.position.x;
        for(int i = 0; i <  all_objects.Length; i++)
        {
            if(all_objects[i].transform.position.x > highest)
            {
                highest = all_objects[i].transform.position.x;
            }
            if(all_objects[i].transform.position.x < lowest)
            {
                lowest = all_objects[i].transform.position.x;
            }
        }
        final = (highest - lowest) / 2;
        for(int i = 0; i < all_objects.Length; i++)
        {
            Vector3 position = all_objects[i].transform.position;
            position.x = final;
            all_objects[i].transform.position = position;
        }
    }

    [MenuItem("Tools/Align/Align_Y")]
    public static void Align_Y()
    {
        float highest, lowest, final;
        GameObject[] all_objects = Selection.gameObjects;
        highest = all_objects[0].transform.position.y;
        lowest = all_objects[0].transform.position.y;
        for (int i = 0; i < all_objects.Length; i++)
        {
            if (all_objects[i].transform.position.y > highest)
            {
                highest = all_objects[i].transform.position.y;
            }
            if (all_objects[i].transform.position.y < lowest)
            {
                lowest = all_objects[i].transform.position.y;
            }
        }
        final = lowest + (highest - lowest) / 2;
        for (int i = 0; i < all_objects.Length; i++)
        {
            Vector3 position = all_objects[i].transform.position;
            position.y = final;
            all_objects[i].transform.position = position;
        }
    }

    [MenuItem("Tools/Align/Align_Z")]
    public static void Align_Z()
    {
        float highest, lowest, final;
        GameObject[] all_objects = Selection.gameObjects;
        highest = all_objects[0].transform.position.z;
        lowest = all_objects[0].transform.position.z;
        for (int i = 0; i < all_objects.Length; i++)
        {
            if (all_objects[i].transform.position.z > highest)
            {
                highest = all_objects[i].transform.position.z;
            }
            if (all_objects[i].transform.position.z < lowest)
            {
                lowest = all_objects[i].transform.position.z;
            }
        }
        final = (highest - lowest) / 2;
        for (int i = 0; i < all_objects.Length; i++)
        {
            Vector3 position = all_objects[i].transform.position;
            position.z = final;
            all_objects[i].transform.position = position;
        }
    }
}

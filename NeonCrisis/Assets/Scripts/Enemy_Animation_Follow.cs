using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Animation_Follow : MonoBehaviour {

    public GameObject[] animation_object_array;
    GameObject current_animation_object;
    int animation_index = 0;


	// Use this for initialization
	void Start () {
		
	}
	
    IEnumerator Animate()
    {
        current_animation_object = animation_object_array[animation_index];
        while(animation_index < animation_object_array.Length)
        {
            if(current_animation_object == null)
            {
                current_animation_object = animation_object_array[0];
            }

            yield return new WaitForSeconds(0);
        }
    }

    void Switch_Animation()
    {

    }
}

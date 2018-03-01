using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Laser : MonoBehaviour {

    public GameObject[] laser_objects;

    private void Start()
    {
        Update_Laser_Power(0);
    }

    public void Update_Laser_Power(int _amount)
    {
        for(int i = 0; i < laser_objects.Length; i++)
        {
            if (i <= _amount)
            {
                laser_objects[i].SetActive(true);
            }
            else
            {
                laser_objects[i].SetActive(false);
            }
        }
    }
}

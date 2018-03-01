using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Health : MonoBehaviour {

    public GameObject[] fill_objects;

    public static UI_Health ui_health_instance;

    private void Start()
    {
        ui_health_instance = this;
    }

    public void Update_Health(int _health)
    {
        int new_health = _health;
        for(int i = 0; i < fill_objects.Length - new_health; i++)
        {
            fill_objects[i].SetActive(false);
        }
    }
}

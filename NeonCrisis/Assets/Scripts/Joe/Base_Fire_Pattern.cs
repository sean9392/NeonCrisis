using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Fire_Pattern : MonoBehaviour {

    public GameObject bullet;
    public Transform fire_point;

    public float bullet_speed;
    public float fire_speed;

    public string fire_pattern_type;
    float next_fire_time;

    /*
     * NEEDS TO BE UPDATED TO USE MULTIPLE STATES LIKE CURVE_MOVEMENT
     */
    private void Update()
    {
        if(Time.fixedTime > next_fire_time)
        {
            Select_Fire();
            next_fire_time = Time.fixedTime + fire_speed;
        }
    }

    public void Select_Fire()
    {
        if(bullet != null)
        {
            if(fire_pattern_type == "straight")
            {
                Fire_Straight();
            }
            else if(fire_pattern_type == "circle")
            {
                Fire_Circle();
            }
            else if(fire_pattern_type == "loop")
            {
                Fire_Loop();
            }
            else if(fire_pattern_type == "homing")
            {
                Fire_Homing();
            }
        }

    }
    
    public void Fire_Straight()
    {
        Basic_Bullet bullet_inst = (Instantiate(bullet, fire_point.transform.position, this.transform.rotation) as GameObject).GetComponent<Basic_Bullet>();
        bullet_inst.bullet_speed = bullet_speed;
    }

    public void Fire_Circle()
    {

    }

    public void Fire_Loop()
    {

    }

    public void Fire_Homing()
    {

    }
}

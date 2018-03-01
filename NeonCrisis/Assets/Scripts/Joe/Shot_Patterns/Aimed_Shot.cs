using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aimed_Shot : Shot_Pattern {

    public override void Shoot()
    {
        base.Shoot();
        Player_Controller player_controller = FindObjectOfType<Player_Controller>();
        if (player_controller != null)
        {
            GameObject player_object = player_controller.gameObject;
            if (player_object != null)
            {
                GameObject bullet_inst = Instantiate(bullet, shot_point.transform.position, Quaternion.identity) as GameObject;
                bullet_inst.transform.up = player_object.transform.position - shot_point.transform.position;
            }
        }
        pew_source.Play();
    }
    
}

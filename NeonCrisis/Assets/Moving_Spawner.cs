using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Spawner : MonoBehaviour {
    public enum spawn_types { enemy_left_short, enemy_right_short, enemy_right_mid, enemy_left_mid, enemy_right_long, enemy_left_long, enemy_left_shallow, enemy_right_shallow, enemy_left_zig_zag, enemy_right_zig_zag};
    public spawn_types spawn_type;
    public enum shot_types { circle_shot, aimed_shot, loop_shot, straight_shot};
    public shot_types shot_type;
    public GameObject enemy_to_spawn;
    public GameObject fire_pattern;
    public int health;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject enemy_inst = Instantiate(enemy_to_spawn, this.transform.position, this.transform.rotation) as GameObject;
        GameObject fire_pattern = null;
        switch (shot_type)
        {
            case shot_types.straight_shot:
                fire_pattern = Instantiate(Resources.Load("prefabs/TEMP/SHOT_TYPES/Straight_Shot", typeof(GameObject)) as GameObject, this.transform.position, Quaternion.identity);
                break;
            case shot_types.aimed_shot:
                fire_pattern = Instantiate(Resources.Load("prefabs/TEMP/SHOT_TYPES/Homing_Shot", typeof(GameObject)) as GameObject, this.transform.position, Quaternion.identity);
                break;
            case shot_types.loop_shot:
                fire_pattern = Instantiate(Resources.Load("prefabs/TEMP/SHOT_TYPES/Loop_Shot", typeof(GameObject)) as GameObject, this.transform.position, Quaternion.identity);
                break;
            case shot_types.circle_shot:
                fire_pattern = Instantiate(Resources.Load("prefabs/TEMP/SHOT_TYPES/Circle_Shot", typeof(GameObject)) as GameObject, this.transform.position, Quaternion.identity);
                break;
            }
        GameObject fire_pattern_inst = Instantiate(fire_pattern, this.transform.position, Quaternion.identity);
        fire_pattern_inst.transform.SetParent(enemy_inst.transform);
        fire_pattern_inst.transform.localPosition = Vector3.zero;
        enemy_destroy enemy_destruction = GetComponent<enemy_destroy>();
        if(enemy_destruction != null)
        {
            enemy_destruction.health = health;
        }
        
        Destroy(this.gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (spawn_type == spawn_types.enemy_right_long)
        {
            Gizmos.DrawIcon(this.transform.position, "Enemy_Right_Long");
        }
        else if(spawn_type == spawn_types.enemy_left_long)
        {
            Gizmos.DrawIcon(this.transform.position, "Enemy_Left_Long");
        }
        else if(spawn_type == spawn_types.enemy_left_short)
        {
            Gizmos.DrawIcon(this.transform.position, "Enemy_Left_Short");
        }
        else if (spawn_type == spawn_types.enemy_right_short)
        {
            Gizmos.DrawIcon(this.transform.position, "Enemy_Right_Short");
        }
        else if (spawn_type == spawn_types.enemy_left_mid)
        {
            Gizmos.DrawIcon(this.transform.position, "Enemy_Left_Mid");
        }
        else if (spawn_type == spawn_types.enemy_right_mid)
        {
            Gizmos.DrawIcon(this.transform.position, "Enemy_Right_Mid");
        }
        else if(spawn_type == spawn_types.enemy_left_zig_zag)
        {
            Gizmos.DrawIcon(this.transform.position, "Enemy_Left_Zig_Zag");
        }
        else if(spawn_type == spawn_types.enemy_right_zig_zag)
        {
            Gizmos.DrawIcon(this.transform.position, "Enemy_Right_Zig_Zag");
        }
        else if (spawn_type == spawn_types.enemy_left_shallow)
        {
            Gizmos.DrawIcon(this.transform.position, "Enemy_Left_Shallow");
        }
        else if (spawn_type == spawn_types.enemy_right_shallow)
        {
            Gizmos.DrawIcon(this.transform.position, "Enemy_Right_Shallow");
        }

    }

    
}

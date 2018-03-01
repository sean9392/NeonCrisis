using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Enemy_base : MonoBehaviour {

    [System.Serializable]
    public struct Enemy_Information_Instance
    {
        public float move_speed, fire_rate, fire_speed, start_time;
        public GameObject movement_curve;
        public string fire_pattern_type;
        
    }

    public string enemy_name;
    public Sprite enemy_sprite;
    float collider_size;

    public float move_speed, fire_rate, fire_speed, start_time;
    public int health;
    public GameObject movement_curve;

    public string fire_pattern_type;
    List<float> times = new List<float>();
    int curent_time_index = 0; // dont need to record index if we removing elements anyway right?

    TEST_Follow_Curve curve_holder;
    Base_Fire_Pattern fire_pattern;

    float timer = 0;
    
    public List<Enemy_Information_Instance> BehaviourSets = new List<Enemy_Information_Instance>();

    // Use this for initialization
    void Awake() {
        curve_holder = GetComponent<TEST_Follow_Curve>();
        for(int i = 0; i < BehaviourSets.Count; i++)
        {
            curve_holder.Add_Curve(BehaviourSets[i].movement_curve, i);
        }
        curve_holder.Begin();
        
        //currently only uses one 
        //fire_pattern = GetComponent<Base_Fire_Pattern>();
        //fire_pattern.Setup(fire_rate, fire_speed, BehaviourSets[0].fire_pattern_type);
	}
	
	// Update is called once per frame
	void Update () {
        Assign_Local_Variables();
		//check time against start times
            //if time > start time
                //switch values with struct variables
        
        //go through variables doing shit
	}

    void Assign_Local_Variables()
    {
        /*timer += Time.deltaTime;
        if(timer >= times[0])
        {            
            move_speed = BehaviourSets[0].move_speed;
            fire_rate = BehaviourSets[0].fire_rate;
            fire_speed = BehaviourSets[0].fire_speed;
            start_time = BehaviourSets[0].start_time;
            movement_curve = BehaviourSets[0].movement_curve;
            fire_pattern_type = BehaviourSets[0].fire_pattern_type;
        }*/
        //assign from current_time_index
    }

    void Set_Values()
    {

    }

    public void Enemy_Constructor(string enemyname, Sprite enemySprite, float collidersize, int _health)
    {
        enemy_name = enemyname;
        enemy_sprite = enemySprite;
        collider_size = collidersize;
        health = _health;
    }

    public virtual void EnemyBehaviourConstructor(float movespeed, float firerate, float firespeed, float starttime, GameObject _movement_curve, string _fire_pattern_type)
    {
        Enemy_Information_Instance behaviour_set_instance;
        behaviour_set_instance.move_speed = movespeed;
        behaviour_set_instance.fire_rate = firerate;
        behaviour_set_instance.fire_speed = firespeed;
        behaviour_set_instance.start_time = starttime;
        behaviour_set_instance.movement_curve = _movement_curve;
        behaviour_set_instance.fire_pattern_type = _fire_pattern_type;

        BehaviourSets.Add(behaviour_set_instance);
    }
}

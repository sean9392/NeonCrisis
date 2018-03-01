using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp_Enemy_Spawner : MonoBehaviour {

    // Use this for initialization
    public GameObject enemy;
    public float spawn_delay, spawn_increase;
    public float next_spawn_time;
	
	// Update is called once per frame
	void Update () {
		if(Time.fixedTime > next_spawn_time)
        {
            Instantiate(enemy, this.transform.position, this.transform.rotation);
            next_spawn_time = Time.fixedTime + spawn_delay;
        }

        spawn_delay -= spawn_increase * Time.deltaTime;
        spawn_delay = Mathf.Clamp(spawn_delay, 0.25f, 5f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMP_Fire_Bullet : MonoBehaviour {

    public GameObject bullet;
    float fire_delay = 0.5f;
    float next_fire_time;
	
	// Update is called once per frame
	void Update () {
		if(bullet != null && Time.fixedTime > next_fire_time)
        {
            Instantiate(bullet, this.transform.position, this.transform.rotation);
            next_fire_time = Time.fixedTime + fire_delay;
        }

	}
}

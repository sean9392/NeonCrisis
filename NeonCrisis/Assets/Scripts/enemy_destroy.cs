using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_destroy : MonoBehaviour {

	public GameObject explosion, pickup;
    public int health;

	// Use this for initialization
	void Start () {
        health = 2;

	}
	
	void OnCollisionEnter2D (Collision2D col) {

		if (col.gameObject.CompareTag("Pew")) {            
            if (health <= 0)
            {
                if (explosion != null)
                {
                    GameObject explosion_inst = Instantiate(explosion, this.transform.position, Quaternion.identity) as GameObject;
                    Destroy(explosion_inst, 4);
                }
                if (Random.Range(0, 5) == 1 && pickup != null)
                {
                    Instantiate(pickup, this.transform.position, Quaternion.identity);
                }
                if (Score_Updater.score_updater != null)
                {
                    Score_Updater.score_updater.Add_Score();
                }
                if(Laser_Power_Holder.laser_power_holder_instance != null)
                {
                    Laser_Power_Holder.laser_power_holder_instance.Add_Power();
                }
                Destroy(this.gameObject);
            }
            else
            {
                health--;
            }
		}
	}
}
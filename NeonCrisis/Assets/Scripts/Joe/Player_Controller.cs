using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour {

    public float move_speed;
    public Transform shot_position, shot_position_two, shot_position_three, shot_position_four, shot_position_five;
    Rigidbody2D rigidbody;
    public GameObject bullet;
    public float shot_delay;
    float next_shot_time;
    public Shield shield;
    public AudioSource pew_source;

    int pickup_index;
    int shot_level = 1;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody2D>();

	}
	
	// Update is called once per frame
	void Update () {
        Handle_User_Input();
	}

    void Handle_User_Input()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(hor * move_speed, ver * move_speed, 0);
        rigidbody.velocity = move;

        if(Input.GetKey(KeyCode.Space))
        {
            Fire();
        }
    }

    void Fire()
    {
        if(bullet != null && shot_position != null && (Time.fixedTime > next_shot_time))
        {
            //do firing
            Instantiate(bullet, shot_position.position, this.transform.rotation);
            
            if(pickup_index >= 3)
            {
                Instantiate(bullet, shot_position_two.position, this.transform.rotation);
                Instantiate(bullet, shot_position_three.position, this.transform.rotation);
            }
            if(pickup_index >= 4)
            {
                Instantiate(bullet, shot_position_four.position, this.transform.rotation);
                Instantiate(bullet, shot_position_five.position, this.transform.rotation);
            }
            next_shot_time = Time.fixedTime + shot_delay;

            if (pew_source.isPlaying == false)
            {
                pew_source.Play();
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Weapon_Pickup pickup = collision.GetComponent<Weapon_Pickup>();
        if (pickup != null)
        {
            if (pickup_index < 2)
            {
                shot_delay *= pickup.weapon_speed_multiplier;
                pickup_index++;
                
            }
            if(pickup_index == 2)
            {
                pickup_index++;
            }
            if(pickup_index == 3)
            {
                pickup_index++;
            }
            if(pickup_index > 3)
            {
                Score_Updater.score_updater.Add_Score(10);
            }
            Destroy(pickup.gameObject);
        }
    }    
}

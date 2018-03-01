using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {
   
    public int shield_cost;
    public float shield_time;
    SpriteRenderer shield_renderer;
    CircleCollider2D shield_collider;
    public bool active;

	// Use this for initialization
	void Start () {
        shield_renderer = GetComponent<SpriteRenderer>();
        shield_collider = GetComponent<CircleCollider2D>();
        Deactivate();
	}

    private void Update()
    {
        this.transform.position = this.transform.parent.transform.position;

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if(Laser_Power_Holder.laser_power_holder_instance != null && Laser_Power_Holder.laser_power_holder_instance.Get_Power() >= shield_cost)
            {
                Laser_Power_Holder.laser_power_holder_instance.Take_Power(shield_cost);
                StartCoroutine(Activate_Shield());
            }
        }
    }

    void Activate()
    {
        shield_collider.enabled = true;
        shield_renderer.enabled = true;
        active = true;
    }

    void Deactivate()
    {
        shield_collider.enabled = false;
        shield_renderer.enabled = false;
        active = false;
    }

    IEnumerator Activate_Shield()
    {
        Activate();
        yield return new WaitForSeconds(shield_time);
        Deactivate();
    }

}

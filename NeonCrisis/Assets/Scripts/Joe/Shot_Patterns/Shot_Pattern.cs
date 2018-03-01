using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot_Pattern : MonoBehaviour {

    public GameObject bullet;
    public Transform shot_point;
    public float shot_delay;
    float next_shot_time;
    public AudioSource pew_source;

    private void Start()
    {
        Destroy(this.gameObject, 15);
        this.transform.Rotate(new Vector3(0, 0, 180));
    }

    // Update is called once per frame
    void Update () {
		if(Time.fixedTime > next_shot_time && bullet != null)
        {
            Shoot();
            next_shot_time = Time.fixedTime + shot_delay;
        }
	}

    public virtual void Shoot()
    {

    }
}

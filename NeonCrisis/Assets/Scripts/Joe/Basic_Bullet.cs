using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_Bullet : MonoBehaviour {

    Rigidbody2D rigidbody;
    public float bullet_speed;

	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, 5);
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = this.transform.up * bullet_speed;
	}

    private void OnCollisionEnter2D(Collision2D collision)
    { 
        Destroy(this.gameObject);
    }
}

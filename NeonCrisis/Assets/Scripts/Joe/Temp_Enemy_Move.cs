using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp_Enemy_Move : MonoBehaviour {

    Rigidbody2D enemy_rigidbody;
    public float move_speed, down_move_time, horizontal_move_time;
    public int horizontal = 1;

	// Use this for initialization
	void Start () {
        enemy_rigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(Move());
	}
	
	IEnumerator Move()
    {
        enemy_rigidbody.velocity = new Vector3(0, move_speed, 0);
        yield return new WaitForSeconds(down_move_time);
        enemy_rigidbody.velocity = new Vector3(horizontal * move_speed, 0, 0);
        yield return new WaitForSeconds(horizontal_move_time);
        Destroy(this.gameObject);

    }
}

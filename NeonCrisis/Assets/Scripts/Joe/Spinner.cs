using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour {

    public float speed;
    Vector3 rotation_vector = new Vector3(0, 0, 1);
	
	// Update is called once per frame
	void Update () {
        this.transform.Rotate(rotation_vector * speed * Time.deltaTime);
	}
}

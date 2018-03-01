using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Move : MonoBehaviour {

    public float speed;
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(-this.transform.up * speed * Time.deltaTime);
	}
}

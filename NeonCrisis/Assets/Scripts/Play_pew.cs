using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play_pew : MonoBehaviour {

	public Animator pew;
	public Rigidbody2D move;
	public SpriteRenderer PewFired;
	float thrust = 200;
	// Use this for initialization
	void Start () {

		pew.GetComponent<Animator> (); 
		pew.GetComponent<Rigidbody2D> ();
		pew.GetComponent<SpriteRenderer> ();

        move.AddForce(transform.up * thrust);
	}
	
	// Update is called once per frame
	void Update () {




        /*
		if (Input.GetKeyDown("p")){
			if (pew != null) {
				print ("BLOOP");
			}
			pew.SetTrigger ("Shoot");

			PewFired.enabled = true; 


			move.AddForce (transform.up * thrust);



			}
		

*/
		

	}

}
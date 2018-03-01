using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAnimation : MonoBehaviour {

    [SerializeField]
    private Animator anim;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("L_L"))
        {
            Destroy(gameObject);
            
        }

	}

    public void DestroyLaser()
    {
        Destroy(gameObject);
    }
}

//Michael Is A Cuck

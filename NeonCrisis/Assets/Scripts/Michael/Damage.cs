using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour {

    public int DamageAmount;

    public bool DestroyOnImpact;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("HIT");
        other.gameObject.GetComponent<Health>().DealDamage(DamageAmount);
        if(DestroyOnImpact)
        {
            Destroy(gameObject);
        }
    }
}

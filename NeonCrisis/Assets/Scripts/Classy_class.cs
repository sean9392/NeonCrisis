using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Classy_class : MonoBehaviour {

	public GameObject pew, explosion_object;
    public int range;
    public GameObject pickup;
    

	// Use this for initialization
	void Start () {
        //Destroy(this.gameObject, 8);
	}
	
	// Update is called once per frame
	void Update () {
        
        if(Random.Range(0, range) == 1)
        {
            Instantiate(pew, transform.position, this.transform.rotation);
        }

	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(explosion_object, this.transform.position, this.transform.rotation);
        if (Random.Range(0, 100) < 5)
        {
            Instantiate(pickup, this.transform.position, this.transform.rotation);
        }
        range /= 3;
        if (Score_Updater.score_updater != null)
        {
            Score_Updater.score_updater.Add_Score();
        }
        //Destroy(this.gameObject, 2);
    }

    private void OnDestroy()
    {
        Instantiate(explosion_object, this.transform.position, this.transform.rotation);
        
    }
}

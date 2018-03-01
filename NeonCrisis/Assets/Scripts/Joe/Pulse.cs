using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulse : MonoBehaviour {

    Vector3 initial_scale;

	// Use this for initialization
	void Start () {
        initial_scale = this.transform.localScale;
    }

    private void Update()
    {
        Vector3 local_scale = this.transform.localScale;
        local_scale *= Mathf.Sin(Time.fixedTime * 2);
        this.transform.localScale = initial_scale + local_scale;
    }
}

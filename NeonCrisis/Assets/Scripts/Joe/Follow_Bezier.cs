using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow_Bezier : MonoBehaviour {

    public BezierCurve curve;
    public float time_modifier;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = curve.GetPointAt(Time.fixedTime * time_modifier);
	}
}

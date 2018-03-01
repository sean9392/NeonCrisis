using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
using UnityEngine.UI;

public class DoScalerX : MonoBehaviour {
    public float Xscaletarget1, Xscaletarget2;
    public float speed, stagger_time;
    public Transform arrow1, arrow2;
    public float move_distance, move_speed;
       


    // Use this for initialization
    void Start()
    {
        Invoke("Do_ScalingX", stagger_time);
    }

    void Do_ScalingX()
    {
        DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
        this.transform.DOScaleX(Xscaletarget1, speed).OnComplete(On_Complete);
        arrow1.DOMoveX(-move_distance, speed);
        arrow2.DOMoveX(move_distance, speed);
    }

    void On_Complete()
    {
        this.transform.DOScaleX(Xscaletarget2, speed);
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

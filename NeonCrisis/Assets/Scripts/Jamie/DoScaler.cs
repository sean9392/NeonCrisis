using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
using UnityEngine.UI;

public class DoScaler : MonoBehaviour {
    public float scaletarget1, scaletarget2;
    public float speed, stagger_time;


    // Use this for initialization
    void Start()
    {
        Invoke("Do_Scaling", stagger_time);
    }

    void Do_Scaling()
    {
        DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
        this.transform.DOScale(scaletarget1, speed).OnComplete(On_Complete);
    }

    void On_Complete()
    {
        this.transform.DOScale(scaletarget2, speed);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

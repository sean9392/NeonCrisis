using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Introduction_Triggers : MonoBehaviour {

    public Do_Tween_Title title;
    public Do_Tween_Intro intro;

    public float delay;

	// Use this for initialization
	void Start () {
        StartCoroutine(Stagger_Intro());
	}
	
	IEnumerator Stagger_Intro()
    {
        title.gameObject.SetActive(true);
        while(title.complete != true)
        {
            yield return new WaitForEndOfFrame();
        }
        intro.gameObject.SetActive(true);
    }
}

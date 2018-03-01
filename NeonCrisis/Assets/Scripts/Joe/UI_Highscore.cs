using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Highscore : MonoBehaviour {

    Text highscore_text;

	// Use this for initialization
	void Start () {
        highscore_text = GetComponent<Text>();
        Show_Highscore();
	}

    public void Show_Highscore()
    {
        if(highscore_text != null && PlayerPrefs.HasKey("Highscore"))
        {
            highscore_text.text = PlayerPrefs.GetInt("Highscore").ToString();
        }
    }
}

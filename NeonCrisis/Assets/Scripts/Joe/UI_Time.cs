using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Time : MonoBehaviour {
    public Text time_text;
    float minutes_raw, seconds_raw;
    string min_string, sec_string;

    private void Start()
    {
        time_text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update () {
        if (time_text != null)
        {
            minutes_raw = Time.fixedTime / 60;
            seconds_raw = Time.fixedTime % 60;
            min_string = ((int)minutes_raw).ToString();
            sec_string = ((int)seconds_raw).ToString();
            if (min_string.Length <= 1)
            {
                min_string = "0" + min_string;
            }
            if (sec_string.Length <= 1)
            {
                sec_string = "0" + sec_string;
            }
            time_text.text = (min_string + ":" + sec_string);
        }
	}
}

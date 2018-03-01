using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Stage : MonoBehaviour {

    public float preceeding_build_indexes;
    Text stage_text;

	// Use this for initialization
	void Start () {
        stage_text = GetComponent<Text>();
        if (stage_text != null)
        {
            int index = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
            stage_text.text = ((index + preceeding_build_indexes) + 1).ToString();
        }
        
	}
}

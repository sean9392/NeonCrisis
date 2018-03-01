using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score_Updater : MonoBehaviour {

    public static Score_Updater score_updater;
    Text score_text;
    public int score = 0;

	// Use this for initialization
	void Start () {
        score_updater = this;
        score_text = GetComponent<Text>();
	}
	
    public void Add_Score()
    {
        score++;
        score_text.text = score.ToString();
    }

    public void Add_Score(int _amount)
    {
        score += _amount;
        score_text.text = score.ToString();
    }

    public void Take_Score(int _amount)
    {
        score -= _amount;
        if(score < 0)
        {
            score = 0;
        }
        score_text.text = score.ToString();
    }

    public void On_End()
    {
        Test_Highscore_Code();
    }

    void Test_Highscore_Code()
    {
        if(PlayerPrefs.HasKey("Highscore") == false)
        {
            PlayerPrefs.SetInt("Highscore", score);
        }
        else
        {
            if (score > PlayerPrefs.GetInt("Highscore"))
            {
                PlayerPrefs.SetInt("Highscore", score);
            }
        }
    }

    void Full_Highscore_Code()
    {

    }
}

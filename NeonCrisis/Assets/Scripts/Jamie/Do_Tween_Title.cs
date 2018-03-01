using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Do_Tween_Title : MonoBehaviour {

    public Transform rect_one, rect_two, rect_three, rect_four;
    public SpriteRenderer text;
    public float max_scale, max_scale_exception, scale_speed, fade_speed, delay_time;
    public bool complete;
    void Start()
    {
        Invoke("Fade_In_Text", delay_time);
    }

    void Fade_In_Text()
    {
        text.DOFade(1, fade_speed).OnComplete(Rect_One);
    }

    void Rect_One()
    {
        rect_one.gameObject.SetActive(true);
        rect_one.DOScale(max_scale, scale_speed).OnComplete(Rect_One_Contract);
    }

    void Rect_One_Contract()
    {
        //Rect_Two();
        rect_one.DOScale(1, scale_speed).OnComplete(Rect_Two);
    }

    void Rect_Two()
    {
        rect_two.gameObject.SetActive(true);
        rect_two.DOScale(max_scale, scale_speed).OnComplete(Rect_Two_Contract);
    }

    void Rect_Two_Contract()
    {
        rect_two.DOScale(1, scale_speed).OnComplete(Rect_Three);
        //Rect_Three();
    }

    void Rect_Three()
    {
        rect_three.gameObject.SetActive(true);
        rect_three.DOScale(max_scale, scale_speed).OnComplete(Rect_Three_Contract);
    }

    void Rect_Three_Contract()
    {
        rect_three.DOScale(1, scale_speed).OnComplete(Rect_Four);
        //Rect_Four();
    }

    void Rect_Four()
    {
        rect_four.gameObject.SetActive(true);
        rect_four.DOScale(max_scale, scale_speed).OnComplete(Rect_Four_Contract);
    }

    void Rect_Four_Contract()
    {
        rect_four.DOScale(max_scale_exception, scale_speed).OnComplete(Complete);
    }

    void Complete()
    {
        complete = true;
    }
}

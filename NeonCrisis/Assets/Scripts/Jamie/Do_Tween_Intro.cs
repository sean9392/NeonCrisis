using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Do_Tween_Intro : MonoBehaviour {
    public Transform left_arrow, right_arrow, box;
    public SpriteRenderer start_render, left_arrow_renderer, right_arrow_renderer;
    public float move_distanceX, move_distanceY, movespeedX, movespeedY, scalespeed, fadespeed, delaytime, arrow_fade_speed;

	void Start () {
        Invoke("Fade_Arrows", delaytime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Fade_Arrows()
    {
        left_arrow_renderer.DOFade(1, arrow_fade_speed);
        right_arrow_renderer.DOFade(1, arrow_fade_speed).OnComplete(Move_Arrows);
    }

    void Move_Arrows()
    {
        left_arrow.DOMoveY(this.transform.position.y + move_distanceY, movespeedY);
        right_arrow.DOMoveY(this.transform.position.y + -move_distanceY, movespeedY).OnComplete(Move_Arrows_Secondary);
        box.DOScaleY(1, movespeedY);
    }

    void Move_Arrows_Secondary()
    {
        left_arrow.DOMoveX(this.transform.position.x + -move_distanceX, movespeedX);
        right_arrow.DOMoveX(this.transform.position.x + move_distanceX, movespeedX);
        Scale_Box();
    }

    void Scale_Box()
    {
        box.DOScaleX(1, scalespeed).OnComplete(Fade_Text);
    }

    void Fade_Text()
    {
        start_render.DOFade(1, fadespeed);
    }
}

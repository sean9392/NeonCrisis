using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFlash : MonoBehaviour {

    public GameObject flash_object;
    public float flash_speed;
    Material material;
    SpriteRenderer sprite;
    Image image;

	// Use this for initialization
	void Start () {
        sprite = flash_object.GetComponent<SpriteRenderer>();
        //image = GetComponent<Image>();
        material = sprite.material;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartCoroutine(Flash());
        }
	}

    public void Flash_Button()
    {
        StartCoroutine("Flash");
    }

    IEnumerator Flash()
    {
        material.SetFloat("_FlashAmount", 1);
        yield return new WaitForSeconds(flash_speed);
        material.SetFloat("_FlashAmount", 0);
        yield return new WaitForSeconds(flash_speed);
        material.SetFloat("_FlashAmount", 1);
        yield return new WaitForSeconds(flash_speed);
        material.SetFloat("_FlashAmount", 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Changer : MonoBehaviour {

	Material sprite_rend_mat;
	float colour_base = 125f;
    public float speed = 5;
    Vector4[] seven_vectors;

    private void Start()
    {
        seven_vectors = new Vector4[7];
        seven_vectors[0] = new Vector4(1, 0.5f, 0.5f);
        seven_vectors[1] = new Vector4(1, 1, 0.5f);
        seven_vectors[2] = new Vector4(0.5f, 1, 0.5f);
        seven_vectors[3] = new Vector4(0.5f, 1, 1);
        seven_vectors[4] = new Vector4(0.5f, 0.5f, 1);
        seven_vectors[5] = new Vector4(1, 0.5f, 1);
        seven_vectors[6] = new Vector4(1, 0.5f, 0.5f);

        sprite_rend_mat = GetComponent<SpriteRenderer>().sharedMaterial;
        if (sprite_rend_mat != null)
        {
            StartCoroutine(Cycle_Color());
        }
    }
    IEnumerator Cycle_Color()
    {
        //_MKGlowTexColor
        Vector4 vec = sprite_rend_mat.GetVector("_MKGlowTexColor");
        int last_index = 0;
        int rand_index = 0;
        vec.w = 0;
        while(true)
        {
            while (rand_index == last_index)
            {
                rand_index = Random.Range(0, seven_vectors.Length);
            }
            while(vec != seven_vectors[rand_index])
            {
                
                vec.x = Mathf.MoveTowards(vec.x, seven_vectors[rand_index].x, speed * Time.deltaTime);
                vec.y = Mathf.MoveTowards(vec.y, seven_vectors[rand_index].y, speed * Time.deltaTime);
                vec.z = Mathf.MoveTowards(vec.z, seven_vectors[rand_index].z, speed * Time.deltaTime);
                sprite_rend_mat.SetVector("_MKGlowTexColor", vec);
                yield return new WaitForEndOfFrame();
            }
            last_index = rand_index;
        }
    }
    
}

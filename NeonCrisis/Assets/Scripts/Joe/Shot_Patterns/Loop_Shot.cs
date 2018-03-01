using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loop_Shot : Shot_Pattern {

    public float radius, rotation_delay;
    public int angle_skip;

    public override void Shoot()
    {
        base.Shoot();
        StartCoroutine(Shooting());
    }

    IEnumerator Shooting()
    {
        float skipped = 0.0f;
        while(skipped < 360f)
        {
            skipped += angle_skip;
            shot_point.transform.Rotate(new Vector3(0, 0, angle_skip));
            //Vector2 xy = new Vector2(shot_point.transform.position.x + radius * Mathf.Cos(i), shot_point.transform.position.y + radius * Mathf.Sin(i));
            GameObject bullet_inst = Instantiate(bullet, shot_point.transform.position, Quaternion.identity) as GameObject;
            bullet_inst.transform.up = shot_point.transform.up;//xy - (Vector2)this.transform.position;
            yield return new WaitForSeconds(rotation_delay);
            pew_source.Play();
        }
    }
}

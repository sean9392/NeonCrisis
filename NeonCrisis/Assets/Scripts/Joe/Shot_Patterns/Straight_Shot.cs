using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Straight_Shot : Shot_Pattern {

    public override void Shoot()
    {
        base.Shoot();
        GameObject bullet_inst = Instantiate(bullet, this.transform.position, Quaternion.identity);
        bullet_inst.transform.up = shot_point.transform.up;
        pew_source.Play();
    }
}

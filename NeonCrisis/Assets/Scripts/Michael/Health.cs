using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    Enemy_base EnemyBase;

    private void Start()
    {
        EnemyBase = GetComponent<Enemy_base>();
    }

    public void DealDamage(int damage)
    {
        EnemyBase.health -= damage;
        CheckHealth();
    }

    private void CheckHealth()
    {
        if(EnemyBase.health <= 0)
        {
            Destroy(gameObject);

        }
    }
}

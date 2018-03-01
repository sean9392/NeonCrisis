using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

    private enum Weapon_Modes
    {
        Empty,
        Pew,
        Laser
    }

    [SerializeField]
    Weapon_Modes CurrentWeapon;

    [SerializeField]
    private GameObject Pew;

    [SerializeField]
    private GameObject Laser;

    public float FireRate;
    private float Timer;

    private GameObject LaserTemp;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Timer += Time.deltaTime;

        UpdateState();

        if(CurrentWeapon == Weapon_Modes.Pew && Timer > FireRate)
        {
            Instantiate(Pew, transform.position, Quaternion.identity);

            Timer = 0;
        }
        else if(CurrentWeapon == Weapon_Modes.Laser)
        {
            if (LaserTemp == null)
            {
            LaserTemp = Instantiate(Laser, transform.GetChild(0).transform.position, Quaternion.identity);
            LaserTemp.transform.parent = transform.GetChild(0);

            }
        }


	}


    void UpdateState ()
    {
        if (Input.GetKey("p"))
        {
            CurrentWeapon = Weapon_Modes.Pew;
        }
        else if (Input.GetKey("l"))
        {
            CurrentWeapon = Weapon_Modes.Laser;
        }
        else
        {
            CurrentWeapon = Weapon_Modes.Empty;
        }


    }
}

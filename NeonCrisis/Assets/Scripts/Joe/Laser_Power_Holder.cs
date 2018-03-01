using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_Power_Holder : MonoBehaviour
{
    public static Laser_Power_Holder laser_power_holder_instance;
    UI_Laser ui_laser;
    int power_level;

    private void Start()
    {
        ui_laser = FindObjectOfType<UI_Laser>();
        laser_power_holder_instance = this;
    }

    public void Add_Power()
    {
        power_level++;
        Update_Power();
    }

    public void Add_Power(int _amount)
    {
        
        power_level += _amount;
        Update_Power();
    }

    void Update_Power()
    {
        if(ui_laser == null)
        {
            ui_laser = FindObjectOfType<UI_Laser>();
        }
        if(ui_laser != null)
        {
            ui_laser.Update_Laser_Power(power_level);
        }
    }

    public int Get_Power()
    {
        return power_level;
    }
    
    public int Take_Power(int _amount)
    {
        if(power_level > _amount)
        {
            power_level -= _amount;
            Update_Power();
            return _amount;
        }
        else
        {
            int power = power_level;
            power_level = 0;
            Update_Power();
            return power;
        }
    }
}
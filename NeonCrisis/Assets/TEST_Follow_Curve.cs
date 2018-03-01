using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_Follow_Curve : MonoBehaviour {

    List<GameObject> curve_objects = new List<GameObject>();

    List<GameObject> instantiated_curves = new List<GameObject>();
    List<BezierCurve> curves = new List<BezierCurve>();
    List<Vector3> end_positions = new List<Vector3>();

    public BezierCurve current_curve;

    int curve_index = 0;

    public float speed;
    float time = 0;

    public void Set_Speed(float _speed)
    {
        speed = _speed;
    }

    void Setup()
    {
        Instantiate_Curves();
        Get_Curve_Components();
        Get_End_Points();
        Line_Up();
        current_curve = curves[curve_index];
    }

    public void Add_Curve(GameObject _curve_object, int _index)
    {
        curve_objects.Add(_curve_object);
    }

    void Instantiate_Curves()
    {
        for(int i = 0; i < curve_objects.Count; i++)
        {
            instantiated_curves.Add(Instantiate(curve_objects[i], this.transform.position, Quaternion.identity) as GameObject);
        }
    }

    void Get_Curve_Components()
    {
        for(int i=  0; i < instantiated_curves.Count; i++)
        {
            curves.Add(instantiated_curves[i].GetComponent<BezierCurve>());
        }
    }
    
    void Get_End_Points()
    {
        for(int i = 0; i < curves.Count; i++)
        {
            Vector3 position = curves[i].GetAnchorPoints()[curves[i].pointCount - 1].position; //get the last point position of the previous curve
            end_positions.Add(position);
        }
    }

    void Line_Up()
    {
        instantiated_curves[0].transform.position = this.transform.position;
        for(int i = 1; i < curves.Count; i++)
        {
            curves[i].transform.position = end_positions[i - 1];
        }
    }


    public void Begin()
    {
        Setup();
    }

    private void Update()
    {
        time += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        Move_Along_Curve();
        Switch_Curve();
    }

    void Move_Along_Curve()
    {
        if (current_curve != null)
        {
            this.transform.position = current_curve.GetPointAt(time * (speed * Time.deltaTime));
            Vector3 target = current_curve.GetPointAt(time * (speed * Time.deltaTime));
            this.transform.position = Vector3.MoveTowards(this.transform.position, target, speed * Time.deltaTime);
        }
    }

    void Switch_Curve()
    {
        if(this.transform.position == current_curve.GetPointAt(1))
        {
            if (curve_index + 1 < curves.Count || curve_index == 0)
            {
                time = 0;
                curve_index++;
                current_curve = curves[curve_index];
            }
            else
            {
                End();
            }
        }
    }

    void End()
    {
        for(int i = 0; i < instantiated_curves.Count; i++)
        {
            Destroy(instantiated_curves[i].gameObject);
        }
        Destroy(current_curve.gameObject);
        Destroy(this.gameObject);
    }

    void OnDestroy()
    {
        for (int i = 0; i < instantiated_curves.Count; i++)
        {
            Destroy(instantiated_curves[i].gameObject);
        }
        Destroy(current_curve.gameObject);
    }

}

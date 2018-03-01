using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diff_Fix : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Fix_Diff();
    }

    void Fix_Diff()
    {
        BezierCurve curve = GetComponent<BezierCurve>();
        Vector3 point_zero_offset = curve.GetPointAt(0) - this.transform.position;
        BezierPoint[] points = curve.GetAnchorPoints();
        for (int i = 0; i < points.Length; i++)
        {
            points[i].transform.localPosition = points[i].transform.localPosition - point_zero_offset;
        }
    }

}

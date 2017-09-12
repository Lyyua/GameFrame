using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodes
{
    public double x = 100;
    public double y = 100;
    public double z = 100;

    public double x_e;
    public double y_e;
    public double z_e;

    public void SetVector3(double x, double y, double z)
    {
        this.x = double.Parse(Convert.ToDouble(x).ToString("0.0000"));
        this.y = double.Parse(Convert.ToDouble(y).ToString("0.0000"));
        this.z = double.Parse(Convert.ToDouble(z).ToString("0.0000"));
    }

    public Vector3 GetVector3()
    {
        return new Vector3((float)x, (float)y, (float)z);
    }

    public void SetEuler(double x, double y, double z)
    {
        this.x_e = double.Parse(Convert.ToDouble(x).ToString("0.0000"));
        this.y_e = double.Parse(Convert.ToDouble(y).ToString("0.0000"));
        this.z_e = double.Parse(Convert.ToDouble(z).ToString("0.0000"));
    }

    public Vector3 GetEuler()
    {
        return new Vector3((float)x_e, (float)y_e, (float)z_e);
    }
}
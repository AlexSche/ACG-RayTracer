using System;
using UnityEngine;

public class SphereObject : GeometryObject
{
    private GameObject sphere;
    private double radius;
    //private Vector3 position;

    public SphereObject(Vector3 position)
    {
        //this.position = position;
        createPrimitive();
        sphere.transform.position = position;
        sphere.transform.localScale *= 20;
        radius = sphere.GetComponent<MeshRenderer>().bounds.extents.magnitude;
    }
    override public double intersect(Vector3 pos, Vector3 dir)
    {
        double b, t, s;
        double xadj, yadj, zadj;

        /* translate ray origin to object's space */
        xadj = pos.x - sphere.transform.position.x;
        yadj = pos.y - sphere.transform.position.y;
        zadj = pos.z - sphere.transform.position.z;

        /* solve quadratic equation */
        b = xadj * dir.x + yadj * dir.y + zadj * dir.z;
        t = b * b - xadj * xadj - yadj * yadj - zadj * zadj + radius * radius;
        if (t < 0) return 0.0;

        s = -b - Math.Sqrt(t);
        /* try smaller solution */
        if (s > 0) return s;

        s = -b + Math.Sqrt(t);
        /* try larger solution */
        if (s > 0) return s;

        return 0.0; /* both solutions <= zero */
    }

    public override Vector3 normalizeVector(Vector3 pos)
    {
        return (pos - sphere.transform.position) / (float)radius;
    }

    public void createPrimitive()
    {
        sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    }
}

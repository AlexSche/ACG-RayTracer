using UnityEngine;
public class GeometryObject
{
    private Vector3 intersectionPoint;
    private Ray rayThatHit;
    public Vector3 IntersectionPoint { get => intersectionPoint; set => intersectionPoint = value; }
    public Ray RayThatHit { get => rayThatHit; set => rayThatHit = value; }

    public virtual double intersect(Ray ray) {
        RayThatHit = ray;
        return 0.0;
    }

    public virtual Vector3 normalizeVector(Vector3 pos) {
        return new Vector3(0,0,0);
    }

    public virtual Color getColorAtIntersection(Lightning lightning, Ray ray) {
        return Color.white;
    }
}

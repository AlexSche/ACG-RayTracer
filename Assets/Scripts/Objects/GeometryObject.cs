using UnityEngine;
public class GeometryObject
{
    public virtual double intersect(Ray ray) {
        return 0.0;
    }

    public virtual Vector3 normalizeVector(Vector3 pos) {
        return new Vector3(0,0,0);
    }

    public virtual Color getColorAtIntersection(Lightning lightning, Ray ray) {
        return Color.white;
    }
}

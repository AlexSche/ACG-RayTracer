using UnityEngine;
public class GeometryObject
{
    public virtual double intersect(Vector3 pos, Vector3 dir) {
        return 0.0;
    }

    public virtual Vector3 normalizeVector(Vector3 pos) {
        return new Vector3(0,0,0);
    }
}

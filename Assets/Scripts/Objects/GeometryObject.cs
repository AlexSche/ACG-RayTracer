using UnityEngine;
public class GeometryObject
{
    private PrimitiveType primitiveType;
    private Vector3 intersectionPoint;
    private Ray rayThatHit;
    public Vector3 IntersectionPoint { get => intersectionPoint; set => intersectionPoint = value; }
    public Ray RayThatHit { get => rayThatHit; set => rayThatHit = value; }

    public virtual double intersect(Ray ray)
    {
        RayThatHit = ray;
        return 0.0;
    }

    public virtual Vector3 getNormVector(Vector3 pos)
    {
        return new Vector3(0, 0, 0);
    }

    public virtual Color getColorAtIntersection(Lightning lightning, Ray ray)
    {
        return Color.white;
    }

    public virtual Ray getReflectedRay(Ray ray)
    {
        Vector3 reflectedDirection = ray.direction;
        Vector3 norm = getNormVector(IntersectionPoint);
        double k = -2.0 * Vector3.Dot(ray.direction, norm);
        reflectedDirection = norm * (float)k + ray.direction;
        return new Ray(IntersectionPoint, reflectedDirection);
    }

    public virtual Ray getLightningRay(Lightning lightning, Ray ray)
    {
        Vector3 lightDir = lightning.getDirectionToLightning(IntersectionPoint);
        Ray lightRay = new Ray(IntersectionPoint, lightDir);
        return lightRay;
    }

    public static GeometryObject intersectObjects(GeometryObject obj, Ray ray, Scene scene)
    {
        GeometryObject hitObject = null;
        double distance, maxDistance;
        maxDistance = double.MaxValue;

        // check ray intersection with all objects in storage
        scene.geometryObjectStorage.objects.ForEach(anObj =>
        {
            if (anObj == null)
            {
                return;
            }
            if (anObj != obj)
            {
                distance = anObj.intersect(ray); // Check intersection
                // keep track of closest intersection
                if ((distance > 0.0) && (distance <= maxDistance))
                {
                    hitObject = anObj;
                    maxDistance = distance;
                }
            }
        });

        if (hitObject == null) // ray hit no object
        {
            return null;
        }
        else // ray hit an object
        {
            // set point of intersection
            hitObject.IntersectionPoint = ray.origin + ray.direction * (float)maxDistance;
            /*
            find normal -> Why do I have to find normal here? -> Is used in the calculation for the reflected Ray!
            normal = hitObject.normalizeVector(hit);
            */
            return hitObject;
        }
    }
}

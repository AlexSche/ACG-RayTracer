using System;
using UnityEngine;

public class SphereObject : GeometryObject
{
    private GameObject sphere;
    private double radius;

    public SphereObject(Vector3 position)
    {
        createPrimitive();
        sphere.transform.position = position;
        sphere.transform.localScale *= 20;
        radius = sphere.GetComponent<MeshRenderer>().bounds.extents.magnitude;
    }
    override public double intersect(Ray ray)
    {
        double b, t, s;
        double xadj, yadj, zadj;

        /* translate ray origin to object's space */
        xadj = ray.origin.x - sphere.transform.position.x;
        yadj = ray.origin.y - sphere.transform.position.y;
        zadj = ray.origin.z - sphere.transform.position.z;

        /* solve quadratic equation */
        b = xadj * ray.direction.x + yadj * ray.direction.y + zadj * ray.direction.z;
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

    public override Vector3 getNormVector(Vector3 pos)
    {
        return (pos - sphere.transform.position) / (float)radius;
    }

    public override Ray getReflectedRay(Ray ray)
    {
        Vector3 reflectedDirection = ray.direction;
        Vector3 norm = getNormVector(IntersectionPoint);
        double k = -2.0 * Vector3.Dot(ray.direction, norm);
        reflectedDirection = norm * (float)k + ray.direction;
        return new Ray(IntersectionPoint, reflectedDirection);
    }

    public override Color getColorAtIntersection(Lightning lightning, Ray ray)
    {
        Ray lightRay = getLightningRay(lightning, ray);
        if (lightning.lightningRayGetsIntersected(this, lightRay))
        {
            // shadow since there is an object in the way
            return Color.grey;
        }
        else
        {
            //calculate color for this pixel with Phong
            //"Im Phong-Beleuchtungsmodell wird die Reflexion von Licht als Kombination aus ambienter, ideal diffuser und ideal spiegelnder Reflexion beschrieben. "
            // Iout = Iambient + Idiffuse + Ispecular
            // calculate ambient light - Iambient = Ia*kambient
            Color ambientLight = lightning.getAmbientLight();
            // calculate diffuse light - Idiffuse = Iin * kdiffuse * (L dot N)
            Color diffuseColor = Color.green;
            float kdiffuse = Vector3.Dot(RayThatHit.direction, lightRay.direction.normalized);
            Color diffuseLight = diffuseColor * lightning.getLightColor() * lightning.getLightIntensity() * kdiffuse;
            // calculate specular light - Ispecular = Iin * kspecular * (R dot V)
            Color specularColor = Color.yellow;
            //float shine = 0.4f;
            int exponent = 3;
            var RdotV = Vector3.Dot(lightRay.direction.normalized, (lightRay.direction + RayThatHit.direction).normalized);
            var specTerm = Mathf.Pow(RdotV, exponent) * (exponent + 2)/(2*MathF.PI);
            Color specularLight = specularColor * lightning.getLightColor() * lightning.getLightIntensity() * specTerm;
            Color phong = ambientLight * diffuseLight * specularLight;
            return phong;
        }
    }

    public void createPrimitive()
    {
        sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    }
}

using UnityEngine;

public class Lightning
{
    private GameObject lightGameObject;
    private Light lightComp;
    private float brightness;
    private float distanceToLight;
    public float Brightness { get => brightness; set => brightness = value; }

    public Lightning(Vector3 lightPos)
    {
        lightGameObject = new GameObject("Lightning");
        lightComp = lightGameObject.AddComponent<Light>();
        lightGameObject.transform.position = lightPos;
    }

    public void setPosition(Vector3 position)
    {
        lightGameObject.transform.position = position;
    }

    public Vector3 getPosition() {
        return lightGameObject.transform.position;
    }

    public Vector3 getDirectionToLightning(Vector3 objectPosition)
    {
        Vector3 lightningDirection;
        lightningDirection = objectPosition - lightGameObject.transform.position;
        distanceToLight = Vector3.Magnitude(lightningDirection);
        return lightningDirection;
    }

    public bool lightningRayGetsIntersected(GeometryObject sourceObject, Ray ray) {
        double section;
        for (int i = 0; i < Scene.Instance.geometryObjectStorage.objects.Count; i++) {
            GeometryObject geometryObject = Scene.Instance.geometryObjectStorage.objects[i];
            if (geometryObject !=sourceObject) {
                section = geometryObject.intersect(ray);
                if ((section > 0.0) && (section <= distanceToLight)) {
                    return true;
                }
            }
        }
        return false;
    }

    public Color getAmbientLight() {
        Color ambientColor = RenderSettings.ambientLight; //Intensität des Umgebungslichts - Ia
        float ambientIntensity = RenderSettings.ambientIntensity; //Materialkonstante - kambient
        return ambientColor * ambientIntensity;
    }

    public Color getLightColor() {
        return lightComp.color;
    }

    public float getLightIntensity() {
        return lightComp.intensity;
    }
}

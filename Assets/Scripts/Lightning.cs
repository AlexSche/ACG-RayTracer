using UnityEngine;

public class Lightning
{
    private GameObject lightGameObject;
    private float brightness;
    private float distanceToLight;
    public float Brightness { get => brightness; set => brightness = value; }

    public Lightning(Vector3 lightPos)
    {
        lightGameObject = new GameObject("Lightning");
        Light lightComp = lightGameObject.AddComponent<Light>();
        lightGameObject.transform.position = lightPos;
    }

    public void SetPosition(Vector3 position)
    {
        lightGameObject.transform.position = position;
    }

    public Vector3 getDirectionToLightning(Vector3 objectPosition)
    {
        Vector3 lightningDirection;
        lightningDirection = lightGameObject.transform.position - objectPosition;
        distanceToLight = Vector3.Magnitude(lightningDirection);
        return lightningDirection;
    }

    public float getBrightness(GeometryObject sourceObject, Ray ray)
    {
        double section;
        for (int i = 0; i < Scene.Instance.geometryObjectStorage.objects.Count; i++) {
            GeometryObject geometryObject = Scene.Instance.geometryObjectStorage.objects[i];
            if (geometryObject !=sourceObject) {
                section = geometryObject.intersect(ray);
                if ((section > 0.0) && (section <= distanceToLight)) {
                    return 0.0f;
                }
            }
        }
        return brightness;
    }
}

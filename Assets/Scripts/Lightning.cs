using UnityEngine;

public class Lightning
{
    private GameObject lightGameObject;
    private Vector3 position;
    private float brightness;
    private GeometryObjectStorage geometryObjectStorage;
    public float Brightness { get => brightness; set => brightness = value; }

    public Lightning(Vector3 lightPos) {
        lightGameObject = new GameObject("Lightning");
        Light lightComp = lightGameObject.AddComponent<Light>();
        lightGameObject.transform.position = lightPos;
    }

    public Vector3 Position { get => Position;}
    
    public void SetPosition(Vector3 position) {
        this.position = position;
        lightGameObject.transform.position = position;
    }

    public Vector3 getDirectionToLightning(Vector3 objectPosition) {
        Vector3 lightningDirection;
        lightningDirection = Position - objectPosition;
        return lightningDirection;
    }

    public float getBrightness(GeometryObject sourceObject, Ray ray) {
        double section = 0;
        Scene.Instance.geometryObjectStorage.objects.ForEach((geometryObject) => {
            if (geometryObject != sourceObject) {
                section = geometryObject.intersect(ray);
            }
        });
        return brightness;
    }
}

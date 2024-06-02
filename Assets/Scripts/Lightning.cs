using UnityEngine;

public class Lightning
{
    private Vector3 position;
    private float brightness;
    private GeometryObjectStorage geometryObjectStorage;
    public Vector3 Position { get => Position; set => Position = value; }
    public float Brightness { get => brightness; set => brightness = value; }

    public Vector3 getDirectionToLightning(Vector3 objectPosition) {
        Vector3 lightningDirection;
        lightningDirection = Position - objectPosition;
        return lightningDirection;
    }

    public float getBrightness(GeometryObject source, Vector3 objectPosition, Vector3 ray) {
        double section = 0;

        return brightness;
    }
}

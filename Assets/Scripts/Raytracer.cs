using System.Collections;
using UnityEngine;

public class Raytracer : MonoBehaviour
{
    private CameraRT cameraRT;
    void Start()
    {
        cameraRT = GetComponent<CameraRT>();
        calculatePicture();
    }

    void calculatePicture()
    {
        for (int x = 0; x < Screen.width; x++)
        {
            for (int y = 0; y < Screen.height; y++)
            {
                Vector3 ray = cameraRT.calculateRayForPoint(new Point(x, y));
                Debug.DrawLine(cameraRT.transform.position, cameraRT.transform.position + ray, Color.cyan, 300f);
                ray = Vector3.Normalize(ray);
                // raytrace
                // putpixel
            }
        }
    }
}


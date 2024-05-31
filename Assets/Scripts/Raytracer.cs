using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Raytracer : MonoBehaviour
{
    private CameraRT cameraRT;
    private Texture2D rendererTexture;
    private int maxDepth = 2;
    private int resolutionX = 0;
    private int resolutionY = 0;
    void Start()
    {
        cameraRT = GetComponent<CameraRT>();
        resolutionX = cameraRT.resXinPixel;
        resolutionY = cameraRT.resYinPixel;
        rendererTexture = new Texture2D(resolutionX, resolutionY);
        Scene scene = new Scene(resolutionX, resolutionY ,transform, 150);
        calculatePicture();
    }

    void calculatePicture()
    {
        for (int x = 0; x < Screen.width; x++)
        {
            for (int y = 0; y < Screen.height; y++)
            {
                Vector3 ray = cameraRT.calculateRayForPoint(new Point(x, y));
                //Debug.DrawLine(cameraRT.transform.position, cameraRT.transform.position + ray, Color.cyan, 300f);
                ray = Vector3.Normalize(ray);
                // raytrace
                // putpixel
                rendererTexture.SetPixel(x, y, testraytracer(ray));
            }
        }
        rendererTexture.Apply();
    }

    private void raytrace(Vector3 ray, int depth, Color color)
    {
        if (depth > maxDepth)
        {
            color = Color.black;
            return;
        }
        else
        {
            //Schneide Strahl mit allen Objekten und ermittle nächstgelegenen Schnittpunkt;
            //if kein Schnittpunkt { Col=background; return}
            /*
            else
            {
            LocalColor=Farbe aus lokalem Beleuchtungmodell (Phong);
            Ermittle ideal reflektierten Strahl;
            Raytrace(ideal reflektierter Strahl, Depth+1, ReflCol);
            Ermittle ideal transmittierten Strahl;
            Raytrace(ideal transmittierter Strahl, Depth+1, TransCol);
            Kombiniere(Col, ReflCol, TransCol);
            }
            */
        }
    }

    private Color testraytracer(Vector3 ray)
    {
        //if ray hits something
        if (false)
        {
            return Color.white;
        }
        else
        {
            return Color.black;
        }
    }

    // draw the picture
    private void OnGUI() {
        GUI.DrawTexture(new Rect(0, 0, resolutionX, resolutionY), rendererTexture);
    }
}


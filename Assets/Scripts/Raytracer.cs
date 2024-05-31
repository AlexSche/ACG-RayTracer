using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Raytracer : MonoBehaviour
{
    private CameraRT cameraRT;
    private Scene scene;
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
        scene = new Scene(resolutionX, resolutionY, cameraRT.depth, 150);
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

    private double intersectObjects(Object obj, Vector3 pos, Vector3 dir, Color col)
    {
        Object hitObject = null;
        double s, ss;
        Vector3 hit, normal;
        ss = (double)1E20; //FAR_AWAY

        ObjectStorage objectStorage = scene.objectStorage;

        /* check ray intersection with all objects */
        objectStorage.objects.ForEach(anObj => {
            if (anObj == null) {
                return;
            }
            /* special check used for reflections */
            if (anObj != obj) /* don't try source object */
            {
                //s = anObj->Intersect(pos, ray); // Check intersaction
                s = 0;
                /* keep track of closest intersection */
                if ((s > 0.0) && (s <= ss))
                {
                    hitObject = anObj;
                    ss = s;
                }
            }
        });

        if (hitObject == null) {
            return 0; /* ray hit no objects */
        }

        /* find point of intersection */
        //hit = pos + dir * ss;

        /* find normal */
        //normal = hitObject->NormVector(hit);

        /* find color at point of intersection */
        col = Color.white;
        //Shade(hit, ray, normal, *anObjectHit, color);

        return (ss);
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

    // draws the picture
    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, resolutionX, resolutionY), rendererTexture);
    }
}


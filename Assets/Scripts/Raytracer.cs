using System;
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
        resolutionX = cameraRT.getWidth();
        resolutionY = cameraRT.getHeight();
        rendererTexture = new Texture2D(resolutionX, resolutionY);
        scene = new Scene(resolutionX, resolutionY, cameraRT.getDepth(), 10);
        calculatePicture();
    }

    void calculatePicture()
    {
        DateTime before = DateTime.Now;
        for (int x = 0; x < Screen.width; x++)
        {
            for (int y = 0; y < Screen.height; y++)
            {
                Vector3 direction = cameraRT.calculateRayForPoint(new Point(x, y));
                Debug.DrawLine(cameraRT.transform.position, cameraRT.transform.position + direction, Color.cyan, 300f);
                direction = Vector3.Normalize(direction);
                Ray ray = new Ray(cameraRT.getPosition(), direction);
                Color color = raytrace(ray,0,Color.black);
                rendererTexture.SetPixel(x, y, color);
            }
        }
        DateTime after = DateTime.Now;
        TimeSpan duration = after.Subtract(before);
        Debug.Log("Raytracer calculation in milliseconds: " + duration.Milliseconds);
        rendererTexture.Apply();
    }

    private Color raytrace(Ray ray, int depth, Color color)
    {
        if (depth > maxDepth)
        {
            return color = Color.black;
            //return;
        }
        else
        {
            //Schneide Strahl mit allen Objekten und ermittle nächstgelegenen Schnittpunkt;
            if(intersectObjects(null, ray, color) > 0) {
                return color = Color.white;
            } else {
                return color = Color.black; //if kein Schnittpunkt { Col=background; return}
            }
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

    private double intersectObjects(GeometryObject obj, Ray ray, Color col)
    {
        GeometryObject hitObject = null;
        double s, ss;
        Vector3 hit, normal;
        ss = (double)1E20; //FAR_AWAY

        GeometryObjectStorage objectStorage = scene.geometryObjectStorage;

        /* check ray intersection with all objects */
        objectStorage.objects.ForEach(anObj => {
            if (anObj == null) {
                return;
            }
            /* special check used for reflections */
            if (anObj != obj) /* don't try source object */
            {
                s = anObj.intersect(ray); // Check intersaction
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
        hit = ray.origin + ray.direction * (float)ss;

        /* find normal */
        normal = hitObject.normalizeVector(hit);

        /* find color at point of intersection */
        col = Color.white;

        //Shade(hit, ray, normal, *anObjectHit, color);
        return ss;
    }

    // draws the picture
    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, resolutionX, resolutionY), rendererTexture);
    }
}


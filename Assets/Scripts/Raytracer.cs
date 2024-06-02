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
        Scene.createScene(resolutionX, resolutionY, cameraRT.getDepth(), 10);
        scene = Scene.Instance;
        calculatePicture();
    }

    void calculatePicture()
    {
        DateTime before = DateTime.Now; // duration variable
        for (int x = 0; x < Screen.width; x++)
        {
            for (int y = 0; y < Screen.height; y++) // for every pixel
            {
                // calculate the direction from the camera to the pixel
                Vector3 direction = cameraRT.calculateRayForPoint(new Point(x, y));
                //Debug.DrawLine(cameraRT.transform.position, cameraRT.transform.position + direction, Color.cyan, 300f);
                direction = Vector3.Normalize(direction);
                Ray ray = new Ray(cameraRT.getPosition(), direction);
                // raytrace the pixel (returns the color for this pixel)
                Color color = raytrace(ray, 0, Color.black);
                // set the color on this pixel
                rendererTexture.SetPixel(x, y, color);
            }
        }
        #region Duration of execution
        DateTime after = DateTime.Now;
        TimeSpan duration = after.Subtract(before);
        Debug.Log("Raytracer calculation in milliseconds: " + duration.Milliseconds);
        #endregion
        rendererTexture.Apply();
    }

    private Color raytrace(Ray ray, int depth, Color color)
    {
        GeometryObject hitObject;
        if (depth > maxDepth)
        {
            return color = Color.black;
        }
        else
        {
            // Intersect with all objects and find the closest object with his intersection point
            hitObject = intersectObjects(null, ray);
            if (hitObject != null && Vector3.Magnitude(hitObject.IntersectionPoint) > 0)
            {
                Debug.DrawRay(ray.origin, ray.direction, Color.white, 200f);
                return color = hitObject.getColorAtIntersection(Scene.Instance.lightning, ray); // calculate color for intersection point
            }
            else
            {
                return color = Color.black; // if no intersection return black;
            }
            /*
            if (hitObject == null) {
                return color = Color.black;
            } else {
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

    private GeometryObject intersectObjects(GeometryObject obj, Ray ray)
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
            find normal -> Why do I have to find normal here?
            normal = hitObject.normalizeVector(hit);
            */
            return hitObject;
        }
    }

    private Color calculateColorForIntersection(GeometryObject hitObject, Ray ray)
    {
        return hitObject.getColorAtIntersection(Scene.Instance.lightning, ray);
    }

    // draws the picture
    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, resolutionX, resolutionY), rendererTexture);
    }

    private double calculateIntersectionAndLightning(GeometryObject obj, Ray ray, Color col)
    {
        GeometryObject hitObject = null;
        double s, ss;
        Vector3 hit, normal;
        ss = double.MaxValue;

        /* check ray intersection with all objects */
        scene.geometryObjectStorage.objects.ForEach(anObj =>
        {
            if (anObj == null)
            {
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

        if (hitObject == null) // ray hit no object
        {
            return 0;
        }
        else // ray hit an object
        {
            /* find point of intersection */
            hit = ray.origin + ray.direction * (float)ss;
            normal = hitObject.normalizeVector(hit);

            // calculate color for point of intersection
            col = hitObject.getColorAtIntersection(Scene.Instance.lightning, ray);
            //Shade(hit, ray, normal, *anObjectHit, color);
            return ss;
        }
    }
}


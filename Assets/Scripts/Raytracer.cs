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
        for (int x = 0; x < resolutionX; x++)
        {
            for (int y = 0; y < resolutionY; y++) // for every pixel
            {
                // calculate the direction from the camera to the pixel
                Vector3 direction = cameraRT.calculateRayForPoint(new Point(x, y));
                //Debug.DrawLine(cameraRT.transform.position, cameraRT.transform.position + direction, Color.cyan, 300f);
                direction = Vector3.Normalize(direction);
                Ray ray = new Ray(cameraRT.getPosition(), direction);
                // raytrace the pixel (returns the color for this pixel)
                Color color = raytrace(ray, 0, Color.black);
                // set the color on this pixel
                rendererTexture.SetPixel(x, resolutionY - y, color); // unity's texture starts bottom-left (Hannah Schweizer Bugfix)
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
            return color;
        }
        else
        {
            // Intersect with all objects and find the closest object with his intersection point
            hitObject = GeometryObject.intersectObjects(null, ray,scene);
            if (hitObject == null) {
                return color;
            } else {
            //Debug.DrawRay(ray.origin, ray.direction, Color.white, 200f);
            color = hitObject.getColorAtIntersection(Scene.Instance.lightning, ray); // LocalColor=Farbe aus lokalem Beleuchtungmodell (Phong);
            Ray reflectedRay = hitObject.getReflectedRay(ray); // Ermittle ideal reflektierten Strahl; Einfallswinkel = Ausfallwinkel
            Color reflectedColor = color;
            color = raytrace(reflectedRay, depth + 1, reflectedColor); // Raytrace(ideal reflektierter Strahl, Depth+1, ReflCol);
            Color transmittedColor = color;
            Ray lightRay = hitObject.getLightningRay(Scene.Instance.lightning, ray); // Strahl zur Lichtquelle
            Ray transmittedRay = hitObject.getReflectedRay(lightRay); // Ermittle ideal transmittierten Strahl; Ausfallwinkel des Schattenstrahl (Strahl zur Lichtquelle)
            color = raytrace(transmittedRay, depth + 1, transmittedColor); // Raytrace(ideal transmittierter Strahl, Depth+1, TransCol);     
            color = color + reflectedColor + transmittedColor; //Kombiniere(Col, ReflCol, TransCol);
            return color;
            }
        }
    }

    // draws the picture
    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, resolutionX, resolutionY), rendererTexture);
    }
}


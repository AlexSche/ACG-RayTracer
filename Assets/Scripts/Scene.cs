using UnityEngine;

public class Scene
{
    //set default values - values are set in constructor
    private int amountOfObjects = 10;
    private int resolutionX = 640;
    private int resolutionY = 480;  
    private Transform transform;
    public ObjectStorage objectStorage;

    public Scene(int resX, int resY, Transform cameraLocation, int amountOfObjects) {
        resolutionX = resX;
        resolutionY = resY;
        this.amountOfObjects = amountOfObjects;
        transform = cameraLocation; 
        objectStorage = new ObjectStorage();
        createScene();
    }

    private void createScene()
    {
        for (int i = 0; i < amountOfObjects; i++)
        {
            createSphereOnPosition(generateRandomPosition());
        }
    }

    private void createSphereOnPosition(Vector3 position)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = position;
        sphere.transform.localScale *= 50;
        objectStorage.addObject(sphere);
    }

    private void addLightning()
    {

    }

    private Vector3 generateRandomPosition()
    {
        return new Vector3(Random.Range(-(Screen.width/2), Screen.width/2), Random.Range(-(Screen.height/2), Screen.height/2), Random.Range(transform.position.z, 1000));
    }
}

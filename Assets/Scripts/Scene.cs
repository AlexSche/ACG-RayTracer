using UnityEngine;

public class Scene
{
    //set default values - values are set in constructor
    private int amountOfObjects = 10;
    private int resolutionX = 640;
    private int resolutionY = 480;  
    private int depth = 0;
    private Transform transform;
    public ObjectStorage objectStorage;

    public Scene(int resX, int resY, int depth, int amountOfObjects) {
        resolutionX = resX;
        resolutionY = resY;
        this.amountOfObjects = amountOfObjects;
        this.depth = depth;
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
        sphere.AddComponent<SphereCollider>();
        objectStorage.addObject(sphere);
    }

    private void addLightning()
    {
        // spawn lightning in the top left corner
    }

    private Vector3 generateRandomPosition()
    {
        return new Vector3(Random.Range(-(Screen.width/2), Screen.width/2), Random.Range(-(Screen.height/2), Screen.height/2), Random.Range(0, depth));
    }
}

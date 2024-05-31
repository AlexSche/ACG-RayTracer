using UnityEngine;

public class Scene
{
    //set default values - values are set in constructor
    private int amountOfObjects = 10;
    private int resolutionX = 640;
    private int resolutionY = 480;  
    private int depth = 0;
    private Transform transform;
    public GeometryObjectStorage geometryObjectStorage;

    public Scene(int resX, int resY, int depth, int amountOfObjects) {
        resolutionX = resX;
        resolutionY = resY;
        this.amountOfObjects = amountOfObjects;
        this.depth = depth;
        geometryObjectStorage = new GeometryObjectStorage();
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
        SphereObject sphereObject = new SphereObject(position);
        geometryObjectStorage.addObject(sphereObject);
    }

    private void addLightning()
    {
        // spawn lightning in the top left corner
    }

    private Vector3 generateRandomPosition()
    {
        return new Vector3(Random.Range(-(resolutionX/2), resolutionX/2), Random.Range(-(resolutionY/2), resolutionY/2), Random.Range(0, depth));
    }
}

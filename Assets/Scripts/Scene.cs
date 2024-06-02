using Unity.VisualScripting;
using UnityEngine;

// cannot inheriate from Scene
public sealed class Scene
{
    private static Scene instance;
    //set default values - values are set in constructor
    private int amountOfObjects = 10;
    private int resolutionX = 640;
    private int resolutionY = 480;
    private int depth = 0;
    private Transform transform;
    public GeometryObjectStorage geometryObjectStorage;

    static Scene() {
    }

    private Scene(int resX, int resY, int depth, int amountOfObjects) {
        resolutionX = resX;
        resolutionY = resY;
        this.amountOfObjects = amountOfObjects;
        this.depth = depth;
        geometryObjectStorage = new GeometryObjectStorage();
        createObjects();
    }

    public static Scene Instance {
        get {
            if (instance == null) {
                throw new System.Exception("Scene not created");
            }
            return instance;
        }
    }

    public static void createScene(int resX, int resY, int depth, int amountOfObjects)
    {
        if (instance != null) {
            throw new System.Exception("Scene already exists!");
        }
        instance = new Scene(resX, resY, depth, amountOfObjects);
    }

    private void createObjects()
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
        return new Vector3(Random.Range(-(resolutionX / 2), resolutionX / 2), Random.Range(-(resolutionY / 2), resolutionY / 2), Random.Range(0, depth));
    }
}

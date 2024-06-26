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
    public GeometryObjectStorage geometryObjectStorage;
    public Lightning lightning;

    static Scene() {
    }

    private Scene(int resX, int resY, int depth, int amountOfObjects) {
        resolutionX = resX;
        resolutionY = resY;
        this.amountOfObjects = amountOfObjects;
        this.depth = depth;
        geometryObjectStorage = new GeometryObjectStorage();
        createObjects();
        createLightningOnPosition(new Vector3(resX, resY, depth));
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

    private void createLightningOnPosition(Vector3 position) {
        lightning = new Lightning(position);
    }

    private Vector3 generateRandomPosition()
    {
        return new Vector3(Random.Range(-(resolutionX / 2), resolutionX / 2), Random.Range(-(resolutionY / 2), resolutionY / 2), Random.Range(0, depth));
    }
}

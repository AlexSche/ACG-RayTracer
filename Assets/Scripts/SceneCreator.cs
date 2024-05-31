using UnityEngine;

public class SceneCreator
{
    
    private int amountOfObjects = 500;
    private int resolutionX = 0;
    private int resolutionY = 0;  
    private Transform transform;
    private ObjectStorage objectStorage;
    public Material material;

    public SceneCreator(int resX, int resY, Transform cameraLocation) {
        resolutionX = resX;
        resolutionY = resY;
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
        sphere.GetComponent<MeshRenderer>().material = material;
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

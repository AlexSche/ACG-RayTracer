using UnityEngine;

public class SceneCreator : MonoBehaviour
{
    private int sceneSizeX;
    private int sceneSizeY;
    private int amountOfObjects = 15;
    public ObjectStorage objectStorage;
    public Material material;
    void Start()
    {
        Vector3 canvasSize = GetComponent<MeshRenderer>().bounds.size;
        sceneSizeX = (int)canvasSize.x;
        sceneSizeY = (int)canvasSize.y;

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
        sphere.GetComponent<MeshRenderer>().material = material;
        //objectStorage.addObject(sphere);
    }

    private void addLightning()
    {

    }

    private Vector3 generateRandomPosition()
    {
        return new Vector3(Random.Range(0, sceneSizeX - 1), Random.Range(0, sceneSizeY - 1), transform.position.z);
    }
}

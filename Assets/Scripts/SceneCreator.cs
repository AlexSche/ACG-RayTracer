using UnityEngine;

public class SceneCreator : MonoBehaviour
{
    private Texture2D rendererTexture;
    private int amountOfObjects = 0;
    public ObjectStorage objectStorage;
    void Start()
    {
        rendererTexture = new Texture2D(Screen.height, Screen.width);
        Debug.Log(Screen.width +"x"+ Screen.height);
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
        //sphere.GetComponent<MeshRenderer>().material = material;
        //objectStorage.addObject(sphere);
    }

    private void addLightning()
    {

    }

    private Vector3 generateRandomPosition()
    {
        return new Vector3(Random.Range(0, 0), Random.Range(0, 0), transform.position.z);
    }
}

using UnityEngine;

public class CameraRT : MonoBehaviour
{
    public GameObject canvas;
    private float canvasX;
    private float canvasY;
    public int resXinPixel;
    public int resYinPixel;
    private Vector3 up; // Orientierung der Kamera
    private Vector3 position; // Vorlesung Variable e - Kameraort
    private Vector3 centerOfCanvas; // Vorlesung Variable l - Mittelpunkt der Bildebenenmitte
    private Vector3 camToCenterOfCanvas; // Vorlesung Variable g - Vektor von Kamera nach Bildebenenmitte
    private Vector3 firstRay; // Vorlesung Variable f - erster Strahl durch Bildebene
    private Vector3 scrnx, scrny; // Vektoren auf Bildebene in x- und y- Richtung
    private float hfov, vfov; // horizontales, vertikales Blickfeld
    private Vector3 x, y; // Zur Bestimmung der Sehstrahlen hfov und vfov

    private Point point;

    // Start is called before the first frame update
    void Start()
    {
        // 
        up = transform.up;
        position = transform.position;
        centerOfCanvas = canvas.transform.position;
        Vector3 canvasSize = canvas.gameObject.GetComponent<MeshRenderer>().bounds.size;
        canvasX = canvasSize.x;
        canvasY = canvasSize.y;
        //resXinPixel = (int) canvasSize.x * 100; // one unity scale equal 100 pixels
        //resYinPixel = (int) canvasSize.y * 100;

        Debug.Log("resolution: " + resXinPixel + "x" + resYinPixel);

        // Bestimmung der Sehstrahlen:
        camToCenterOfCanvas = centerOfCanvas - position;
        Vector3 dist = Vector3.Normalize(camToCenterOfCanvas);

        scrnx = Vector3.Cross(camToCenterOfCanvas, up);
        scrnx = Vector3.Normalize(scrnx);

        scrny = Vector3.Cross(scrnx, camToCenterOfCanvas);
        scrny = Vector3.Normalize(scrny);

        x = scrnx * (canvasSize.x / 2);
        y = scrny * (canvasSize.y / 2);

        // scrnx und scrny ein pixel auf der Bildebene lang machen
        float mx = 2 * Vector3.Magnitude(x) / resXinPixel;
        scrnx = scrnx * mx;
        // looks into -x, so I invert it (dirty fix)
        scrnx = -scrnx;

        float my = 2 * Vector3.Magnitude(y) / resYinPixel;
        scrny = scrny * my;

        calculateFirstRay();
        displayAllVector(); // used to debug the calculated vector
    }

    private void calculateFirstRay()
    {
        firstRay = (centerOfCanvas - position) - scrnx * resXinPixel / 2 + scrny * resYinPixel / 2;
    }

    public Vector3 calculateRayForPoint(Point point) {
        Vector3 ray = firstRay + scrnx * point.getX() + scrny * point.getY();
        return ray;
    }

    // displays all calculated vector
    private void displayAllVector()
    {
        // up
        Debug.DrawLine(position, position + up, Color.green, 10f);
        // cam to center
        Debug.DrawLine(position, position + camToCenterOfCanvas, Color.blue, 10f);
        // scrnx
        Debug.DrawLine(centerOfCanvas, centerOfCanvas + scrnx, Color.red, 10f);
        // scrny
        Debug.DrawLine(centerOfCanvas, centerOfCanvas + scrny, Color.green, 10f);
        // first ray
        Debug.DrawLine(position, position + firstRay, Color.yellow, 20f);
    }
}
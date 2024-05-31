using UnityEngine;

public class CameraRT : MonoBehaviour
{
    private int resXinPixel;
    private int resYinPixel;
    private int depth;
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
    void Awake()
    {
        resXinPixel = Screen.width;
        resYinPixel = Screen.height;
        depth = 1000;
        up = transform.up;
        position = transform.position;
        centerOfCanvas = new Vector3(position.x, position.y, position.z + depth);

        // Bestimmung der Sehstrahlen:
        camToCenterOfCanvas = centerOfCanvas - position;
        Vector3 dist = Vector3.Normalize(camToCenterOfCanvas);

        scrnx = Vector3.Cross(camToCenterOfCanvas, up);
        scrnx = Vector3.Normalize(scrnx);

        scrny = Vector3.Cross(scrnx, camToCenterOfCanvas);
        scrny = Vector3.Normalize(scrny);

        x = scrnx * (resXinPixel / 2);
        y = scrny * (resYinPixel / 2);

        // scrnx und scrny ein pixel auf der Bildebene lang machen
        float mx = 2 * Vector3.Magnitude(x) / resXinPixel;
        scrnx = scrnx * mx;
        // looks into -x, so I invert it (dirty fix)
        scrnx = -scrnx;

        float my = 2 * Vector3.Magnitude(y) / resYinPixel;
        scrny = scrny * my;

        calculateFirstRay();
        //displayAllVector(); // show all calculated vector for debugging the scene
    }

    public CameraRT(int width, int height)
    {
        resXinPixel = width;
        resYinPixel = height;
    }

    private void calculateFirstRay()
    {
        firstRay = (centerOfCanvas - position) - scrnx * resXinPixel / 2 + scrny * resYinPixel / 2;
    }

    public Vector3 calculateRayForPoint(Point point)
    {
        Vector3 ray = firstRay + (scrnx * point.getX()) - (scrny * point.getY());
        return ray;
    }

    // displays all calculated vector
    public void displayAllVector()
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

    public int getWidth() {
        return resXinPixel;
    }

    public int getHeight() {
        return resYinPixel;
    }

    public Vector3 getPosition() {
        return position;
    }

    public int getDepth() {
        return depth;
    }
}

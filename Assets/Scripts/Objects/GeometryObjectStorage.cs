using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GeometryObjectStorage
{
    public List<GeometryObject> objects;

    public GeometryObjectStorage() {
        objects = new List<GeometryObject>();
    }
    public int getNumberOfObjects() {
        return objects.Count;
    }

    public GeometryObject getObjectOnIndex(int index) {
        return objects[index];
    }

    public GeometryObject getFirstObject() {
        return objects[0];
    }

    public GeometryObject getLastObject() {
        return objects.Last();
    }

    public void addObject(GeometryObject obj) {
        objects.Add(obj);
    }
}

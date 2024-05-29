using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectStorage
{
    public List<Object> objects;
    public int getNumberOfObjects() {
        return objects.Count;
    }

    public Object getObjectOnIndex(int index) {
        return objects[index];
    }

    public Object getFirstObject() {
        return objects[0];
    }

    public Object getLastObject() {
        return objects.Last();
    }

    public void addObject(Object obj) {
        objects.Add(obj);
    }
}

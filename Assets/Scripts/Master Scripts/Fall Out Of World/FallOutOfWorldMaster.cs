using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallOutOfWorldMaster : MonoBehaviour
{
    public int destroyHeight;

    // Start is called before the first frame update
    void Start()
    {
        object[] obj = FindObjectsOfType(typeof(GameObject));

        foreach (Object o in obj)
        {
            GameObject g = (GameObject)o;
            var fow = g.AddComponent<FallOutOfWorld>();
            fow.destroyHeight = destroyHeight;
        }
    }
}

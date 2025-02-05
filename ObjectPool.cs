using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab;
    public Transform parent;
    public int maxObjects = 30;
    List<GameObject> pool;
    void Start()
    {
        pool = new List<GameObject>();
        for (int i = 0; i < maxObjects; i++)
        {
            GameObject obj = Instantiate(prefab,parent);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }
    public GameObject Get()
    {
        foreach(GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }
        return null;
    }
}

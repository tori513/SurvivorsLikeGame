using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    GameObject Prefab;

    [SerializeField]
    int size;

    Stack<GameObject> pool = new Stack<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < size; i++)
        {
            MakeObject();
        }
    }

    void MakeObject()
    {
        GameObject obj = Instantiate(Prefab, transform);
        pool.Push(obj);
        obj.SetActive(false);       
    }

    public GameObject GetObject()
    {
        if(pool.Count == 0)
        {
            MakeObject();
        }
        GameObject obj = pool.Pop();
        obj.SetActive(true);
        return obj;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        pool.Push(obj);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolBase : MonoBehaviour
{
    public int initialCapacity = 20;

    private readonly Queue<GameObject> pool = new Queue<GameObject>();

    public void Init(GameObject structure)
    {
        Structure = structure;
        Prewarm(initialCapacity);
    }

    private void Prewarm(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Create();
        }
    }

    private GameObject Create(Transform transform = null)
    {
        GameObject obj;
        if (transform == null)
        {
            obj = Instantiate(Structure, new Vector3(), new Quaternion());
        }
        else
        {
            obj = Instantiate(Structure, transform);
        }

        obj.SetActive(false);
        pool.Enqueue(obj);
        return obj;
    }

    private GameObject Create(Vector3 position, Quaternion rotation)
    {
        var obj = Instantiate(Structure, position, rotation);
        obj.SetActive(false);
        pool.Enqueue(obj);
        return obj;
    }

    public GameObject GetFromPool(Transform transform)
    {
        GameObject obj;
        if (pool.Count > 0)
        {
            obj = pool.Dequeue();
        }
        else
        {
            obj = Create(transform);
        }

        obj.GetComponent<Transform>().position = transform.position;
        obj.SetActive(true);
        return obj;
    }

    public GameObject GetFromPool(Vector3 position, Quaternion rotation)
    {
        GameObject obj;
        if (pool.Count > 0)
        {
            obj = pool.Dequeue();
        }
        else
        {
            obj = Create(position, rotation);
        }

        var transform = obj.GetComponent<Transform>();
        transform.position = position;
        transform.rotation = rotation;
        obj.SetActive(true);
        return obj;
    }

    public void ReturnToPool(GameObject obj)
    {
        if (pool.Contains(obj))
        {
            return;
        }

        obj.SetActive(false);
        pool.Enqueue(obj);
    }

    private GameObject Structure { get; set; }

}

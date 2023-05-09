using UnityEngine;
using System.Collections;

public class ObjectPool<T> where T : MonoBehaviour
{
    private int current;
    private T[] pool;

    protected ObjectPool()
    {
    }

    public ObjectPool(int size, GameObject prefab)
    {
        pool = new T[size];
        for (int i = 0; i < pool.Length; i++)
        {
            GameObject obj = GameObject.Instantiate(prefab) as GameObject;
            pool[i] = obj.GetComponent<T>();
            obj.SetActive(false);
        }
        current = 0;
    }

    public int GetSize()
    {
        return pool.Length;
    }

    public T Get()
    {
        int t = current;
        while (pool[current].gameObject.activeSelf)
        {
            current++;
            if (current == pool.Length) current = 0;
            if (current == t) break;
        }
        return pool[current];
    }

    public void Remove(T obj)
    {
        for (int i = 0; i < pool.Length; i++)
        {
            if (pool[i] == obj)
            {
                obj.gameObject.SetActive(false);
                return;
            }
        }
    }
}

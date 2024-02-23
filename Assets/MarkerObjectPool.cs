using System.Collections.Generic;
using UnityEngine;

public class MarkerObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _prefab; // Havuzdaki nesnelerin prototipi
    [SerializeField] private  int _poolSize = 10; // Havuzdaki nesne sayısı

    private Queue<GameObject> _pool = new Queue<GameObject>();

    void Start()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject obj = Instantiate(_prefab, transform);
            obj.SetActive(false);
            _pool.Enqueue(obj);
        }
    }

    public GameObject GetObject()
    {
        if (_pool.Count > 0)
        {
            GameObject obj = _pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            GameObject obj = Instantiate(_prefab, transform);
            return obj;
        }
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.parent = null;
        _pool.Enqueue(obj);
    }
}
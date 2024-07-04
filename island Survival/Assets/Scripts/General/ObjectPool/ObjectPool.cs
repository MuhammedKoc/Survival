using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public PooledObject[] _prefab;
    public int[] _poolSizes;
    public List<PooledObject>[] _pools;

    #region Singleton

    private static ObjectPool instance = null;

    public static ObjectPool Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.Log(instance.GetType().Name + "Instance is Null");
            }

            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    #endregion

    void Start()
    {
        _pools = new List<PooledObject>[_prefab.Length];

        for (int j = 0; j < _pools.Length; j++)
        {
            _pools[j] = new List<PooledObject>();
            for (int i = 0; i < _poolSizes[j]; i++)
            {
                PooledObject obj = Instantiate(_prefab[j]) as PooledObject;
                obj.gameObject.SetActive(false);
                _pools[j].Add(obj);
            }
        }
    }


    //array pooledobject
    public PooledObject Get(PooledObject _projectile)
    {
        for (int i = 0; i < _prefab.Length; i++)
        {
            if (_prefab[i] == _projectile)
            {
                for (int j = 0; j < _pools[i].Count; j++)
                {
                    if (_pools[i][j].gameObject.activeInHierarchy == false)
                    {
                        _pools[i][j].gameObject.SetActive(true);
                        return _pools[i][j];
                    }

                    //if we're at the end of the list's count/length, then we need to increase the pool and use an additional object
                    if (((j == (_pools[i].Count - 1))) && (_pools[i][j].gameObject.activeInHierarchy == true))
                    {
                        IncreasePool(i);

                        return _pools[i][j + 1];
                    }
                }
            }
        }

        return null;
    }

    public PooledObject Get(PooledObject _projectile, Transform parent)
    {
        PooledObject result = null;

        for (int i = 0; i < _prefab.Length; i++)
        {
            if (_prefab[i] == _projectile)
            {
                for (int j = 0; j < _pools[i].Count; j++)
                {
                    if (_pools[i][j].gameObject.activeInHierarchy == false)
                    {
                        _pools[i][j].gameObject.SetActive(true);
                        result = _pools[i][j];
                        break;
                    }

                    //if we're at the end of the list's count/length, then we need to increase the pool and use an additional object
                    if (((j == (_pools[i].Count - 1))) && (_pools[i][j].gameObject.activeInHierarchy == true))
                    {
                        IncreasePool(i);

                        result = _pools[i][j + 1];
                        break;
                    }
                }
            }
        }

        if (result != null)
        {
            result.transform.SetParent(parent);
            return result;
        }
        else
            return null;
    }

    //Increase the pool by instantiating another object correctly coorelating to the specified List, make sure it's active, and add it to the correct List.
    private void IncreasePool(int _specifiedPool)
    {
        PooledObject obj = Instantiate(_prefab[_specifiedPool], parent: null) as PooledObject;
        obj.gameObject.SetActive(true);
        _pools[_specifiedPool].Add(obj);
    }
}
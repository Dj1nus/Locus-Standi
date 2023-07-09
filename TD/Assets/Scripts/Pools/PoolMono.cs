using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolMono<T> where T : MonoBehaviour
{
    public T Prefab { get; }
    public bool IsAutoExpand { get; set; }
    public Transform Container { get; }

    private List<T> _pool;

    public PoolMono(T prefab, int cout, Transform container)
    {
        this.Prefab = prefab;
        this.Container = container;
        _pool = new List<T>();

        CreatePool(cout);
    }

    private void CreatePool(int count)
    {
        for (int i = 0; i < count; i++)
        {
            CreateObject();
        }
    }

    private T CreateObject(bool isActiveByDefault = false)
    {
        var createdObject = UnityEngine.Object.Instantiate(Prefab, Container);
        createdObject.gameObject.SetActive(isActiveByDefault);
        _pool.Add(createdObject);

        return createdObject;
    }

    public bool IsHasFreeElement(out T element)
    {
        foreach (var item in _pool)
        {
            if (!item.gameObject.activeInHierarchy)
            {
                element = item;
                item.gameObject.SetActive(true);
                return true;
            }
        }

        element = null;
        return false;
    }

    public T GetFreeElement()
    {
        if (IsHasFreeElement(out var element))
        {
            return element;
        }
        if (IsAutoExpand)
        {
            return CreateObject(true);
        }

        throw new Exception($"“ы лох: в пуле {typeof(T)} нет свободных алиментов!");
    }
}


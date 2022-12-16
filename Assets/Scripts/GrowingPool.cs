using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This pooler is not worky that well for this use case because the objects change around often but good for static objects
//TODO: Use old pooler for uses in enemy instantiation
[System.Serializable]
public class GrowingPool<T> where T : PooledItem
{
    //Ultra cringe cant serialize field
    public int poolCount;
    //public int PoolAmount { get { return available.Count; } }

    Queue<T> available;
    T _prefab;
    GameObject _holder;
    public GrowingPool(T prefab, int count)
    {
        _prefab = prefab;
        available = new Queue<T>();

        _holder = new GameObject($"Pool ({typeof(T)})");
        for (int i = 0; i < count; i++)
        {
            T entity = GameObject.Instantiate(prefab);
            entity.transform.SetParent(_holder.transform);
            entity.onDestroy += (x) =>
            {
                available.Enqueue(x as T);
                poolCount = available.Count;
            };
            available.Enqueue(entity);
            entity.gameObject.SetActive(false);
        }
        poolCount = count;
    }

    public bool IsAvailable()
    {
        return available.Count > 0;
    }

    public T Instantiate()
    {
        return Instantiate(Vector3.zero, Quaternion.identity);
    }

    public T Instantiate(Vector3 position, Quaternion rotation)
    {
        poolCount = available.Count - 1;
        if (available.Count <= 0)
        {
            Debug.Log($"Spare instance of {_prefab.ToString()} instantiated");
            T entity = GameObject.Instantiate(_prefab);
            entity.transform.SetParent(_holder.transform);
            entity.transform.SetPositionAndRotation(position, rotation);
            entity.onDestroy += (x) => available.Enqueue(x as T);
            entity.gameObject.SetActive(true);
            return entity;
        }

        T instantiateEntity = available.Dequeue();
        instantiateEntity.transform.SetPositionAndRotation(position, rotation);
        instantiateEntity.gameObject.SetActive(true);
        return instantiateEntity;

    }

    //~GrowingPool()
    //{
    //    foreach(Transform child in _holder.transform)
    //    {
    //        GameObject.Destroy(child.gameObject);
    //    }
    //}

}

public abstract class PooledItem : MonoBehaviour
{

    public event System.Action<PooledItem> onDestroy;

    protected void DestroyPooled()
    {
        Reset();
        gameObject.SetActive(false);
        onDestroy?.Invoke(this);
    }

    protected abstract void Reset();
}

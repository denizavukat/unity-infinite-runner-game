using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{

    [System.Serializable]
    //PoolData
    public class Pool
    {

        public string type;
        public GameObject prefab;
        public int size;
        public Queue<GameObject> queue;
    }

     public static ObjectPooler instance;

     private void Awake()
     {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        
        poolDictionary = new Dictionary<string, Pool>();
        foreach (Pool pool in Pools)
        {
            pool.queue = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                SpawnPoolObject(pool);

            }

            poolDictionary.Add(pool.type, pool);
        }
    }

    private GameObject SpawnPoolObject(Pool pool)
    {
        GameObject currentObject = Instantiate(pool.prefab);
        currentObject.SetActive(false);
     
        pool.queue.Enqueue(currentObject);
        return currentObject;
    }

    public List<Pool> Pools;
    public Dictionary<string, Pool> poolDictionary;


    void Start()
    {
        

    }

    //GetFrom
    public GameObject GetFromPool(string type)
    {
        if (type == "City")
        {
            Debug.Log("citye girdi");
        }
        if (!poolDictionary.ContainsKey(type))
        {
            return null;
        }
        GameObject objectToSpawn = null;

        if(poolDictionary[type].queue.Count == 0)
        {
            SpawnPoolObject(poolDictionary[type]);
        }
       
        objectToSpawn = poolDictionary[type].queue.Dequeue();

      
        objectToSpawn.SetActive(true);
        

        

        return objectToSpawn;
    }

    public void ReturnToPool(string type, GameObject obj)
    {
        obj.SetActive(false);
        poolDictionary[type].queue.Enqueue(obj);
    }

}

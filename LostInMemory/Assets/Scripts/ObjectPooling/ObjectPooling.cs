using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    [System.Serializable]
    public class objectPool
    {
        public string objectName;
        public int size;
        public GameObject poolPrefab;
    }

    [Header("References")]
    public List<objectPool> pools;
    public Dictionary<string, Queue<GameObject>> poolsOfDictionary;

    public static ObjectPooling instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        poolsOfDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(objectPool pool in pools)
        {
            Queue<GameObject> poolQueue = new Queue<GameObject>();

            for(int i=0; i<pool.size;i++)
            {
                GameObject objPool = Instantiate(pool.poolPrefab);
                objPool.SetActive(false);
                poolQueue.Enqueue(objPool);
            }
            poolsOfDictionary.Add(pool.objectName, poolQueue);
        }
    }

    public GameObject SpawnPool(string poolName,Vector3 position, Quaternion rotation)
    {
        GameObject objectToSpawn = poolsOfDictionary[poolName].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        poolsOfDictionary[poolName].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
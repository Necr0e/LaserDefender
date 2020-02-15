using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public static ObjectPooler Instance;
#pragma warning disable 0649
    [SerializeField] private List<Pool> pools;
#pragma warning restore 0649
    private Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int index = 0; index < pool.size; index++)
            {
                GameObject item = Instantiate(pool.prefab, gameObject.transform, true);
                if (item == null) return;
                item.SetActive(false);
                objectPool.Enqueue(item);
            }
            
            poolDictionary.Add(pool.tag, objectPool);
        }
    }
    public GameObject GetPooledObject(string projectileTag, Vector2 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(projectileTag)) return null;
        GameObject objectToSpawn = poolDictionary[projectileTag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        poolDictionary[projectileTag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}

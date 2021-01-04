using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolHandler : MonoBehaviour {

    [System.Serializable]
    public class Pool {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    public static ObjectPoolHandler instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in pools) {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++) {
                GameObject go = Instantiate(pool.prefab);
                go.SetActive(false);
                objectPool.Enqueue(go);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 pos, Quaternion rotation) {
        if (poolDictionary.ContainsKey(tag)) {
            GameObject go = poolDictionary[tag].Dequeue();
            go.SetActive(true);
            go.transform.position = pos;
            go.transform.rotation = rotation;
            poolDictionary[tag].Enqueue(go);
            return go;
        }
        Debug.LogError("No entry in dictionary for tag:" + tag);
        return null;
    }
}

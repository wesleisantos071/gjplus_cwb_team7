using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolHandler : MonoBehaviour {

    private GameObject playerRef;

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
        PopulateDictionary();
    }

    private void Start() {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        ReloadHandler.instance.onClickRetry += ResetPool;
    }

    public void ResetPool() {
        GameObject[] platformsInPool = GameObject.FindGameObjectsWithTag("PlatformInPool");
        int max = platformsInPool.Length;
        for (int i = 0; i < max; i++) {
            Destroy(platformsInPool[i]);
        }
        PopulateDictionary();
    }

    private void PopulateDictionary() {
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
            Queue<GameObject> objQueue = poolDictionary[tag];
            GameObject go = null;
            go = objQueue.Dequeue();
            if (IsBehindPlayer(go.transform.position)) {
                go.SetActive(true);
                go.transform.position = pos;
                go.transform.rotation = rotation;
                poolDictionary[tag].Enqueue(go);
                Debug.Log("Spawn finished:" + go.transform.position.z);
                return go;
            } else {
                //Debug.Log("Could not find a platform behind the player for tag:" + tag);
                poolDictionary[tag].Enqueue(go);
                return null;
            }

        } else {
            Debug.LogError("No entry in dictionary for tag:" + tag);
            return null;
        }
    }

    private bool IsBehindPlayer(Vector3 targetPos) {
        if (playerRef.transform.position.z < PlatformController.instance.platformSize) {//check if player is on the 1st platform
            return true;
        } else {
            return playerRef.transform.position.z > (targetPos.z + PlatformController.instance.platformSize);
        }
    }

    private void OnDestroy() {
        ReloadHandler.instance.onClickRetry -= ResetPool;
    }
}

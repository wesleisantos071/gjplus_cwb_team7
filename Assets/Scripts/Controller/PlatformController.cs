using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {
    public static PlatformController instance;
    public GameObject lastPlatform;
    private GameObject firstPlatform;
    public System.Action<GameObject> onCreatePlaform;
    public float platformSize;
    List<string> platformTags;
    ObjectPoolHandler platformPooler;
    public int batchSize = 1;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        firstPlatform = lastPlatform; // in the first run, the lastplatform is the first one
    }

    private void Start() {
        platformPooler = ObjectPoolHandler.instance;
        platformTags = new List<string>(platformPooler.poolDictionary.Keys);
        PlayerMovementHandler.instance.onPlayerDie += ResetLastPlatform;
        DistanceController.instance.onReachDistance += CreateMorePlatforms;
        CreateMorePlatforms();
    }


    private void CreateMorePlatforms() {
        StartCoroutine(SchedulePlatformCreation());
    }

    IEnumerator SchedulePlatformCreation() {
        yield return new WaitForSeconds(0.01f);
        for (int i = 0; i < batchSize; i++) {
            int tagIndex = UnityEngine.Random.Range(0, platformTags.Count);
            string tagName = platformTags[tagIndex];
            Vector3 pos = lastPlatform.transform.position;
            pos.z = pos.z + platformSize;
            Debug.Log("Spawning platform on:" + pos.z);
            GameObject go = platformPooler.SpawnFromPool(tagName, pos, Quaternion.identity);
            lastPlatform = go;
            onCreatePlaform?.Invoke(go);
        }
    }

    private void ResetLastPlatform() {
        platformPooler.ResetPool();
        lastPlatform = firstPlatform;
    }

    private void OnDestroy() {
        PlayerMovementHandler.instance.onPlayerDie -= ResetLastPlatform;
        DistanceController.instance.onReachDistance -= CreateMorePlatforms;
    }
}

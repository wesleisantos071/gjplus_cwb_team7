using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {
    public static PlatformController instance;
    private GameObject lastPlatform;
    [SerializeField]
    private GameObject firstPlatform;
    public float platformSize;
    List<string> platformTags;
    ObjectPoolHandler platformPooler;
    public int batchSize = 1;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        lastPlatform = firstPlatform; // in the first run, the lastplatform is the first one
    }

    private void Start() {
        platformPooler = ObjectPoolHandler.instance;
        platformTags = new List<string>(platformPooler.poolDictionary.Keys);
        PlayerDistanceController.instance.onReachDistance += CreateMorePlatforms;
        ReloadHandler.instance.onClickPlay += CreateMorePlatforms;
        ReloadHandler.instance.onClickRetry += ResetLastPlatform;
    }



    private void CreateMorePlatforms() {
        StartCoroutine(DelayedPlatformCreation());
    }

    IEnumerator DelayedPlatformCreation() {
        yield return new WaitForSeconds(0.0001f);
        //Debug.Log("last platform is:" + lastPlatform);
        for (int i = 0; i < batchSize; i++) {
            int tagIndex = UnityEngine.Random.Range(0, platformTags.Count);
            string tagName = platformTags[tagIndex];
            Vector3 pos = lastPlatform.transform.position;
            pos.z = pos.z + platformSize;
            GameObject go = platformPooler.SpawnFromPool(tagName, pos, Quaternion.identity);
            if (go != null) {
                lastPlatform = go;
            }
        }
    }

    private void ResetLastPlatform() {
        lastPlatform = firstPlatform;
        CreateMorePlatforms();
    }

    private void OnDestroy() {
        PlayerMovementHandler.instance.onPlayerDie -= ResetLastPlatform;
        PlayerDistanceController.instance.onReachDistance -= CreateMorePlatforms;
        ReloadHandler.instance.onClickPlay -= CreateMorePlatforms;
        ReloadHandler.instance.onClickRetry -= CreateMorePlatforms;
    }
}

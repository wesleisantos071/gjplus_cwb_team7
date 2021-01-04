using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {
    public static PlatformController instance;
    public GameObject lastPlatform;
    public System.Action<GameObject> onCreatePlaform;
    public float platformSize;
    List<string> platformTags;
    ObjectPoolHandler platformPooler;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    private void Start() {
        platformPooler = ObjectPoolHandler.instance;
        platformTags = new List<string>(platformPooler.poolDictionary.Keys);
        lastPlatform = transform.GetChild(0).gameObject;
        CreatePlatform();
    }


    public void CreatePlatform() {
        int tagIndex = UnityEngine.Random.Range(0, platformTags.Count);
        string tagName = platformTags[tagIndex];
        Vector3 pos = lastPlatform.transform.position;
        pos.z = pos.z + platformSize;
        GameObject go = platformPooler.SpawnFromPool(tagName, pos, Quaternion.identity);
        lastPlatform = go;
        onCreatePlaform(go);
    }
}

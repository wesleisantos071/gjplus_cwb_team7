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
        Debug.Log("getting object pool isntance");
        platformPooler = ObjectPoolHandler.instance;
        Debug.Log("getting pool dictionary keys");
        platformTags = new List<string>(platformPooler.poolDictionary.Keys);
        Debug.Log("getting last platform");
        lastPlatform = transform.GetChild(0).gameObject;
        Debug.Log("creating more platforms");
        CreatePlatform();
    }


    public void CreatePlatform() {
        Debug.Log("getting random index of platform tags");
        int tagIndex = UnityEngine.Random.Range(0, platformTags.Count);
        string tagName = platformTags[tagIndex];
        Debug.Log("got tagname:" + tagName);
        Vector3 pos = lastPlatform.transform.position;
        pos.z = pos.z + platformSize;
        Debug.Log("spawning platform from pool");
        GameObject go = platformPooler.SpawnFromPool(tagName, pos, Quaternion.identity);
        lastPlatform = go;
        Debug.Log("trowing event on create platform");
        onCreatePlaform?.Invoke(go);
    }
}

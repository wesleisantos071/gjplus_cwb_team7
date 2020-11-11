using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {
    public static PlatformController instance;
    public GameObject[] emptyPlatformPrefabs;
    public GameObject[] easyPlatformPrefabs;
    public float distanceToCreateMorePlatform;
    GameObject lastPlatform;
    public System.Action<GameObject> onCreatePlaform;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    private void Start() {
        lastPlatform = transform.GetChild(0).gameObject;
        CreatePlatform();
    }

    public void CreatePlatform() {
        int rand = UnityEngine.Random.Range(0, 2);
        switch (rand) {
            case 0:
                CreatePlatformEmpty();
                break;
            case 1:
                CreatePlatformEasy();
                break;
        }
    }

    private float GetLastZ() {
        Vector3 lastWorldPoint = new Vector3();
        foreach (Transform transform in lastPlatform.transform) {
            if (transform.position.z > lastWorldPoint.z) {
                lastWorldPoint = transform.position;
            }
        }
        return lastPlatform.transform.InverseTransformVector(lastWorldPoint).z;
    }

    private void RepositionPlatform(GameObject platform) {
        Vector3 pos = platform.transform.position;
        pos.x = lastPlatform.transform.localPosition.x;
        pos.y = lastPlatform.transform.localPosition.y;
        pos.z = GetLastZ();
        platform.transform.localPosition = pos;
        lastPlatform = platform;
        onCreatePlaform?.Invoke(lastPlatform);
    }

    private void CreatePlatformEmpty() {
        int rand = Random.Range(0, emptyPlatformPrefabs.Length);
        GameObject prefab = emptyPlatformPrefabs[rand];
        GameObject go = GameObject.Instantiate(prefab, transform);
        RepositionPlatform(go);
    }

    private void CreatePlatformEasy() {
        int rand = Random.Range(0, easyPlatformPrefabs.Length);
        GameObject prefab = easyPlatformPrefabs[rand];
        GameObject go = GameObject.Instantiate(prefab, transform);
        RepositionPlatform(go);
    }
}

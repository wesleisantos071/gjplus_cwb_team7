using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMarkNotifyHandler : MonoBehaviour {

    Queue<GameObject> platformMarkers;

    private void Start() {
        platformMarkers = new Queue<GameObject>();
        PlatformController.instance.onCreatePlaform += AddPlatformMarker;
    }

    private void OnDestroy() {
        PlatformController.instance.onCreatePlaform -= AddPlatformMarker;
    }

    void AddPlatformMarker(GameObject lastPlatform) {
        platformMarkers.Enqueue(lastPlatform);
    }

    private void Update() {
        float currentZ = transform.position.z;
        if (platformMarkers.Count>0) {
            GameObject nextMarker = platformMarkers.Peek();
            if (currentZ > nextMarker.transform.position.z) {
                platformMarkers.Dequeue();
                PlatformController.instance.CreatePlatform();
            }
        }
    }
}

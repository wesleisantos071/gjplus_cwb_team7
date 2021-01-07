using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollowHandler : MonoBehaviour {
    public GameObject target;
    Vector3 offset;
    public bool followX;
    public bool followY;
    public bool followZ;

    private void Start() {
        offset = target.transform.position - transform.position;
    }

    public void SetTarget(GameObject go) {
        target = go;
        offset = target.transform.position - transform.position;
    }

    private void Update() {
        Vector3 tgtPos = target.transform.position;
        Vector3 futurePos = transform.position;
        if (followX) {
            futurePos.x = tgtPos.x - offset.x;
        }
        if (followY) {
            futurePos.y = tgtPos.y - offset.y;
        }
        if (followZ) {
            futurePos.z = tgtPos.z - offset.z;
        }
        transform.position = futurePos;
    }
}

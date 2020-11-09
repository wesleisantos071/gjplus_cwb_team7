using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollower : MonoBehaviour {
    public GameObject target;
    Vector3 offset;
    public bool followX = true;
    public bool followY = true;
    public bool followZ = true;

    private void Start() {
        offset = new Vector3();
        Vector3 pos = transform.position;
        if (followX)
            offset.x = target.transform.position.x - pos.x;
        if (followY)
            offset.y = target.transform.position.y - pos.y;
        if (followZ)
            offset.z = target.transform.position.z - pos.z;
    }

    private void Update() {
        Vector3 tgtPos = target.transform.position;
        transform.position = tgtPos - offset;
    }
}

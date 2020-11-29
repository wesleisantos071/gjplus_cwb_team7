using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterHandler : MonoBehaviour, IDestructable {
    float originalY;

    private void Start() {
        originalY = transform.position.y;
    }

    public void SimulateDestruction() {
        StartCoroutine(ActivateAgain());
        Vector3 pos = transform.position;
        pos.y = pos.y - 5;
        transform.position = pos;
    }

    private IEnumerator ActivateAgain() {
        yield return new WaitForSeconds(2);
        Vector3 pos = transform.position;
        pos.y = originalY;
        transform.position = pos;
    }
}

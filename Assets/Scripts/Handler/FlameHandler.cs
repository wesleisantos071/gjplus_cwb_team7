using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameHandler : MonoBehaviour, IDestructable {

    public GameObject smoke;
    public GameObject flame;
    BoxCollider collider;

    private void Start() {
        collider = GetComponent<BoxCollider>();
    }

    public void SimulateDestruction() {
        AudioHandler.instance.Play("FireEnd");
        collider.enabled = false;
        flame.GetComponentInChildren<ParticleSystem>().Stop();
        smoke.GetComponentInChildren<ParticleSystem>().Play();
        StartCoroutine(ActivateAgain());
    }

    private IEnumerator ActivateAgain() {
        yield return new WaitForSeconds(2);
        smoke.GetComponentInChildren<ParticleSystem>().Stop();
        flame.GetComponentInChildren<ParticleSystem>().Play();
        collider.enabled = true;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameHandler : MonoBehaviour, IDestructable {

    public GameObject smoke;
    public GameObject flame;
    BoxCollider boxCollider;

    private void Start() {
        boxCollider = GetComponent<BoxCollider>();
    }

    public void SimulateDestruction() {
        AudioHandler.instance.Play("FireEnd");
        boxCollider.enabled = false;
        flame.GetComponentInChildren<ParticleSystem>().Stop();
        smoke.GetComponentInChildren<ParticleSystem>().Play();
        StartCoroutine(ActivateAgain());
    }

    private IEnumerator ActivateAgain() {
        yield return new WaitForSeconds(2);
        smoke.GetComponentInChildren<ParticleSystem>().Stop();
        flame.GetComponentInChildren<ParticleSystem>().Play();
        boxCollider.enabled = true;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleHandler : MonoBehaviour {
    public static PlayerParticleHandler instance;
    public ParticleSystem[] waterParticleEmiters;
    public ParticleSystem smokeEmiter;

    public GameObject smoke;
    public Action onFireExtinct;

    public LayerMask fireMask;
    public float raycastDistance;
    public Transform raycastOrigin;
    RaycastHit hit;
    bool raycastEnabled = false;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    void Start() {
        PlayerMovementHandler.instance.onJump += StartWaterParticle;
        PlayerCollisionHandler.instance.onHitFire += TurnOnSmoke;
    }

    private void TurnOnSmoke() {
        smokeEmiter.Play();
    }


    private void StartWaterParticle() {
        foreach (ParticleSystem part in waterParticleEmiters) {
            part.Play();
        }
        raycastEnabled = true;
        AudioManager.instance.PlayWaterJet();
        PlayerCollisionHandler.instance.onHitFloor += StopWaterParticle;
    }

    private void StopWaterParticle() {
        PlayerCollisionHandler.instance.onHitFloor -= StopWaterParticle;
        raycastEnabled = false;
        foreach (ParticleSystem part in waterParticleEmiters) {
            part.Stop();
        }
        AudioManager.instance.StopWaterJet();
    }

    private void OnDestroy() {
        PlayerMovementHandler.instance.onJump -= StartWaterParticle;
        PlayerCollisionHandler.instance.onHitFire -= TurnOnSmoke;
    }

    private void FixedUpdate() {
        if (raycastEnabled && Physics.Raycast(raycastOrigin.position,
            raycastOrigin.up * -1, out hit, raycastDistance, fireMask, QueryTriggerInteraction.Collide)) {
            hit.collider.GetComponentInChildren<ParticleSystem>().Stop();
            hit.collider.GetComponentInChildren<BoxCollider>().enabled = false;
            onFireExtinct?.Invoke();
            AudioManager.instance.Play("FireEnd");
            GameObject go = GameObject.Instantiate(smoke);
            go.transform.parent = null;
            go.transform.position = hit.collider.gameObject.transform.position;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(raycastOrigin.position, raycastOrigin.up * -raycastDistance);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleHandler : MonoBehaviour {
    public ParticleSystem[] parts;

    public GameObject smoke;

    [SerializeField]
    private LayerMask fireMask;
    [SerializeField]
    private Transform raycastOrigin;
    [SerializeField]
    private float raycastDistance;
    RaycastHit hit;
    void Start() {
        PlayerMovementHandler.instance.onJump += StartWaterParticle;
    }
    bool raycastEnabled = false;

    private void StartWaterParticle() {
        foreach (ParticleSystem part in parts) {
            part.Play();
        }
        raycastEnabled = true;
        PlayerCollisionHandler.instance.onHitFloor += StopWaterParticle;
    }

    private void StopWaterParticle() {
        PlayerCollisionHandler.instance.onHitFloor -= StopWaterParticle;
        raycastEnabled = false;
        foreach (ParticleSystem part in parts) {
            part.Stop();
        }
    }

    private void OnDestroy() {
        PlayerMovementHandler.instance.onJump -= StartWaterParticle;
    }

    private void FixedUpdate() {
        if (raycastEnabled && Physics.Raycast(raycastOrigin.position,
            raycastOrigin.up * -1, out hit, raycastDistance, fireMask, QueryTriggerInteraction.Collide)) {
            hit.collider.GetComponentInChildren<ParticleSystem>().Stop();
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(raycastOrigin.position, raycastOrigin.up * -raycastDistance);
    }

}

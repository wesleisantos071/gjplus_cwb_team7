using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleHandler : MonoBehaviour {
    public ParticleSystem[] parts;

    public GameObject smoke;

    void Start() {
        PlayerMovementHandler.instance.onJump += StartWaterParticle;
    }

    private void StartWaterParticle() {
        foreach (ParticleSystem part in parts) {
            part.Play();
        }
        PlayerCollisionHandler.instance.onHitFloor += StopWaterParticle;
    }

    private void StopWaterParticle() {
        PlayerCollisionHandler.instance.onHitFloor -= StopWaterParticle;
        foreach (ParticleSystem part in parts) {
            part.Stop();
        }
    }

    private void OnDestroy() {
        PlayerMovementHandler.instance.onJump -= StartWaterParticle;
    }

    private void OnParticleCollision(GameObject other) {

    }

}

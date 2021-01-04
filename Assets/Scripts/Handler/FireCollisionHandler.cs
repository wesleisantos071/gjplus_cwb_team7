using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCollisionHandler : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.name.Equals("PlayerWater")) {
            Destroy(gameObject);
        }
    }
}

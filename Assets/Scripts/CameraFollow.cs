using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform target;
    float distance;

    private void Start() {
        distance = target.position.z - transform.position.z;
    }


    private void LateUpdate() {
        Vector3 pos = transform.position;
        if (target.transform.position.z > (pos.z + distance)) {
            Vector3 targetPos = new Vector3(pos.x, pos.y, target.transform.position.z - distance);
            transform.position = targetPos;
        }
    }
}

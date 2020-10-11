using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform target;
    float zDistance;
    float yDistance;
    public float smoothSpeed = 1.5f;

    private void Start() {
        zDistance = target.position.z - transform.position.z;
        yDistance = transform.position.y - target.position.y;
    }


    private void LateUpdate() {
        FollowInY();
        FollowInZ();
    }

    private void FollowInY() {
        Vector3 currentPos = transform.position;
        float yOffset = (target.transform.position.y + yDistance);
        Vector3 newPos = new Vector3(currentPos.x, yOffset, currentPos.z);
        transform.position = Vector3.Lerp(currentPos, newPos, smoothSpeed * Time.deltaTime);
    }

    private void FollowInZ() {
        Vector3 currentPos = transform.position;
        float zOffset = (target.transform.position.z - zDistance);
        Vector3 newPos = new Vector3(currentPos.x, currentPos.y, zOffset);
        transform.position = newPos;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour {
    public Material ashMaterial;
    GameObject originalBody;

    void Start() {
        originalBody = GameObject.FindGameObjectWithTag("Body");
        PlayerCollisionHandler.instance.onHitTree += OnHitTree;
        PlayerCollisionHandler.instance.onHitFire += OnHitFire;
        ReloadHandler.instance.onClickPlay += EnableBody;
    }

    private void OnHitTree() {
        GameObject copyBody = GameObject.Instantiate(originalBody);
        copyBody.transform.position = originalBody.transform.position;
        copyBody.transform.parent = null;
        originalBody.SetActive(false);
        List<GameObject> playerParts = new List<GameObject>();
        foreach (Transform childT in copyBody.transform) {
            GameObject go = childT.gameObject;
            if (go.CompareTag("PlayerPart")) {
                playerParts.Add(go);
            }
        }
        foreach (GameObject playerPart in playerParts) {
            if (playerPart.GetComponent<Rigidbody>() == null) {
                playerPart.AddComponent<Rigidbody>();
                playerPart.AddComponent<CapsuleCollider>();
            }
        }
        Rigidbody rb = copyBody.GetComponent<Rigidbody>();
        Destroy(rb);
        Destroy(copyBody, 2);
    }

    private void EnableBody() {
        originalBody.SetActive(true);
    }

    private void OnHitFire() {
        GameObject copyBody = GameObject.Instantiate(originalBody);
        copyBody.transform.position = originalBody.transform.position;
        copyBody.transform.parent = null;
        originalBody.SetActive(false);
        copyBody.GetComponent<MeshRenderer>().material = ashMaterial;
        Rigidbody rb = copyBody.GetComponent<Rigidbody>();
        Destroy(rb);
        Destroy(copyBody, 2);
    }

    private void OnDestroy() {
        PlayerCollisionHandler.instance.onHitTree -= OnHitTree;
        PlayerCollisionHandler.instance.onHitFire -= OnHitFire;
        ReloadHandler.instance.onClickPlay -= EnableBody;
    }

}

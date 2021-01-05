using UnityEngine;

public class MenuCloseHandler : MonoBehaviour {

    public void OnClose() {
        gameObject.transform.parent.gameObject.SetActive(false);
    }

}
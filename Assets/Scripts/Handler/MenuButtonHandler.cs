using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonHandler : MonoBehaviour {
    public GameObject menuPrefab;
    private GameObject menuInstance;

    public void OnClick() {
        if (menuInstance == null) {
            menuInstance = GameObject.Instantiate(menuPrefab);
        }
        menuInstance.SetActive(true);
    }
}

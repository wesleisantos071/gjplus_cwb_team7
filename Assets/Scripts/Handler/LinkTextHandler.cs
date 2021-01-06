using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LinkTextHandler : MonoBehaviour {
    string url;

    private void Start() {
        url = GetComponent<TextMeshProUGUI>().text;

        Button button = gameObject.AddComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public void OnClick() {
        Application.OpenURL(url);
    }
}

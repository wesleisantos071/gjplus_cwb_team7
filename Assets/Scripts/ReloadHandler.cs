using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadHandler : MonoBehaviour {
    public void OnClick() {
        SceneManager.LoadScene(0);
    }

    public void ReloadSandbox() {
        SceneManager.LoadScene(1);
    }
}

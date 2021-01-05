using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetScoreHandler : MonoBehaviour {
    public void OnResetHighScore() {
        DataHandler.instance.ResetHighScore();
        transform.parent.gameObject.SetActive(false);
    }
}

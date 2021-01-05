using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DropBoxInitializer : MonoBehaviour {
    void Start() {
        GetComponent<TMP_Dropdown>().SetValueWithoutNotify((int)DataHandler.instance.selectedLanguage);
    }

}
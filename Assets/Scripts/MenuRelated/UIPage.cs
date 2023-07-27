using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPage : MonoBehaviour {
    [SerializeField] private Button defaultSelected;

    private void OnEnable() {
        defaultSelected.Select();
    }
}

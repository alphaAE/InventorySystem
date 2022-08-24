using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TipsPanel : MonoBehaviour, IPointerUpHandler {
    private void Update() {
        if (Input.anyKey) {
            gameObject.SetActive(false);
        }
    }

    public void OnPointerUp(PointerEventData eventData) {
        gameObject.SetActive(false);
    }
}
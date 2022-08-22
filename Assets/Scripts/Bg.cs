using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bg : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public void OnPointerEnter(PointerEventData eventData) {
        InventoryManager.Instance.InBackground = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        InventoryManager.Instance.InBackground = false;
    }
}
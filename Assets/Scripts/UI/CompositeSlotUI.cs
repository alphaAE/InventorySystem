using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class CompositeSlotUI : SlotUI {
    public override void OnPointerDown(PointerEventData eventData) {
        // 快捷放回
        if (Input.GetKey(KeyCode.LeftShift) && !PickedItem.Instance.HasItem && ItemUI) {
            if (Knapsack.Instance.StoreItem(ItemUI.Item, ItemUI.Amount)) {
                DestroyImmediate(ItemUI.gameObject);
            }

            return;
        }

        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData) {
        base.OnPointerUp(eventData);
        Composite.Instance.RefreshSlot();
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class CompositeOutSlotUI : SlotUI {
    public override void OnPointerDown(PointerEventData eventData) {
        // 快捷制作
        if (Input.GetKey(KeyCode.LeftShift) && !PickedItem.Instance.HasItem && ItemUI) {
            if (Knapsack.Instance.StoreItem(ItemUI.Item, ItemUI.Amount)) {
                DestroyImmediate(ItemUI.gameObject);
                Composite.Instance.CompositeItem();
            }

            return;
        }

        // 左键 选中物品
        if (eventData.button == PointerEventData.InputButton.Left) {
            // PickedItem为空 && 当前格不为空 则选中
            // Debug.Log($"已经选中物品：{PickedItem.Instance.HasItem} \n 格子中有物品：{(bool)_itemUI}");
            if (!PickedItem.Instance.HasItem && ItemUI) {
                // 屏蔽此次抬起事件
                IsPickupTime = true;
                ToolTipUI.Instance.Hide();
                PickedItem.Instance.SetItem(ItemUI);
                DestroyImmediate(ItemUI.gameObject);
                Composite.Instance.CompositeItem();
            }
        }
    }

    public override void OnPointerUp(PointerEventData eventData) { }
}
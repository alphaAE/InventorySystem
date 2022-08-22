using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public SlotUI[] slots;
    private CanvasGroup _canvasGroup;

    private void Start() {
        slots = GetComponentsInChildren<SlotUI>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public bool StoreItem(int id) {
        Item item = InventoryManager.Instance.GetItemById(id);
        return StoreItem(item);
    }

    public bool StoreItem(Item item) {
        if (item is null) {
            Debug.LogWarning("存储物品为控");
            return false;
        }

        if (item.MaxCapacity == 1) {
            SlotUI slot = FindEmptySlot();
            if (slot) {
                slot.StoreItem(item);
                return true;
            }

            Debug.LogWarning("未找到空物品槽");
            return false;
        }
        else {
            SlotUI slot = FindSameIdSlot(item);
            if (slot) {
                slot.StoreItem(item);
                return true;
            }

            slot = FindEmptySlot();
            if (slot) {
                slot.StoreItem(item);
                return true;
            }

            Debug.LogWarning("未找到空物品槽");
            return false;
        }
    }

    private SlotUI FindEmptySlot() {
        foreach (var slot in slots) {
            if (slot.transform.childCount == 0) {
                return slot;
            }
        }

        return null;
    }

    private SlotUI FindSameIdSlot(Item item) {
        foreach (var slot in slots) {
            if (slot.transform.childCount >= 1 && slot.GetItemId() == item.ID && !slot.IsFilled()) {
                return slot;
            }
        }

        return null;
    }

    public void DisplaySwitch() {
        if (_canvasGroup.alpha == 0) {
            _canvasGroup.blocksRaycasts = true;
            DOTween.To(() => _canvasGroup.alpha, x => _canvasGroup.alpha = x, 1, .5f);
        }
        else {
            _canvasGroup.blocksRaycasts = false;
            DOTween.To(() => _canvasGroup.alpha, x => _canvasGroup.alpha = x, 0, .5f);
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public SlotUI[] slots;

    private void Start() {
        slots = GetComponentsInChildren<SlotUI>();
    }

    public bool StoreItem(int id) {
        Debug.Log(id);
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
            SlotUI slot = FindSameTypeSlot(item);
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

    private SlotUI FindSameTypeSlot(Item item) {
        foreach (var slot in slots) {
            if (slot.transform.childCount >= 1 && slot.GetItemType() == item.Type && !slot.IsFilled()) {
                return slot;
            }
        }

        return null;
    }
}
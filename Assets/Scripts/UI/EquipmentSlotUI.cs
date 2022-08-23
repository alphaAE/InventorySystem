using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class EquipmentSlotUI : SlotUI {
    public Equipment.EquipmentType type;

    private GameObject _icon;

    private void Start() {
        _icon = transform.Find("Icon").gameObject;
    }

    public override void OnPointerDown(PointerEventData eventData) {
        // 快捷装备
        if (Input.GetKey(KeyCode.LeftShift) && !PickedItem.Instance.HasItem && ItemUI) {
            Character.Instance.QuicklyExitEquip(this);
            return;
        }

        if (eventData.button == PointerEventData.InputButton.Right && PickedItem.Instance.HasItem) {
            var popItemUI = PickedItem.Instance.PopItem();
            StorePopEquipment(popItemUI);
            return;
        }

        // 左键 || 右键 选中物品
        if (eventData.button == PointerEventData.InputButton.Left ||
            eventData.button == PointerEventData.InputButton.Right) {
            // PickedItem为空 && 当前格不为空 则选中
            if (!PickedItem.Instance.HasItem && ItemUI) {
                // 屏蔽此次抬起事件
                IsPickupTime = true;
                ToolTipUI.Instance.Hide();
                PickedItem.Instance.SetItem(ItemUI);
                DestroyImmediate(ItemUI.gameObject);
            }

            SwitchDisplayIcon();
        }
    }

    public override void OnPointerUp(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Left) {
            if (!IsPickupTime && PickedItem.Instance.HasItem) {
                var popItemUI = PickedItem.Instance.PopItem();
                StorePopEquipment(popItemUI);
            }
            else {
                IsPickupTime = false;
            }
        }
        else if (eventData.button == PointerEventData.InputButton.Right) {
            if (IsPickupTime) {
                IsPickupTime = false;
            }
        }

        SwitchDisplayIcon();
    }

    /// <summary>
    /// 尝试将装备放入该格
    /// </summary>
    /// <param name="popItemUI"></param>
    /// <returns></returns>
    private bool StorePopEquipment(ItemUI popItemUI) {
        // 筛选仅限装备
        if ((popItemUI.Item as Equipment) == null) {
            PickedItem.Instance.SetItem(popItemUI);
            return false;
        }

        var equipment = (Equipment)popItemUI.Item;
        // 符合该格的装备 
        if (equipment.EquipType == type) {
            // 当前格为空 则放下
            if (!ItemUI) {
                StoreItem(popItemUI.Item);
                ToolTipUI.Instance.Show(ItemUI.Item.GetToolTipText());
            }
            // 当前格不为空 则交换
            else {
                Item tempItem = ItemUI.Item;
                int amount = ItemUI.Amount;

                DestroyImmediate(ItemUI.gameObject);
                StoreItem(popItemUI.Item, popItemUI.Amount);
                PickedItem.Instance.SetItem(tempItem, amount);
            }

            return true;
        }

        // 无法放置则 回退到PickedItem
        PickedItem.Instance.SetItem(equipment);
        return false;
    }

    public void SwitchDisplayIcon() {
        if (!ItemUI) {
            _icon.SetActive(true);
        }
        else {
            _icon.SetActive(false);
        }
    }
}
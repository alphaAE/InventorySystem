using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ShopSlotUI : SlotUI {
    public override void OnPointerDown(PointerEventData eventData) {
        // 快捷买入
        if (Input.GetKey(KeyCode.LeftControl) && !PickedItem.Instance.HasItem && ItemUI) {
            // 检测是否能买一组
            if (Shop.Instance.QuickBuyItem(ItemUI.Item, ItemUI.Item.MaxCapacity)) return;
            int amount = Player.Instance.CoinAmount / ItemUI.Item.BuyPrice;

            if (amount > 0) {
                Shop.Instance.QuickBuyItem(ItemUI.Item, amount);
            }

            return;
        }

        // 左键购买一组 或 最大可购个数
        if (eventData.button == PointerEventData.InputButton.Left && !PickedItem.Instance.HasItem && ItemUI) {
            // 检测是否能买一组
            if (Shop.Instance.BuyItem(ItemUI.Item, ItemUI.Item.MaxCapacity)) return;
            int amount = Player.Instance.CoinAmount / ItemUI.Item.BuyPrice;

            if (amount > 0) {
                Shop.Instance.BuyItem(ItemUI.Item, amount);
            }

            return;
        }

        // 右键购买单个
        if (eventData.button == PointerEventData.InputButton.Right && !PickedItem.Instance.HasItem && ItemUI) {
            Shop.Instance.BuyItem(ItemUI.Item);
            return;
        }

        // 卖出整组
        if (eventData.button == PointerEventData.InputButton.Left && PickedItem.Instance.HasItem) {
            Shop.Instance.SellPickedItem();
            return;
        }

        // 卖出单个
        if (eventData.button == PointerEventData.InputButton.Right && PickedItem.Instance.HasItem) {
            Shop.Instance.SellPickedItem(1);
            return;
        }
    }

    public override void OnPointerUp(PointerEventData eventData) { }

    public override void OnPointerEnter(PointerEventData eventData) {
        base.OnPointerEnter(eventData);
        if (PickedItem.Instance.HasItem) {
            ToolTipUI.Instance.Show("卖出");
        }
    }

    public override void OnPointerExit(PointerEventData eventData) {
        base.OnPointerExit(eventData);
        if (PickedItem.Instance.HasItem) {
            ToolTipUI.Instance.Hide();
        }
    }
}
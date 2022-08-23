using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shop : Inventory, IPointerDownHandler {
    public static Shop Instance { get; private set; }

    private ShopSlotUI[] _shopSlotUis;

    private int[] _itemIds = { 1, 2, 10, 14, 17, 5, 6, 7, 8, };

    private void Awake() {
        Instance = this;
        _shopSlotUis = GetComponentsInChildren<ShopSlotUI>();
    }

    protected override void Start() {
        // 初始化商店物品
        base.Start();
        slots = _shopSlotUis;
        foreach (var itemId in _itemIds) {
            Item item = InventoryManager.Instance.GetItemById(itemId);
            StoreItem(item);
        }
    }

    public void OnPointerDown(PointerEventData eventData) {
        // 卖出
        if (eventData.button == PointerEventData.InputButton.Left && PickedItem.Instance.HasItem) {
            SellPickedItem();
            return;
        }

        // 卖出单个
        if (eventData.button == PointerEventData.InputButton.Right && PickedItem.Instance.HasItem) {
            Shop.Instance.SellPickedItem(1);
            return;
        }
    }

    public bool BuyItem(Item item, int amount = 1) {
        if (Player.Instance.ConsumeCoin(item.BuyPrice * amount)) {
            PickedItem.Instance.SetItem(item, amount);
            ToolTipUI.Instance.Hide();
            return true;
        }

        return false;
    }

    public void SellPickedItem(int amount = -1) {
        if (amount < 0) {
            amount = PickedItem.Instance.ItemUI.Amount;
        }

        if (amount > PickedItem.Instance.ItemUI.Amount) return;
        SellItem(PickedItem.Instance.ItemUI.Item, amount);
        var itemUI = PickedItem.Instance.PopItem();
        var item = itemUI.Item;
        var iAmount = (itemUI.Amount -= amount);
        if (iAmount > 0) {
            PickedItem.Instance.SetItem(item, iAmount);
        }
        // ToolTipUI.Instance.Show();
    }

    public void SellItem(Item item, int amount = 1) {
        Player.Instance.EarnCoin(item.SellPrice * amount);
    }
}
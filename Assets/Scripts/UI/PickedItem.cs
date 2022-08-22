using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickedItem : MonoBehaviour {
    public static PickedItem Instance { get; private set; }

    public ItemUI ItemUI { get; private set; }

    public bool HasItem { get; private set; }

    private RectTransform _canvasRect;


    private void Awake() {
        Instance = this;
        ItemUI = GetComponent<ItemUI>();
        ItemUI.IsPlayAnim = false;
        ItemUI.Hide();

        _canvasRect = GameObject.Find("Canvas").GetComponent<RectTransform>();
    }

    private void Update() {
        if (HasItem) {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvasRect, Input.mousePosition, null, out pos);
            transform.localPosition = pos;
        }
    }


    public void AddItem(ItemUI itemUI) {
        AddItem(itemUI.Item, itemUI.Amount);
    }

    public void AddItem(Item item, int amount = 1) {
        HasItem = true;
        ItemUI.SetItem(item, amount);
        ItemUI.Show();
    }

    public ItemUI PopItem() {
        ItemUI.Hide();
        HasItem = false;
        return ItemUI;
    }
}
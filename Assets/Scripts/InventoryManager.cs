using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour {
    public static InventoryManager Instance { get; private set; }

    private List<Item> _items = new();

    private bool _inBackground;

    public bool InBackground {
        get => _inBackground;
        set {
            if (value && PickedItem.Instance.HasItem) {
                ToolTipUI.Instance.Show("丢弃");
            }
            else if (!value) {
                ToolTipUI.Instance.Hide();
            }

            _inBackground = value;
        }
    }

    private void Awake() {
        Instance = this;
        ParserItemJson();
    }

    private void Update() {
        // 处理丢弃物品
        if (Input.GetMouseButtonDown(0) && InBackground && PickedItem.Instance.HasItem) {
            PickedItem.Instance.PopItem();
            ToolTipUI.Instance.Hide();
        }
    }

    private void ParserItemJson() {
        TextAsset itemText = Resources.Load<TextAsset>("items");
        JArray jArray = JArray.Parse(itemText.text);

        foreach (var jToken in jArray) {
            var typeStr = jToken.Value<String>("type");
            if (typeStr is null) continue;
            Type type = Type.GetType(typeStr);
            if (type is null) continue;
            Item item = (Item)jToken.ToObject(type);

            if (item is not null) {
                _items.Add(item);
            }
        }
    }

    public Item GetItemById(int id) {
        foreach (var item in _items) {
            if (item.ID == id)
                return item;
        }

        return null;
    }
}
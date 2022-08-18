using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour {
    public static InventoryManager Instance { get; set; }

    private List<Item> _items = new();

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        ParserItemJson();
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
                Debug.Log(item.ToString());
            }
        }
    }
}
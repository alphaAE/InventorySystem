using System;
using System.Collections;
using System.Collections.Generic;
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
        Debug.Log(itemText.text);
    }
}
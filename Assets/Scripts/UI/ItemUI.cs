using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUI : MonoBehaviour {
    public Item Item { get; set; }
    public int Amount { get; set; }

    public void SetItem(Item item, int amount = 1) {
        Item = item;
        Amount = amount;
        UpdateUI();
    }

    public void AddAmount(int amount = 1) {
        Amount += amount;
        UpdateUI();
    }

    private void UpdateUI() {
        //TODO
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour {
    public Item Item { get; set; }
    public int Amount { get; set; }

    private Image _itemImg;
    private Text _amountText;

    private void Awake() {
        _itemImg = GetComponent<Image>();
        _amountText = GetComponentInChildren<Text>();
    }

    public void SetItem(Item item, int amount = 1) {
        Item = item;
        Amount = amount;
        // update ui
        _itemImg.sprite = Resources.Load<Sprite>(item.Sprite);
        if (Amount > 1) {
            _amountText.text = Amount.ToString();
        }
        else {
            _amountText.text = "";
        }
    }

    public void AddAmount(int amount = 1) {
        Amount += amount;
        // update ui
        _amountText.text = Amount.ToString();
    }
}
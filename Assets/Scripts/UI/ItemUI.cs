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

    public void SetItem(ItemUI itemUI) {
        SetItem(itemUI.Item, itemUI.Amount);
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

    public void SetAmount(int amount) {
        Amount = amount;
        _amountText.text = Amount.ToString();
    }

    public void AddAmount(int amount = 1) {
        Amount += amount;
        // update ui
        _amountText.text = Amount.ToString();
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    public void Selected() {
        _itemImg.color = new Color(_itemImg.color.r, _itemImg.color.g, _itemImg.color.b, 0.5f);
        _amountText.color = new Color(_amountText.color.r, _amountText.color.g, _amountText.color.b, 0.5f);
    }

    public void CancelSelected() {
        _itemImg.color = new Color(_itemImg.color.r, _itemImg.color.g, _itemImg.color.b, 1f);
        _amountText.color = new Color(_amountText.color.r, _amountText.color.g, _amountText.color.b, 1f);
    }
}
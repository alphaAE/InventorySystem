using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour {
    public Item Item { get; set; }
    public int Amount { get; set; }

    public bool IsPlayAnim { get; set; }

    private Image _itemImg;

    private Image ItemImg {
        get {
            if (!_itemImg) {
                _itemImg = GetComponent<Image>();
            }

            return _itemImg;
        }
        set => _itemImg = value;
    }

    private Text _amountText;

    private Text AmountText {
        get {
            if (!_amountText) {
                _amountText = GetComponentInChildren<Text>();
            }

            return _amountText;
        }
        set => _amountText = value;
    }

    private void Awake() {
        IsPlayAnim = true;
    }

    public void SetItem(Item item, int amount = 1) {
        Item = item;
        Amount = amount;
        // update ui
        ItemImg.sprite = Resources.Load<Sprite>(item.Sprite);
        if (Amount > 1) {
            AmountText.text = Amount.ToString();
        }
        else {
            AmountText.text = "";
        }

        ScaleAnim();
    }

    public void SetAmount(int amount) {
        Amount = amount;
        ScaleAnim();
        // update ui
        if (Amount > 1) {
            AmountText.text = Amount.ToString();
        }
        else {
            AmountText.text = "";
        }
    }

    public void AddAmount(int amount = 1) {
        Amount += amount;
        ScaleAnim();
        // update ui
        if (Amount > 1) {
            AmountText.text = Amount.ToString();
        }
        else {
            AmountText.text = "";
        }
    }

    public void ReduceAmount(int amount = 1) {
        Amount -= amount;
        if (Amount <= 0) {
            DestroyImmediate(gameObject);
            return;
        }

        ScaleAnim();
        // update ui
        if (Amount > 1) {
            AmountText.text = Amount.ToString();
        }
        else {
            AmountText.text = "";
        }
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    private void ScaleAnim() {
        if (IsPlayAnim) {
            transform.DOScale(Vector3.one * 1.2f, 0.2f).OnComplete(() => { transform.DOScale(Vector3.one, 0.2f); });
        }
    }

    public void Selected() {
        ItemImg.color = new Color(ItemImg.color.r, ItemImg.color.g, ItemImg.color.b, 0.5f);
        AmountText.color = new Color(AmountText.color.r, AmountText.color.g, AmountText.color.b, 0.5f);
    }

    public void CancelSelected() {
        ItemImg.color = new Color(ItemImg.color.r, ItemImg.color.g, ItemImg.color.b, 1f);
        AmountText.color = new Color(AmountText.color.r, AmountText.color.g, AmountText.color.b, 1f);
    }
}
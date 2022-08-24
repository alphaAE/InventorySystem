using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BtnRandomItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public static BtnRandomItem Instance;
    private CanvasGroup _canvasGroup;
    private Button _btn;

    private void Awake() {
        Instance = this;
    }

    void Start() {
        _canvasGroup = GetComponent<CanvasGroup>();
        _btn = GetComponent<Button>();
        _btn.onClick.AddListener(OnButtonClick);
        if (_canvasGroup.alpha != 0) {
            IsDisplay = true;
        }
    }

    public void OnButtonClick() {
        for (int i = 0; i < 5; i++) {
            int id = Random.Range(1, 22);
            Item item = InventoryManager.Instance.GetItemById(id);
            Knapsack.Instance.StoreItem(item, item.MaxCapacity);
        }
    }

    public void DisplaySwitch() {
        if (!IsDisplay) {
            Show();
            IsDisplay = true;
        }
        else {
            Hide();
            IsDisplay = false;
        }
    }

    public bool IsDisplay { get; set; }


    public void Show() {
        _canvasGroup.blocksRaycasts = true;
        DOTween.To(() => _canvasGroup.alpha, x => _canvasGroup.alpha = x, 1, .5f);
    }

    public void Hide() {
        _canvasGroup.blocksRaycasts = false;
        DOTween.To(() => _canvasGroup.alpha, x => _canvasGroup.alpha = x, 0, .5f);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (!PickedItem.Instance.HasItem) {
            ToolTipUI.Instance.Show("随机生成物品");
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (!PickedItem.Instance.HasItem && ToolTipUI.Instance.IsDisplay) {
            ToolTipUI.Instance.Hide();
        }
    }
}
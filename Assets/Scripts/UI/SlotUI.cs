using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    private VarsManager _vars;
    private ItemUI _item;
    private IPointerEnterHandler _pointerEnterHandlerImplementation;

    private void Awake() {
        _vars = VarsManager.GetVarsManager();
    }

    /// <summary>
    /// 把Item存放到自身下
    /// 如果不存在则实例化一个ItemUI
    /// 如果已存在物品则 Amount++
    /// </summary>
    /// <param name="item"></param>
    public void StoreItem(Item item) {
        if (!_item) {
            GameObject itemObj = Instantiate(_vars.itemUIPre, transform);
            itemObj.transform.localPosition = Vector3.zero;
            _item = itemObj.GetComponent<ItemUI>();
            _item.SetItem(item);
        }
        else {
            _item.AddAmount();
        }
    }

    /// <summary>
    /// 得到当前物品槽存放的物品类型
    /// </summary>
    /// <returns></returns>
    public Item.ItemType GetItemType() {
        return _item.Item.Type;
    }
    
    /// <summary>
    /// 得到当前物品槽存放的物品ID
    /// </summary>
    /// <returns></returns>
    public int GetItemId() {
        return _item.Item.ID;
    }

    public bool IsFilled() {
        return _item.Amount >= _item.Item.MaxCapacity;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (!_item) return;
        ToolTipUI.Instance.Show(_item.Item.GetToolTipText());
        Debug.Log(_item.Item.GetToolTipText());
    }

    public void OnPointerExit(PointerEventData eventData) {
        ToolTipUI.Instance.Hide();
    }
}
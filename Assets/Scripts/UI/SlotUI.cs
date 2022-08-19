using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotUI : MonoBehaviour {
    private VarsManager _vars;
    private ItemUI _item;

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

    public bool IsFilled() {
        return _item.Amount >= _item.Item.MaxCapacity;
    }
}
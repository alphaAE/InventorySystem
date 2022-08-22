using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class SlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler {
    private VarsManager _vars;
    private ItemUI _itemUI;
    private IPointerEnterHandler _pointerEnterHandlerImplementation;

    private bool _isPickupTime;

    private void Awake() {
        _vars = VarsManager.GetVarsManager();
    }

    /// <summary>
    /// 把Item存放到自身下
    /// 如果不存在则实例化一个ItemUI
    /// 如果已存在物品则 Amount++
    /// </summary>
    /// <param name="item"></param>
    /// <param name="amount"></param>
    public void StoreItem(Item item, int amount = 1) {
        if (!(bool)_itemUI) {
            GameObject itemObj = Instantiate(_vars.itemUIPre, transform);
            itemObj.transform.localPosition = Vector3.zero;
            _itemUI = itemObj.GetComponent<ItemUI>();
            _itemUI.SetItem(item, amount);
        }
        else {
            _itemUI.AddAmount();
        }
    }

    /// <summary>
    /// 得到当前物品槽存放的物品类型
    /// </summary>
    /// <returns></returns>
    public Item.ItemType GetItemType() {
        return _itemUI.Item.Type;
    }

    /// <summary>
    /// 得到当前物品槽存放的物品ID
    /// </summary>
    /// <returns></returns>
    public int GetItemId() {
        return _itemUI.Item.ID;
    }

    public bool IsFilled() {
        return _itemUI.Amount >= _itemUI.Item.MaxCapacity;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (!_itemUI || PickedItem.Instance.HasItem) return;
        ToolTipUI.Instance.Show(_itemUI.Item.GetToolTipText());
    }

    public void OnPointerExit(PointerEventData eventData) {
        ToolTipUI.Instance.Hide();
    }

    /// <summary>
    /// 鼠标按下
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Right && PickedItem.Instance.HasItem) {
            var popItemUI = PickedItem.Instance.PopItem();
            // 当前为空 则放下单个
            if (!_itemUI) {
                StoreItem(popItemUI.Item);
                if (popItemUI.Amount - 1 > 0) {
                    PickedItem.Instance.AddItem(popItemUI.Item, popItemUI.Amount - 1);
                }
                else {
                    ToolTipUI.Instance.Show(_itemUI.Item.GetToolTipText());
                }
            }
            // 物品相同 则尝试放下单个
            else if (popItemUI.Item == _itemUI.Item) {
                // --不满
                if (_itemUI.Amount < _itemUI.Item.MaxCapacity) {
                    StoreItem(popItemUI.Item, _itemUI.Amount + 1);
                    if (popItemUI.Amount - 1 > 0) {
                        PickedItem.Instance.AddItem(popItemUI.Item, popItemUI.Amount - 1);
                    }
                    else {
                        ToolTipUI.Instance.Show(_itemUI.Item.GetToolTipText());
                    }
                }
                else {
                    PickedItem.Instance.AddItem(popItemUI);
                }
            }
            else {
                PickedItem.Instance.AddItem(popItemUI);
            }

            return;
        }

        // 左键 || 右键 选中物品
        if (eventData.button == PointerEventData.InputButton.Left ||
            eventData.button == PointerEventData.InputButton.Right) {
            // PickedItem为空 && 当前格不为空 则选中
            // Debug.Log($"已经选中物品：{PickedItem.Instance.HasItem} \n 格子中有物品：{(bool)_itemUI}");
            if (!PickedItem.Instance.HasItem && _itemUI) {
                // 屏蔽此次抬起事件
                _isPickupTime = true;
                ToolTipUI.Instance.Hide();
                PickedItem.Instance.AddItem(_itemUI);
                DestroyImmediate(_itemUI.gameObject);
            }
        }
    }

    /// <summary>
    /// 鼠标抬起
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Left) {
            if (!_isPickupTime && PickedItem.Instance.HasItem) {
                var popItemUI = PickedItem.Instance.PopItem();
                // 当前格为空则放下
                if (!_itemUI) {
                    StoreItem(popItemUI.Item, popItemUI.Amount);
                    ToolTipUI.Instance.Show(_itemUI.Item.GetToolTipText());
                }
                // 当前格不为空
                else {
                    // --相同物品
                    if (popItemUI.Item == _itemUI.Item) {
                        // 当前格能放下 放下全部
                        if (_itemUI.Amount + popItemUI.Amount <= _itemUI.Item.MaxCapacity) {
                            _itemUI.SetAmount(_itemUI.Amount + popItemUI.Amount);
                            ToolTipUI.Instance.Show(_itemUI.Item.GetToolTipText());
                        }
                        // 当前格不能放下 放下部分
                        else {
                            int poorCapacity = _itemUI.Amount + popItemUI.Amount - _itemUI.Item.MaxCapacity;
                            _itemUI.SetAmount(_itemUI.Item.MaxCapacity);
                            PickedItem.Instance.AddItem(popItemUI.Item, poorCapacity);
                        }
                    }
                    // --不相同 则将与PickedItem交换
                    else {
                        Item tempItem = _itemUI.Item;
                        int amount = _itemUI.Amount;

                        DestroyImmediate(_itemUI.gameObject);
                        StoreItem(popItemUI.Item, popItemUI.Amount);
                        PickedItem.Instance.AddItem(tempItem, amount);
                    }
                }
            }
            else {
                _isPickupTime = false;
            }
        }
        else if (eventData.button == PointerEventData.InputButton.Right) {
            if (_isPickupTime) {
                _isPickupTime = false;
            }
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class SlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler {
    protected VarsManager Vars;
    protected ItemUI ItemUI;

    protected bool IsPickupTime;

    private void Awake() {
        Vars = VarsManager.GetVarsManager();
    }

    /// <summary>
    /// 把Item存放到自身下
    /// 如果不存在则实例化一个ItemUI
    /// 如果已存在物品则 Amount++
    /// </summary>
    /// <param name="item"></param>
    /// <param name="amount"></param>
    public void StoreItem(Item item, int amount = 1) {
        if (!(bool)ItemUI) {
            GameObject itemObj = Instantiate(Vars.itemUIPre, transform);
            itemObj.transform.localPosition = Vector3.zero;
            ItemUI = itemObj.GetComponent<ItemUI>();
            ItemUI.SetItem(item, amount);
        }
        else {
            ItemUI.AddAmount();
        }
    }

    /// <summary>
    /// 得到当前物品槽存放的物品类型
    /// </summary>
    /// <returns></returns>
    public Item.ItemType GetItemType() {
        return ItemUI.Item.Type;
    }

    /// <summary>
    /// 得到当前物品槽存放的物品ID
    /// </summary>
    /// <returns></returns>
    public int GetItemId() {
        return ItemUI.Item.ID;
    }

    public bool IsFilled() {
        return ItemUI.Amount >= ItemUI.Item.MaxCapacity;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (!ItemUI || PickedItem.Instance.HasItem) return;
        ToolTipUI.Instance.Show(ItemUI.Item.GetToolTipText());
    }

    public void OnPointerExit(PointerEventData eventData) {
        ToolTipUI.Instance.Hide();
    }

    /// <summary>
    /// 鼠标按下
    /// </summary>
    /// <param name="eventData"></param>
    public virtual void OnPointerDown(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Right && PickedItem.Instance.HasItem) {
            var popItemUI = PickedItem.Instance.PopItem();
            // 当前为空 则放下单个
            if (!ItemUI) {
                StoreItem(popItemUI.Item);
                if (popItemUI.Amount - 1 > 0) {
                    PickedItem.Instance.AddItem(popItemUI.Item, popItemUI.Amount - 1);
                }
                else {
                    ToolTipUI.Instance.Show(ItemUI.Item.GetToolTipText());
                }
            }
            // 物品相同 则尝试放下单个
            else if (popItemUI.Item == ItemUI.Item) {
                // --不满
                if (ItemUI.Amount < ItemUI.Item.MaxCapacity) {
                    StoreItem(popItemUI.Item, ItemUI.Amount + 1);
                    if (popItemUI.Amount - 1 > 0) {
                        PickedItem.Instance.AddItem(popItemUI.Item, popItemUI.Amount - 1);
                    }
                    else {
                        ToolTipUI.Instance.Show(ItemUI.Item.GetToolTipText());
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

        // 左键 选中物品
        if (eventData.button == PointerEventData.InputButton.Left) {
            // PickedItem为空 && 当前格不为空 则选中
            // Debug.Log($"已经选中物品：{PickedItem.Instance.HasItem} \n 格子中有物品：{(bool)_itemUI}");
            if (!PickedItem.Instance.HasItem && ItemUI) {
                // 屏蔽此次抬起事件
                IsPickupTime = true;
                ToolTipUI.Instance.Hide();
                PickedItem.Instance.AddItem(ItemUI);
                DestroyImmediate(ItemUI.gameObject);
            }
        }

        // 右键 选中物品一半
        if (eventData.button == PointerEventData.InputButton.Right) {
            if (!PickedItem.Instance.HasItem && ItemUI) {
                // 屏蔽此次抬起事件
                IsPickupTime = true;
                ToolTipUI.Instance.Hide();
                // 个数大于1 则拿起一半
                if (ItemUI.Amount > 1) {
                    int half = ItemUI.Amount / 2;
                    PickedItem.Instance.AddItem(ItemUI.Item, half);
                    ItemUI.SetAmount(ItemUI.Amount - half);
                }
                else {
                    PickedItem.Instance.AddItem(ItemUI);
                    DestroyImmediate(ItemUI.gameObject);
                }
            }
        }
    }

    /// <summary>
    /// 鼠标抬起
    /// </summary>
    /// <param name="eventData"></param>
    public virtual void OnPointerUp(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Left) {
            if (!IsPickupTime && PickedItem.Instance.HasItem) {
                var popItemUI = PickedItem.Instance.PopItem();
                // 当前格为空则放下
                if (!ItemUI) {
                    StoreItem(popItemUI.Item, popItemUI.Amount);
                    ToolTipUI.Instance.Show(ItemUI.Item.GetToolTipText());
                }
                // 当前格不为空
                else {
                    // --相同物品
                    if (popItemUI.Item == ItemUI.Item) {
                        // 当前格能放下 放下全部
                        if (ItemUI.Amount + popItemUI.Amount <= ItemUI.Item.MaxCapacity) {
                            ItemUI.SetAmount(ItemUI.Amount + popItemUI.Amount);
                            ToolTipUI.Instance.Show(ItemUI.Item.GetToolTipText());
                        }
                        // 当前格不能放下 放下部分
                        else {
                            int poorCapacity = ItemUI.Amount + popItemUI.Amount - ItemUI.Item.MaxCapacity;
                            ItemUI.SetAmount(ItemUI.Item.MaxCapacity);
                            PickedItem.Instance.AddItem(popItemUI.Item, poorCapacity);
                        }
                    }
                    // --不相同 则将与PickedItem交换
                    else {
                        Item tempItem = ItemUI.Item;
                        int amount = ItemUI.Amount;

                        DestroyImmediate(ItemUI.gameObject);
                        StoreItem(popItemUI.Item, popItemUI.Amount);
                        PickedItem.Instance.AddItem(tempItem, amount);
                    }
                }
            }
            else {
                IsPickupTime = false;
            }
        }
        else if (eventData.button == PointerEventData.InputButton.Right) {
            if (IsPickupTime) {
                IsPickupTime = false;
            }
        }
    }
}
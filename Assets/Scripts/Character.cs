using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : Inventory {
    public static Character Instance { get; private set; }

    private EquipmentSlotUI[] _equipmentSlotUis;

    private Text _coinText;

    private void Awake() {
        Instance = this;
        _equipmentSlotUis = GetComponentsInChildren<EquipmentSlotUI>();
        _coinText = transform.Find("Coin/Text").GetComponent<Text>();
        slots = _equipmentSlotUis;
    }

    public bool QuicklyEnterEquip(SlotUI slotUI) {
        // 判断是否是装备
        if ((slotUI.ItemUI.Item as Equipment) == null) {
            return false;
        }

        var equipment = (Equipment)slotUI.ItemUI.Item;

        // 寻找符合槽位
        for (int i = 0; i < _equipmentSlotUis.Length; i++) {
            if (_equipmentSlotUis[i].type == equipment.EquipType) {
                // 槽位空 则放置
                if (!_equipmentSlotUis[i].ItemUI) {
                    DestroyImmediate(slotUI.ItemUI.gameObject);
                    _equipmentSlotUis[i].StoreItem(equipment);
                    _equipmentSlotUis[i].SwitchDisplayIcon();
                }
                // 槽位不空 则交换位置
                else {
                    Item tempItem = _equipmentSlotUis[i].ItemUI.Item;
                    int amount = _equipmentSlotUis[i].ItemUI.Amount;

                    DestroyImmediate(_equipmentSlotUis[i].ItemUI.gameObject);
                    _equipmentSlotUis[i].StoreItem(equipment);

                    DestroyImmediate(slotUI.ItemUI.gameObject);
                    slotUI.StoreItem(tempItem, amount);
                    _equipmentSlotUis[i].SwitchDisplayIcon();
                }

                return true;
            }
        }

        return false;
    }

    public bool QuicklyExitEquip(EquipmentSlotUI equipmentSlotUI) {
        var equipment = (Equipment)equipmentSlotUI.ItemUI.Item;
        // 尝试向背包放置
        if (Knapsack.Instance.StoreItem(equipment)) {
            DestroyImmediate(equipmentSlotUI.ItemUI.gameObject);
            equipmentSlotUI.SwitchDisplayIcon();
            return true;
        }

        return false;
    }

    public void SetCoinText(int amount) {
        _coinText.text = amount.ToString();
    }
}
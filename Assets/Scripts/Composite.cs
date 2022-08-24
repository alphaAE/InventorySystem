using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Composite : Inventory {
    public static Composite Instance { get; private set; }

    private CompositeSlotUI[] _compositeSlotUis;
    private CompositeOutSlotUI _compositeOutSlot;

    private List<Formula> _formulas;

    private void Awake() {
        Instance = this;
    }

    protected override void Start() {
        base.Start();
        _compositeSlotUis = GetComponentsInChildren<CompositeSlotUI>();
        _compositeOutSlot = GetComponentInChildren<CompositeOutSlotUI>();
        _formulas = InventoryManager.Instance.GetFormulas();
        slots = _compositeSlotUis;
    }

    private int _compositeAmount;

    public void RefreshSlot() {
        var (ids, amounts) = GetItems();
        Formula formula = GetFormula(ids);
        if (formula != null) {
            if (_compositeOutSlot.ItemUI) {
                DestroyImmediate(_compositeOutSlot.ItemUI.gameObject);
            }

            // 计算可制作个数
            var compositeAmount = amounts.Min();
            Item item = InventoryManager.Instance.GetItemById(formula.ResultId);
            if (compositeAmount > item.MaxCapacity) {
                compositeAmount = item.MaxCapacity;
            }

            // 按个数生成
            _compositeAmount = compositeAmount;
            _compositeOutSlot.StoreItem(item, compositeAmount);
        }
        else {
            if (_compositeOutSlot.ItemUI) {
                DestroyImmediate(_compositeOutSlot.ItemUI.gameObject);
            }
        }
    }

    private Tuple<int[], int[]> GetItems() {
        // 得到当前合成台内物品
        int[] ids = new int[_compositeSlotUis.Length];
        int[] amounts = new int[_compositeSlotUis.Length];
        for (int i = 0; i < _compositeSlotUis.Length; i++) {
            if (!_compositeSlotUis[i].ItemUI) {
                ids[i] = 0;
                amounts[i] = Int32.MaxValue;
                continue;
            }

            ids[i] = _compositeSlotUis[i].ItemUI.Item.ID;
            amounts[i] = _compositeSlotUis[i].ItemUI.Amount;
        }

        return Tuple.Create(ids, amounts);
    }

    private Formula GetFormula(int[] ids) {
        for (int i = 0; i < _formulas.Count; i++) {
            if (_formulas[i].Match(ids)) {
                return _formulas[i];
            }
        }

        return null;
    }

    public void CompositeItem() {
        for (int i = 0; i < _compositeSlotUis.Length; i++) {
            if (_compositeSlotUis[i].ItemUI) {
                _compositeSlotUis[i].ItemUI.ReduceAmount(_compositeAmount);
            }
        }

        RefreshSlot();
    }
}
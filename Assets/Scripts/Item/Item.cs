using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {
    public int ID { get; set; }
    public string Name { get; set; }
    public ItemType Type { get; set; }
    public ItemQuality Quality { get; set; }
    public string Description { get; set; }
    public int MaxCapacity { get; set; }
    public int BuyPrice { get; set; }
    public int SellPrice { get; set; }

    public string Sprite { get; set; }

    public Item(int id, string name, ItemType type, ItemQuality quality, string description, int maxCapacity,
        int buyPrice, int sellPrice, string sprite) {
        ID = id;
        Name = name;
        Type = type;
        Quality = quality;
        Description = description;
        MaxCapacity = maxCapacity;
        BuyPrice = buyPrice;
        SellPrice = sellPrice;
        Sprite = sprite;
    }

    public virtual string GetToolTipText() {
        return Name;
    }

    /// <summary>
    /// 物品类型
    /// </summary>
    public enum ItemType {
        Consumable,
        Equipment,
        Weapon,
        Material,
    }

    /// <summary>
    /// 品质
    /// </summary>
    public enum ItemQuality {
        Common,
        Rare,
        Epic,
        Legendary,
        Artifact
    }
}
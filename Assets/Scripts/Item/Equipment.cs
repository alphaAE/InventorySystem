using System;

public class Equipment : Item {
    //力量
    public int Strength { get; set; }

    //智力
    public int Intellect { get; set; }

    //敏捷
    public int Agility { get; set; }

    //体力
    public int Stamina { get; set; }

    public EquipmentType EquipType { get; set; }

    public Equipment(int id, string name, ItemType type, ItemQuality quality, string description, int maxCapacity,
        int buyPrice, int sellPrice, string sprite, int strength, int intellect, int agility, int stamina,
        EquipmentType equipType) : base(id, name, type, quality, description, maxCapacity, buyPrice, sellPrice,
        sprite) {
        Strength = strength;
        Intellect = intellect;
        Agility = agility;
        Stamina = stamina;
        EquipType = equipType;
    }

    public override string GetToolTipText() {
        var text = base.GetToolTipText();
        if (Strength > 0) {
            text += $"<color=yellow>\n力量: {Strength}</color>";
        }

        if (Intellect > 0) {
            text += $"<color=yellow>\n智力: {Intellect}</color>";
        }

        if (Agility > 0) {
            text += $"<color=yellow>\n敏捷: {Agility}</color>";
        }

        if (Stamina > 0) {
            text += $"<color=yellow>\n体力: {Stamina}</color>";
        }

        text += $"<color=blue>\n类型: {GetEquipTypeText()}</color>";

        return text;
    }

    private String GetEquipTypeText() {
        switch (EquipType) {
            case EquipmentType.Head:
                return "头部";
            case EquipmentType.Chest:
                return "胸甲";
            case EquipmentType.Leg:
                return "腿部";
            case EquipmentType.Boots:
                return "靴子";
            case EquipmentType.OffHand:
                return "副手";
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public enum EquipmentType {
        Head,
        Chest,
        Leg,
        Boots,
        OffHand,
    }
}
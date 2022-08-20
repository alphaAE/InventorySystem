using System;

public class Weapon : Item {
    public int Damage { get; set; }

    public WeaponType wpType { get; set; }

    public Weapon(int id, string name, ItemType type, ItemQuality quality, string description, int maxCapacity,
        int buyPrice, int sellPrice, string sprite, int damage, WeaponType wpType) : base(id, name, type, quality,
        description, maxCapacity, buyPrice, sellPrice, sprite) {
        Damage = damage;
        this.wpType = wpType;
    }

    public override string GetToolTipText() {
        var text = base.GetToolTipText();
        if (Damage > 0) {
            text += $"<color=yellow>\n伤害: {Damage}</color>";
        }

        text += $"<color=blue>\n类型: {GetWeaponTypeText()}</color>";

        return text;
    }

    private string GetWeaponTypeText() {
        switch (wpType) {
            case WeaponType.MainHand:
                return "主手";
            case WeaponType.OffHand:
                return "副手";
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public enum WeaponType {
        MainHand,
        OffHand
    }
}
public class Weapon : Item {
    public int Damage { get; set; }

    public WeaponType wpType { get; set; }

    public Weapon(int id, string name, ItemType type, ItemQuality quality, string description, int maxCapacity,
        int buyPrice, int sellPrice, string sprite, int damage, WeaponType wpType) : base(id, name, type, quality,
        description, maxCapacity, buyPrice, sellPrice, sprite) {
        Damage = damage;
        this.wpType = wpType;
    }

    public enum WeaponType {
        MainHand,
        OffHand
    }
}
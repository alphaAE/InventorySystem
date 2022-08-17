public class Consumable : Item {
    public int Hp { get; set; }
    public int Mp { get; set; }

    public Consumable(int id, string name, ItemType type, ItemQuality quality, string description, int maxCapacity,
        int buyPrice, int sellPrice, string sprite, int hp, int mp) : base(id, name, type, quality, description,
        maxCapacity, buyPrice, sellPrice, sprite) {
        Hp = hp;
        Mp = mp;
    }
}
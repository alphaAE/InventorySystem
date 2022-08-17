public class Material : Item {
    public Material(int id, string name, ItemType type, ItemQuality quality, string description, int maxCapacity,
        int buyPrice, int sellPrice, string sprite) : base(id, name, type, quality, description, maxCapacity, buyPrice,
        sellPrice, sprite) { }
}
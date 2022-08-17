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

    public enum EquipmentType {
        Head,
        Neck,
        Chest,
        Ring,
        Leg,
        Bracer,
        Boots,
        Trinket,
        Shoulder,
        Belt,
        OffHand,
    }
}
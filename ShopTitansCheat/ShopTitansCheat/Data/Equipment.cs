namespace ShopTitansCheat.Data
{
    public class Equipment
    {
        public int Count { get; set; }
        public bool Double { get; set; }
        public ItemQuality ItemQuality { get; set; }
        public string Name { get; set; }
        public bool Success { get; set; }

        public Equipment(string name, ItemQuality itemItemQuality, bool @double)
        {
            Name = name;
            ItemQuality = itemItemQuality;
            Double = @double;
        }
    }
}
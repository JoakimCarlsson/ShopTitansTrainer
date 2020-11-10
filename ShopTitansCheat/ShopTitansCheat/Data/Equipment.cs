namespace ShopTitansCheat.Data
{
    public class Equipment
    {
        public Equipment(string name, ItemQuality itemQuality, bool @double)
        {
            Name = name;
            ItemQuality = itemQuality;
            Double = @double;
        }

        public Equipment()
        {
            
        }

        public bool Double { get; set; }
        public ItemQuality ItemQuality { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }

    }
}
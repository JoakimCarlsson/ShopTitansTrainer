namespace ShopTitansCheat.Data
{
    public class Equipment
    {
        public Equipment(string shortName, ItemQuality itemQuality, bool @double)
        {
            ShortName = shortName;
            ItemQuality = itemQuality;
            Double = @double;
        }

        public Equipment()
        {
            
        }

        public bool Double { get; set; }
        public ItemQuality ItemQuality { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public bool Done { get; set; }


        public override string ToString()
        {
            return $"Name: {ShortName}, Quality: {ItemQuality}, Double: {Double},";
        }
    }
}
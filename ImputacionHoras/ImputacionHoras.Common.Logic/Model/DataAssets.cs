namespace ImputacionHoras.Common.Logic.Model
{
    public class DataAsset
    {
        public string Product { get; set; }
        public string Asset { get; set; }

        public DataAsset()
        {
        }

        public DataAsset(string product, string asset)
        {
            Product = product;
            Asset = asset;
        }
    }
}

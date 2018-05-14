namespace ImputacionHoras.Common.Logic.Model
{
    public class DataAssets
    {
        public string Product { get; set; }
        public string Asset { get; set; }

        public DataAssets()
        {
        }

        public DataAssets(string product, string asset)
        {
            Product = product;
            Asset = asset;
        }
    }
}

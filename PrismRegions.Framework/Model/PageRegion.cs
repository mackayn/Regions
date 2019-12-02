using System;

namespace PrismRegions.Framework.Model
{
    public class PageRegion
    {
        public PageRegion()
        {
            AutoResolve = true;
        }

        public Type RegionType { get; set; }
        public string RegionName { get; set; }
        public bool AutoResolve { get; set; }
    }
}

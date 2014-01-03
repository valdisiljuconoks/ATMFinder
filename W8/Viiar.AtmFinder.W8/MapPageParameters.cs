using Viiar.AtmFinder.W8.DataSources;

namespace Viiar.AtmFinder.W8
{
    public class MapPageParameters
    {
        public bool ShowSingleEntity { get; set; }
        public EntityDataItem Entity { get; set; }
        public bool ShowAllGroups { get; set; }
        public bool ShowTopItems { get; set; }
        public EntityDataGroup ShowGroup { get; set; }
        public bool ShowSearchResults { get; set; }
        public double SearchMatchLatitude { get; set; }
        public double SearchMatchLongitude { get; set; }
    }
}

namespace Viiar.AtmFinder.W8
{
    public class MapHelper
    {
        public static string CreateMiniMap(double latitude, double longitude, int zoomLevel, int width, int heigh, int iconStyle = 36)
        {
            return string.Format(
                                 "http://dev.virtualearth.net/REST/v1/Imagery/Map/Road/{0},{1}/{2}?mapSize={3},{4}&pushpin={0},{1};{5}&key={6}", 
                                 latitude, 
                                 longitude, 
                                 zoomLevel, 
                                 width, 
                                 heigh,
                                 iconStyle,
                                 Constants.BingMapsKey);
        }
    }
}

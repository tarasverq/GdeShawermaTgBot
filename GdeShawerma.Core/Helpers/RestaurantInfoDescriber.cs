using System.Globalization;
using System.Text;
using System.Web;
using GdeShawerma.Core.Api.Dtos;
using Location = Telegram.Bot.Types.Location;

namespace GdeShawerma.Core.Helpers;

internal class RestaurantInfoDescriber
{
    const char GoldStar = '⭐';

    private readonly Restaurant _restaurant;
    private readonly Location _userLocation;

    public RestaurantInfoDescriber(Restaurant restaurant) : this(restaurant, null)
    {
    }

    public RestaurantInfoDescriber(Restaurant restaurant, Location userLocation)
    {
        _restaurant = restaurant;
        _userLocation = userLocation;
    }

    public string Describe()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendFormat($"*{_restaurant.Name}*{Environment.NewLine}");
        
        sb.AppendLine();
        sb.AppendFormat($"[{_restaurant.Description}]({GetMapUrl(_restaurant)}){Environment.NewLine}");
        if (_userLocation is not null)
        {
            var distance = GetDistance(_userLocation.Longitude, _userLocation.Latitude,
                _restaurant.Location.Lng, _restaurant.Location.Lat);
            sb.AppendFormat($"Дистанция: {distance:F0} м.{Environment.NewLine}");
        }
        
        sb.AppendLine();
        sb.AppendFormat($"{new string(GoldStar, (int)Math.Round(_restaurant.Rate))}{Environment.NewLine}");
        sb.AppendFormat($"Оценок: {_restaurant.RatesCount}"); 

        if (_restaurant is RestaurantExtended extended)
        {
            sb.AppendFormat($"; Просмотров: {_restaurant.Visits}{Environment.NewLine}");
            if (extended.CommentsCount > 0)
                sb.AppendFormat($"Отзывов: {extended.CommentsCount}{Environment.NewLine}");

            if (!string.IsNullOrWhiteSpace(extended.WorkTime))
                sb.AppendFormat($"🕒: {extended.WorkTime}{Environment.NewLine}");

            if (!string.IsNullOrWhiteSpace(extended.Price))
                sb.AppendFormat($"💵: {extended.Price}₽{Environment.NewLine}");

            var services = GetServices(extended);
            if (services.Any())
            {
                sb.AppendLine();
                sb.AppendFormat($"*Сервисы в заведении:*{Environment.NewLine}");
                sb.AppendJoin(Environment.NewLine, services);
            }
        }


        return sb.ToString();
    }

    private ICollection<string> GetServices(RestaurantExtended restaurant)
    {
        List<string> result = new List<string>();
        if (restaurant.HasVisa)
            result.Add($"💳 Оплата по карте");

        if (restaurant.HasFalafel)
            result.Add($"🌿 Вегетарианские блюда");

        if (restaurant.HasBeer)
            result.Add($"🍺 Пиво");

        if (restaurant.HasHygiene)
            result.Add($"🤚 Гигиена на кухне");

        if (restaurant.HasWc)
            result.Add($"🚻 Туалет");

        if (restaurant.HasOnCoal)
            result.Add($"♨ блюда на огне");

        if (restaurant.HasDelivery)
            result.Add($"🚗 Доставка");

        return result;
    }

    private string GetMapUrl(Restaurant restaurant)
    {
        string query =
            HttpUtility.UrlEncode(
                $"{restaurant.Location.Lat.ToString(CultureInfo.InvariantCulture)}," +
                $"{restaurant.Location.Lng.ToString(CultureInfo.InvariantCulture)}");
        return $"https://www.google.com/maps/search/?api=1&query={query}";
    }
    
    private double GetDistance(double longitude, double latitude, double otherLongitude, double otherLatitude)
    {
        var d1 = latitude * (Math.PI / 180.0);
        var num1 = longitude * (Math.PI / 180.0);
        var d2 = otherLatitude * (Math.PI / 180.0);
        var num2 = otherLongitude * (Math.PI / 180.0) - num1;
        var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) + Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);
    
        return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
    }
}
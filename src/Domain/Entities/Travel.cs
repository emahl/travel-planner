using Domain.Enums;
using System.Globalization;

namespace Domain.Entities;

public class Travel
{
    private Travel() 
    {
        Type = TravelType.Unknown;
        Notes = string.Empty;
        DepartureLocation = string.Empty;
        ArrivalLocation = string.Empty;
    }

    public string Notes { get; set; }
    public TravelType Type { get; set; }

    private DateTime? _departureDate; 
    public DateTime? DepartureDate
    {
        get => _departureDate;
        set
        {
            _departureDate = value;
            if (ArrivalDate == null)
            {
                ArrivalDate = _departureDate;
            }
        }
    }
    public string DepartureLocation { get; set; }
    public DateTime? ArrivalDate { get; set; }
    public string ArrivalLocation { get; set; }

    private CultureInfo _culture = CultureInfo.CreateSpecificCulture("en-US");

    private bool IsSet => !string.IsNullOrEmpty(ArrivalLocation) && !string.IsNullOrEmpty(DepartureLocation)
        && ArrivalDate.HasValue && DepartureDate.HasValue;
    public string Title => IsSet ? $"{DepartureDate!.Value.ToString("dddd, d MMMM", _culture)} {DepartureLocation} - {ArrivalLocation}" : "?";

    public string ShortTitle => IsSet ? $"{DepartureDate!.Value.ToString("d MMMM yyyy", _culture)} {DepartureLocation} - {ArrivalLocation}" : "?";

    public static Travel Empty => new();
}

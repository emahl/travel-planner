using Domain.Enums;

namespace Domain.Entities;

public class Travel : BaseEntity
{
    public string? Notes { get; private set; }
    public TravelType Type { get; private set; }
    public DateTime? DepartureDate { get; private set; }
    public string? DepartureLocation { get; private set; }
    public DateTime? ArrivalDate { get; private set; }
    public string? ArrivalLocation { get; private set; }
}

using System;

namespace Midterm_App_Dev.Models;

// ============================================================================
// 6. ENCAPSULATION: Interactive Route Search, Trip Estimator & Advisory Models
// ============================================================================

/// <summary>
/// Encapsulates the user's route search parameters and enforces domain invariants
/// (e.g., origin and destination ports cannot be identical).
/// </summary>
public sealed class RouteSearchQuery
{
    private string _departurePort = "Cebu City (Pier 1 & 3)";
    private string _destinationPort = "Tagbilaran, Bohol";
    private string _vesselPreference = "All Vessel Classes";

    public string DeparturePort
    {
        get => _departurePort;
        set => _departurePort = string.IsNullOrWhiteSpace(value) ? _departurePort : value.Trim();
    }

    public string DestinationPort
    {
        get => _destinationPort;
        set => _destinationPort = string.IsNullOrWhiteSpace(value) ? _destinationPort : value.Trim();
    }

    public string VesselPreference
    {
        get => _vesselPreference;
        set => _vesselPreference = string.IsNullOrWhiteSpace(value) ? _vesselPreference : value.Trim();
    }

    public bool HasSameOriginAndDestination =>
        string.Equals(_departurePort, _destinationPort, StringComparison.OrdinalIgnoreCase);

    public void SwapPorts()
    {
        (_departurePort, _destinationPort) = (_destinationPort, _departurePort);
    }

    public bool TryValidate(out string? errorMessage)
    {
        if (HasSameOriginAndDestination)
        {
            errorMessage = "Departure and destination ports cannot be the same. Please select a different destination port.";
            return false;
        }

        errorMessage = null;
        return true;
    }
}

/// <summary>
/// Encapsulates passenger fare estimation parameters with strict real-world domain guards:
/// - Passenger count is clamped between 1 and 10 (prevents 0 or negative quantities).
/// - Travel date is guarded against past dates (cannot book a crossing in the past).
/// </summary>
public sealed class TripBookingRequest
{
    public const int MinPassengers = 1;
    public const int MaxPassengers = 10;

    private int _passengerCount = 1;
    private DateOnly _travelDate = DateOnly.FromDateTime(DateTime.Today);

    public int PassengerCount
    {
        get => _passengerCount;
        set => _passengerCount = Math.Clamp(value, MinPassengers, MaxPassengers);
    }

    public DateOnly TravelDate
    {
        get => _travelDate;
        set
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var maxFutureDate = today.AddMonths(6);
            if (value < today)
            {
                _travelDate = today;
                LastValidationNotice = "Past travel dates are not permitted. Reset to today's sailing date.";
            }
            else if (value > maxFutureDate)
            {
                _travelDate = maxFutureDate;
                LastValidationNotice = "Advance schedules are open up to 6 months ahead.";
            }
            else
            {
                _travelDate = value;
                LastValidationNotice = null;
            }
        }
    }

    public bool ApplyStatutoryDiscount { get; set; }

    public string? LastValidationNotice { get; private set; }

    public void IncrementPassengers()
    {
        if (_passengerCount >= MaxPassengers)
        {
            LastValidationNotice = $"Online fare estimator supports up to {MaxPassengers} passengers per booking. For group manifests, inquire at the pier counter.";
            return;
        }

        _passengerCount++;
        LastValidationNotice = null;
    }

    public void DecrementPassengers()
    {
        if (_passengerCount <= MinPassengers)
        {
            LastValidationNotice = "Passenger count cannot be less than 1.";
            return;
        }

        _passengerCount--;
        LastValidationNotice = null;
    }
}

public sealed record FareEstimateResult(
    int PassengerCount,
    decimal BaseFarePerPassenger,
    decimal BaseSubtotalPhp,
    decimal DiscountAmountPhp,
    decimal TerminalFeeTotalPhp,
    decimal GrandTotalPhp,
    string FormattedDate
);

public sealed record RouteCorridorOption(
    string RouteId,
    string ShortLabel,
    string VesselTag
);

public sealed class WeatherAdvisory : TransitEntityBase
{
    public string StatusBadge { get; }
    public string Summary { get; }
    public string RadarUrl { get; }

    public WeatherAdvisory(string id, string displayTitle, string statusBadge, string summary, string radarUrl)
        : base(id, displayTitle)
    {
        StatusBadge = statusBadge;
        Summary = summary;
        RadarUrl = radarUrl;
    }
}

public sealed class BoardingTip : TransitEntityBase
{
    public string Description { get; }
    public string ActionText { get; }
    public string ActionUrl { get; }

    public BoardingTip(string id, string displayTitle, string description, string actionText, string actionUrl)
        : base(id, displayTitle)
    {
        Description = description;
        ActionText = actionText;
        ActionUrl = actionUrl;
    }
}

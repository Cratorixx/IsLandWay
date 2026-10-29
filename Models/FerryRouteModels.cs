namespace Midterm_App_Dev.Models;

// ============================================================================
// 2. INHERITANCE & POLYMORPHISM: Ferry Route Hierarchy (Page 1)
// ============================================================================

/// <summary>
/// Abstract base model for an inter-island ferry passage.
/// Subclasses override polymorphic methods for vessel badges and fare labels.
/// </summary>
public abstract class FerryRouteBase : TransitEntityBase, IRoutePricing
{
    private decimal _minFarePhp;
    private decimal _maxFarePhp;

    public string OriginCity { get; protected set; }
    public string DestinationCity { get; protected set; }
    public string TerminalDetails { get; protected set; }
    public string CrossingDuration { get; protected set; }
    public string ImageUrl { get; protected set; }
    public string ImageAlt { get; protected set; }
    public VesselClassType VesselType { get; protected set; }

    public decimal MinFarePhp
    {
        get => _minFarePhp;
        protected set => _minFarePhp = value >= 0 ? value : 0;
    }

    public decimal MaxFarePhp
    {
        get => _maxFarePhp;
        protected set => _maxFarePhp = value >= _minFarePhp ? value : _minFarePhp;
    }

    protected FerryRouteBase(
        string id,
        string originCity,
        string destinationCity,
        string terminalDetails,
        string crossingDuration,
        decimal minFarePhp,
        decimal maxFarePhp,
        string imageUrl,
        string imageAlt,
        VesselClassType vesselType)
        : base(id, $"{originCity} ➔ {destinationCity}")
    {
        OriginCity = originCity;
        DestinationCity = destinationCity;
        TerminalDetails = terminalDetails;
        CrossingDuration = crossingDuration;
        MinFarePhp = minFarePhp;
        MaxFarePhp = maxFarePhp;
        ImageUrl = imageUrl;
        ImageAlt = imageAlt;
        VesselType = vesselType;
    }

    // Polymorphic members overridden by concrete route types
    public abstract string GetVesselBadgeText();
    public abstract string GetFareCategoryLabel();

    public virtual string GetFormattedFareRange() =>
        $"₱{MinFarePhp:N0} – ₱{MaxFarePhp:N0}";
}

/// <summary>
/// Concrete derived model representing high-speed catamaran/fastcraft routes.
/// </summary>
public sealed class FastcraftRoute : FerryRouteBase
{
    public FastcraftRoute(
        string id,
        string originCity,
        string destinationCity,
        string terminalDetails,
        string crossingDuration,
        decimal minFarePhp,
        decimal maxFarePhp,
        string imageUrl,
        string imageAlt)
        : base(
            id,
            originCity,
            destinationCity,
            terminalDetails,
            crossingDuration,
            minFarePhp,
            maxFarePhp,
            imageUrl,
            imageAlt,
            VesselClassType.Fastcraft)
    {
    }

    public override string GetVesselBadgeText() => "Fastcraft";
    public override string GetFareCategoryLabel() => "Regular tourist fare";
}

/// <summary>
/// Concrete derived model representing Roll-On/Roll-Off overnight cargo & sleeper routes.
/// </summary>
public sealed class RoRoOvernightRoute : FerryRouteBase
{
    public RoRoOvernightRoute(
        string id,
        string originCity,
        string destinationCity,
        string terminalDetails,
        string crossingDuration,
        decimal minFarePhp,
        decimal maxFarePhp,
        string imageUrl,
        string imageAlt)
        : base(
            id,
            originCity,
            destinationCity,
            terminalDetails,
            crossingDuration,
            minFarePhp,
            maxFarePhp,
            imageUrl,
            imageAlt,
            VesselClassType.RoRoOvernight)
    {
    }

    public override string GetVesselBadgeText() => "Ro-Ro";
    public override string GetFareCategoryLabel() => "Bunk & vehicle fare";
}

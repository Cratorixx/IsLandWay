using System.Collections.Generic;

namespace Midterm_App_Dev.Models;

// ============================================================================
// 3. INHERITANCE & POLYMORPHISM: Vessel Class Comparison Hierarchy (Page 1)
// ============================================================================

public sealed record VesselSpecRow(string IconClass, string Label, string Value);

/// <summary>
/// Abstract base class for maritime vessel classifications.
/// Encapsulates technical specifications and exposes polymorphic display behavior.
/// </summary>
public abstract class FerryVesselBase : TransitEntityBase
{
    public string Subtitle { get; protected set; }
    public string ImageUrl { get; protected set; }
    public string ImageAlt { get; protected set; }
    public string CruisingSpeed { get; protected set; }
    public string CabinSeating { get; protected set; }
    public string RoughWaterRide { get; protected set; }
    public string VehicleTransport { get; protected set; }

    protected FerryVesselBase(
        string id,
        string displayTitle,
        string subtitle,
        string imageUrl,
        string imageAlt,
        string cruisingSpeed,
        string cabinSeating,
        string roughWaterRide,
        string vehicleTransport)
        : base(id, displayTitle)
    {
        Subtitle = subtitle;
        ImageUrl = imageUrl;
        ImageAlt = imageAlt;
        CruisingSpeed = cruisingSpeed;
        CabinSeating = cabinSeating;
        RoughWaterRide = roughWaterRide;
        VehicleTransport = vehicleTransport;
    }

    public abstract string GetUsageBadgeText();
    public abstract string GetHeaderIconClass();
    public abstract IReadOnlyList<VesselSpecRow> GetSpecifications();
}

/// <summary>
/// Concrete derived vessel class for High-Speed Fastcraft (Catamarans & Hydrofoils).
/// </summary>
public sealed class FastcraftVessel : FerryVesselBase
{
    public FastcraftVessel(
        string id,
        string displayTitle,
        string subtitle,
        string imageUrl,
        string imageAlt,
        string cruisingSpeed,
        string cabinSeating,
        string roughWaterRide,
        string vehicleTransport)
        : base(
            id,
            displayTitle,
            subtitle,
            imageUrl,
            imageAlt,
            cruisingSpeed,
            cabinSeating,
            roughWaterRide,
            vehicleTransport)
    {
    }

    public override string GetUsageBadgeText() => "High-Speed Fastcraft";
    public override string GetHeaderIconClass() => "fa-solid fa-bolt";

    public override IReadOnlyList<VesselSpecRow> GetSpecifications() =>
    [
        new VesselSpecRow("fa-solid fa-gauge-high", "Speed", CruisingSpeed),
        new VesselSpecRow("fa-solid fa-ticket", "Seating", CabinSeating),
        new VesselSpecRow("fa-solid fa-water", "Sea State", RoughWaterRide),
        new VesselSpecRow("fa-solid fa-car-side", "Cargo", VehicleTransport)
    ];
}

/// <summary>
/// Concrete derived vessel class for Roll-On / Roll-Off (Ro-Ro) Multi-Deck Ferries.
/// </summary>
public sealed class RoRoVessel : FerryVesselBase
{
    public RoRoVessel(
        string id,
        string displayTitle,
        string subtitle,
        string imageUrl,
        string imageAlt,
        string cruisingSpeed,
        string cabinSeating,
        string roughWaterRide,
        string vehicleTransport)
        : base(
            id,
            displayTitle,
            subtitle,
            imageUrl,
            imageAlt,
            cruisingSpeed,
            cabinSeating,
            roughWaterRide,
            vehicleTransport)
    {
    }

    public override string GetUsageBadgeText() => "Roll-on / Roll-off";
    public override string GetHeaderIconClass() => "fa-solid fa-ship";

    public override IReadOnlyList<VesselSpecRow> GetSpecifications() =>
    [
        new VesselSpecRow("fa-solid fa-gauge", "Speed", CruisingSpeed),
        new VesselSpecRow("fa-solid fa-bed", "Seating", CabinSeating),
        new VesselSpecRow("fa-solid fa-water", "Sea State", RoughWaterRide),
        new VesselSpecRow("fa-solid fa-car-side", "Cargo", VehicleTransport)
    ];
}

using System.Collections.Generic;

namespace Midterm_App_Dev.Models;

// ============================================================================
// 4. INHERITANCE & POLYMORPHISM: Route Overview Timetable & Operators (Page 2)
// ============================================================================

/// <summary>
/// Abstract base class for a scheduled daily sailing on a specific passage.
/// Demonstrates Inheritance and Polymorphism for passenger vs. cargo Ro-Ro departures.
/// </summary>
public abstract class SailingScheduleBase : TransitEntityBase
{
    public string DepartureTime24h { get; protected set; }
    public string ArrivalTimeFormatted { get; protected set; }
    public string OperatorKey { get; protected set; }
    public string BerthRouteText { get; protected set; }
    public string AvailabilityStatus { get; protected set; }
    public string AvailabilityColorClass { get; protected set; }

    protected SailingScheduleBase(
        string id,
        string vesselName,
        string operatorKey,
        string departureTime24h,
        string arrivalTimeFormatted,
        string berthRouteText,
        string availabilityStatus,
        string availabilityColorClass)
        : base(id, vesselName)
    {
        OperatorKey = operatorKey;
        DepartureTime24h = departureTime24h;
        ArrivalTimeFormatted = arrivalTimeFormatted;
        BerthRouteText = berthRouteText;
        AvailabilityStatus = availabilityStatus;
        AvailabilityColorClass = availabilityColorClass;
    }

    public abstract string GetServiceBadgeText();
    public abstract string GetServiceBadgeCssClass();
    public abstract string GetFormattedPrice();
    public abstract decimal GetBasePassengerFarePhp();
    public abstract string GetActionButtonLabel();
    public abstract bool IsPrimaryActionStyle();
}

/// <summary>
/// Concrete derived class for standard high-speed passenger fastcraft sailings.
/// </summary>
public sealed class PassengerFastcraftSailing : SailingScheduleBase
{
    private decimal _ticketFarePhp;

    public decimal TicketFarePhp
    {
        get => _ticketFarePhp;
        private set => _ticketFarePhp = value >= 0 ? value : 0;
    }

    public PassengerFastcraftSailing(
        string id,
        string vesselName,
        string operatorKey,
        string departureTime24h,
        string arrivalTimeFormatted,
        string berthRouteText,
        decimal ticketFarePhp,
        string availabilityStatus,
        string availabilityColorClass)
        : base(
            id,
            vesselName,
            operatorKey,
            departureTime24h,
            arrivalTimeFormatted,
            berthRouteText,
            availabilityStatus,
            availabilityColorClass)
    {
        TicketFarePhp = ticketFarePhp;
    }

    public override string GetServiceBadgeText() => "Fastcraft";
    public override string GetServiceBadgeCssClass() => "bg-[#00F0D0]/15 text-[#00F0D0] border border-[#00F0D0]/30";
    public override string GetFormattedPrice() => $"₱{TicketFarePhp:N0}";
    public override decimal GetBasePassengerFarePhp() => TicketFarePhp;
    public override string GetActionButtonLabel() => "Book Seat";
    public override bool IsPrimaryActionStyle() => true;
}

/// <summary>
/// Concrete derived class for Roll-On / Roll-Off vehicle and cargo sailings.
/// </summary>
public sealed class CargoRoRoSailing : SailingScheduleBase
{
    public decimal MinPassFarePhp { get; }
    public decimal MaxVehicleFarePhp { get; }

    public CargoRoRoSailing(
        string id,
        string vesselName,
        string operatorKey,
        string departureTime24h,
        string arrivalTimeFormatted,
        string berthRouteText,
        decimal minPassFarePhp,
        decimal maxVehicleFarePhp,
        string availabilityStatus,
        string availabilityColorClass)
        : base(
            id,
            vesselName,
            operatorKey,
            departureTime24h,
            arrivalTimeFormatted,
            berthRouteText,
            availabilityStatus,
            availabilityColorClass)
    {
        MinPassFarePhp = minPassFarePhp >= 0 ? minPassFarePhp : 0;
        MaxVehicleFarePhp = maxVehicleFarePhp >= MinPassFarePhp ? maxVehicleFarePhp : MinPassFarePhp;
    }

    public override string GetServiceBadgeText() => "Vehicle & Cargo";
    public override string GetServiceBadgeCssClass() => "bg-amber-400/20 text-amber-300 border border-amber-300/30";
    public override string GetFormattedPrice() => $"₱{MinPassFarePhp:N0} – ₱{MaxVehicleFarePhp:N0}";
    public override decimal GetBasePassengerFarePhp() => MinPassFarePhp;
    public override string GetActionButtonLabel() => "Cargo Rates";
    public override bool IsPrimaryActionStyle() => false;
}

/// <summary>
/// Abstract base class for ferry operators serving a corridor.
/// </summary>
public abstract class FerryOperatorBase : TransitEntityBase
{
    public string OperatorBadgeName { get; protected set; }
    public string DailyTripsText { get; protected set; }
    public string Description { get; protected set; }
    public IReadOnlyList<string> Highlights { get; protected set; }
    public string StartingFareText { get; protected set; }
    public string DurationText { get; protected set; }

    protected FerryOperatorBase(
        string id,
        string operatorBadgeName,
        string vesselTypeHeading,
        string dailyTripsText,
        string description,
        IReadOnlyList<string> highlights,
        string startingFareText,
        string durationText)
        : base(id, vesselTypeHeading)
    {
        OperatorBadgeName = operatorBadgeName;
        DailyTripsText = dailyTripsText;
        Description = description;
        Highlights = highlights;
        StartingFareText = startingFareText;
        DurationText = durationText;
    }

    public abstract string GetBadgeThemeClasses();
    public abstract string GetFrequencyThemeClass();
    public abstract string GetDurationThemeClass();
}

public sealed class FastFerryOperator : FerryOperatorBase
{
    private readonly string _badgeThemeClasses;

    public FastFerryOperator(
        string id,
        string operatorBadgeName,
        string vesselTypeHeading,
        string dailyTripsText,
        string description,
        IReadOnlyList<string> highlights,
        string startingFareText,
        string durationText,
        string badgeThemeClasses)
        : base(
            id,
            operatorBadgeName,
            vesselTypeHeading,
            dailyTripsText,
            description,
            highlights,
            startingFareText,
            durationText)
    {
        _badgeThemeClasses = badgeThemeClasses;
    }

    public override string GetBadgeThemeClasses() => _badgeThemeClasses;
    public override string GetFrequencyThemeClass() => "text-white/60";
    public override string GetDurationThemeClass() => "text-[#00F0D0]";
}

public sealed class RoRoCargoOperator : FerryOperatorBase
{
    public RoRoCargoOperator(
        string id,
        string operatorBadgeName,
        string vesselTypeHeading,
        string dailyTripsText,
        string description,
        IReadOnlyList<string> highlights,
        string startingFareText,
        string durationText)
        : base(
            id,
            operatorBadgeName,
            vesselTypeHeading,
            dailyTripsText,
            description,
            highlights,
            startingFareText,
            durationText)
    {
    }

    public override string GetBadgeThemeClasses() => "text-amber-300 bg-amber-400/10 border border-amber-300/30";
    public override string GetFrequencyThemeClass() => "text-white/60";
    public override string GetDurationThemeClass() => "text-amber-300";
}

/// <summary>
/// Represents a numbered embarkation or arrival step at a port terminal.
/// </summary>
public sealed record TerminalProtocolStep(int StepNumber, string HighlightLabel, string InstructionText);

/// <summary>
/// Abstract base class for port terminal guides (Departure vs. Arrival).
/// </summary>
public abstract class TerminalPortGuideBase : TransitEntityBase
{
    public string PortLocationSubtitle { get; protected set; }
    public IReadOnlyList<TerminalProtocolStep> Steps { get; protected set; }

    protected TerminalPortGuideBase(
        string id,
        string terminalName,
        string portLocationSubtitle,
        IReadOnlyList<TerminalProtocolStep> steps)
        : base(id, terminalName)
    {
        PortLocationSubtitle = portLocationSubtitle;
        Steps = steps;
    }

    public abstract string GetTerminalRolePrefix();
    public abstract bool IsDepartureTerminal();
}

public sealed class DepartureTerminalGuide : TerminalPortGuideBase
{
    public DepartureTerminalGuide(
        string id,
        string terminalName,
        string portLocationSubtitle,
        IReadOnlyList<TerminalProtocolStep> steps)
        : base(id, terminalName, portLocationSubtitle, steps)
    {
    }

    public override string GetTerminalRolePrefix() => "Departure Terminal";
    public override bool IsDepartureTerminal() => true;
}

public sealed class ArrivalTerminalGuide : TerminalPortGuideBase
{
    public ArrivalTerminalGuide(
        string id,
        string terminalName,
        string portLocationSubtitle,
        IReadOnlyList<TerminalProtocolStep> steps)
        : base(id, terminalName, portLocationSubtitle, steps)
    {
    }

    public override string GetTerminalRolePrefix() => "Arrival Terminal";
    public override bool IsDepartureTerminal() => false;
}

/// <summary>
/// Encapsulates the Route Overview domain entity and state.
/// Search and filtering operations are delegated to ITransitService.
/// </summary>
public sealed class RouteOverviewDetail : TransitEntityBase
{
    private readonly string _initialOrigin;
    private readonly string _initialDestination;
    private readonly string _forwardBreadcrumb;
    private readonly string _reverseBreadcrumb;
    private readonly string _forwardTerminalArrival;
    private readonly string _reverseTerminalArrival;
    private bool _isReversed;

    public string CorridorLabel { get; }
    public string OriginName { get; private set; }
    public string DestinationName { get; private set; }
    public string BreadcrumbLabel { get; private set; }
    public string ReverseSwitchLabel { get; private set; }
    public string OperationalStatusText { get; }
    public string VerificationText { get; }
    public string AvgTransitTime { get; }
    public string DailyCrossingsText { get; }
    public string ServiceTypesText { get; }
    public string HeroImageUrl { get; }
    public string HeroImageAlt { get; }
    public string HeroBadgeText { get; }
    public string HeroTerminalArrivalText { get; private set; }
    public string HeroHeadline { get; }
    public string TouristFareRange { get; }
    public string FirstFerryTime { get; }
    public string LastFerryTime { get; }
    public string EditorialSummary { get; }
    public decimal TerminalFeePhp { get; }
    public int TotalDailyDepartures { get; }
    public IReadOnlyList<SailingScheduleBase> Sailings { get; }
    public IReadOnlyList<FerryOperatorBase> Operators { get; }
    public IReadOnlyList<TerminalPortGuideBase> TerminalGuides { get; }

    public RouteOverviewDetail(
        string id,
        string corridorLabel,
        string originName,
        string destinationName,
        string breadcrumbLabel,
        string reverseBreadcrumbLabel,
        string reverseSwitchLabel,
        string operationalStatusText,
        string verificationText,
        string avgTransitTime,
        string dailyCrossingsText,
        string serviceTypesText,
        string heroImageUrl,
        string heroImageAlt,
        string heroBadgeText,
        string heroTerminalArrivalText,
        string reverseTerminalArrivalText,
        string heroHeadline,
        string touristFareRange,
        string firstFerryTime,
        string lastFerryTime,
        string editorialSummary,
        decimal terminalFeePhp,
        int totalDailyDepartures,
        IReadOnlyList<SailingScheduleBase> sailings,
        IReadOnlyList<FerryOperatorBase> operators,
        IReadOnlyList<TerminalPortGuideBase> terminalGuides)
        : base(id, $"{originName} to {destinationName}")
    {
        _initialOrigin = originName;
        _initialDestination = destinationName;
        _forwardBreadcrumb = breadcrumbLabel;
        _reverseBreadcrumb = reverseBreadcrumbLabel;
        _forwardTerminalArrival = heroTerminalArrivalText;
        _reverseTerminalArrival = reverseTerminalArrivalText;

        CorridorLabel = corridorLabel;
        OriginName = originName;
        DestinationName = destinationName;
        BreadcrumbLabel = breadcrumbLabel;
        ReverseSwitchLabel = reverseSwitchLabel;
        OperationalStatusText = operationalStatusText;
        VerificationText = verificationText;
        AvgTransitTime = avgTransitTime;
        DailyCrossingsText = dailyCrossingsText;
        ServiceTypesText = serviceTypesText;
        HeroImageUrl = heroImageUrl;
        HeroImageAlt = heroImageAlt;
        HeroBadgeText = heroBadgeText;
        HeroTerminalArrivalText = heroTerminalArrivalText;
        HeroHeadline = heroHeadline;
        TouristFareRange = touristFareRange;
        FirstFerryTime = firstFerryTime;
        LastFerryTime = lastFerryTime;
        EditorialSummary = editorialSummary;
        TerminalFeePhp = terminalFeePhp >= 0 ? terminalFeePhp : 25m;
        TotalDailyDepartures = totalDailyDepartures;
        Sailings = sailings;
        Operators = operators;
        TerminalGuides = terminalGuides;
    }

    public void ToggleRouteDirection()
    {
        _isReversed = !_isReversed;
        if (_isReversed)
        {
            OriginName = _initialDestination;
            DestinationName = _initialOrigin;
            BreadcrumbLabel = _reverseBreadcrumb;
            ReverseSwitchLabel = $"Switch to {_forwardBreadcrumb.Replace("–", "→")}";
            HeroTerminalArrivalText = _reverseTerminalArrival;
        }
        else
        {
            OriginName = _initialOrigin;
            DestinationName = _initialDestination;
            BreadcrumbLabel = _forwardBreadcrumb;
            ReverseSwitchLabel = $"Switch to {_reverseBreadcrumb.Replace("–", "→")}";
            HeroTerminalArrivalText = _forwardTerminalArrival;
        }

        DisplayTitle = $"{OriginName} to {DestinationName}";
    }
}

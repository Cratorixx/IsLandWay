using System.Collections.Generic;
using System.Threading.Tasks;
using Midterm_App_Dev.Models;

namespace Midterm_App_Dev.Services;

public interface ITransitService
{
    Task<IReadOnlyList<FerryRouteBase>> GetTopRoutesAsync();
    Task<IReadOnlyList<FerryVesselBase>> GetVesselClassesAsync();
    Task<WeatherAdvisory> GetCurrentWeatherAdvisoryAsync();
    Task<BoardingTip> GetBoardingTipAsync();
    Task<RouteOverviewDetail> GetRouteOverviewAsync(string routeId = "cebu-bohol");
    Task<BoardingWalkthroughGuide> GetBoardingWalkthroughGuideAsync();

    IReadOnlyList<string> GetAvailablePorts();
    IReadOnlyList<string> GetVesselPreferences();
    IReadOnlyList<RouteCorridorOption> GetRouteCorridors();

    // Domain Filtering, Search Resolution & Calculation Services
    IReadOnlyList<FerryRouteBase> FilterRoutesByPreference(IReadOnlyList<FerryRouteBase> routes, string? vesselPreference);
    IReadOnlyList<SailingScheduleBase> FilterSailingsByOperator(RouteOverviewDetail? overview, string? operatorFilter);
    IReadOnlyList<string> GetOperatorFilterTabs(RouteOverviewDetail? overview);
    string ResolveRouteIdFromSearch(RouteSearchQuery query);
    PortWalkthroughSample? FindPortSampleById(BoardingWalkthroughGuide? guide, string portId);
    FareEstimateResult CalculateTripEstimate(SailingScheduleBase sailing, TripBookingRequest request, decimal terminalFeePerPax);
}

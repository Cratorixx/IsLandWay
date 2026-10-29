using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Midterm_App_Dev.Models;

namespace Midterm_App_Dev.Services;

public sealed class FakeTransitService : ITransitService
{
    private const decimal StatutoryDiscountRate = 0.20m;

    private static readonly IReadOnlyList<FerryRouteBase> _topRoutes =
    [
        new FastcraftRoute(
            id: "cebu-bohol",
            originCity: "Cebu",
            destinationCity: "Bohol",
            terminalDetails: "Pier 1 to Tagbilaran Port",
            crossingDuration: "2h",
            minFarePhp: 800m,
            maxFarePhp: 1200m,
            imageUrl: "https://lh3.googleusercontent.com/aida-public/AB6AXuBvyNTFtDbznYiltfQ8yVzCcHt6HhuZpnYIWzofSGfZ4MuM8ZFsJku9TqAkt2Gqy42PRHTcHolu6cNfe8F37g5GGwYx5y3PkkzMUJM8ifpWO8wMq_AaNz4J2B2ZhsVMHmvXpJ0nwWULsAhEC5bEFz2yXOUb5Xdp5excOlOyZQ16JihQhFrafPNjkveVpmV4x-sM0tSw07Tz63ez7xsdWyo1vrW7ubsWSiZQBhwtu-XqErjg2YGuDyJamQ",
            imageAlt: "Loboc river in Bohol Philippines"
        ),
        new FastcraftRoute(
            id: "cebu-ormoc",
            originCity: "Cebu",
            destinationCity: "Ormoc",
            terminalDetails: "Pier 1 & 3 to Ormoc Port Leyte",
            crossingDuration: "3h",
            minFarePhp: 1100m,
            maxFarePhp: 1450m,
            imageUrl: "https://lh3.googleusercontent.com/aida-public/AB6AXuDuEp3J91iBKMyebeoIyOtqYf7iJsU6-hRHwd4IZ3VBx_Kcs3ugd3ER7WK1mso_OTxC0qP42ipnan6Z673VN8eTiOK2Xzyovaf02C0pCIXlfoYFERoj49-y4VM1Jbi7g8jN3Ve6FrKzV7_UaV49sl-EEj_wIHUBfplem2Hhq6f96JmtNhqJZdkRbRPRQng9hESucYVI4N6Br6rGSgxNi75Xo4KFeu0jC97m_K1a5AqLhoArOhsd58gO3Q",
            imageAlt: "Ormoc Bay coastline Leyte"
        ),
        new FastcraftRoute(
            id: "dumaguete-siquijor",
            originCity: "Dumaguete",
            destinationCity: "Siquijor",
            terminalDetails: "Dumaguete to Larena / Siquijor Port",
            crossingDuration: "50m",
            minFarePhp: 250m,
            maxFarePhp: 400m,
            imageUrl: "https://lh3.googleusercontent.com/aida-public/AB6AXuAUz0YQqAMeLLR0tI2lFT6b1j9YJk3ZaKEYQ6S33VY1q7RsmYnfr7Xm74ygGR2uQGeOOSdZ_8gnzYV-Wq3yPuMaY3XZNE3airUaAEpP-OqN8RQL8YsDgFSxzzEg2c2jKpu9u09R3o4_xlB5lv_Gwi25lEsLj5yKdXaehnHZ8wkIKB9m0vt8hmqy0a_HtbxxxDw72YGlKdrFauXgbt-WE8jFF3K_qm7sFGtfn3edzoBKX3kMSdRnkU5IvQ",
            imageAlt: "Siquijor beach tropical turquoise water"
        ),
        new RoRoOvernightRoute(
            id: "batangas-caticlan",
            originCity: "Batangas",
            destinationCity: "Caticlan",
            terminalDetails: "Batangas Port to Caticlan Jetty",
            crossingDuration: "9h",
            minFarePhp: 1500m,
            maxFarePhp: 2900m,
            imageUrl: "https://lh3.googleusercontent.com/aida-public/AB6AXuDvzJkVTsYEJW90de1cW0BK9cTqjhAY3XUZQSVAru4cd5TQFXcL8pfWjcZoibknJbhcWrHeZnWoWYNC2OXtfnFlZaeLs7pd-hVFHUGkbJ7p5kd47jhySzggo2MPewMMyJxRDlGr1aPzKXa5iOcbsvpghj9g7kJcvejkKoXOTGeHY3ljEg0r03jvHGtXcP0oO5l2iVQjg2mt1TyRzex4KdaM9HeUKkITjDQBFxLTUvZNA2C8W9NSM8Y98Q",
            imageAlt: "Passenger overnight RoRo ferry at sunset"
        )
    ];

    private static readonly IReadOnlyList<FerryVesselBase> _vesselClasses =
    [
        new FastcraftVessel(
            id: "high-speed-fastcraft",
            displayTitle: "Catamaran / Hydrofoil",
            subtitle: "Twin-hull catamarans & hydrofoils",
            imageUrl: "https://lh3.googleusercontent.com/aida-public/AB6AXuCc0o7Xn7fqt--oM-WUYVzjXrs3-uBnQHma2q-kGvsNo-InQQL9PkPEhhgL3Wf2xDIoENDK7Pp3WlZDsbp47IEAZ9C-qYq_ZZPN2125c9b54xDnOXtebZGFIx5RtR3Ki59ZdXYIfPmEJjYBHxsLRWTjTayoq1Ptc-N4cSJx8HVAXt2MnxaxmdVati8V2RCrjwGUv52aMoJkR2O-e5ROOAvfNPq7CY51cky6x43F8Slp5Hvd908f2555Tg",
            imageAlt: "White passenger high speed catamaran ferry at sea",
            cruisingSpeed: "24–32 kts",
            cabinSeating: "Tourist AC / Econ",
            roughWaterRide: "Signal #1 Suspended",
            vehicleTransport: "Pax Only"
        ),
        new RoRoVessel(
            id: "roro-ferry",
            displayTitle: "Ro-Ro Ferry",
            subtitle: "Multi-deck vehicle & passenger vessels",
            imageUrl: "https://lh3.googleusercontent.com/aida-public/AB6AXuBTNnGk2P0lqgmnMVLo_wUxILpSQZPyALSF8andail8sszD0e3XZt3_vW77UnCWF8XI4QejYITHwY6MRfzhAJfY1ejugWHhDHPrHFpHvQMyapBNqtqx1H7VuDO7EzdgEZmbSCBDWDRt20u66IQWHkDjBEHG3852KJNEtjqxwYSUCmdGRxwv4o0M4qE1I2Za0_w8it_y2kqEzLU2lEt7Sz8bUGwJsdKgIW4Vm3DE-YUYaZ8aVbztIfJAXA",
            imageAlt: "Philippine roll on roll off passenger overnight ferry",
            cruisingSpeed: "12–16 kts",
            cabinSeating: "Aircon Bunks & Cabins",
            roughWaterRide: "Superior Swell Stability",
            vehicleTransport: "Vehicles & Trucks OK"
        )
    ];

    private static readonly WeatherAdvisory _weatherAdvisory = new(
        id: "pagasa-visayas-advisory",
        displayTitle: "PAGASA Marine Weather Advisory",
        statusBadge: "Normal Operations",
        summary: "Visayan sea lanes and Bohol Strait currently reporting calm to moderate seas (Wave heights: 0.8m – 1.4m). All scheduled fastcraft and Ro-Ro sailings operating normally.",
        radarUrl: "https://bagong.pagasa.dost.gov.ph/marine"
    );

    private static readonly BoardingTip _boardingTip = new(
        id: "fastcraft-luggage-tip",
        displayTitle: "Crucial boarding tip for fastcraft travelers",
        description: "Luggage above 15kg or larger than carry-on dimensions must be checked at the pier baggage counter 45 minutes before departure for a small auxiliary fee.",
        actionText: "Terminal rules guide",
        actionUrl: "/walkthrough"
    );

    private static readonly IReadOnlyList<string> _availablePorts =
    [
        "Cebu City (Pier 1 & 3)",
        "Tagbilaran, Bohol",
        "Ormoc Port, Leyte",
        "Dumaguete Port",
        "Siquijor / Larena Port",
        "Batangas Port",
        "Caticlan Jetty (Boracay)",
        "Iloilo Fastcraft Terminal",
        "Bacolod (BREDCO Port)"
    ];

    private static readonly IReadOnlyList<string> _vesselPreferences =
    [
        "All Vessel Classes",
        "Fastcraft / Catamaran",
        "Ro-Ro / Overnight"
    ];

    private static readonly IReadOnlyList<RouteCorridorOption> _routeCorridors =
    [
        new RouteCorridorOption("cebu-bohol", "Cebu – Bohol", "Fastcraft & Ro-Ro"),
        new RouteCorridorOption("cebu-ormoc", "Cebu – Ormoc", "Fastcraft & Ro-Ro"),
        new RouteCorridorOption("dumaguete-siquijor", "Dumaguete – Siquijor", "Short-Haul"),
        new RouteCorridorOption("batangas-caticlan", "Batangas – Caticlan", "Overnight Ro-Ro"),
        new RouteCorridorOption("iloilo-bacolod", "Iloilo – Bacolod", "Guimaras Strait")
    ];

    public Task<IReadOnlyList<FerryRouteBase>> GetTopRoutesAsync() =>
        Task.FromResult(_topRoutes);

    public Task<IReadOnlyList<FerryVesselBase>> GetVesselClassesAsync() =>
        Task.FromResult(_vesselClasses);

    public Task<WeatherAdvisory> GetCurrentWeatherAdvisoryAsync() =>
        Task.FromResult(_weatherAdvisory);

    public Task<BoardingTip> GetBoardingTipAsync() =>
        Task.FromResult(_boardingTip);

    public IReadOnlyList<string> GetAvailablePorts() => _availablePorts;

    public IReadOnlyList<string> GetVesselPreferences() => _vesselPreferences;

    public IReadOnlyList<RouteCorridorOption> GetRouteCorridors() => _routeCorridors;

    public IReadOnlyList<FerryRouteBase> FilterRoutesByPreference(IReadOnlyList<FerryRouteBase> routes, string? vesselPreference)
    {
        if (routes == null || routes.Count == 0 || string.IsNullOrWhiteSpace(vesselPreference) ||
            vesselPreference.Equals("All Vessel Classes", StringComparison.OrdinalIgnoreCase))
        {
            return routes ?? [];
        }

        if (vesselPreference.Contains("Fastcraft", StringComparison.OrdinalIgnoreCase))
        {
            return routes.Where(r => r.VesselType == VesselClassType.Fastcraft).ToList();
        }

        if (vesselPreference.Contains("Ro-Ro", StringComparison.OrdinalIgnoreCase))
        {
            return routes.Where(r => r.VesselType == VesselClassType.RoRoOvernight).ToList();
        }

        return routes;
    }

    public IReadOnlyList<SailingScheduleBase> FilterSailingsByOperator(RouteOverviewDetail? overview, string? operatorFilter)
    {
        if (overview == null)
        {
            return [];
        }

        if (string.IsNullOrWhiteSpace(operatorFilter) ||
            operatorFilter.Equals("All", StringComparison.OrdinalIgnoreCase))
        {
            return overview.Sailings;
        }

        return overview.Sailings
            .Where(s => s.OperatorKey.Equals(operatorFilter, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public IReadOnlyList<string> GetOperatorFilterTabs(RouteOverviewDetail? overview)
    {
        if (overview == null)
        {
            return ["All"];
        }

        var tabs = new List<string> { "All" };
        tabs.AddRange(overview.Sailings.Select(s => s.OperatorKey).Distinct(StringComparer.OrdinalIgnoreCase));
        return tabs;
    }

    public string ResolveRouteIdFromSearch(RouteSearchQuery query)
    {
        if (query == null)
        {
            return "cebu-bohol";
        }

        var combined = $"{query.DeparturePort} {query.DestinationPort}".ToLowerInvariant();

        if (combined.Contains("ormoc") || combined.Contains("leyte"))
        {
            return "cebu-ormoc";
        }

        if (combined.Contains("dumaguete") || combined.Contains("siquijor") || combined.Contains("larena"))
        {
            return "dumaguete-siquijor";
        }

        if (combined.Contains("batangas") || combined.Contains("caticlan") || combined.Contains("boracay"))
        {
            return "batangas-caticlan";
        }

        if (combined.Contains("iloilo") || combined.Contains("bacolod") || combined.Contains("bredco"))
        {
            return "iloilo-bacolod";
        }

        return "cebu-bohol";
    }

    public PortWalkthroughSample? FindPortSampleById(BoardingWalkthroughGuide? guide, string portId)
    {
        if (guide == null || string.IsNullOrWhiteSpace(portId))
        {
            return null;
        }

        return guide.PortSamples.FirstOrDefault(p => p.Id.Equals(portId, StringComparison.OrdinalIgnoreCase));
    }

    public FareEstimateResult CalculateTripEstimate(
        SailingScheduleBase sailing,
        TripBookingRequest request,
        decimal terminalFeePerPax)
    {
        var paxCount = Math.Clamp(request.PassengerCount, TripBookingRequest.MinPassengers, TripBookingRequest.MaxPassengers);
        var baseFare = sailing.GetBasePassengerFarePhp();
        var baseSubtotal = baseFare * paxCount;
        var discount = request.ApplyStatutoryDiscount ? Math.Round(baseSubtotal * StatutoryDiscountRate, 2) : 0m;
        var safeTerminalFee = terminalFeePerPax >= 0 ? terminalFeePerPax : 25m;
        var terminalFeeTotal = safeTerminalFee * paxCount;
        var grandTotal = (baseSubtotal - discount) + terminalFeeTotal;

        return new FareEstimateResult(
            PassengerCount: paxCount,
            BaseFarePerPassenger: baseFare,
            BaseSubtotalPhp: baseSubtotal,
            DiscountAmountPhp: discount,
            TerminalFeeTotalPhp: terminalFeeTotal,
            GrandTotalPhp: grandTotal,
            FormattedDate: request.TravelDate.ToString("MMM dd, yyyy")
        );
    }

    public Task<RouteOverviewDetail> GetRouteOverviewAsync(string routeId = "cebu-bohol")
    {
        var normalizedId = (routeId ?? "cebu-bohol").Trim().ToLowerInvariant();

        var overview = normalizedId switch
        {
            "cebu-ormoc" => BuildCebuOrmocOverview(),
            "dumaguete-siquijor" => BuildDumagueteSiquijorOverview(),
            "batangas-caticlan" => BuildBatangasCaticlanOverview(),
            "iloilo-bacolod" => BuildIloiloBacolodOverview(),
            _ => BuildCebuBoholOverview()
        };

        return Task.FromResult(overview);
    }

    private static RouteOverviewDetail BuildCebuBoholOverview() =>
        new(
            id: "cebu-bohol",
            corridorLabel: "POPULAR FASTCRAFT CORRIDOR",
            originName: "Cebu City",
            destinationName: "Tagbilaran, Bohol",
            breadcrumbLabel: "Cebu – Bohol",
            reverseBreadcrumbLabel: "Bohol – Cebu",
            reverseSwitchLabel: "Switch to Bohol → Cebu",
            operationalStatusText: "Normal Operations • Wave 0.8m",
            verificationText: "Verified today via PCG Central Visayas",
            avgTransitTime: "2 hrs 00 mins",
            dailyCrossingsText: "24 daily crossings",
            serviceTypesText: "Fastcraft & Ro-Ro available",
            heroImageUrl: "https://lh3.googleusercontent.com/aida/AEtjO1XNQACPVOtB_B7qUV1Yeg9W32_eK7ebw6ZSWca_QGP53I98Ua45i4IMLGtuOTuwu_5NekdKC3rQHJCsfGaY8Vc4rWAI6091ZNsB72T1S62S_CfnQ73mnYSMCeBxt2cMmhoYhbdQWJHSriC9v6r59sNq33lcdekdCrpy-uszzju6UHstvQaC-UOAU9DBrLHoaN-iunS05FZbiJ-aC_IrV4PW-oMlyrzr4OMhpduVZWobkWF5nRiliAyXnYhH",
            heroImageAlt: "Bohol Tagbilaran Coastline",
            heroBadgeText: "Bohol Strait Crossing",
            heroTerminalArrivalText: "Pier 1, Cebu → Tagbilaran Sea Port",
            reverseTerminalArrivalText: "Tagbilaran Sea Port → Pier 1, Cebu",
            heroHeadline: "The Gateway to Panglao & Chocolate Hills",
            touristFareRange: "₱800–₱1,200",
            firstFerryTime: "5:10 AM",
            lastFerryTime: "6:40 PM",
            editorialSummary: "Fastcraft catamarans operate every 45 to 60 minutes across the Bohol Strait. Passengers departing Cebu City Pier 1 should arrive 45 minutes prior for mandatory bag screening and Bohol terminal fee stamps (₱25).",
            terminalFeePhp: 25m,
            totalDailyDepartures: 24,
            sailings:
            [
                new PassengerFastcraftSailing(
                    id: "sailing-oceanjet-88",
                    vesselName: "OceanJet 88",
                    operatorKey: "OceanJet",
                    departureTime24h: "06:00",
                    arrivalTimeFormatted: "08:00 AM",
                    berthRouteText: "Cebu Pier 1 → Tagbilaran Main Berth",
                    ticketFarePhp: 800m,
                    availabilityStatus: "94 seats open",
                    availabilityColorClass: "text-emerald-400"
                ),
                new PassengerFastcraftSailing(
                    id: "sailing-supercat-caimito",
                    vesselName: "SuperCat St. Caimito",
                    operatorKey: "SuperCat",
                    departureTime24h: "08:15",
                    arrivalTimeFormatted: "10:15 AM",
                    berthRouteText: "Cebu Pier 1 → Tagbilaran Main Berth",
                    ticketFarePhp: 850m,
                    availabilityStatus: "12 seats left",
                    availabilityColorClass: "text-amber-300"
                ),
                new PassengerFastcraftSailing(
                    id: "sailing-oceanjet-168",
                    vesselName: "OceanJet 168",
                    operatorKey: "OceanJet",
                    departureTime24h: "09:20",
                    arrivalTimeFormatted: "11:20 AM",
                    berthRouteText: "Cebu Pier 1 → Tagbilaran Main Berth",
                    ticketFarePhp: 800m,
                    availabilityStatus: "Available",
                    availabilityColorClass: "text-emerald-400"
                ),
                new CargoRoRoSailing(
                    id: "sailing-lite-roro",
                    vesselName: "Lite Shipping Ro-Ro",
                    operatorKey: "Lite Ferries",
                    departureTime24h: "12:30",
                    arrivalTimeFormatted: "04:45 PM",
                    berthRouteText: "Cebu Pier 3 → Tagbilaran Ro-Ro Slip",
                    minPassFarePhp: 430m,
                    maxVehicleFarePhp: 2400m,
                    availabilityStatus: "Vehicle slots open",
                    availabilityColorClass: "text-white/60"
                )
            ],
            operators:
            [
                new FastFerryOperator(
                    id: "op-oceanjet",
                    operatorBadgeName: "OceanJet Fast Ferry",
                    vesselTypeHeading: "High-Speed Catamaran",
                    dailyTripsText: "14 trips/day",
                    description: "Twin-hull aluminum cruising at 26 knots. Indoor AC Tourist & Business Class.",
                    highlights:
                    [
                        "Free 15kg baggage allowance",
                        "Cebu Pier 1 fast boarding",
                        "Instant digital mobile QR"
                    ],
                    startingFareText: "From ₱800 / person",
                    durationText: "2h 00m",
                    badgeThemeClasses: "text-[#00F0D0] bg-[#00F0D0]/10 border border-[#00F0D0]/30"
                ),
                new FastFerryOperator(
                    id: "op-supercat",
                    operatorBadgeName: "SuperCat (2GO Group)",
                    vesselTypeHeading: "Monohull Fast Vessel",
                    dailyTripsText: "6 trips/day",
                    description: "Air-conditioned passenger cabins with cinema entertainment and kiosk.",
                    highlights:
                    [
                        "Free 15kg check-in per ticket",
                        "Pier 1 aircon waiting lounge",
                        "Senior & Student fare discounts"
                    ],
                    startingFareText: "From ₱850 / person",
                    durationText: "2h 05m",
                    badgeThemeClasses: "text-sky-300 bg-sky-400/10 border border-sky-400/30"
                ),
                new RoRoCargoOperator(
                    id: "op-lite-ferries",
                    operatorBadgeName: "Lite Ferries Ro-Ro",
                    vesselTypeHeading: "Roll-on / Roll-off Carrier",
                    dailyTripsText: "4 trips/day",
                    description: "Accommodates SUVs, cars, motorbikes, cargo trucks, and economy foot passengers.",
                    highlights:
                    [
                        "Vehicle drive-on ramp boarding",
                        "Pier 3 cargo & passenger gate",
                        "Budget economy bunk decks"
                    ],
                    startingFareText: "From ₱430 / pass",
                    durationText: "4h 15m"
                )
            ],
            terminalGuides:
            [
                new DepartureTerminalGuide(
                    id: "terminal-cebu-pier1",
                    terminalName: "Cebu Pier 1",
                    portLocationSubtitle: "Cebu Port Authority, Quezon Blvd, Cebu City",
                    steps:
                    [
                        new TerminalProtocolStep(1, "Terminal Fee:", "Pay ₱25 CPA passenger fee at Window 3 before security screening."),
                        new TerminalProtocolStep(2, "Baggage Drop:", "Check oversized bags over 15kg at the carrier counter (₱100/bag)."),
                        new TerminalProtocolStep(3, "Boarding Gate:", "Gates close 15 mins before sailing. Aircon waiting area available.")
                    ]
                ),
                new ArrivalTerminalGuide(
                    id: "terminal-tagbilaran-port",
                    terminalName: "Tagbilaran Port",
                    portLocationSubtitle: "Port Area, Tagbilaran City, Bohol",
                    steps:
                    [
                        new TerminalProtocolStep(1, "Panglao Transfer:", "Taxis (₱500–₱600) and tricycles (₱250) in official port loop."),
                        new TerminalProtocolStep(2, "Car Rentals:", "Pre-booked self-drive SUVs & tourist vans park at pickup zone."),
                        new TerminalProtocolStep(3, "Return Crossing:", "Weekend return trips to Cebu fill quickly; book return early.")
                    ]
                )
            ]
        );

    private static RouteOverviewDetail BuildCebuOrmocOverview() =>
        new(
            id: "cebu-ormoc",
            corridorLabel: "CAMOTES SEA EXPRESS CORRIDOR",
            originName: "Cebu City",
            destinationName: "Ormoc, Leyte",
            breadcrumbLabel: "Cebu – Ormoc",
            reverseBreadcrumbLabel: "Ormoc – Cebu",
            reverseSwitchLabel: "Switch to Ormoc → Cebu",
            operationalStatusText: "Normal Operations • Wave 1.1m",
            verificationText: "Verified today via PCG Eastern & Central Visayas",
            avgTransitTime: "2 hrs 55 mins",
            dailyCrossingsText: "10 daily crossings",
            serviceTypesText: "Fastcraft & Overnight Ro-Ro",
            heroImageUrl: "https://lh3.googleusercontent.com/aida-public/AB6AXuDuEp3J91iBKMyebeoIyOtqYf7iJsU6-hRHwd4IZ3VBx_Kcs3ugd3ER7WK1mso_OTxC0qP42ipnan6Z673VN8eTiOK2Xzyovaf02C0pCIXlfoYFERoj49-y4VM1Jbi7g8jN3Ve6FrKzV7_UaV49sl-EEj_wIHUBfplem2Hhq6f96JmtNhqJZdkRbRPRQng9hESucYVI4N6Br6rGSgxNi75Xo4KFeu0jC97m_K1a5AqLhoArOhsd58gO3Q",
            heroImageAlt: "Ormoc Bay Coastline Leyte",
            heroBadgeText: "Camotes Sea Crossing",
            heroTerminalArrivalText: "Pier 1 & 3, Cebu → Ormoc City Port",
            reverseTerminalArrivalText: "Ormoc City Port → Pier 1 & 3, Cebu",
            heroHeadline: "Primary Sea Link to Western Leyte & Lake Danao",
            touristFareRange: "₱1,100–₱1,450",
            firstFerryTime: "6:00 AM",
            lastFerryTime: "10:00 PM",
            editorialSummary: "High-speed catamarans connect Cebu Pier 1 to Ormoc Port in under 3 hours across the Camotes Sea, complemented by evening Ro-Ro cargo and sleeper vessels.",
            terminalFeePhp: 25m,
            totalDailyDepartures: 10,
            sailings:
            [
                new PassengerFastcraftSailing(
                    id: "sailing-ormoc-oceanjet-1",
                    vesselName: "OceanJet 12",
                    operatorKey: "OceanJet",
                    departureTime24h: "06:00",
                    arrivalTimeFormatted: "08:50 AM",
                    berthRouteText: "Cebu Pier 1 → Ormoc Fastcraft Berth",
                    ticketFarePhp: 1100m,
                    availabilityStatus: "68 seats open",
                    availabilityColorClass: "text-emerald-400"
                ),
                new PassengerFastcraftSailing(
                    id: "sailing-ormoc-supercat-1",
                    vesselName: "SuperCat St. Sariel",
                    operatorKey: "SuperCat",
                    departureTime24h: "10:30",
                    arrivalTimeFormatted: "01:30 PM",
                    berthRouteText: "Cebu Pier 1 → Ormoc Fastcraft Berth",
                    ticketFarePhp: 1150m,
                    availabilityStatus: "24 seats left",
                    availabilityColorClass: "text-amber-300"
                ),
                new PassengerFastcraftSailing(
                    id: "sailing-ormoc-oceanjet-2",
                    vesselName: "OceanJet 8",
                    operatorKey: "OceanJet",
                    departureTime24h: "15:00",
                    arrivalTimeFormatted: "05:50 PM",
                    berthRouteText: "Cebu Pier 1 → Ormoc Fastcraft Berth",
                    ticketFarePhp: 1100m,
                    availabilityStatus: "Available",
                    availabilityColorClass: "text-emerald-400"
                ),
                new CargoRoRoSailing(
                    id: "sailing-ormoc-roble",
                    vesselName: "Roble Shipping M/V Joyful",
                    operatorKey: "Roble Shipping",
                    departureTime24h: "21:30",
                    arrivalTimeFormatted: "03:30 AM",
                    berthRouteText: "Cebu Pier 3 → Ormoc Ro-Ro Ramp",
                    minPassFarePhp: 580m,
                    maxVehicleFarePhp: 3200m,
                    availabilityStatus: "Bunks & vehicle deck open",
                    availabilityColorClass: "text-white/60"
                )
            ],
            operators:
            [
                new FastFerryOperator(
                    id: "op-ormoc-oceanjet",
                    operatorBadgeName: "OceanJet Fast Ferry",
                    vesselTypeHeading: "Camotes Sea Catamaran",
                    dailyTripsText: "4 trips/day",
                    description: "Direct high-speed service between Cebu Pier 1 and Ormoc City plaza pier.",
                    highlights:
                    [
                        "2h 50m express crossing",
                        "Tourist AC & Business cabins",
                        "Walkable arrival to Ormoc centro"
                    ],
                    startingFareText: "From ₱1,100 / person",
                    durationText: "2h 50m",
                    badgeThemeClasses: "text-[#00F0D0] bg-[#00F0D0]/10 border border-[#00F0D0]/30"
                ),
                new FastFerryOperator(
                    id: "op-ormoc-supercat",
                    operatorBadgeName: "SuperCat Fast Ferry",
                    vesselTypeHeading: "Aircon Catamaran",
                    dailyTripsText: "4 trips/day",
                    description: "Spacious twin-hull passenger decks with onboard refreshment counter.",
                    highlights:
                    [
                        "Guaranteed numbered seating",
                        "15kg free hand-carry allowance",
                        "Online voucher check-in"
                    ],
                    startingFareText: "From ₱1,150 / person",
                    durationText: "3h 00m",
                    badgeThemeClasses: "text-sky-300 bg-sky-400/10 border border-sky-400/30"
                ),
                new RoRoCargoOperator(
                    id: "op-ormoc-roble",
                    operatorBadgeName: "Roble Shipping Lines",
                    vesselTypeHeading: "Overnight Ro-Ro & Sleeper",
                    dailyTripsText: "2 trips/day",
                    description: "Nightly passenger bunks and commercial rolling cargo across the Camotes Sea.",
                    highlights:
                    [
                        "Overnight aircon & economy bunks",
                        "Full vehicle & motorcycle ramp",
                        "generous 30kg baggage quota"
                    ],
                    startingFareText: "From ₱580 / pass",
                    durationText: "6h 00m"
                )
            ],
            terminalGuides:
            [
                new DepartureTerminalGuide(
                    id: "terminal-cebu-pier1-ormoc",
                    terminalName: "Cebu Pier 1 & Pier 3",
                    portLocationSubtitle: "Cebu Port Authority, Quezon Blvd, Cebu City",
                    steps:
                    [
                        new TerminalProtocolStep(1, "Pier Check:", "Fastcrafts depart Pier 1; Roble Ro-Ro vessels depart Pier 3."),
                        new TerminalProtocolStep(2, "Terminal Fee:", "Pay ₱25 CPA terminal fee and keep receipt attached to ticket."),
                        new TerminalProtocolStep(3, "Check-in Cutoff:", "Counter closes 30 mins before scheduled Camotes Sea departure.")
                    ]
                ),
                new ArrivalTerminalGuide(
                    id: "terminal-ormoc-port",
                    terminalName: "Ormoc City Port",
                    portLocationSubtitle: "Ormoc Bay Waterfront, Ormoc City, Leyte",
                    steps:
                    [
                        new TerminalProtocolStep(1, "Town Plaza Exit:", "Ormoc Port opens directly onto the waterfront park and food strip."),
                        new TerminalProtocolStep(2, "Tacloban Vans:", "Grand Terminal v-hires to Tacloban (₱220) wait 250m from the pier gate."),
                        new TerminalProtocolStep(3, "Lake Danao Trips:", "Chartered habal-habal and tricycles available at the registered stand.")
                    ]
                )
            ]
        );

    private static RouteOverviewDetail BuildDumagueteSiquijorOverview() =>
        new(
            id: "dumaguete-siquijor",
            corridorLabel: "SHORT-HAUL ISLAND HOPPER",
            originName: "Dumaguete",
            destinationName: "Siquijor Island",
            breadcrumbLabel: "Dumaguete – Siquijor",
            reverseBreadcrumbLabel: "Siquijor – Dumaguete",
            reverseSwitchLabel: "Switch to Siquijor → Dumaguete",
            operationalStatusText: "Normal Operations • Calm 0.6m",
            verificationText: "Verified today via PCG Negros Oriental",
            avgTransitTime: "50 mins",
            dailyCrossingsText: "16 daily crossings",
            serviceTypesText: "Fastcraft & Short-Haul Ro-Ro",
            heroImageUrl: "https://lh3.googleusercontent.com/aida-public/AB6AXuAUz0YQqAMeLLR0tI2lFT6b1j9YJk3ZaKEYQ6S33VY1q7RsmYnfr7Xm74ygGR2uQGeOOSdZ_8gnzYV-Wq3yPuMaY3XZNE3airUaAEpP-OqN8RQL8YsDgFSxzzEg2c2jKpu9u09R3o4_xlB5lv_Gwi25lEsLj5yKdXaehnHZ8wkIKB9m0vt8hmqy0a_HtbxxxDw72YGlKdrFauXgbt-WE8jFF3K_qm7sFGtfn3edzoBKX3kMSdRnkU5IvQ",
            heroImageAlt: "Turquoise waters of Siquijor Port",
            heroBadgeText: "Mindanao Sea Short-Haul",
            heroTerminalArrivalText: "Dumaguete Port → Siquijor / Larena Pier",
            reverseTerminalArrivalText: "Siquijor / Larena Pier → Dumaguete Port",
            heroHeadline: "Quick Island Hop to Cambugahay Falls & Paliton Beach",
            touristFareRange: "₱250–₱400",
            firstFerryTime: "5:30 AM",
            lastFerryTime: "6:00 PM",
            editorialSummary: "The shortest inter-island hop in the Central Visayas. Fastcrafts cross in 50 minutes while Montenegro Ro-Ro ferries carry scooters and cars in 85 minutes.",
            terminalFeePhp: 15m,
            totalDailyDepartures: 16,
            sailings:
            [
                new PassengerFastcraftSailing(
                    id: "sailing-dgt-oceanjet-1",
                    vesselName: "OceanJet 288",
                    operatorKey: "OceanJet",
                    departureTime24h: "07:20",
                    arrivalTimeFormatted: "08:10 AM",
                    berthRouteText: "Dumaguete Pier → Siquijor Town Pier",
                    ticketFarePhp: 380m,
                    availabilityStatus: "52 seats open",
                    availabilityColorClass: "text-emerald-400"
                ),
                new CargoRoRoSailing(
                    id: "sailing-dgt-montenegro-1",
                    vesselName: "Montenegro M/V Maria Matilde",
                    operatorKey: "Montenegro Lines",
                    departureTime24h: "10:00",
                    arrivalTimeFormatted: "11:25 AM",
                    berthRouteText: "Dumaguete Ro-Ro Ramp → Siquijor Pier",
                    minPassFarePhp: 250m,
                    maxVehicleFarePhp: 1350m,
                    availabilityStatus: "Scooter & car slots open",
                    availabilityColorClass: "text-emerald-400"
                ),
                new PassengerFastcraftSailing(
                    id: "sailing-dgt-oceanjet-2",
                    vesselName: "OceanJet 288",
                    operatorKey: "OceanJet",
                    departureTime24h: "13:30",
                    arrivalTimeFormatted: "02:20 PM",
                    berthRouteText: "Dumaguete Pier → Siquijor Town Pier",
                    ticketFarePhp: 380m,
                    availabilityStatus: "18 seats left",
                    availabilityColorClass: "text-amber-300"
                ),
                new CargoRoRoSailing(
                    id: "sailing-dgt-aleson",
                    vesselName: "Aleson M/V Antonia",
                    operatorKey: "Aleson Shipping",
                    departureTime24h: "16:00",
                    arrivalTimeFormatted: "05:20 PM",
                    berthRouteText: "Dumaguete Pier → Larena Port, Siquijor",
                    minPassFarePhp: 230m,
                    maxVehicleFarePhp: 1250m,
                    availabilityStatus: "Open deck available",
                    availabilityColorClass: "text-white/60"
                )
            ],
            operators:
            [
                new FastFerryOperator(
                    id: "op-dgt-oceanjet",
                    operatorBadgeName: "OceanJet Fast Ferry",
                    vesselTypeHeading: "50-Min Island Express",
                    dailyTripsText: "6 trips/day",
                    description: "Fastest crossing directly into Siquijor town pier with air-conditioned seating.",
                    highlights:
                    [
                        "50-minute crossing time",
                        "Arrives at Siquijor town pier",
                        "Online advance seat booking"
                    ],
                    startingFareText: "From ₱380 / person",
                    durationText: "50 mins",
                    badgeThemeClasses: "text-[#00F0D0] bg-[#00F0D0]/10 border border-[#00F0D0]/30"
                ),
                new RoRoCargoOperator(
                    id: "op-dgt-montenegro",
                    operatorBadgeName: "Montenegro Shipping",
                    vesselTypeHeading: "Short-Haul Ro-Ro Ferry",
                    dailyTripsText: "6 trips/day",
                    description: "Popular choice for bringing rented motorbikes or cars across to Siquijor.",
                    highlights:
                    [
                        "Motorbike & car roll-on deck",
                        "Open-air breeze & AC lounges",
                        "Budget ₱250 passenger ticket"
                    ],
                    startingFareText: "From ₱250 / pass",
                    durationText: "1h 25m"
                ),
                new FastFerryOperator(
                    id: "op-dgt-aleson",
                    operatorBadgeName: "Aleson Shipping",
                    vesselTypeHeading: "Larena & Siquijor Shuttle",
                    dailyTripsText: "4 trips/day",
                    description: "Reliable daily crossings serving both Siquijor town and Larena port.",
                    highlights:
                    [
                        "Lowest base commuter fare",
                        "Serves Larena & Siquijor piers",
                        "Walk-up ticket booths at gate"
                    ],
                    startingFareText: "From ₱230 / person",
                    durationText: "1h 20m",
                    badgeThemeClasses: "text-sky-300 bg-sky-400/10 border border-sky-400/30"
                )
            ],
            terminalGuides:
            [
                new DepartureTerminalGuide(
                    id: "terminal-dumaguete-port",
                    terminalName: "Port of Dumaguete",
                    portLocationSubtitle: "Looc Port Road, end of Rizal Blvd, Dumaguete",
                    steps:
                    [
                        new TerminalProtocolStep(1, "PPA Terminal Fee:", "Pay ₱15 terminal fee at the outer PPA window before entering the gate."),
                        new TerminalProtocolStep(2, "Port Verification:", "Verify whether your ticket lands at Siquijor Town Pier or Larena Pier."),
                        new TerminalProtocolStep(3, "Scooter Manifest:", "Motorbikes require OR/CR presentation at the Coast Guard desk 1 hour early.")
                    ]
                ),
                new ArrivalTerminalGuide(
                    id: "terminal-siquijor-pier",
                    terminalName: "Siquijor Town Pier",
                    portLocationSubtitle: "Poblacion, Siquijor Island",
                    steps:
                    [
                        new TerminalProtocolStep(1, "Eco-Tourism Fee:", "Pay ₱20 provincial environmental fee at the welcome kiosk on the pier."),
                        new TerminalProtocolStep(2, "Scooter Rentals:", "Licensed scooter rental shops line the port exit (₱350–₱500/day)."),
                        new TerminalProtocolStep(3, "San Juan Transfers:", "Tricycles to San Juan beach strip cost ₱250–₱300 per ride.")
                    ]
                )
            ]
        );

    private static RouteOverviewDetail BuildBatangasCaticlanOverview() =>
        new(
            id: "batangas-caticlan",
            corridorLabel: "LUZON TO PANAY OVERNIGHT SEA LANE",
            originName: "Batangas Port",
            destinationName: "Caticlan (Boracay)",
            breadcrumbLabel: "Batangas – Caticlan",
            reverseBreadcrumbLabel: "Caticlan – Batangas",
            reverseSwitchLabel: "Switch to Caticlan → Batangas",
            operationalStatusText: "Normal Operations • Wave 1.2m",
            verificationText: "Verified today via PCG Southern Tagalog",
            avgTransitTime: "9 hrs 00 mins",
            dailyCrossingsText: "6 daily crossings",
            serviceTypesText: "Overnight Sleeper & Ro-Ro",
            heroImageUrl: "https://lh3.googleusercontent.com/aida-public/AB6AXuDvzJkVTsYEJW90de1cW0BK9cTqjhAY3XUZQSVAru4cd5TQFXcL8pfWjcZoibknJbhcWrHeZnWoWYNC2OXtfnFlZaeLs7pd-hVFHUGkbJ7p5kd47jhySzggo2MPewMMyJxRDlGr1aPzKXa5iOcbsvpghj9g7kJcvejkKoXOTGeHY3ljEg0r03jvHGtXcP0oO5l2iVQjg2mt1TyRzex4KdaM9HeUKkITjDQBFxLTUvZNA2C8W9NSM8Y98Q",
            heroImageAlt: "Overnight passenger Ro-Ro vessel sailing toward Caticlan",
            heroBadgeText: "Tablas Strait Overnight Passage",
            heroTerminalArrivalText: "Batangas PTB → Caticlan Jetty Port",
            reverseTerminalArrivalText: "Caticlan Jetty Port → Batangas PTB",
            heroHeadline: "Overnight Cruise from Southern Luzon to Boracay",
            touristFareRange: "₱1,500–₱2,900",
            firstFerryTime: "8:00 AM",
            lastFerryTime: "9:00 PM",
            editorialSummary: "Multi-deck Ro-Ro vessels sail across the Verde Island Passage and Tablas Strait directly to Caticlan Jetty Port—just 10 minutes from Boracay Island.",
            terminalFeePhp: 30m,
            totalDailyDepartures: 6,
            sailings:
            [
                new CargoRoRoSailing(
                    id: "sailing-btg-2go-morning",
                    vesselName: "2GO M/V Masikap",
                    operatorKey: "2GO Travel",
                    departureTime24h: "09:00",
                    arrivalTimeFormatted: "06:00 PM",
                    berthRouteText: "Batangas Terminal 3 → Caticlan Main Berth",
                    minPassFarePhp: 1550m,
                    maxVehicleFarePhp: 6500m,
                    availabilityStatus: "Tourist bunks open",
                    availabilityColorClass: "text-emerald-400"
                ),
                new CargoRoRoSailing(
                    id: "sailing-btg-starlite-day",
                    vesselName: "Starlite M/V Stella Del Mar",
                    operatorKey: "Starlite Ferries",
                    departureTime24h: "14:00",
                    arrivalTimeFormatted: "11:00 PM",
                    berthRouteText: "Batangas Ro-Ro Berth → Caticlan Jetty",
                    minPassFarePhp: 1500m,
                    maxVehicleFarePhp: 5800m,
                    availabilityStatus: "Reclining & bunks open",
                    availabilityColorClass: "text-emerald-400"
                ),
                new CargoRoRoSailing(
                    id: "sailing-btg-2go-night",
                    vesselName: "2GO M/V St. Michael",
                    operatorKey: "2GO Travel",
                    departureTime24h: "21:00",
                    arrivalTimeFormatted: "06:00 AM (+1)",
                    berthRouteText: "Batangas Terminal 3 → Caticlan Main Berth",
                    minPassFarePhp: 1750m,
                    maxVehicleFarePhp: 6800m,
                    availabilityStatus: "9 cabin beds left",
                    availabilityColorClass: "text-amber-300"
                )
            ],
            operators:
            [
                new FastFerryOperator(
                    id: "op-btg-2go",
                    operatorBadgeName: "2GO Travel",
                    vesselTypeHeading: "Cruise-Style Ro-Ro Liner",
                    dailyTripsText: "2 trips/day",
                    description: "Hotel-style staterooms, tourist aircon bunks, dining salon, and full vehicle deck.",
                    highlights:
                    [
                        "50kg generous baggage allowance",
                        "Hot meal inclusion on select classes",
                        "Wake up at Caticlan at 6:00 AM"
                    ],
                    startingFareText: "From ₱1,550 / person",
                    durationText: "9h 00m",
                    badgeThemeClasses: "text-[#00F0D0] bg-[#00F0D0]/10 border border-[#00F0D0]/30"
                ),
                new RoRoCargoOperator(
                    id: "op-btg-starlite",
                    operatorBadgeName: "Starlite Ferries",
                    vesselTypeHeading: "Modern Roll-On Roll-Off",
                    dailyTripsText: "4 trips/day",
                    description: "Purpose-built Ro-Ro catamarans and monohulls serving Batangas and Caticlan.",
                    highlights:
                    [
                        "Private family cabin upgrades",
                        "Seamless SUV & van transport",
                        "Direct Caticlan Jetty berthing"
                    ],
                    startingFareText: "From ₱1,500 / pass",
                    durationText: "9h 00m"
                )
            ],
            terminalGuides:
            [
                new DepartureTerminalGuide(
                    id: "terminal-batangas-ptb",
                    terminalName: "Batangas Port PTB",
                    portLocationSubtitle: "Santa Clara, Batangas City (STAR Tollway Exit)",
                    steps:
                    [
                        new TerminalProtocolStep(1, "Unified PTB Hall:", "Enter Passenger Terminal Building Gate 2 for X-ray and ₱30 terminal pass."),
                        new TerminalProtocolStep(2, "Early Arrival:", "Overnight Ro-Ro boarding begins 2 hours prior to departure."),
                        new TerminalProtocolStep(3, "Rolling Cargo:", "Vehicles must clear Coast Guard manifest inspection 3 hours prior.")
                    ]
                ),
                new ArrivalTerminalGuide(
                    id: "terminal-caticlan-jetty",
                    terminalName: "Caticlan Jetty Port",
                    portLocationSubtitle: "Malay, Aklan (Boracay Transfer Hub)",
                    steps:
                    [
                        new TerminalProtocolStep(1, "Boracay Transfer:", "Walk 3 minutes to the Boracay e-trike and bangka boat verification hall."),
                        new TerminalProtocolStep(2, "Mandatory Fees:", "Prepare hotel voucher, environmental fee (₱150/local), and jetty fee."),
                        new TerminalProtocolStep(3, "Kalibo / Roxas Buses:", "Ceres buses to Kalibo and Iloilo depart right outside the port compound.")
                    ]
                )
            ]
        );

    private static RouteOverviewDetail BuildIloiloBacolodOverview() =>
        new(
            id: "iloilo-bacolod",
            corridorLabel: "GUIMARAS STRAIT COMMUTER LANE",
            originName: "Iloilo City",
            destinationName: "Bacolod (BREDCO)",
            breadcrumbLabel: "Iloilo – Bacolod",
            reverseBreadcrumbLabel: "Bacolod – Iloilo",
            reverseSwitchLabel: "Switch to Bacolod → Iloilo",
            operationalStatusText: "Normal Operations • Wave 0.7m",
            verificationText: "Verified today via PCG Western Visayas",
            avgTransitTime: "1 hr 05 mins",
            dailyCrossingsText: "20 daily crossings",
            serviceTypesText: "Fastcraft & Ro-Ro Catamaran",
            heroImageUrl: "https://lh3.googleusercontent.com/aida-public/AB6AXuCc0o7Xn7fqt--oM-WUYVzjXrs3-uBnQHma2q-kGvsNo-InQQL9PkPEhhgL3Wf2xDIoENDK7Pp3WlZDsbp47IEAZ9C-qYq_ZZPN2125c9b54xDnOXtebZGFIx5RtR3Ki59ZdXYIfPmEJjYBHxsLRWTjTayoq1Ptc-N4cSJx8HVAXt2MnxaxmdVati8V2RCrjwGUv52aMoJkR2O-e5ROOAvfNPq7CY51cky6x43F8Slp5Hvd908f2555Tg",
            heroImageAlt: "High speed ferry crossing the Guimaras Strait",
            heroBadgeText: "Guimaras Strait Express",
            heroTerminalArrivalText: "Iloilo Fastcraft Pier → Bacolod BREDCO Port",
            reverseTerminalArrivalText: "Bacolod BREDCO Port → Iloilo Fastcraft Pier",
            heroHeadline: "Rapid Panay-to-Negros Commuter & Business Link",
            touristFareRange: "₱550–₱750",
            firstFerryTime: "5:45 AM",
            lastFerryTime: "5:30 PM",
            editorialSummary: "Connecting the regional capitals of Panay and Negros across the Guimaras Strait in just 60 minutes via Lapuz Wharf and BREDCO Reclamation Port.",
            terminalFeePhp: 30m,
            totalDailyDepartures: 20,
            sailings:
            [
                new PassengerFastcraftSailing(
                    id: "sailing-ilo-oceanjet-1",
                    vesselName: "OceanJet 15",
                    operatorKey: "OceanJet",
                    departureTime24h: "06:30",
                    arrivalTimeFormatted: "07:35 AM",
                    berthRouteText: "Iloilo Lapuz Terminal → Bacolod BREDCO",
                    ticketFarePhp: 600m,
                    availabilityStatus: "80 seats open",
                    availabilityColorClass: "text-emerald-400"
                ),
                new PassengerFastcraftSailing(
                    id: "sailing-ilo-weesam-1",
                    vesselName: "Weesam Express 7",
                    operatorKey: "Weesam",
                    departureTime24h: "09:15",
                    arrivalTimeFormatted: "10:25 AM",
                    berthRouteText: "Iloilo Lapuz Terminal → Bacolod BREDCO",
                    ticketFarePhp: 550m,
                    availabilityStatus: "Available",
                    availabilityColorClass: "text-emerald-400"
                ),
                new CargoRoRoSailing(
                    id: "sailing-ilo-fastcat",
                    vesselName: "FastCat M14",
                    operatorKey: "FastCat",
                    departureTime24h: "13:00",
                    arrivalTimeFormatted: "02:45 PM",
                    berthRouteText: "Dumangas Ro-Ro Port → Bacolod BREDCO",
                    minPassFarePhp: 320m,
                    maxVehicleFarePhp: 1950m,
                    availabilityStatus: "Vehicle deck open",
                    availabilityColorClass: "text-white/60"
                )
            ],
            operators:
            [
                new FastFerryOperator(
                    id: "op-ilo-oceanjet",
                    operatorBadgeName: "OceanJet Fast Ferry",
                    vesselTypeHeading: "60-Min Strait Catamaran",
                    dailyTripsText: "8 trips/day",
                    description: "High-frequency commuter catamarans between Iloilo City Proper and Bacolod SM area.",
                    highlights:
                    [
                        "1-hour city-center to city-center",
                        "Business & Tourist AC decks",
                        "Frequent morning & afternoon departures"
                    ],
                    startingFareText: "From ₱600 / person",
                    durationText: "1h 05m",
                    badgeThemeClasses: "text-[#00F0D0] bg-[#00F0D0]/10 border border-[#00F0D0]/30"
                ),
                new FastFerryOperator(
                    id: "op-ilo-weesam",
                    operatorBadgeName: "Weesam Express",
                    vesselTypeHeading: "Commuter Fastcraft",
                    dailyTripsText: "6 trips/day",
                    description: "Value fastcraft service favored by students and daily inter-island commuters.",
                    highlights:
                    [
                        "Competitive economy & tourist fares",
                        "Direct BREDCO pier berthing",
                        "20% Student & Senior discounts"
                    ],
                    startingFareText: "From ₱550 / person",
                    durationText: "1h 10m",
                    badgeThemeClasses: "text-sky-300 bg-sky-400/10 border border-sky-400/30"
                ),
                new RoRoCargoOperator(
                    id: "op-ilo-fastcat",
                    operatorBadgeName: "FastCat Ro-Ro",
                    vesselTypeHeading: "Catamaran Ro-Ro Carrier",
                    dailyTripsText: "6 trips/day",
                    description: "Twin-hull roll-on/roll-off ferry carrying cars, trucks, and passengers across Guimaras Strait.",
                    highlights:
                    [
                        "Fastest vehicle Ro-Ro crossing",
                        "Aircon, Premium, and Open decks",
                        "Smooth twin-hull stability"
                    ],
                    startingFareText: "From ₱320 / pass",
                    durationText: "1h 45m"
                )
            ],
            terminalGuides:
            [
                new DepartureTerminalGuide(
                    id: "terminal-iloilo-lapuz",
                    terminalName: "Iloilo Fastcraft Terminal",
                    portLocationSubtitle: "Progreso, Lapuz, Iloilo City",
                    steps:
                    [
                        new TerminalProtocolStep(1, "Terminal Fee:", "Pay ₱30 terminal fee at the main glass lobby before X-ray screening."),
                        new TerminalProtocolStep(2, "Check-In Window:", "Present valid ID for boarding pass seat number assignment."),
                        new TerminalProtocolStep(3, "Ro-Ro Note:", "Bringing a car? Proceed to Dumangas Port (30 mins north) for Ro-Ro ramps.")
                    ]
                ),
                new ArrivalTerminalGuide(
                    id: "terminal-bacolod-bredco",
                    terminalName: "Bacolod BREDCO Port",
                    portLocationSubtitle: "Reclamation Area, Bacolod City, Negros Occidental",
                    steps:
                    [
                        new TerminalProtocolStep(1, "SM City Proximity:", "BREDCO Port is a 5-minute taxi or grab ride from SM City Bacolod."),
                        new TerminalProtocolStep(2, "Silay Airport:", "Shuttle vans and metered taxis queue outside the main arrival gate."),
                        new TerminalProtocolStep(3, "Return Tickets:", "Friday afternoon sailings sell out fast; secure return seats upon arrival.")
                    ]
                )
            ]
        );

    public Task<BoardingWalkthroughGuide> GetBoardingWalkthroughGuideAsync()
    {
        var guide = new BoardingWalkthroughGuide(
            id: "pier-to-pier-walkthrough",
            displayTitle: "How to Board an Inter-Island Ferry: Pier-to-Pier Walkthrough",
            eyebrowCategory: "FASTCRAFT / FERRIES / PROCEDURES",
            subtitleDescription: "From the first security queue to your ride off the pier: know what to show, where to pay, and what happens next—before you set sail.",
            heroImageUrl: "https://lh3.googleusercontent.com/aida/AEtjO1W1TwaeZu95qtgJV9Gh01l7jFbnaudr41yDmynAEYcgIMgJNf3IuyDxXgj86FpzvOANW0T5DQKFudX0wcY4bv0_0UItPT12RFGutkEH_ZhWpOQIQvRRF50wvcADqHq_XlPNi1Fe0HrJKz298n7TW4SOA4Yp8rC9idsmj0cMe8N9_ErSTwh7fzsu_K2vz61rsJleZJmbjgYD4lQ1ea1aXJkhLZ9a3ACtvetK2dVbStj3fSWTPeJVrIeyZhGb",
            heroImageAlt: "Passengers boarding inter-island ferry gangway at Philippine tropical pier",
            heroImageBadgeCaption: "Cebu Pier 1 Fastcraft Gangway",
            heroImageSubCaption: "Be ready on the dock. Clear regular checks with confidence and enjoy smooth departures.",
            goldenPortRuleText: "Keep your ID, boarding pass, terminal fee receipt, and luggage stubs together until you leave the arrival pier.",
            quickChips:
            [
                new WalkthroughQuickChip("alarm", "Arrive 45m early"),
                new WalkthroughQuickChip("payments", "Bring small cash for fees"),
                new WalkthroughQuickChip("badge", "Valid ID & voucher ready")
            ],
            portSamples:
            [
                new PortWalkthroughSample(
                    Id: "cebu-pier-1",
                    PortSelectorLabel: "Cebu Pier 1 / CPBO",
                    RouteSummaryTitle: "Pier 1 to Tagbilaran / Tubigon",
                    CrossingDurationTag: "• 1h 45m fastcraft crossing",
                    AdvisoryNote: "A sample pier for this walkthrough. Exact gate order, fees and baggage rules apply across port hubs: follow signs and staff announcements.",
                    GangwayBadgeCaption: "Cebu Pier 1 Fastcraft Gangway"
                ),
                new PortWalkthroughSample(
                    Id: "dumaguete-port",
                    PortSelectorLabel: "Dumaguete Port / DGT",
                    RouteSummaryTitle: "Dumaguete to Siquijor / Larena",
                    CrossingDurationTag: "• 50m fastcraft crossing",
                    AdvisoryNote: "Pay PPA terminal fee (₱15–₱25) at the entrance booth before entering the main passenger pre-departure hall.",
                    GangwayBadgeCaption: "Port of Dumaguete Boarding Ramp"
                ),
                new PortWalkthroughSample(
                    Id: "batangas-pier",
                    PortSelectorLabel: "Batangas Pier / BTG",
                    RouteSummaryTitle: "Batangas Terminal 3 to Caticlan / Calapan",
                    CrossingDurationTag: "• Ro-Ro & fastcraft hub",
                    AdvisoryNote: "Unified automated terminal fee pass issued after initial baggage screening at Passenger Terminal Building.",
                    GangwayBadgeCaption: "Batangas PTB Terminal 3 Gangway"
                )
            ],
            steps:
            [
                new StandardTipWalkthroughStep(
                    id: "step-01-security",
                    stepNumberCode: "01",
                    shortPillLabel: "Security",
                    materialIconName: "shield",
                    locationEyebrow: "AT THE TERMINAL ENTRANCE",
                    stepTitle: "Terminal Entrance & X-Ray Security",
                    leadDescription: "Join the passenger entrance queue, not the vehicular lanes. Keep your ticket or booking voucher and original government ID within reach.",
                    bullets:
                    [
                        new WalkthroughBulletItem("At the scanner:", "Place bags on the belt, collect loose items before moving on. Be ready for a manual bag check."),
                        new WalkthroughBulletItem("Do not pack:", "Firearms, explosives, flammable gas canisters (fuel, butane), raw unprocessed pork, knives, or unmanifested large items without cargo tickets.")
                    ],
                    calloutEyebrow: "BEFORE YOU ENTER",
                    calloutTitle: "One easy-access travel pouch",
                    calloutParagraphs:
                    [
                        "Keep your ID, voucher, and small cash together. Save an offline copy of your booking mobile receipt in case reception is patchy."
                    ],
                    calloutFooterNote: "Ensure name on ticket matches ID accurately."
                ),
                new ChecklistWalkthroughStep(
                    id: "step-02-checkin",
                    stepNumberCode: "02",
                    shortPillLabel: "Check-in",
                    materialIconName: "confirmation_number",
                    locationEyebrow: "AT REGISTRATION COUNTER",
                    stepTitle: "Check-In & Seat Numbering",
                    leadDescription: "Find the counter for the operator of your booking (OceanJet, SuperCat, Lite Ferries). Show your voucher and ID; where required, exchange the voucher for a physical boarding pass.",
                    bullets:
                    [
                        new WalkthroughBulletItem("Before you walk away:", "Check your name, destination, sailing time, cabin class, and seat number; a voucher alone may not get you through the gate."),
                        new WalkthroughBulletItem("First come, first served:", "Confirm the boarding gate and cutoff. If no seat is printed, ask whether seats are assigned or open seating.")
                    ],
                    calloutEyebrow: "VALIDATION CHECK",
                    calloutTitle: "Check these three details",
                    validationItems:
                    [
                        new WalkthroughBulletItem("Destination:", "exact port"),
                        new WalkthroughBulletItem("Seat / row:", "as assigned by operator"),
                        new WalkthroughBulletItem("Gate:", "reconfirm on the departure board")
                    ],
                    calloutFooterNote: "Keep the ticket and ID handy. Roughly ten minutes ahead, lines form for seat verification."
                ),
                new FeeHighlightWalkthroughStep(
                    id: "step-03-terminal-fee",
                    stepNumberCode: "03",
                    shortPillLabel: "Terminal fee",
                    materialIconName: "receipt_long",
                    locationEyebrow: "AT TERMINAL FEE COUNTER",
                    stepTitle: "Terminal Fee Booth",
                    leadDescription: "Check whether your fare already includes the terminal fee. If it does not, pay at the marked booth and ask for an official receipt.",
                    bullets:
                    [
                        new WalkthroughBulletItem("Keep the papers together:", "Clip the receipt to your ticket or slip it inside your passport cover so you never misplace it on walking gates."),
                        new WalkthroughBulletItem("Before paying again:", "Inquire if the fee is already rolled into your ticket. If environmental or municipal fees apply separately, clarify where each amount goes.")
                    ],
                    calloutEyebrow: "MANDATORY CASH FEE (OFFICIAL DESK)",
                    feeRangeText: "₱25 – ₱50",
                    feeUnitText: "/ adult passenger",
                    feeBodyText: "Cash only. Small bills help lines move quickly. Exact payment avoids delays and keeps lines moving swiftly.",
                    calloutFooterNote: "A separate environmental pass may apply depending on port and destination."
                ),
                new StandardTipWalkthroughStep(
                    id: "step-04-luggage",
                    stepNumberCode: "04",
                    shortPillLabel: "Luggage",
                    materialIconName: "luggage",
                    locationEyebrow: "AT BAGGAGE DROP & SCALE",
                    stepTitle: "Checked Luggage & Porter Guidelines",
                    leadDescription: "Check oversize luggage at the baggage counter. Have weight taken at the operator's desk. Tag each bag and hold its claim stub until disembarkation.",
                    bullets:
                    [
                        new WalkthroughBulletItem("Keep with you:", "Carry ID, medication, electronics, power banks, and valuables. Ask about excess weight charges before handing over a bag."),
                        new WalkthroughBulletItem("If you hire a porter:", "Choose a uniformed, badged official porter. Agree the price before handing over a bag, and note luggage tags right away.")
                    ],
                    calloutEyebrow: "ALLOWABLE BAGGAGE & PORTERS",
                    calloutTitle: "7 kg carry-on • 15 kg checked",
                    calloutParagraphs:
                    [
                        "Standard included allowance per passenger—varies by operator/vessel class.",
                        "Uniformed port porters: ₱50–₱100 per bag for standard pier-to-vessel transfer. Extra cargo fees for surfboards or folding bikes."
                    ],
                    calloutFooterNote: "Always receive official stamped baggage stubs before walking to the gate.",
                    calloutTitleSizeClass: "text-[17px]"
                ),
                new StandardTipWalkthroughStep(
                    id: "step-05-boarding",
                    stepNumberCode: "05",
                    shortPillLabel: "Board & arrive",
                    materialIconName: "directions_boat",
                    locationEyebrow: "TO THE GATE / ON THE JETTY",
                    stepTitle: "Gate Boarding & Disembarkation",
                    leadDescription: "Wait in the numbered gate and listen for your operator, vessel and destination—not just schedule times. Show your ticket and ID when called.",
                    bullets:
                    [
                        new WalkthroughBulletItem("On the gangway:", "Follow crew directions, hold handrails, and locate your assigned seat. Note the safety briefing and life vest positions."),
                        new WalkthroughBulletItem("When you arrive:", "Stay seated until lines secure for disembarkation. Match your baggage stub, then follow exit signs to the official transport area.")
                    ],
                    calloutEyebrow: "ARRIVAL SAFETY",
                    calloutTitle: "Walk past the port touts",
                    calloutParagraphs:
                    [
                        "Do not hand your luggage or ticket to anyone promising an \"organized ride\". Use the marked terminal exits to reach desks and registered queues for clear pricing."
                    ],
                    calloutFooterNote: "Keep ticket stub and ID safe until you step out of the arrival port gate."
                )
            ],
            lastMileOptions:
            [
                new LocalTealTransitOption(
                    id: "lastmile-tricycles",
                    vehicleTitle: "Tricycles",
                    categoryBadgeText: "LOCAL SHORT-HAUL",
                    materialIconName: "two_wheeler",
                    fareRangeText: "₱100–150",
                    fareUnitText: "per tricycle / fare split",
                    description: "Ideal for 1-2 passengers with light bags traveling 2–5km to town center. Fast, open-air, and omnipresent outside terminal gates.",
                    dispatchAdviceText: "Use the authorized tricycle stand outside the gate. Agree total price first; ignore aggressive touts."
                ),
                new ShuttleBlueTransitOption(
                    id: "lastmile-vans",
                    vehicleTitle: "Vans",
                    categoryBadgeText: "RESORT/AIRPORT TRANSIT",
                    materialIconName: "airport_shuttle",
                    fareRangeText: "₱150–250",
                    fareUnitText: "per passenger / shared van",
                    description: "Perfect for longer trips (15–30km) to beach resorts (e.g. Panglao). Comfortable air-conditioned cabins with rear luggage space.",
                    dispatchAdviceText: "Line up at the dispatch kiosk for shared services. A private charter is worth ₱800–₱1,200 without middleman markups."
                ),
                new RegionalGreenTransitOption(
                    id: "lastmile-buses",
                    vehicleTitle: "Public Buses",
                    categoryBadgeText: "CHEAPEST REGIONAL RIDE",
                    materialIconName: "directions_bus",
                    fareRangeText: "₱30–60",
                    fareUnitText: "per passenger / route sector",
                    description: "Regional route connecting provincial towns along national highways. Reliable, ultra-cheap option for backpackers without excessive luggage.",
                    dispatchAdviceText: "Look for the signed terminal depot beyond the port. Conductors issue physical tickets with fare formulas."
                )
            ]
        );

        return Task.FromResult(guide);
    }
}

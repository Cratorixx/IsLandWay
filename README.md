# Islandway — Philippine Inter-Island Ferry Transit Guide

**Islandway** is a modern maritime transit guide and route planning web application built with **ASP.NET Core 8 Blazor (`InteractiveServer`)** and **Tailwind CSS v3.4**. It provides domestic travelers and inter-island commuters with verified route schedules, operator comparisons, interactive fare estimation, and a step-by-step pier boarding walkthrough across major Visayas and Luzon sea lanes.

---

## 🧭 Application Pages & User Flows

| Page | Route(s) | Key Features & Intuition |
| :--- | :--- | :--- |
| **1. Home & Route Finder** | `/` | Interactive Origin/Destination Port Finder with port swap, real-time vessel class filtering (`All`, `Fastcraft`, `Ro-Ro`), 4 featured sea lane cards, and a 2×2 vessel specification comparison grid. |
| **2. Route Overview & Timetable** | `/routes`, `/routes/{RouteId}`, `/cebu-bohol` | Multi-corridor switcher (`Cebu–Bohol`, `Cebu–Ormoc`, `Dumaguete–Siquijor`, `Batangas–Caticlan`, `Iloilo–Bacolod`), bidirectional route toggle (`Origin ⇄ Destination`), operator timetable filtering, **Interactive Trip Fare Estimator**, carrier breakdown, and departure/arrival terminal guides. |
| **3. Pier-to-Pier Walkthrough** | `/walkthrough`, `/guide`, `/travel-guide` | Interactive Departing Port selector (`Cebu Pier 1`, `Dumaguete Port`, `Batangas Pier`), 5-step boarding checklist (`Security`, `Check-in`, `Terminal Fee`, `Luggage`, `Boarding`), Golden Port Rule banner, and arrival last-mile transit directory (`Tricycles`, `Vans`, `Public Buses`). |

---

## 🏗️ Project Architecture & Folder Structure

The codebase strictly separates **Domain Models**, **Business/Filtering Services**, and **Presentation Components**:

```text
Midterm_App_Dev/
├── Components/
│   ├── Layout/                         # Application shell & navigation
│   │   ├── MainLayout.razor            # Root layout with ambient oceanic glows
│   │   ├── NavBar.razor                # Sticky desktop & mobile navigation bar
│   │   └── Footer.razor                # PAGASA weather banner & corridor links
│   ├── Pages/                          # Routable page views
│   │   ├── Home.razor                  # Page 1: Route Finder & Vessel Comparison
│   │   ├── RouteOverview.razor         # Page 2: Corridor Timetables & Fare Estimator
│   │   ├── BoardingWalkthrough.razor   # Page 3: 5-Step Pier-to-Pier Boarding Guide
│   │   └── Error.razor                 # Glassmorphic error boundary view
│   └── Shared/                         # Reusable UI components
│       ├── RouteFinderBar.razor        # Interactive port & vessel search bar
│       ├── RouteCard.razor             # Sea lane summary card
│       ├── VesselComparisonCard.razor  # Polymorphic vessel specification card
│       ├── SailingScheduleRow.razor    # Timetable departure row
│       ├── OperatorCard.razor          # Ferry operator & class card
│       ├── TerminalGuideCard.razor     # Departure/Arrival terminal protocol card
│       ├── WalkthroughStepCard.razor   # Polymorphic 5-step boarding card
│       └── LastMileVehicleCard.razor   # Arrival port transport card
├── Models/                             # Pure OOP Frontend Domain Models
│   ├── TransitEntityBase.cs            # Core interfaces & abstract base entity
│   ├── FerryRouteModels.cs             # FastcraftRoute & RoRoOvernightRoute
│   ├── VesselClassModels.cs            # FastcraftVessel & RoRoVessel
│   ├── RouteOverviewModels.cs          # Sailings, Operators, Terminals & Overview
│   ├── BoardingWalkthroughModels.cs    # Walkthrough Steps & Last-Mile Transit
│   └── TransitSearchAndBookingModels.cs# Search query & booking validation models
├── Services/                           # Business Logic, Filtering & Calculations
│   ├── ITransitService.cs              # Service contract registered via DI
│   └── FakeTransitService.cs           # Route filtering, search matching & fare math
├── Styles/
│   └── tailwind.input.css              # Tailwind source & frosted-glass utilities
└── wwwroot/css/
    └── tailwind.output.css             # Compiled Tailwind CSS bundle
```

---

## 🛡️ Real-World Domain Logic & Validation

1. **Origin vs. Destination Port Guard (`RouteSearchQuery` & `RouteFinderBar.razor`)**:
   - Prevents users from selecting the exact same port for both Departure and Destination (e.g., sailing from Cebu City to Cebu City is blocked with a clear validation alert).
2. **Non-Past Sailing Date Validation (`TripBookingRequest`)**:
   - The Interactive Trip Fare Estimator enforces `TravelDate >= DateTime.Today` (up to 6 months ahead), preventing users from booking or estimating ferry crossings in the past.
3. **Positive Passenger Quantity Clamping (`TripBookingRequest`)**:
   - Passenger count is strictly clamped between `1` and `10` (`Math.Clamp(value, 1, 10)`), preventing `0` or negative passenger quantities.
4. **Philippine Maritime Fare & Statutory Discount Calculation (`FakeTransitService.CalculateTripEstimate`)**:
   - Computes base passenger fare × passenger count, applies the mandatory 20% statutory discount for Students, Senior Citizens, and PWDs when toggled, and adds the port-specific CPA/PPA terminal fee (`₱15–₱30` per passenger).
5. **Service-Layer Filtering & Search Resolution (`FakeTransitService`)**:
   - All filtering algorithms (`FilterRoutesByPreference`, `FilterSailingsByOperator`, `ResolveRouteIdFromSearch`, `FindPortSampleById`) reside strictly inside `Services/FakeTransitService.cs` rather than inside model classes or Razor markup.

---

## 🧩 Object-Oriented Programming (OOP) Principles in `Models/`

- **Abstraction**: `ITransitEntity`, `IRoutePricing`, `TransitEntityBase`, `FerryRouteBase`, `FerryVesselBase`, `SailingScheduleBase`, `FerryOperatorBase`, `TerminalPortGuideBase`, `WalkthroughStepBase`, and `LastMileTransitBase`.
- **Encapsulation**: Private backing fields with validation guards (e.g., non-empty IDs/titles in `TransitEntityBase`, non-negative fares in `FerryRouteBase`, clamped passenger counts and date bounds in `TripBookingRequest`, and bidirectional state toggling in `RouteOverviewDetail`).
- **Inheritance**: Specialized subclasses inherit shared state from abstract base classes (`FastcraftRoute` / `RoRoOvernightRoute`, `PassengerFastcraftSailing` / `CargoRoRoSailing`, `StandardTipWalkthroughStep` / `ChecklistWalkthroughStep` / `FeeHighlightWalkthroughStep`).
- **Polymorphism**: UI components invoke overridden virtual/abstract methods (`GetVesselBadgeText()`, `GetSpecifications()`, `GetBasePassengerFarePhp()`, `GetCalloutContainerClasses()`, `GetCategoryBadgeClasses()`) without tight coupling to concrete types.

---

## 🚀 Running the Project Locally

```powershell
# 1. Compile Tailwind CSS & build the .NET 8 project
dotnet build

# 2. Start the server
dotnet run
```

Then open `http://localhost:5104` in your browser.

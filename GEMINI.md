# Midterm App Dev - Project Guidelines & UI Design System

This project is configured with modern UI/UX design standards adapted from top industry design lead skills (`frontend-design` and `baseline-ui`). All components and UI implementations must strictly adhere to the following rules:

---

## 📌 Project Reference & Tech Stack (`App Dev` Architecture)

- **Base Architecture Reference**: `C:\Users\L23Y16W12\Desktop\App Dev`
- **Tech Stack**:
  - **Framework**: ASP.NET Core 8 Blazor Web App (`InteractiveServer`)
  - **Styles & Build**: Local Tailwind CSS v3.4.17 build pipeline (`package.json`, `tailwind.config.js`, `Styles/tailwind.input.css` -> `wwwroot/css/tailwind.output.css`) triggered automatically on `dotnet build`.
  - **Component Structure**:
    - `Components/Layout/` (`MainLayout.razor`, `NavBar.razor`, `Footer.razor`)
    - `Components/Pages/` (`Home.razor`, `RouteOverview.razor`, `BoardingWalkthrough.razor`, `Error.razor`)
    - `Components/Shared/` (`RouteFinderBar.razor`, `RouteCard.razor`, `VesselComparisonCard.razor`, `SailingScheduleRow.razor`, `OperatorCard.razor`, `TerminalGuideCard.razor`, `WalkthroughStepCard.razor`, `LastMileVehicleCard.razor`)
  - **Frontend OOP Domain Models & Services**:
    - `Models/` (`TransitEntityBase.cs`, `FerryRouteModels.cs`, `VesselClassModels.cs`, `RouteOverviewModels.cs`, `BoardingWalkthroughModels.cs`, `TransitSearchAndBookingModels.cs`): Strictly implements Object-Oriented Programming (OOP) principles (**Abstraction**, **Encapsulation**, **Inheritance**, **Polymorphism**) and real-world domain invariants.
    - `Services/ITransitService.cs` & `Services/FakeTransitService.cs`: Registered via Dependency Injection in `Program.cs`. Encapsulates all search, filtering, route resolution, and fare calculation algorithms.

---

## 🎨 Islandway Design System & Theme Tokens

### 1. Brand Color Palette
- **Primary Brand Teal**: `#00A896` (`brand-teal`), Hover: `#009282`, Dark Teal: `#007D72` (`brand-darkTeal`), Light Tint: `#E8F6F3` (`brand-tealLight`)
- **Maritime Navy**: `#0A2540` (`brand-navy`), Deep Navy: `#071F36` (`brand-deepNavy`), Panel: `#153B51` (`brand-navyPanel`), Panel Border: `#315468` (`brand-navyBorder`)
- **Surfaces & Neutrals**:
  - Background Light: `#F8F9FA` (`brand-bgLight`)
  - Section Light Blue: `#EDF3F7` (`brand-lightBlue`)
  - Muted Slate Text: `#607080` (`brand-slate`)
  - Border Token: `#DFE6EA` (`brand-border`)
  - Muted Ice (Footer Text): `#BBCBD7` (`brand-mutedIce`)
  - Mint Accent: `#7FE0D3` (`brand-mintAccent`)

### 2. Typography & Icons
- **Font Family**: `Inter`, `-apple-system`, `sans-serif`
- **Icons**: Font Awesome 6.4.0 (`fa-solid`, `fa-regular`)
- Enforce `text-balance` on headings and `tabular-nums` on fares, durations, and numeric metrics.

---

## ⚡ UI Engineering Constraints (Anti-Slop Guidelines)

1. **Motion & Performance**:
   - Animate strictly on `transform` and `opacity` (hardware accelerated).
   - Micro-interaction transitions must stay between 150ms and 200ms (`duration-150` or `duration-200`).
2. **Accessibility**:
   - Provide clear focus rings (`focus-visible:ring-2 focus-visible:ring-[#00A896]`).
   - All icon-only buttons must have descriptive `aria-label` attributes.
3. **Layout**:
   - Always use `min-h-dvh` instead of `min-h-screen`.
   - Prevent unintentional horizontal scroll with `overflow-x-hidden`.

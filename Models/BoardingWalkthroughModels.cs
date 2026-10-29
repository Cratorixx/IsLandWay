using System.Collections.Generic;

namespace Midterm_App_Dev.Models;

// ============================================================================
// 5. INHERITANCE & POLYMORPHISM: Pier-to-Pier Walkthrough & Last-Mile (Page 3)
// ============================================================================

public sealed record WalkthroughBulletItem(string HighlightLabel, string DetailText);

public sealed record WalkthroughQuickChip(string MaterialIcon, string Text);

public sealed record PortWalkthroughSample(
    string Id,
    string PortSelectorLabel,
    string RouteSummaryTitle,
    string CrossingDurationTag,
    string AdvisoryNote,
    string GangwayBadgeCaption
);

/// <summary>
/// Abstract base class for each step in the 5-step terminal boarding walkthrough.
/// Uses Inheritance and Polymorphism to specialize the right-hand callout panel
/// (Standard Tip vs. Validation Checklist vs. Mandatory Cash Fee).
/// </summary>
public abstract class WalkthroughStepBase : TransitEntityBase
{
    public string StepNumberCode { get; protected set; }
    public string ShortPillLabel { get; protected set; }
    public string MaterialIconName { get; protected set; }
    public string LocationEyebrow { get; protected set; }
    public string LeadDescription { get; protected set; }
    public IReadOnlyList<WalkthroughBulletItem> Bullets { get; protected set; }
    public string CalloutEyebrow { get; protected set; }
    public string CalloutFooterNote { get; protected set; }

    protected WalkthroughStepBase(
        string id,
        string stepNumberCode,
        string shortPillLabel,
        string materialIconName,
        string locationEyebrow,
        string stepTitle,
        string leadDescription,
        IReadOnlyList<WalkthroughBulletItem> bullets,
        string calloutEyebrow,
        string calloutFooterNote)
        : base(id, stepTitle)
    {
        StepNumberCode = stepNumberCode;
        ShortPillLabel = shortPillLabel;
        MaterialIconName = materialIconName;
        LocationEyebrow = locationEyebrow;
        LeadDescription = leadDescription;
        Bullets = bullets;
        CalloutEyebrow = calloutEyebrow;
        CalloutFooterNote = calloutFooterNote;
    }

    public abstract string GetCalloutContainerClasses();
    public abstract string GetCalloutEyebrowColorClass();
    public abstract string GetCalloutFooterColorClass();
}

/// <summary>
/// Concrete derived step with a standard headline and one or more guidance paragraphs in the right callout box.
/// Used for Step 01 (Before You Enter), Step 04 (Allowable Baggage), and Step 05 (Arrival Safety).
/// </summary>
public sealed class StandardTipWalkthroughStep : WalkthroughStepBase
{
    private readonly string _containerClasses;
    public string CalloutTitle { get; }
    public string CalloutTitleSizeClass { get; }
    public IReadOnlyList<string> CalloutParagraphs { get; }

    public StandardTipWalkthroughStep(
        string id,
        string stepNumberCode,
        string shortPillLabel,
        string materialIconName,
        string locationEyebrow,
        string stepTitle,
        string leadDescription,
        IReadOnlyList<WalkthroughBulletItem> bullets,
        string calloutEyebrow,
        string calloutTitle,
        IReadOnlyList<string> calloutParagraphs,
        string calloutFooterNote,
        string containerClasses = "bg-slate-900/50 backdrop-blur-md border border-white/10",
        string calloutTitleSizeClass = "text-[16px]")
        : base(
            id,
            stepNumberCode,
            shortPillLabel,
            materialIconName,
            locationEyebrow,
            stepTitle,
            leadDescription,
            bullets,
            calloutEyebrow,
            calloutFooterNote)
    {
        CalloutTitle = calloutTitle;
        CalloutParagraphs = calloutParagraphs;
        _containerClasses = containerClasses;
        CalloutTitleSizeClass = calloutTitleSizeClass;
    }

    public override string GetCalloutContainerClasses() => _containerClasses;
    public override string GetCalloutEyebrowColorClass() => "text-[#46E5D1]";
    public override string GetCalloutFooterColorClass() => "text-[#46E5D1]";
}

/// <summary>
/// Concrete derived step featuring a structured validation checklist in the right callout box.
/// Used for Step 02 (Check-In & Seat Numbering).
/// </summary>
public sealed class ChecklistWalkthroughStep : WalkthroughStepBase
{
    public string CalloutTitle { get; }
    public IReadOnlyList<WalkthroughBulletItem> ValidationItems { get; }

    public ChecklistWalkthroughStep(
        string id,
        string stepNumberCode,
        string shortPillLabel,
        string materialIconName,
        string locationEyebrow,
        string stepTitle,
        string leadDescription,
        IReadOnlyList<WalkthroughBulletItem> bullets,
        string calloutEyebrow,
        string calloutTitle,
        IReadOnlyList<WalkthroughBulletItem> validationItems,
        string calloutFooterNote)
        : base(
            id,
            stepNumberCode,
            shortPillLabel,
            materialIconName,
            locationEyebrow,
            stepTitle,
            leadDescription,
            bullets,
            calloutEyebrow,
            calloutFooterNote)
    {
        CalloutTitle = calloutTitle;
        ValidationItems = validationItems;
    }

    public override string GetCalloutContainerClasses() => "bg-slate-900/50 backdrop-blur-md border border-white/10";
    public override string GetCalloutEyebrowColorClass() => "text-[#7DD3FC]";
    public override string GetCalloutFooterColorClass() => "text-[#7DD3FC]";
}

/// <summary>
/// Concrete derived step featuring a prominent fee range display in the right callout box.
/// Used for Step 03 (Terminal Fee Booth).
/// </summary>
public sealed class FeeHighlightWalkthroughStep : WalkthroughStepBase
{
    public string FeeRangeText { get; }
    public string FeeUnitText { get; }
    public string FeeBodyText { get; }

    public FeeHighlightWalkthroughStep(
        string id,
        string stepNumberCode,
        string shortPillLabel,
        string materialIconName,
        string locationEyebrow,
        string stepTitle,
        string leadDescription,
        IReadOnlyList<WalkthroughBulletItem> bullets,
        string calloutEyebrow,
        string feeRangeText,
        string feeUnitText,
        string feeBodyText,
        string calloutFooterNote)
        : base(
            id,
            stepNumberCode,
            shortPillLabel,
            materialIconName,
            locationEyebrow,
            stepTitle,
            leadDescription,
            bullets,
            calloutEyebrow,
            calloutFooterNote)
    {
        FeeRangeText = feeRangeText;
        FeeUnitText = feeUnitText;
        FeeBodyText = feeBodyText;
    }

    public override string GetCalloutContainerClasses() => "bg-slate-900/50 backdrop-blur-md border border-amber-400/30";
    public override string GetCalloutEyebrowColorClass() => "text-amber-300";
    public override string GetCalloutFooterColorClass() => "text-amber-300";
}

/// <summary>
/// Abstract base class for arrival port last-mile transport options (Tricycles, Vans, Buses).
/// Demonstrates Inheritance and Polymorphism for regional vs. local transit styling.
/// </summary>
public abstract class LastMileTransitBase : TransitEntityBase
{
    public string CategoryBadgeText { get; protected set; }
    public string MaterialIconName { get; protected set; }
    public string FareRangeText { get; protected set; }
    public string FareUnitText { get; protected set; }
    public string Description { get; protected set; }
    public string DispatchAdviceText { get; protected set; }

    protected LastMileTransitBase(
        string id,
        string vehicleTitle,
        string categoryBadgeText,
        string materialIconName,
        string fareRangeText,
        string fareUnitText,
        string description,
        string dispatchAdviceText)
        : base(id, vehicleTitle)
    {
        CategoryBadgeText = categoryBadgeText;
        MaterialIconName = materialIconName;
        FareRangeText = fareRangeText;
        FareUnitText = fareUnitText;
        Description = description;
        DispatchAdviceText = dispatchAdviceText;
    }

    public abstract string GetCategoryBadgeClasses();
    public abstract string GetIconCircleClasses();
}

public sealed class LocalTealTransitOption : LastMileTransitBase
{
    public LocalTealTransitOption(
        string id,
        string vehicleTitle,
        string categoryBadgeText,
        string materialIconName,
        string fareRangeText,
        string fareUnitText,
        string description,
        string dispatchAdviceText)
        : base(
            id,
            vehicleTitle,
            categoryBadgeText,
            materialIconName,
            fareRangeText,
            fareUnitText,
            description,
            dispatchAdviceText)
    {
    }

    public override string GetCategoryBadgeClasses() => "text-teal-300 bg-teal-500/15 border border-teal-400/30";
    public override string GetIconCircleClasses() => "bg-teal-500/15 text-teal-300 border border-teal-400/30";
}

public sealed class ShuttleBlueTransitOption : LastMileTransitBase
{
    public ShuttleBlueTransitOption(
        string id,
        string vehicleTitle,
        string categoryBadgeText,
        string materialIconName,
        string fareRangeText,
        string fareUnitText,
        string description,
        string dispatchAdviceText)
        : base(
            id,
            vehicleTitle,
            categoryBadgeText,
            materialIconName,
            fareRangeText,
            fareUnitText,
            description,
            dispatchAdviceText)
    {
    }

    public override string GetCategoryBadgeClasses() => "text-cyan-300 bg-cyan-400/15 border border-cyan-400/30";
    public override string GetIconCircleClasses() => "bg-cyan-400/15 text-cyan-300 border border-cyan-400/30";
}

public sealed class RegionalGreenTransitOption : LastMileTransitBase
{
    public RegionalGreenTransitOption(
        string id,
        string vehicleTitle,
        string categoryBadgeText,
        string materialIconName,
        string fareRangeText,
        string fareUnitText,
        string description,
        string dispatchAdviceText)
        : base(
            id,
            vehicleTitle,
            categoryBadgeText,
            materialIconName,
            fareRangeText,
            fareUnitText,
            description,
            dispatchAdviceText)
    {
    }

    public override string GetCategoryBadgeClasses() => "text-emerald-300 bg-emerald-400/15 border border-emerald-400/30";
    public override string GetIconCircleClasses() => "bg-emerald-400/15 text-emerald-300 border border-emerald-400/30";
}

/// <summary>
/// Encapsulates the complete Page 3 Pier-to-Pier Boarding Walkthrough guide and state.
/// </summary>
public sealed class BoardingWalkthroughGuide : TransitEntityBase
{
    public string EyebrowCategory { get; }
    public string SubtitleDescription { get; }
    public string HeroImageUrl { get; }
    public string HeroImageAlt { get; }
    public string HeroImageBadgeCaption { get; private set; }
    public string HeroImageSubCaption { get; }
    public string GoldenPortRuleText { get; }
    public IReadOnlyList<WalkthroughQuickChip> QuickChips { get; }
    public IReadOnlyList<PortWalkthroughSample> PortSamples { get; }
    public PortWalkthroughSample SelectedPortSample { get; private set; }
    public IReadOnlyList<WalkthroughStepBase> Steps { get; }
    public IReadOnlyList<LastMileTransitBase> LastMileOptions { get; }

    public BoardingWalkthroughGuide(
        string id,
        string displayTitle,
        string eyebrowCategory,
        string subtitleDescription,
        string heroImageUrl,
        string heroImageAlt,
        string heroImageBadgeCaption,
        string heroImageSubCaption,
        string goldenPortRuleText,
        IReadOnlyList<WalkthroughQuickChip> quickChips,
        IReadOnlyList<PortWalkthroughSample> portSamples,
        IReadOnlyList<WalkthroughStepBase> steps,
        IReadOnlyList<LastMileTransitBase> lastMileOptions)
        : base(id, displayTitle)
    {
        EyebrowCategory = eyebrowCategory;
        SubtitleDescription = subtitleDescription;
        HeroImageUrl = heroImageUrl;
        HeroImageAlt = heroImageAlt;
        HeroImageBadgeCaption = heroImageBadgeCaption;
        HeroImageSubCaption = heroImageSubCaption;
        GoldenPortRuleText = goldenPortRuleText;
        QuickChips = quickChips;
        PortSamples = portSamples;
        SelectedPortSample = portSamples[0];
        Steps = steps;
        LastMileOptions = lastMileOptions;
    }

    public void ApplyPortSample(PortWalkthroughSample sample)
    {
        if (sample != null)
        {
            SelectedPortSample = sample;
            HeroImageBadgeCaption = sample.GangwayBadgeCaption;
        }
    }
}

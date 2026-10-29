using System;

namespace Midterm_App_Dev.Models;

// ============================================================================
// 1. ABSTRACTION: Core Contracts & Abstract Base Classes
// ============================================================================

public interface ITransitEntity
{
    string Id { get; }
    string DisplayTitle { get; }
}

public interface IRoutePricing
{
    decimal MinFarePhp { get; }
    decimal MaxFarePhp { get; }
    string GetFareCategoryLabel();
    string GetFormattedFareRange();
}

public enum VesselClassType
{
    All,
    Fastcraft,
    RoRoOvernight
}

/// <summary>
/// Abstract base class providing identity and common metadata for all transit domain entities.
/// Demonstrates Abstraction and Encapsulation with strict invariant guards.
/// </summary>
public abstract class TransitEntityBase : ITransitEntity
{
    private string _id = string.Empty;
    private string _displayTitle = string.Empty;

    public string Id
    {
        get => _id;
        protected set => _id = string.IsNullOrWhiteSpace(value)
            ? throw new ArgumentException("Entity ID cannot be empty.", nameof(Id))
            : value.Trim();
    }

    public string DisplayTitle
    {
        get => _displayTitle;
        protected set => _displayTitle = string.IsNullOrWhiteSpace(value)
            ? throw new ArgumentException("Display title cannot be empty.", nameof(DisplayTitle))
            : value.Trim();
    }

    protected TransitEntityBase(string id, string displayTitle)
    {
        Id = id;
        DisplayTitle = displayTitle;
    }
}

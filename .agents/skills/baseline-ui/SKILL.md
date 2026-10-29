---
name: baseline-ui
description: Opinionated UI and frontend engineering constraints. Enforces strict spacing, accessibility standards, animation performance, and Tailwind CSS hygiene to eliminate AI interface slop.
---

# Baseline UI Quality Skill

## Purpose
Apply strict frontend engineering and design constraints to build responsive, accessible, high-performance web components using modern CSS and Tailwind standards.

---

## 1. Stack & Tailwind Hygiene
- **Tailwind Conventions**: Use standard Tailwind utilities first; rely on consistent spacing increments (`4`, `6`, `8`, `12`, `16`).
- **Dynamic Viewports**: Never use `h-screen`; always use `h-dvh` or `min-h-dvh` to handle mobile dynamic address bars.
- **Horizontal Overflow**: Set `overflow-x-hidden` on outer application containers to prevent inadvertent side-scrolling.
- **Z-Index Scale**: Avoid arbitrary numbers like `z-[9999]`. Adhere to a clean hierarchy:
  - Base content: `z-0`
  - Sticky navigation/bars: `z-30`
  - Dropdowns and popovers: `z-40`
  - Modals and backdrops: `z-50`
  - Toast notifications and tooltips: `z-60`

---

## 2. Accessibility (a11y) Baseline
- **Touch Targets**: Minimum 44×44px interactive bounding box on touch/mobile screens (`min-h-[44px] min-w-[44px]`).
- **Icon Buttons**: Any icon-only `<button>` or `<a>` MUST have an explicit `aria-label="Describe action"`.
- **Focus Rings**: Never use `outline-none` without an explicit replacement. Always provide visible keyboard focus styles: `focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-cyan-400`.
- **Color Contrast**: Ensure text meets WCAG AA standards (minimum 4.5:1 for normal body text, 3:1 for large display text).

---

## 3. Typography Rules
- **Headings**: Apply `text-balance` on `<h1>` through `<h3>` to avoid orphan words.
- **Paragraphs**: Apply `text-pretty` to body text paragraphs for clean right-rag layout.
- **Numeric Data**: Apply `tabular-nums` for counters, financial metrics, time tracking, and data grids.
- **Contrast Comfort**: Avoid pure `#000000` text on pure `#ffffff` background (and pure `#ffffff` on `#000000`). Prefer slight slate/gray undertones (`text-slate-100` on dark, `text-slate-900` on light).

---

## 4. Animation & Interaction Constraints
- **GPU Compositor Properties Only**: Animate strictly with `transform` (scale, translate, rotate) and `opacity`.
- **Duration**: Snappy micro-interactions must stay between 150ms and 200ms (`duration-150` or `duration-200`).
- **Easings**: Use `ease-out` for entrances and user clicks; use `ease-in` for exits.
- **State Coverage**: Every interactive control must specify hover, active, focus-visible, and disabled states.

---

## 5. Visual Consistency & Surfaces
- **Radius Consistency**: Harmonize border-radii across the view (e.g., cards `rounded-xl` or `rounded-2xl`, inputs/buttons `rounded-lg`, pills `rounded-full`).
- **Subtle Borders over Heavy Shadows**: In dark modes, use subtle translucent borders (`border border-slate-800` or `border border-white/10`) to separate surfaces cleanly.

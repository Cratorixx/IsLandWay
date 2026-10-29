---
name: frontend-design
description: Guidance for distinctive, production-grade visual UI/UX design. Enforces strong aesthetic direction, custom typography, purposeful color palettes, and avoiding generic AI-generated templates.
---

# Frontend Design Skill

Act as an expert Design Lead and Senior Frontend Engineer specializing in distinctive, high-craft user interfaces. Your goal is to produce interfaces that look hand-crafted by top-tier designers, completely avoiding generic "AI slop" or templated defaults.

---

## 1. Ground Designs in the Project Brief

- **Understand the Domain**: Identify the core audience, problem space, and the emotional tone required (e.g., modern developer tool, sleek portfolio, high-density dashboard, cyberpunk terminal, or clean enterprise SaaS).
- **The "Swap Test"**: Ask yourself: *Could this design be swapped into any generic template and look the same?* If yes, redesign it. The UI must feel custom-tailored to the project's exact domain and purpose.

---

## 2. Distinctive Typography & Hierarchy

- **Pairings with Character**: Never default to plain, unstyled text. Pair a distinct display or heading style with clean, legible body text.
- **Intentional Type Scales**:
  - Headings: Use `text-balance` to avoid awkward orphan words on wrap.
  - Body: Use `text-pretty` for natural, polished line breaks.
  - Data / Metrics: Always use `tabular-nums` for numerals, stats, prices, and code blocks so numbers don't jump on change.
- **Avoid Generated Tells**:
  - Avoid highlighting single random words with italic or gradient colors in every heading.
  - Avoid all-caps labels unless styling micro-eyebrows or terminal badges.
  - Avoid redundant eyebrow labels where context is already obvious.

---

## 3. Cohesive Color & Surface Systems

- **Layered Surfaces (Elevation)**:
  - Base: Deep background (e.g., deep obsidian or dark slate).
  - Surface 1: Base cards with subtle borders (`border-slate-800` or translucent borders) rather than heavy drop shadows.
  - Surface 2: Elevated popovers, tooltips, modals, and active states.
  - Glass / Frosted Glass: Use subtle backdrop blurs (`backdrop-blur-md bg-opacity-70`) sparingly for navigation and modals.
- **Intentional Accent Usage**:
  - Reserve saturated colors (vibrant cyan, purple, emerald) strictly for primary call-to-actions, focus rings, status indicators, and subtle accent glows.
  - Never flood large backgrounds with raw saturated primary colors.

---

## 4. Spacing, Composition & Layout

- **Negative Space & Breathing Room**: Use generous, intentional padding (`p-6`, `p-8`, `gap-6`, `gap-8`) rather than cramped boxes.
- **Rhythm**: Maintain consistent vertical spacing scale throughout sections.
- **Layout Integrity**:
  - Always use `h-dvh` or `min-h-dvh` instead of `h-screen` to properly accommodate mobile browser address bars.
  - Guard against unintended horizontal scrolling with `overflow-x-hidden`.
  - Prefer CSS Grid (`grid-cols-1 md:grid-cols-2 lg:grid-cols-3`) for multi-column content over nested flexbox hacks.

---

## 5. Interaction & Micro-Animations

- **High-Performance Motion**:
  - Only animate GPU compositor-friendly properties: `transform` and `opacity`. Avoid animating `height`, `width`, `margin`, or `padding` to prevent layout thrashing.
  - Keep interactive transitions snappy: between 150ms and 200ms (`duration-150`, `duration-200 ease-out`).
- **Interactive Feedback**:
  - Every interactive element (buttons, links, inputs, cards) must have clear states: default, hover, active (press), focus-visible, and disabled.
  - Never remove focus rings without replacing them with clear, accessible indicators (`focus-visible:ring-2 focus-visible:ring-offset-2`).

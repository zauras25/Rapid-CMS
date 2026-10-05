# Rapid-CMS

## Figma-First Visual Development Platform

**Engine-First · Native Design Model · Production-Oriented**

Rapid-CMS is a Figma-first visual development platform built around a native design-development engine.

The core product is **not the visual editor**.

The core product is the **engine and native design model** that understands design semantics, structure, layout, components, styles, variables, assets, prototypes, responsive behavior, and production intent.

The editor, renderer, code generator, SEO system, deployment system, and AI layer are consumers of that foundation.

Figma Semantics ↓ Native Design Model ↓ Core Engines ↓ Resolved Design State ↓ Semantic / Responsive / SEO / Transformation ↓ Production Outputs ↓ Editor / Renderer / Code Generation / Deployment / AI


> **Development Philosophy:** Engine First → Interface Second

---

## Project Vision

Rapid-CMS aims to become a native design-to-production platform where a single authoritative design state can drive multiple production outputs.

The long-term platform includes:

- Figma-compatible design semantics
- Native design domain model
- Layout and responsive engines
- Component and instance system
- Style and variable/token system
- Asset management
- Prototype system
- Rendering
- Semantic understanding
- SEO
- Code generation
- Next.js generation
- Build and preview
- Visual editor
- Deployment
- AI-assisted workflows
- Advanced Figma compatibility

The editor is therefore **not the architecture**.

The editor is a **consumer of the architecture**.

---

# Core Architecture

┌───────────────────────┐ │ Editor │ ├───────────────────────┤ │ Tools │ ├───────────────────────┤ │ Prototype UI │ ├───────────────────────┤ │ SEO UI │ ├───────────────────────┤ │ CodeGen UI │ ├───────────────────────┤ │ Deployment UI │ ├───────────────────────┤ │ AI UI │ └───────────┬───────────┘ │ ▼ ┌───────────────────────┐ │ Core Engines │ └───────────┬───────────┘ │ ▼ ┌───────────────────────┐ │ Native Design Model │ └───────────┬───────────┘ │ ▼ ┌───────────────────────┐ │ Figma Semantics │ └───────────────────────┘


The **Native Design Model** is the central source of truth.

---

# Native Design Model

Core concepts include:

- Project
- Document
- Page
- Node
- Frame
- Section
- Group
- Shape
- Vector
- Text
- Image
- Component
- Component Set
- Instance
- Variant
- Style
- Variable
- Token
- Asset
- Prototype
- Interaction
- Reference
- Metadata
- Compatibility Metadata

The model emphasizes:

- Stable native IDs
- Explicit ownership
- First-class references
- Semantic geometry
- Semantic layout
- Auto Layout
- Constraints
- Responsive behavior
- Styles
- Variables
- Resolved and source values
- Components and instances
- Inheritance
- Overrides
- Property-level reset
- Variants
- Assets
- Images
- Vectors
- SVG preservation
- Masks
- Effects
- Text runs
- Typography
- Prototype semantics
- Serialization
- Determinism
- Referential integrity
- Project isolation

### Fidelity Priority

Semantic Fidelity ↓ Structural Fidelity ↓ Visual Fidelity ↓ Implementation Convenience


---

# Engine Architecture

Planned core engines include:

- Document Engine
- Node Engine
- Layout Engine
- Responsive Engine
- Component Engine
- Override Engine
- Style Engine
- Variable / Token Engine
- Asset Engine
- Prototype Engine
- Dependency Engine
- Change Propagation
- Command Engine
- Undo / Redo
- Validation
- Referential Integrity
- Rendering Boundary
- Semantic Engine Boundary
- SEO Engine Boundary
- Code Generation Boundary
- Deployment Boundary

Native Design Model ↓ Core Engines ↓ Resolved State ↓ Rendering / Semantic / Responsive ↓ SEO / Code Generation ↓ Production Outputs


---

# Architecture Principles

## Native Design State Is the Source of Truth

There is one authoritative design state.

Generated code, caches, rendered output, and UI state are not the source of truth.

## UI Is a Consumer

UI business logic must not become the core architecture.

The engine must be usable without the editor.

## Commands Control Major State Changes

Uncontrolled direct state mutation is prohibited for major state transitions.

## Engines Must Be Independently Testable

Major engines must be testable without requiring the visual editor.

## Figma Compatibility Is an Integration Boundary

The platform may understand Figma semantics, but the core domain must not become dependent on Figma internals.

## Code Generation Starts From the Native Model

Generated code is derived from the Native Design Model, not from editor UI state.

## Generated Code Is Not the Source Document

Production output can be regenerated.

The native design state remains authoritative.

## AI Is Not a Replacement for Deterministic Engines

AI may assist the platform, but deterministic engines remain responsible for authoritative behavior.

## Circular Dependencies Are Forbidden

Dependency direction must remain explicit and controlled.

## Framework-Specific Logic Stays at the Boundary

For example:

CodeGen └── NextJs


Next.js-specific behavior must not leak into the core domain.

---

# Development Governance

Rapid-CMS follows a **strict sequential, acceptance-gated, freeze-based development model**.

## One Active Milestone

Only one implementation milestone can be active at any time.

Current Milestone ↓ Discovery ↓ Requirements ↓ Architecture ↓ Implementation ↓ Tests ↓ Validation ↓ Acceptance ↓ FREEZE ↓ Next Milestone


> **No Next Milestone Before Current Milestone Acceptance & Freeze.**

Code Exists ≠ Complete


Instead:

Requirements + Implementation + Tests + Validation + Acceptance + Freeze = Complete


---

# Milestone Lifecycle

Every milestone must pass:

1. Discovery
2. Requirements Extraction
3. Acceptance Matrix
4. Architecture / Design
5. Implementation
6. Unit Tests
7. Integration Tests
8. Edge Case Validation
9. Documentation
10. Acceptance Review
11. Accepted
12. Frozen

Only after the milestone is frozen may the next implementation milestone become active.

---

# Master Roadmap

## Architecture Foundation

M0 → Product & Architecture Rules ↓ M1 → Figma Compatibility Strategy ↓ M2 → Core Domain Model & Figma-Native Behavior ↓ M3 → Core Engines & Design-to-Production Architecture ↓ M4 → Visual Studio Implementation Architecture


M0–M4 establish the architectural foundation.

---

# Engineering Implementation

M5 → Solution Bootstrap & Implementation Foundation M6 → Document & Node Engine M7 → Command / State / Transaction M8 → History & Undo / Redo M9 → Layout & Responsive M10 → Style & Variable Engine M11 → Asset Engine M12 → Component / Instance Engine M13 → Prototype Engine M14 → Persistence & Package Storage M15 → Figma Compatibility Implementation M16 → Rendering Engine M17 → Semantic / Web Understanding M18 → SEO Engine M19 → Code Generation Engine M20 → Next.js Generator M21 → Code Quality & Production Validation M22 → Build / Preview Engine M23 → Editor Application M24 → Tool System M25 → Project Application Layer M26 → Deployment Engine M27 → Performance & Scalability M28 → Security M29 → Complete Test & Certification M30 → AI Layer M31 → Advanced Figma Compatibility


### Execution Rule

M0 ↓ Accepted ↓ Frozen ↓ M1 ↓ Accepted ↓ Frozen ↓ M2 ↓ ... ↓ M31 ↓ Final Freeze


No milestone may be skipped, silently bypassed, merged into another milestone, or declared complete merely because related code already exists.

---

# Current Development Status

**Current Active Milestone: M2 — Core Domain Model & Figma-Native Behavior**

M2 is responsible for establishing the Native Design Model and its fundamental Figma-native behavior.

Native Domain Foundation ↓ Requirements Complete ↓ Tests Complete ↓ Validation Complete ↓ Acceptance ↓ FREEZE


Until M2 is formally accepted and frozen, M3 implementation remains inactive.

---

# Future-Code Policy

Code related to a future milestone may exist in the repository.

That does **not** mean the milestone is complete.

M12-related code exists ↓ M12 ≠ Complete ↓ M12 remains Future


When M12 becomes active:

Existing Foundation ↓ Requirements Audit ↓ Gap Analysis ↓ Required Implementation ↓ Tests ↓ Acceptance ↓ M12 Freeze


Existing code receives no automatic milestone-completion credit.

---

# Controlled Dependency Exception

A future concept may only be implemented early when its **minimum technical foundation is required by the current milestone**.

Current Requirement ↓ Required Supporting Abstraction ↓ Minimum Foundation ↓ Return to Current Milestone


This does not activate the future milestone and does not count as completing it.

---

# Definition of Engine Complete

An engine is considered complete only when:

- Its specification exists.
- Its responsibility is clearly defined.
- Required contracts exist.
- Implementation is complete.
- Validation exists.
- Unit tests exist.
- Integration tests exist where applicable.
- Edge cases are covered.
- Failure and recovery behavior are defined.
- Performance is acceptable.
- Serialization/recovery is complete where applicable.
- Documentation exists.
- The engine can operate without the UI.
- Dependent engines can consume it.
- Acceptance criteria pass.

ENGINE ↓ ACCEPTED ↓ FROZEN FOUNDATION


---

# Architecture Change Policy

Frozen architectural decisions cannot be silently changed.

Existing Decision ↓ Reason for Change ↓ Impact Analysis ↓ New Decision ↓ Affected Documents ↓ Documentation Update ↓ New Baseline ↓ Acceptance ↓ Freeze


---

# Source-of-Truth Hierarchy

When project documents conflict:

Approved Milestone Discovery
↓

Approved Acceptance Criteria
↓

Master Roadmap
↓

Actual Codebase
↓

Current Implementation Notes

If implementation conflicts with an approved requirement, the implementation must be corrected to satisfy the approved requirement.

---

# Expected Solution Architecture

DesignPlatform.sln │ ├── src │ ├── DesignPlatform.Domain │ ├── DesignPlatform.Application │ ├── DesignPlatform.Contracts │ ├── DesignPlatform.Engine │ ├── DesignPlatform.Engine.Layout │ ├── DesignPlatform.Engine.Components │ ├── DesignPlatform.Engine.Variables │ ├── DesignPlatform.Engine.Assets │ ├── DesignPlatform.Engine.Prototype │ ├── DesignPlatform.Engine.History │ ├── DesignPlatform.Engine.Validation │ ├── DesignPlatform.Engine.Dependency │ ├── DesignPlatform.Rendering │ ├── DesignPlatform.Import │ ├── DesignPlatform.Semantics │ ├── DesignPlatform.SEO │ ├── DesignPlatform.CodeGen │ ├── DesignPlatform.Storage │ └── DesignPlatform.Editor │ └── tests


The exact implementation structure may evolve through approved milestone decisions, but domain isolation and dependency direction remain architectural priorities.

---

# M5 Vertical Slice

The first major implementation foundation is intended to prove:

Create Document ↓ Create Page ↓ Create Node ↓ Validate ↓ Save ↓ Reload ↓ Verify Semantic Equality


---

# Quality Principles

Rapid-CMS prioritizes:

- Deterministic behavior
- Semantic fidelity
- Referential integrity
- Stable identity
- Explicit ownership
- Testability
- Recoverability
- Serialization correctness
- Architecture isolation
- Production-oriented outputs
- Long-term extensibility

Convenience must not compromise the integrity of the Native Design Model.

---

# What Rapid-CMS Is Not

Rapid-CMS is not designed as:

- A UI-first editor with an engine added later
- A Figma clone whose internals depend directly on Figma
- A code generator driven by editor state
- An AI-first design system
- A framework-specific CMS
- A collection of independent UI features

Instead:

> **Rapid-CMS is a native design-development engine platform with visual interfaces built on top.**

---

# Product Direction

NATIVE DESIGN STATE │ ┌──────────────┼──────────────┐ │ │ │ ▼ ▼ ▼ Renderer Semantics CodeGen │ │ │ ▼ ▼ ▼ Preview SEO Next.js / Output │ │ └──────────────┬──────────────┘ ▼ Production │ ▼ Deployment


The visual editor eventually becomes the primary human interface to this system, but it does not become the source of truth.

---

# Development Philosophy

> **Build the machine first. Build the interface second.**

DOMAIN ↓ ENGINE ↓ ENGINE ↓ ENGINE ↓ ENGINE ↓ COMPLETE PLATFORM FOUNDATION ↓ EDITOR ↓ TOOLS ↓ PRODUCTION OUTPUT


---

# Project Status

| Property | Status |
|---|---|
| Architecture Model | M0–M4 |
| Implementation Roadmap | M5–M31 |
| Current Active Milestone | **M2** |
| Development Mode | Strict Sequential |
| Active Milestones | **1** |
| Acceptance Model | Gate-Based |
| Freeze Model | Mandatory |
| Architecture Philosophy | Engine First → Interface Second |

---

# Master Rule

ONE ACTIVE MILESTONE ↓ ONE ACCEPTANCE GATE ↓ ONE FREEZE ↓ NEXT MILESTONE


> **No Skip.**  
> **No Parallel Milestone Implementation.**  
> **No Silent Completion.**  
> **No Silent Architectural Drift.**

> **No Next Milestone Before Current Milestone Acceptance & Freeze.**

---

## Documentation

The README provides the public project overview.

The detailed **M2 Acceptance Matrix** and **Master Roadmap / Architecture / Development Governance** documents are maintained separately as the project's living development specifications.

---

## License

License information will be defined by the project maintainers.

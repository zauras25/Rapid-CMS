# M1 — Figma Compatibility Research & Compatibility Strategy

**Status:** FROZEN

**Depends On:** M0 — Product Definition / Architecture

## Freeze Decision

M1 defines the Figma compatibility research, compatibility strategy,
import architecture, preservation rules, identity strategy, testing
strategy, and architectural boundaries required for future Figma integration.

M1 does not implement the Figma importer.

## Core Principle

Maximum Figma Structural Fidelity
→ Compatibility Layer
→ Native Platform Model
→ Independent Native Source of Truth

Figma is an external source.

After import:

Native Project Document = Authoritative Source of Truth

Figma re-import is a new import operation and must not silently mutate
an existing Native Project.

## Frozen Architectural Rules

- Native Identity is independent from Source Identity.
- Figma IDs must never become Native Core identity.
- Figma-specific logic must remain behind the Compatibility Layer.
- Core engines must not depend on Figma-specific types or APIs.
- Unsupported information must never be silently discarded.
- Compatibility metadata remains separate from Core Node data.
- Compatibility levels L0–L4 are defined.
- Official Figma APIs are the primary integration mechanism.
- `.fig` is not a core architectural dependency.
- Future source adapters must be supported without modifying Core engines.
- Large documents must support page-level/lazy-loading architecture.
- Visual and behavioral compatibility must both be testable.
- Import must be deterministic where applicable.

## Compatibility Levels

### L0 — Fully Native
Fully represented and editable in the Native Model.

### L1 — Native + Compatibility Metadata
Native representation exists with additional source metadata.

### L2 — Preserved Source / Limited Editing
Partial native representation with preserved source information.

### L3 — Render-Only / Fallback
Preserved/renderable without full native editing semantics.

### L4 — Unsupported but Preserved
Not currently understood natively, but source information is retained.

## Import Pipeline

Source
→ Source Adapter
→ Parser
→ Source Schema Validation
→ Compatibility Analysis
→ Identity Mapping
→ Node Mapping
→ Asset Mapping
→ Style Mapping
→ Variable Mapping
→ Component Mapping
→ Prototype Mapping
→ Metadata Preservation
→ Native Document Construction
→ Document Validation
→ Compatibility Report
→ Project

## Native Identity

Every imported object has:

- Native Identity
- Source Identity

Example:

Node
- nativeId
- sourceIdentity
  - source = figma
  - sourceId = original-id

## Supported Strategy Areas

M1 defines strategy for:

- Document
- Pages
- Sections
- Frames
- Groups
- Shapes
- Vectors
- Text
- Images
- Components
- Instances
- Variants
- Component Properties
- Overrides
- Styles
- Variables
- Auto Layout
- Responsive Sizing
- Constraints
- Grid
- Boolean Operations
- SVG
- Masks
- Effects
- Prototype
- Prototype Flows
- Assets
- Metadata
- Review/Comments separation
- Dev Resources
- Source Preservation
- Large Documents
- Future Source Adapters

## Validation Strategy

Import validation covers:

### Structural
- Parent/child relationships
- IDs
- References
- Pages
- Components
- Instances

### Visual
- Geometry
- Styles
- Typography
- Effects
- Images
- Vectors

### Behavioral
- Auto Layout
- Constraints
- Components
- Variables
- Prototype

### Preservation
- Unsupported data
- Source metadata
- Source IDs
- Missing assets
- Missing fonts

## Compatibility Testing

Representative artifacts must support:

Figma
→ Import
→ Native Document
→ Render
→ Visual Comparison
→ Compatibility Score

Testing includes:

- Pixel comparison
- Bounding-box comparison
- Typography
- Color
- Layout
- Component state
- Behavioral validation
- Source preservation
- Deterministic import

## `.fig` Strategy

`.fig` may be researched or supported in the future.

It is not a Core Architecture dependency.

Undocumented/private format assumptions must not become foundational architecture.

## Figma Plugin Strategy

A future Figma Plugin Adapter may provide:

Figma Plugin
→ Native Export Package
→ Rapid-CMS Import Adapter
→ Native Document

The Plugin Adapter is not part of the Core Document Engine.

## M1 Acceptance

The following M1 requirements are accepted:

- Figma compatibility strategy
- Import architecture
- Native mapping strategy
- Node mapping
- Identity mapping
- Asset strategy
- Style strategy
- Variable strategy
- Component strategy
- Prototype strategy
- Unsupported feature strategy
- Preservation strategy
- `.fig` strategy
- Compatibility levels
- Visual testing strategy
- Behavioral testing strategy
- Large-document strategy
- Figma-specific isolation
- Import acceptance matrix
- Native Source-of-Truth rule
- No Figma Leakage rule
- Future-source extensibility

## Final Decision

M1 — Figma Compatibility Research & Compatibility Strategy

**ACCEPTED**

**FROZEN**

M1 establishes the compatibility and architectural contract required
for future Figma integration.

Actual Figma importer implementation belongs to later milestones.

## Milestone Handoff

M0 — Product Definition
→ FROZEN

M1 — Figma Compatibility Research & Compatibility Strategy
→ FROZEN

M2 — Core Domain Model
→ NEXT

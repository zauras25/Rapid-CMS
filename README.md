Rapid-CMS

Rapid-CMS is a Figma-first, engine-first visual web development platform built around a Native Design Model.

The core architectural principle is:

The Native Design Model is the source of truth. Generated code, rendered output, editor state, and deployment artifacts are derived outputs.

Rapid-CMS is being developed as a deterministic design-to-production system rather than as a UI editor built first and connected to a backend later.

Project Status
Governance Status

M0 — Product & Architecture Rules: ACCEPTED + FROZEN ❄️

M0 is the current frozen governance baseline.

The M0 acceptance audit passed all mandatory acceptance criteria, including:

product boundary and scope

engine-first architecture

Native Design Model as the source of truth

milestone ordering and dependency rules

single active milestone governance

acceptance-before-next-milestone

freeze-before-next-milestone

future-milestone code does not constitute completion

UI/business-logic separation

engine/UI independence

command-based state mutation

independently testable engines

dependency-direction rules

formal acceptance and freeze records

The frozen M0 baseline is the architectural and governance contract for all subsequent milestones.

Important Status Rule

Implementation status and milestone acceptance status are separate.

A milestone may have source code, tests, types, handlers, or partial infrastructure already present without being formally accepted or frozen.

Therefore:

Code exists ≠ milestone accepted.

A milestone becomes complete only after its requirements have been audited, implemented, tested, validated, formally accepted, and frozen according to the M0 governance rules.

Milestone Roadmap

The roadmap is maintained as a governed sequence of milestones.

Milestone	Area	Implementation	Formal Status
M0	Product & Architecture Rules	🟢	✅ ACCEPTED / ❄️ FROZEN
M1	Figma Compatibility Strategy	🟡	Not yet accepted
M2	Core Domain Model & Figma-Native Behavior	🟢	Not yet accepted
M3	Core Engines & Design-to-Production Architecture	🟡	Not yet accepted
M4	Visual Studio Implementation Architecture	🟢	Not yet accepted
M5	Solution Bootstrap	🟢	Not yet accepted
M6	Document & Node Engine	🟢	Not yet accepted
M7	Command / State / Transaction	🟡	Not yet accepted
M8	History / Undo / Redo	🔴	Not yet accepted
M9	Layout / Responsive	🔴	Not yet accepted
M10	Style / Variable Engine	🟡	Not yet accepted
M11	Asset Engine	🟡	Not yet accepted
M12	Component / Instance Engine	🟡	Not yet accepted
M13	Prototype Engine	🟡	Not yet accepted
M14	Persistence / Package Storage	🟡	Not yet accepted
M15	Figma Compatibility	🔴	Not yet accepted
M16	Rendering Engine	🔴	Not yet accepted
M17	Semantic / Web Understanding	🔴	Not yet accepted
M18	SEO Engine	🔴	Not yet accepted
M19	Code Generation	🔴	Not yet accepted
M20	Next.js Generator	🔴	Not yet accepted
M21	Production Validation	🟡	Not yet accepted
M22	Build / Preview	🔴	Not yet accepted
M23	Editor	🔴	Not yet accepted
M24	Editor Tools	🔴	Not yet accepted
M25	Project Application Layer	🟡	Not yet accepted
M26	Deployment	🔴	Not yet accepted
M27	Performance / Scalability	🔴	Not yet accepted
M28	Security	🔴	Not yet accepted
M29	Testing / Certification	🟡	Not yet accepted
M30	AI	🔴	Not yet accepted
M31	Advanced Figma Compatibility	🔴	Not yet accepted
Status Legend

🟢 — substantial implementation exists

🟡 — partial implementation / foundation exists

🔴 — implementation not yet substantially available

✅ — milestone formally accepted

❄️ — milestone formally frozen

The implementation indicator must never be interpreted as formal milestone completion.

Current Governance Position

M0 is frozen.

The next milestone to be governed is M1 — Figma Compatibility Strategy.

M1 must be evaluated against its own requirements before later implementation is treated as milestone progress.

Existing code belonging conceptually to later milestones may remain in the repository as foundation, experimentation, or forward implementation, but it does not bypass the milestone sequence or grant acceptance credit.

The governing sequence is:

Requirements
    ↓
Architecture / Design
    ↓
Implementation
    ↓
Tests
    ↓
Validation
    ↓
Acceptance
    ↓
Freeze
    ↓
Next Milestone

Architecture Principle

Rapid-CMS is built around a Native Design Model rather than around generated web code.

The intended flow is:

Figma / Design Input
        ↓
Figma Semantics
        ↓
Native Design Semantics
        ↓
Native Design Model
        ↓
Runtime / Engines
        ↓
Rendering / Code Generation
        ↓
Production Web Application
        ↓
Build / Preview / Deployment


The editor is a client of the platform, not the source of truth.

Similarly, generated React/Next.js/HTML/CSS output is a derived artifact, not the canonical representation of the project.

Engine-First Architecture

The platform is intended to separate the core design system from presentation and application surfaces.

Conceptually:

                Native Design Model
                         │
        ┌────────────────┼────────────────┐
        │                │                │
   Domain Model      Runtime Model     References
        │                │                │
        └────────────────┼────────────────┘
                         │
                     Engines
                         │
        ┌────────────────┼────────────────┐
        │                │                │
      Layout           Style          Components
        │                │                │
     Assets         Prototype        Rendering
        │                │                │
        └────────────────┼────────────────┘
                         │
                  Production Layer
                         │
          ┌──────────────┼──────────────┐
          │              │              │
       Codegen        Preview       Deployment
          │
       Next.js


The visual editor and its tools consume these capabilities rather than defining them.

Milestone Governance Rules

The following rules are inherited from the frozen M0 baseline.

1. One Active Milestone

Only one milestone may be considered the active governed milestone at a time.

2. Acceptance Before Advancement

A milestone must satisfy its acceptance criteria before the next milestone becomes formally active.

3. Freeze Before Advancement

Accepted milestones must be frozen before the project advances to the next governed milestone.

4. Future Code Does Not Bypass Governance

Code for future milestones may exist in the repository, but:

Pre-existing implementation does not constitute milestone acceptance.

5. Source of Truth

The Native Design Model remains the source of truth.

Generated output must not become the canonical project representation.

6. Engine Independence

Core engines must remain independently testable and must not depend on the visual editor.

7. UI Separation

The editor must consume application/engine capabilities rather than embedding domain business logic inside UI components.

8. Command-Based Mutation

State-changing operations must use the defined command/state architecture rather than arbitrary direct mutation.

What Rapid-CMS Is Building

Rapid-CMS is not simply a CMS and is not merely a visual page editor.

The long-term objective is a system in which a design can move through a deterministic pipeline:

Design
  ↓
Native Design Model
  ↓
Semantic Understanding
  ↓
Layout / Style / Component Resolution
  ↓
Rendering
  ↓
Web Semantics
  ↓
SEO
  ↓
Code Generation
  ↓
Next.js Application
  ↓
Build / Preview
  ↓
Deployment


AI is intended to operate on top of this deterministic foundation rather than replace the underlying domain model and engines.

Current Focus

With M0 frozen, the immediate governed focus is:

M1 — Figma Compatibility Strategy

M1 defines how Figma concepts and semantics are interpreted and mapped into the Native Design Model without making Figma's data format the internal source of truth.

The purpose of M1 is therefore not merely to implement an importer.

It establishes the compatibility strategy that later milestones will rely upon.

Development Philosophy

Rapid-CMS follows the principle:

Build the machine first. Build the interface second.

The platform is therefore being developed from the domain and engine layers upward.

The intended order is:

Domain
  ↓
Runtime
  ↓
Engines
  ↓
Application Layer
  ↓
Editor
  ↓
Tools
  ↓
Production
  ↓
Deployment


This allows the same underlying design model to power:

visual editing

rendering

previews

code generation

Next.js generation

SEO

deployment

automated workflows

future AI capabilities

without making the editor itself the architectural center of the system.

Important Note

The roadmap describes the intended development sequence.

The repository may contain implementation ahead of the currently accepted milestone. Such implementation is treated as available foundation or forward work until the corresponding milestone passes its formal acceptance and freeze process.

Formal milestone status is determined by acceptance evidence, not by the number of files, classes, tests, or features currently present in the repository.
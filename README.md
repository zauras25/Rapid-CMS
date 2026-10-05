Rapid-CMS
Figma-First Visual Development Platform

Rapid-CMS is an Engine-First Native Design Development Platform.

The core product is not the visual editor.

The core product is the Native Design Model and the engines that operate on it.

The Editor, Tools, Rendering, Code Generation, SEO, Deployment, and AI layers are consumers of the platform.

Figma Semantics
      ↓
Native Design Model
      ↓
Core Engines
      ↓
Resolved / Semantic State
      ↓
Rendering / SEO / Code Generation / Deployment
      ↓
Editor & Tools

Architecture Principles

Rapid-CMS follows these architectural principles:

The Native Design Model is the primary source of truth.

The Editor is a consumer, not the architectural foundation.

The Core Domain remains independent of UI, Figma, storage, renderer, and target frameworks.

Engines must operate independently of the UI.

Major state changes use controlled commands.

Deterministic engine behavior is preferred over AI-driven behavior.

Generated code is an output, not the source document.

Cache is never the source of truth.

Project boundaries and ownership remain explicit.

Circular dependencies are prohibited.

Figma compatibility is represented through native semantics rather than leaking Figma-specific concepts into the core.

Architecture must remain explicit, testable, deterministic, and evolvable.

Development Governance

Development follows a strict sequential, acceptance-gated, freeze-based milestone model.

Discovery
   ↓
Requirements
   ↓
Architecture
   ↓
Implementation
   ↓
Tests
   ↓
Validation
   ↓
Acceptance
   ↓
FREEZE
   ↓
Next Milestone

One Active Milestone

Only one implementation milestone may be active at a time.

No next-milestone implementation may be formally started before the current milestone is Accepted and Frozen.

Existing code does not automatically mean that its related milestone is complete.

Code Exists ≠ Milestone Accepted

A milestone is considered complete only when its requirements, implementation, tests, validation, acceptance criteria, and freeze requirements have been satisfied.

Current Repository Status
M0 — Product & Architecture Rules

M0 is complete, Accepted, and Frozen.

M0 established the project's initial product, architectural, development-governance, and milestone-control baseline.

The M0 baseline has been validated through:

Solution/project structure validation

Project-reference validation

Architecture governance tests

Full solution build

Full automated test execution

Acceptance documentation

Freeze documentation

M0 Verification
Verification	Result
Build	✅ Passed
Tests	✅ 248 passed
Failed tests	0
Skipped tests	0
Governance tests	✅ Passed
Project references	✅ Validated
Solution structure	✅ Validated
Architecture governance	✅ Validated
Acceptance record	✅ Present
Freeze record	✅ Present
Working tree	✅ Clean
M0 baseline commit	cf0371f
Branch	main

The M0 governance records are maintained under:

docs/governance/milestones/M0/


including:

M0-Acceptance-Matrix.md
M0-Acceptance-Record.md
M0-Freeze-Record.md


M0 is now the project's frozen governance baseline.

Status Legend

The repository distinguishes implementation maturity from formal governance state.

Symbol	Meaning
🟢	Implemented / substantially complete
🟡	Partially implemented
🔴	Not implemented
✅	Formally Accepted
❄️	Formally Frozen
—	No formal acceptance/freeze established

Important:

🟢 Implementation maturity does not automatically mean Accepted.

Accepted does not automatically mean Frozen.

A milestone becomes a stable baseline only after formal acceptance followed by a recorded freeze.

Implementation
      ↓
Validation
      ↓
Acceptance
      ↓
FREEZE

Milestone Status
Milestone	Implementation	Acceptance	Freeze
M0 — Product & Architecture Rules	🟢	✅	❄️
M1 — Figma Compatibility Strategy	🟡	—	—
M2 — Core Domain Model	🟢	—	—
M3 — Core Engines & Production Architecture	🟡	—	—
M4 — Visual Studio Implementation Architecture	🟢	—	—
M5 — Solution Bootstrap	🟢	—	—
M6 — Document & Node Engine	🟢	—	—
M7 — Command / State / Transaction	🟡	—	—
M8 — History & Undo/Redo	🔴	—	—
M9 — Layout & Responsive	🔴	—	—
M10 — Style & Variable Engine	🟡	—	—
M11 — Asset Engine	🟡	—	—
M12 — Component / Instance Engine	🟡	—	—
M13 — Prototype Engine	🟡	—	—
M14 — Persistence & Package Storage	🟡	—	—
M15 — Figma Compatibility Implementation	🔴	—	—
M16 — Rendering Engine	🔴	—	—
M17 — Semantic / Web Understanding	🔴	—	—
M18 — SEO Engine	🔴	—	—
M19 — Code Generation Engine	🔴	—	—
M20 — Next.js Generator	🔴	—	—
M21 — Code Quality & Production Validation	🟡	—	—
M22 — Build / Preview Engine	🔴	—	—
M23 — Editor Application	🔴	—	—
M24 — Tool System	🔴	—	—
M25 — Project Application Layer	🟡	—	—
M26 — Deployment Engine	🔴	—	—
M27 — Performance & Scalability	🔴	—	—
M28 — Security	🔴	—	—
M29 — Complete Test & Certification	🟡	—	—
M30 — AI Layer	🔴	—	—
M31 — Advanced Figma Compatibility	🔴	—	—

A — in the Acceptance or Freeze column means that no formal acceptance or freeze has been established for that milestone.

This table is intentionally conservative.

Existing implementation is not treated as evidence of milestone completion.

Governance Interpretation

A milestone may have substantial existing code and still require:

Requirements audit

Missing implementation

Unit tests

Integration tests

Edge-case validation

Documentation

Acceptance review

Formal Freeze

Therefore:

🟢 Implementation
      ≠
Accepted
      ≠
Frozen


This distinction is mandatory throughout the project.

Architecture Roadmap
M0  Product & Architecture Rules
 ↓
M1  Figma Compatibility Strategy
 ↓
M2  Core Domain Model & Figma-Native Behavior
 ↓
M3  Core Engines & Design-to-Production Architecture
 ↓
M4  Visual Studio Implementation Architecture
 ↓
M5+ Engineering Implementation


The roadmap defines the intended architectural progression.

Milestone acceptance determines when each stage becomes an approved project baseline.

Implementation Roadmap
M5   Solution Bootstrap
M6   Document / Node Engine
M7   Command / State / Transaction
M8   History / Undo / Redo
M9   Layout / Responsive
M10  Style / Variable Engine
M11  Asset Engine
M12  Component / Instance Engine
M13  Prototype Engine
M14  Persistence / Package Storage
M15  Figma Compatibility
M16  Rendering
M17  Semantic / Web Understanding
M18  SEO
M19  Code Generation
M20  Next.js Generator
M21  Production Validation
M22  Build / Preview
M23  Editor
M24  Tools
M25  Project Application Layer
M26  Deployment
M27  Performance / Scalability
M28  Security
M29  Testing / Certification
M30  AI Layer
M31  Advanced Figma Compatibility


Milestones are developed sequentially.

Existing partial or pre-built foundations do not bypass the milestone order.

Current Development Rule

M0 is the current frozen governance baseline.

The next active milestone must be determined through a formal milestone acceptance audit rather than simply by selecting the highest-numbered code already present in the repository.

The process is:

Existing Implementation
        ↓
Milestone Requirements Audit
        ↓
Gap Analysis
        ↓
Implementation
        ↓
Testing
        ↓
Validation
        ↓
Acceptance
        ↓
FREEZE
        ↓
Next Milestone


Future milestone code that already exists is treated as pre-existing foundation, not as automatic milestone completion.

No milestone may be declared Accepted or Frozen solely because related implementation exists.

Core Domain

The platform already contains a substantial native domain foundation covering concepts such as:

Projects

Documents

Pages

Nodes

Components

Styles

Variables

Assets

Prototypes

References

Runtime representations

Domain/application commands and handlers

The long-term domain direction includes:

Stable native IDs

Explicit ownership

Referential integrity

Semantic geometry

Semantic layout

Responsive behavior

Components and instances

Variants and overrides

Styles and variables

Assets and vectors

Typography

Prototype semantics

Serialization

Deterministic behavior

Project isolation

Comprehensive testing

The Core Domain remains independent of the Editor and other presentation-layer concerns.

Testing

The repository contains dedicated test projects for:

Domain

Application

Engine

Governance

The current verified baseline contains:

248 tests
248 passed
0 failed
0 skipped


The existing test suite provides a substantial engineering foundation.

However, test presence alone does not constitute final platform certification.

Future milestones must add the required validation appropriate to their scope, including:

Unit tests

Integration tests

Persistence tests

API tests

Edge-case validation

Recovery tests

Performance validation

Certification evidence

The M0 governance tests specifically protect the architectural and project-structure rules established by the M0 baseline.

Project Structure

The repository is organized around explicit architectural boundaries.

Rapid-CMS/
│
├── src/
│   ├── Api/
│   ├── App/
│   ├── Contracts/
│   ├── Domain/
│   ├── Engine/
│   └── Infra/
│
├── tests/
│   ├── App.Tests/
│   ├── Domain.Tests/
│   ├── Engine.Tests/
│   └── RapidCMS.Governance.Tests/
│
├── docs/
│   └── governance/
│       └── milestones/
│           └── M0/
│
├── Rapid-CMS.slnx
└── README.md


The exact implementation structure may evolve as milestones progress, but project ownership and dependency direction must remain explicit.

Circular dependencies are prohibited.

Master Principle

Build the machine first. Build the interface second.

DOMAIN
  ↓
ENGINES
  ↓
PLATFORM FOUNDATION
  ↓
EDITOR
  ↓
TOOLS
  ↓
PRODUCTION OUTPUT


The interface is not the architecture.

The interface uses the architecture.

Source of Truth

The Native Design Model is the runtime and architectural source of truth for design state.

Generated code is not the source document.

Caches are not the source of truth.

The detailed milestone requirements, architecture decisions, acceptance criteria, and development governance are maintained in the project's Master Roadmap / Living Specification and associated governance records.

When implementation and approved requirements disagree:

Approved Requirement
        ↑
    Actual Code


The code must be corrected to satisfy the approved architectural and milestone requirements.

Governance records provide the evidence for milestone acceptance and freeze decisions.

Figma Compatibility

Figma compatibility is treated as a platform capability rather than as the foundation of the Core Domain.

The architecture therefore follows:

Figma Semantics
      ↓
Native Design Semantics
      ↓
Native Design Model


Figma-specific concepts must not leak unnecessarily into the core domain.

The platform should represent compatible behavior through stable native semantics that can support:

Design import

Semantic mapping

Native representation

Editing

Rendering

Code generation

Round-trip compatibility where supported

Figma compatibility is therefore an adapter/capability concern around the native platform model, not the definition of the platform itself.

Determinism

Rapid-CMS prioritizes deterministic platform behavior.

The system should prefer:

Explicit commands

Explicit state transitions

Stable identifiers

Deterministic resolution

Predictable serialization

Reproducible builds

Testable engine behavior

AI may assist future workflows, but AI output must not replace the deterministic platform model or become the authoritative source of design state.

Future Platform Layers

The long-term platform is expected to evolve through the following major layers:

Native Design Model
        ↓
Core Engines
        ↓
Resolved / Semantic State
        ↓
Rendering
        ↓
SEO
        ↓
Code Generation
        ↓
Deployment
        ↓
Editor / Tools
        ↓
AI Assistance


Each layer should consume stable platform abstractions rather than bypassing the Core Domain.

Development Discipline

All milestone development must preserve the following rules:

One active milestone at a time.

Requirements are established before implementation is considered complete.

Existing code does not automatically satisfy milestone requirements.

Tests are part of milestone completion.

Validation is required before acceptance.

Acceptance must be explicit.

Frozen milestones establish stable baselines.

Later work must not silently invalidate a frozen baseline.

Architectural drift must be surfaced and resolved explicitly.

No milestone may be silently declared complete.

M0 Frozen Baseline

M0 establishes the initial governance baseline for Rapid-CMS.

The M0 baseline establishes that:

Project boundaries are explicit.

Dependency direction is governed.

Circular dependencies are prohibited.

The solution structure is validated.

Governance tests exist.

Acceptance evidence is recorded.

Freeze evidence is recorded.

The repository can build successfully.

The existing automated test suite passes.

Future milestone work must follow the sequential acceptance/freeze process.

M0 is therefore not merely a documentation milestone.

It is the governance foundation for all subsequent engineering milestones.

Final Rule
No Skip.
No Parallel Milestone Implementation.
No Silent Completion.
No Silent Architectural Drift.


The governing sequence is:

One Milestone
     ↓
Implementation
     ↓
Testing
     ↓
Validation
     ↓
Acceptance
     ↓
FREEZE
     ↓
Next Milestone


M0 is the current frozen baseline.

Implementation status tells us what exists.

Acceptance status tells us what has been formally approved.

Freeze status tells us what is now an established stable baseline.

That distinction is fundamental to the Rapid-CMS development process.
Rapid-CMS
Figma-First Visual Development Platform
Rapid-CMS is an Engine-First Native Design Development Platform.

The core product is not the visual editor. The core product is the Native Design Model and the engines that operate on it.

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
Native Design Model is the primary source of truth.
The Editor is a consumer, not the architectural foundation.
Core Domain remains independent of UI, Figma, storage, renderer, and target frameworks.
Engines must operate without the UI.
Major state changes use controlled commands.
Deterministic engine behavior is preferred over AI-driven behavior.
Generated code is an output, not the source document.
Cache is never the source of truth.
Project boundaries and ownership remain explicit.
Circular dependencies are prohibited.
Figma compatibility is represented through native semantics rather than leaking Figma-specific concepts into the core.
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

No next milestone implementation before the current milestone is Accepted and Frozen.
Existing code does not automatically mean that its related milestone is complete.

Code Exists ≠ Milestone Accepted
A milestone is considered complete only when its requirements, implementation, tests, validation, acceptance criteria, and freeze requirements have been satisfied.

Current Repository Status
The repository has already progressed beyond the initial bootstrap stages.

Status legend:

🟢 Implemented / substantially complete
🟡 Partially implemented
🔴 Not implemented
❄️ Frozen only after formal acceptance
Green indicates implementation maturity. It does not automatically mean Accepted/Frozen.
| Milestone | Status | |---|:---:| | M0 — Product & Architecture Rules | 🟡 | | M1 — Figma Compatibility Strategy | 🟡 | | M2 — Core Domain Model | 🟢 | | M3 — Core Engines & Production Architecture | 🟡 | | M4 — Visual Studio Implementation Architecture | 🟢 | | M5 — Solution Bootstrap | 🟢 | | M6 — Document & Node Engine | 🟢 | | M7 — Command / State / Transaction | 🟡 | | M8 — History & Undo/Redo | 🔴 | | M9 — Layout & Responsive | 🔴 | | M10 — Style & Variable Engine | 🟡 | | M11 — Asset Engine | 🟡 | | M12 — Component / Instance Engine | 🟡 | | M13 — Prototype Engine | 🟡 | | M14 — Persistence & Package Storage | 🟡 | | M15 — Figma Compatibility Implementation | 🔴 | | M16 — Rendering Engine | 🔴 | | M17 — Semantic / Web Understanding | 🔴 | | M18 — SEO Engine | 🔴 | | M19 — Code Generation Engine | 🔴 | | M20 — Next.js Generator | 🔴 | | M21 — Code Quality & Production Validation | 🟡 | | M22 — Build / Preview Engine | 🔴 | | M23 — Editor Application | 🔴 | | M24 — Tool System | 🔴 | | M25 — Project Application Layer | 🟡 | | M26 — Deployment Engine | 🔴 | | M27 — Performance & Scalability | 🔴 | | M28 — Security | 🔴 | | M29 — Complete Test & Certification | 🟡 | | M30 — AI Layer | 🔴 | | M31 — Advanced Figma Compatibility | 🔴 |

Important
This table describes the current implementation state, not milestone acceptance.

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
Milestones are developed sequentially. Existing partial or pre-built foundations do not bypass the milestone order.

Current Development Rule
The next active milestone will be determined by milestone acceptance audit, not simply by the highest-numbered code already present in the repository.

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

Core Domain
The current platform already contains a substantial native domain foundation covering concepts such as:

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
Testing
The repository already contains dedicated test projects for:

Domain
Application
Engine
The existing test suite provides a substantial foundation, but test presence alone does not constitute final platform certification.

Future milestones must add the required:

Unit tests
Integration tests
Persistence tests
API tests
Edge-case validation
Recovery tests
Performance validation
Certification evidence
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
The detailed milestone requirements, architecture decisions, acceptance criteria, and development governance are maintained in the project's Master Roadmap / Living Specification.

When implementation and approved requirements disagree:

Approved Requirement
        ↑
Actual Code
The code must be corrected to satisfy the approved architectural and milestone requirements.

Final Rule
No Skip. No Parallel Milestone Implementation. No Silent Completion. No Silent Architectural Drift.

One Milestone
     ↓
Acceptance
     ↓
Freeze
     ↓
Next Milestone
Implementation status tells us what exists. Acceptance status tells us what is approved. Freeze status tells us what is now a stable baseline
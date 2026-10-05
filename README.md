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
Core Domain must remain independent of UI, Figma, storage, renderer, and target frameworks.
Engines must work without the UI.
Major state changes use controlled commands rather than uncontrolled mutation.
Deterministic engine behavior is preferred over AI-driven behavior.
Generated code is an output, not the source document.
Cache is never the source of truth.
Project boundaries and ownership must remain explicit.
Circular dependencies are prohibited.
Figma compatibility is represented through native semantics rather than leaking Figma-specific concepts throughout the core.
Development Model
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

Code Exists ≠ Milestone Complete
A milestone is complete only when its requirements, implementation, tests, validation, acceptance criteria, and freeze requirements have all been satisfied.

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
Current Status
Active Milestone: M2 — Core Domain Model & Figma-Native Behavior

M2 establishes the native semantic foundation for:

Projects and Documents
Pages and Nodes
Frames, Groups, Shapes and Vectors
Text and Typography
Components, Instances and Variants
Styles, Variables and Tokens
Assets and Images
Prototype and Interaction data
References and metadata
Semantic geometry and layout
Responsive behavior
Referential integrity
Serialization and deterministic state
M2 must be Accepted and Frozen before M3 implementation begins.

Master Rule
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
The detailed development governance, acceptance criteria, architecture decisions, and milestone specifications are maintained separately as the project's Master Roadmap / Source of Truth. :::
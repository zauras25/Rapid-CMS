Milestone Status

Rapid-CMS development is tracked through milestones.
Each milestone records its intended scope, completion status, verification state, and the next development target.

M0 — Project Foundation

Status: ✅ COMPLETE

M0 established the initial Rapid-CMS repository and development foundation.

M0 Completed

✅ Repository initialized

✅ Solution structure established

✅ Core .NET projects created

✅ Domain project established

✅ Application project established

✅ Contracts project established

✅ Engine project established

✅ Infrastructure project established

✅ API project established

✅ Test projects established

✅ Solution/project references established

✅ Initial documentation established

✅ Git-based development workflow established

M0 provided the structural foundation required for further development.

M0: DONE ✅

M1 — Architecture & Domain Direction

Status: ✅ COMPLETE

M1 established the architectural direction of Rapid-CMS and defined the Domain-first approach used by subsequent milestones.

M1 Completed

✅ Engine-first architecture established

✅ Native Design Model identified as the core source of truth

✅ Domain layer separated from Application, Infrastructure and API concerns

✅ Strongly typed identity model introduced

✅ Entity and Value Object foundations established

✅ Initial Project / Document / Page / Node boundaries established

✅ Core domain ownership direction established

✅ Component, Style, Variable and Reference concepts introduced

✅ Prototype concept introduced

✅ Domain invariants made explicit

✅ Automated Domain testing established

✅ Architecture direction documented in README

M1 Architectural Principle

Engine First → Interface Second

The visual editor is a consumer of the platform rather than the foundation of the platform.

M1: DONE ✅

M2 — Native Domain Foundation

Status: ✅ COMPLETE

M2 implemented and verified the first stable Native Design Model foundation.

M2 Completed

✅ Project aggregate and document ownership

✅ Stable strongly-typed domain IDs

✅ Project → Document boundary

✅ Document → Page ownership

✅ Page root-node ownership

✅ Node hierarchy

✅ Parent/child relationships

✅ Duplicate-child protection

✅ Multiple-parent protection

✅ Direct cycle protection

✅ Indirect cycle protection

✅ Node component ownership

✅ Node style ownership

✅ Style properties

✅ Style property replacement/update

✅ Variables and variable mutation

✅ Document references

✅ Node references

✅ Prototype links

✅ Domain validation and invariants

✅ Value-object equality

✅ Entity identity boundaries

✅ Empty/invalid identity protection

✅ Domain-focused automated tests

M2 Verification

The complete solution was restored, built and tested successfully.

.NET SDK: 10.0.401

Projects:
- RapidCMS.Api
- RapidCMS.Application
- RapidCMS.Contracts
- RapidCMS.Domain
- RapidCMS.Engine
- RapidCMS.Infrastructure
- RapidCMS.Application.Tests
- RapidCMS.Domain.Tests
- RapidCMS.Engine.Tests

Test Result:
234 total
234 passed
0 failed
0 skipped


Latest verification:

Build succeeded
Test summary: total: 234, failed: 0, succeeded: 234, skipped: 0


git diff --check also completed without reported whitespace errors.

M2 Definition of Done

M2 is considered complete because:

The Domain project builds successfully.

Domain invariants are explicitly enforced.

Core identities reject empty values.

Project/document/page/node ownership boundaries are represented.

Node hierarchy prevents invalid parent relationships and cycles.

Components and styles cannot be attached to the wrong node.

References and prototype links enforce their basic invariants.

Domain behavior is covered by automated tests.

The complete solution passes the automated test suite.

M2: DONE ✅

Roadmap
M3 — Expanded Native Design Model

Status: ⏳ NEXT

Planned areas:

Component Sets

Component Variants

Instances

Component Properties

Assets

Text and typography model

Frames

Sections

Groups

Shapes

Vectors

Additional design-semantic primitives

Richer document structure

M4 — Layout & Responsive Engine

Status: ⏳ PLANNED

Planned areas:

Layout constraints

Auto-layout semantics

Sizing modes

Spacing

Alignment

Responsive behavior

Breakpoints

Resolved layout state

M5 — Design State & Rendering Foundation

Status: ⏳ PLANNED

Planned areas:

Resolved design state

Rendering model

Design-to-render transformation

Semantic representation

Deterministic output

M6 — Code Generation & Production Output

Status: ⏳ PLANNED

Planned areas:

Component generation

Next.js output

CSS generation

Design-token generation

Production build pipeline

Preview system

M7 — Visual Editor

Status: ⏳ PLANNED

Planned areas:

Canvas

Selection

Inspector

Component editing

Layout editing

Style editing

Prototype editing

M8 — Advanced Platform Capabilities

Status: ⏳ PLANNED

Planned areas:

SEO

Deployment

AI-assisted workflows

Advanced Figma compatibility

Production automation

Current Milestone Position
M0  Project Foundation             ✅ COMPLETE
M1  Architecture & Domain Direction ✅ COMPLETE
M2  Native Domain Foundation       ✅ COMPLETE
M3  Expanded Native Design Model   ⏳ NEXT
M4  Layout & Responsive Engine     ⏳
M5  Design State & Rendering       ⏳
M6  Code Generation & Production   ⏳
M7  Visual Editor                  ⏳
M8  Advanced Platform Capabilities ⏳


Current development target: M3 — Expanded Native Design Model

Development Principle

Engine First → Interface Second

The editor is a consumer of the platform, not the foundation.

The authoritative architecture is:

Figma Semantics
      ↓
Native Design Model
      ↓
Core Engines
      ↓
Resolved Design State
      ↓
Semantic / Responsive / SEO / Transformation
      ↓
Production Outputs
      ↓
Editor / Renderer / Code Generation / Deployment / AI


The Native Design Model remains the authoritative source of truth for the platform.
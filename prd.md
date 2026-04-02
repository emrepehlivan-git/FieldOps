# Product Requirements Document (PRD)

## Product Name

**FieldOps – Maintenance & Field Service Management Platform**

---

## 1. Overview

FieldOps is a modular monolith enterprise platform designed to manage field service operations, maintenance workflows, technician assignments, and SLA-driven service execution.

The system enables organizations to efficiently handle service requests, assign field technicians, track work progress, enforce SLAs, and generate operational insights.

This project is designed as a **portfolio-grade system** demonstrating:

* Modular Monolith Architecture
* Domain-Driven Design (DDD)
* Advanced Authorization (Keycloak)
* Workflow & State Management
* Event-driven communication using RabbitMQ + NServiceBus
* Clean Architecture with single database (PostgreSQL)
* Single frontend architecture (Next.js)

---

## 2. Goals

### Primary Goals

* Model real-world field service operations
* Demonstrate strong domain modeling (DDD)
* Showcase modular architecture and boundaries
* Implement enterprise-grade authentication & authorization
* Demonstrate asynchronous messaging inside a modular monolith
* Provide end-to-end workflow from request to completion

### Secondary Goals

* Demonstrate eventual consistency patterns
* Showcase message-based module decoupling
* Provide clean UI separation via role-based rendering

---

## 3. Non-Goals

* Full microservices architecture
* Real-time GPS tracking
* Native mobile apps

---

## 4. Personas

### Customer

* Creates service requests
* Tracks request status

### Dispatcher

* Assigns technicians
* Manages scheduling and prioritization

### Technician

* Executes field work
* Reports execution details

### Manager

* Monitors SLA compliance and performance

### Admin

* Manages system configuration and authorization

---

## 5. Core Features

### 5.1 Service Request Management

* Create and manage service requests
* Categorize issue types
* Define priority levels
* Attach supporting files

### 5.2 Work Order Management

* Transform service requests into work orders
* Manage lifecycle transitions
* Assign SLA definitions

### 5.3 Scheduling & Dispatch

* Assign technicians based on rules
* Schedule service windows
* Reassign dynamically when needed

### 5.4 SLA Management

* Define SLA rules and thresholds
* Track SLA violations
* Trigger alerts and escalation

### 5.5 Technician Execution

* Start work
* Add notes and attachments
* Complete work orders

### 5.6 Notifications

* Event-based notifications
* SLA breach alerts
* Assignment notifications

### 5.7 Reporting

* Work order metrics
* SLA performance tracking
* Operational dashboards

---

## 6. Domain Model (High-Level)

### Aggregates

* WorkOrder
* ServiceRequest
* Technician
* Assignment
* SLA

### Entities

* Visit
* Note
* Attachment

### Value Objects

* Priority
* Status
* Location
* TimeWindow

---

## 7. Bounded Contexts

* Request Management
* Work Order Management
* Scheduling
* SLA & Monitoring
* Identity & Access
* Notification
* Reporting

---

## 8. Workflow

1. Request Created
2. Work Order Generated
3. Technician Assigned
4. Work Started
5. Work Completed
6. Closed

---

## 9. Messaging Architecture

### Technology

* RabbitMQ (transport)
* NServiceBus (abstraction & messaging framework)

### Communication Model

* Modules communicate asynchronously via messages
* Commands and Events are used for decoupling

### Message Types

#### Commands

* AssignTechnicianCommand
* StartWorkCommand
* CompleteWorkCommand

#### Events

* WorkOrderCreatedEvent
* TechnicianAssignedEvent
* WorkStartedEvent
* WorkCompletedEvent
* SlaBreachedEvent

### Patterns Used

* Outbox Pattern (guaranteed delivery)
* Inbox Pattern (idempotency)
* Eventually Consistent Read Models

---

## 10. Authorization (Keycloak)

### Roles

* admin
* dispatcher
* technician
* manager
* customer

### Features

* Role-based access control
* Policy-based authorization
* Claim-based UI rendering
* Organization-based access control

---

## 11. Data Architecture

### PostgreSQL

* Single source of truth
* Transactional consistency
* Optimized for write-heavy workloads

---

## 12. Technical Architecture

### Backend

* ASP.NET Core
* Modular Monolith
* CQRS
* NServiceBus integration

### Frontend

* Next.js (Single application)
* Role-based UI rendering

### Infrastructure

* Docker Compose
* RabbitMQ
* Keycloak

---

## 13. Success Metrics

* SLA compliance rate
* Average resolution time
* Assignment efficiency
* Message processing reliability

---

## 14. Future Enhancements

* Mobile application
* AI-based technician assignment
* Advanced analytics

---

## 15. Repository Structure

* /backend
* /frontend-next
* /docker
* /docs
* /db

  * /postgres
* docker-compose.yml

---

## 16. Deployment

* Docker Compose setup
* Single command startup

---

## 17. Risks

* Over-engineering messaging layer
* Message ordering and consistency challenges
* Domain complexity growth

---

## 18. Timeline (High-Level)

* Week 1-2: Domain design
* Week 3-5: Core modules
* Week 6-7: Messaging integration
* Week 8-9: UI development
* Week 10-12: Integration & polish

---

## 19. Summary

FieldOps demonstrates enterprise-grade architecture, strong domain modeling, asynchronous messaging patterns, and full-stack engineering capabilities in a single cohesive system.


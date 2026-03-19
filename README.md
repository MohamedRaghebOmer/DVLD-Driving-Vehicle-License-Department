# DVLD - Driving License Management System

![License](https://img.shields.io/badge/license-MIT-blue.svg)
![Platform](https://img.shields.io/badge/platform-Windows-lightgray.svg)
![.NET](https://img.shields.io/badge/.NET_Framework-4.8-purple.svg)

## Project Summary

A real-world desktop application that manages the full lifecycle of driving licenses, including applications, testing, issuance, and enforcement.

* Built with C# and .NET WinForms
* Implements a clean 3-Tier Architecture
* Uses ADO.NET with SQL Server for efficient data handling

---

<h2>🎬 Demo Video</h2>

<p>Click the thumbnail below to watch full system walkthrough demonstrating all core features.</p>

<a href="https://youtu.be/9oLnLDZ63cs?si=E5AmxEMNU_48F4s1">
  <img src="https://img.youtube.com/vi/9oLnLDZ63cs/0.jpg" width="800"/>
</a>

---

## Key Features

### License Management

* Issue new licenses across multiple vehicle classes
* Renew expired licenses
* Replace lost or damaged licenses
* Issue international driving licenses

### Test Management

* Schedule vision, theory, and practical tests
* Record test results and enforce test sequence
* Handle test retakes with dynamic fee calculation

### Driver & People Management

* Maintain centralized person registry
* Prevent duplicate entries using national ID

### Enforcement

* Detain licenses for violations
* Process fines and release detained licenses

### Administration

* Manage users, roles, and application types
* Configure fees, rules, and license validity

---

## Technologies Used

| Category        | Details                   |
| --------------- | ------------------------- |
| Language        | C#                        |
| Framework       | .NET Framework (WinForms) |
| Architecture    | 3-Tier Architecture       |
| Database        | Microsoft SQL Server      |
| Data Access     | ADO.NET                   |
| Version Control | Git & GitHub              |

---

## System Overview & Architecture

The system follows a structured 3-Tier Architecture to ensure separation of concerns and maintainability.

1. **DVLD.Core** – Contains DTOs, Enums, and shared utilities.
2. **DVLD.Data** – Handles database operations using ADO.NET.
3. **DVLD.Business** – Implements business rules and validation logic.
4. **DVLD.WinForms** – Provides the graphical user interface.

---

## Installation and Setup

### Prerequisites

* Visual Studio 2022 or later
* Microsoft SQL Server

### Steps

1. Clone the repository:

```bash
git clone https://github.com/MohamedRaghebOmer/DVLD-Driving-License-Management-System.git
```

2. Setup the database:

* Open SQL Server Management Studio
* Run `Database/DVLD_Full_Script.sql`

3. Configure connection string:

* Update `DVLD.Data/DataSettings.cs`

4. Run the project:

* Open `DVLD.slnx`
* Set `DVLD.WinForms` as startup project
* Build and run

---

## Screenshots

### Login Screen

<p align="center">
  <img src="Assets/login-screen.png" width="700"/>
</p>

<br/>

### Main Dashboard

<p align="center">
  <img src="Assets/main-dashboard.png" width="700"/>
</p>

<br/>

### Person Management & Details

<p align="center">
  <img src="Assets/manage-people.png" width="700"/>
</p>

<p align="center">
  <img src="Assets/person-details.png" width="700"/>
</p>

<br/>

### New Local Driving License Application

<p align="center">
  <img src="Assets/new-local-driving-license-application.png" width="700"/>
</p>

<p align="center">
  <img src="Assets/new-local-driving-license-application2.png" width="700"/>
</p>

<br/>

### Test Scheduling (Vision / Theory / Practical)

<p align="center">
  <img src="Assets/schedule-vision-test.png" width="700"/>
</p>

<p align="center">
  <img src="Assets/schedule-written-test.png" width="700"/>
</p>

<p align="center">
  <img src="Assets/schedule-street-test.png" width="700"/>
</p>

---

## What I Learned

* Designed a fully decoupled 3-tier architecture from scratch
* Designed and enforced complex business rules
* Built reusable UI components using WinForms UserControls
* Implemented structured error handling and logging

---

## Future Improvements

* Migrate to Entity Framework Core
* Add unit testing (xUnit)
* Upgrade UI to WPF
* Generate PDF reports

---

## Version

**v1.0 – Initial Release**

---

## Contact

* Author: Mohamed Ragheb Omer
* LinkedIn: [https://www.linkedin.com/in/mohamedraghebomer](https://www.linkedin.com/in/mohamedraghebomer)
* Email: [mohamedraghebomer@gmail.com](mailto:mohamedraghebomer@gmail.com)

# Ticket Management System (Backend)

This is the **C# ASP.NET Core** backend for the **Ticket Management System**. The backend provides an API for managing counters and tickets. It uses **SQL Server** as the database, and follows a clean architecture using controllers, entities, repositories, services, and DTOs.

### Frontend Application
The project connects to a frontend Application built in **Svelte** using **TypeScript** (available in [this](https://github.com/RaduCruceat/TicketApplication-FE) repository).

## Features

### Counter Management
- **Create Counter:** Adds a new counter to the system.
- **Edit Counter:** Updates an existing counter.
- **Delete Counter:** Removes a counter from the system.
- **Change Counter State:** Toggles a counter's status between `Active` and `Inactive`.

### Ticket Management
- **Create Ticket:** Adds a new ticket to a specified counter.
- **Edit Ticket:** Updates an existing ticket.
- **Delete Ticket:** Removes a ticket from the system.
- **Change Ticket Status:** Allows tickets to transition between `Received`, `In Progress`, and `Closed`.
- **Timestamp Management:** Automatically records the creation and last edited time of each ticket.

## Tech Stack

- **ASP.NET Core**: For building the web API.
- **SQL Server**: Database for persisting data, managed through **SQL Server Management Studio**.
- **Entity Framework Core (EF Core)**: For database context management.
- **FluentMigrator**: For handling database migrations.
- **Dapper**: Lightweight ORM for executing raw SQL queries.
- **AutoMapper**: For mapping between entities and DTOs.
- **FluentValidation**: For validating DTOs.



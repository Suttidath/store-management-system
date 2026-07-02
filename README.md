# Store Management System

A desktop inventory management app built with **C# WinForms (.NET Framework 4.7.2)**. Handles product & category CRUD, stock in/out movements with a running balance, and a movement history log — backed by SQL Server LocalDB.

## Features

- Login / signup
- Dashboard with live stock totals and a recent-movements feed
- Product management (code, name, category, brand, description, quantity)
- Category management
- Stock in/out with department + reason tracking and a running current-stock display
- Full movement history with search and date filtering

## Tech stack

- C# / WinForms, .NET Framework 4.7.2
- SQL Server LocalDB (`MSSQLLocalDB` instance) via `System.Data.SqlClient`
- No external NuGet packages

## Getting started

**Requirements**
- Windows with Visual Studio 2022+ (or the .NET Framework 4.7.2 targeting pack + MSBuild)
- SQL Server Express LocalDB (installed by default with the Visual Studio "Data storage and processing" workload)

**Run it**
1. Open `StoreManagementSystem.slnx` in Visual Studio
2. Press F5

The app creates its own `StoreManagementDB` database and tables in LocalDB on first run — no setup or connection string editing required. See [Database.cs](StoreManagementSystem/Database.cs) for details.

## Project structure

See [PROJECT_FLOW.md](PROJECT_FLOW.md) for the full architecture walkthrough, screen-by-screen flow, and known improvement areas.

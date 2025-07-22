# FILE_TRACKER 📂

A simple **CRUD web application** built using **ASP.NET Core** with **Razor Pages**, designed to help administrative staff efficiently **track, organize, and manage client file records**.

---

## Features

- Home Page listing all existing file records
- Create, Edit, Delete, and Search functionality
- Duplicate entry prevention
- Server-side validation for form inputs
- Built with Entity Framework Core and MySQL

---

## Tech Stack

- ASP.NET Core (Razor Pages)
- Entity Framework Core
- MySQL (or SQL Server)

---
 
## Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/en-us/download)  
- [MySQL](https://www.mysql.com/) or SQL Server  
- C# IDE (e.g., Visual Studio, Rider, or VS Code)

---
## 🚀 Setup Instructions

###  **Clone the Repository [locally stored as Rec_Tracker]**

```bash
git clone https://github.com/Ari2606/FILE_TRACKER.git
 
cd FILE_TRACKER/Rec_Tracker 

```
Apply Migrations as required. 
```
dotnet ef migrations add InitialCreate
dotnet ef database update
```

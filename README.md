# 🛒 Store Management System

A web application for managing products, inventory, shopping carts, and customer orders in a store environment.


![.NET Version](https://img.shields.io/badge/.NET-8.0-purple)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-8.0-blue)

---

## 📋 Table of Contents

- [About the Project](#about-the-project)
- [Technologies Used](#technologies-used)
- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
- [Project Structure](#project-structure)
- [Features](#features)
- [Usage](#usage)
- [Database Setup](#database-setup)

---

## 📖 About the Project
Store Management System offers users variety of features such as:
- Browsing Products
- Managing a Product(Create, Edit, Delete)
- Adding a Product to Cart
- Removing a Product from Cart
- Placing Order containing Products

Upon registering, the user's role by default is "User". The user can access basic functionalitites (adding a product to cart, placing order). <br>
The "Manager" role is capable of creating, editing and removing products. <br>
"Admin" has the ability to assign other users to different roles. 

Admin login credentials can be found in appsettings.Development.json


---
## 🛠️ Technologies Used

| Technology            | Version  | Purpose                          |
|-----------------------|----------|----------------------------------|
| ASP.NET Core MVC      | 8.0      | Web framework                    |
| Entity Framework Core | 8.0      | ORM / Database access            |
| SQL Server            | -        | Database                         |
| Tailwind              | -        | Frontend styling                 |
| Razor Pages / Views   | -        | Server-side HTML rendering       |

---

## ✅ Prerequisites

Make sure you have the following installed before running the project:

- [.NET SDK 8.0+](https://dotnet.microsoft.com/download)
- [Visual Studio 2022](https://visualstudio.microsoft.com/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server) 
- [Git](https://git-scm.com/)

---

## 🚀 Getting Started

Follow these steps to get the project running locally.

### 1. Clone the repository

```bash
git clone https://github.com/georgiT00/StoreManagementSystem-assignment2026.git
```
### 2. Open Project Solution
```
StoreManagementSystem-assignment2026.sln
```

### 3. Apply database migrations using PMC (Select "Data/StoreManagementSystem.Data" as a default project) 

```
Update-Database
```

### 3. Run the application



---

## 📁 Project Structure

```
StoreManagementSystem/
│
├── Controllers/          # MVC Controllers
├── Models/               # Domain models 
├── Views/                # Razor Views (.cshtml)
├── ViewModels/           # ViewModels
├── Data/                 # DbContext and migrations
├── Services/             # Business logic / service layer
├── Tests/                # Unit Tests for services
├── wwwroot/              # Static files (CSS, JS, images)
├── appsettings.json      # App configuration
└── Program.cs            # App entry point and middleware setup
```

---

## ✨ Features

- [ ] User registration and login (ASP.NET Identity)
- [ ] CRUD operations for Product
- [ ] Input validation (server-side & client-side)
- [ ] Responsive UI with Tailwind

---

## 💻 Usage


```
1. Navigate to /Register to create an account.
2. Log in at /Login.
3. Use the dashboard to manage your Products.
```

---

## 🗄️ Database Setup

The project uses **Entity Framework Core** with a Code-First approach.

Connection string is configured in `appsettings.Development.json`:

```json
"Database": {
  "ConnectionString": "Server=.;Database=StoreManagementSystem;Trusted_Connection=True;Encrypt=False"
}
```
Make sure to put your own server address in the connection string!

To create and seed the database in PMC:

```bash
Update-Database
```


*Built as part of the **ASP.NET Advanced** course.*

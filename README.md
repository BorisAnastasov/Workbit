# Workbit HCM

Workbit HCM is a **Human Capital Management (HCM) platform** built with ASP.NET Core MVC.  
It helps organizations manage employees, departments, payroll, attendance, and budgets, with **role-based dashboards for CEOs, Managers, Employees, and Admins**.

---

## Features
- **Role-specific dashboards** for each user type.
- **Attendance tracking** with filters by role and date.
- **Payroll management** with salaries, bonuses, and taxes.
- **Department budgeting** with allocation and tracking.
- **Custom error pages** (403, 404, 500) for better UX.
- **Responsive UI** using Bootstrap 5.

---

## Database Schema

![ERD](docs/screenshots/screenshot_21.png)

### Tables Overview
- **ApplicationUser** – Base user with identity authentication.
- **Ceo, Manager, Employee** – Role-specific profile tables linked to ApplicationUser.
- **AttendanceEntry** – Tracks check-ins and check-outs.
- **Payment** – Stores salary, bonuses, and tax deductions.
- **Company & Department** – Defines the organizational structure.
- **Job & DepartmentBudget** – Job assignments and budget allocations.

---

## Screenshots

### Home & Authentication
![Home](docs/screenshots/screenshot_1.png) ![Privacy](docs/screenshots/screenshot_2.png)  
![Login](docs/screenshots/screenshot_3.png) ![Register](docs/screenshots/screenshot_4.png)

### CEO Dashboard & Modules
![CEO Dashboard](docs/screenshots/screenshot_5.png) ![Departments](docs/screenshots/screenshot_6.png)  
![Attendance](docs/screenshots/screenshot_7.png) ![Payments](docs/screenshots/screenshot_8.png)

### Manager & Employee Modules
![Manager Dashboard](docs/screenshots/screenshot_9.png) ![Team Management](docs/screenshots/screenshot_10.png)  
![Jobs](docs/screenshots/screenshot_11.png) ![Manager Payments](docs/screenshots/screenshot_12.png)  
![Employee Dashboard](docs/screenshots/screenshot_13.png) ![Employee Payments](docs/screenshots/screenshot_14.png)

### Admin Panel & Payment History
![Admin Users](docs/screenshots/screenshot_15.png) ![Company Payment History](docs/screenshots/screenshot_16.png)

### Error Pages
![403](docs/screenshots/screenshot_20.png) ![404](docs/screenshots/screenshot_18.png)  
![500](docs/screenshots/screenshot_19.png)

---

## Installation

1. **Clone Repository**
```bash
git clone https://github.com/your-repo/workbit-hcm.git
cd workbit-hcm
```

2. **Setup Database**
- Update connection string in `appsettings.json`.
- Apply migrations:
```bash
dotnet ef database update
```

3. **Run Application**
```bash
dotnet run
```

The app will run at `http://localhost:5000`.

---

## Technologies
- **ASP.NET Core MVC**
- **Entity Framework Core**
- **SQL Server**
- **Bootstrap 5**
- **Identity Authentication**

---

## License
Licensed under the MIT License. See the `LICENSE` file for more information.

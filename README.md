# CivicFix 🏙️

**CivicFix** is a web application designed to help citizens report and track local civic issues (such as road problems, electricity issues, or water supply concerns) directly to the relevant authorities.

## 🚀 Technologies Used

- **Backend**: C# using [ASP.NET Core MVC](https://dotnet.microsoft.com/en-us/apps/aspnet/mvc)
- **Frontend**: HTML5, CSS3, JavaScript, and Razor Pages
- **Database**: SQL Server (LocalDB)
- **Styling**: Vanilla CSS (Custom styles)

## 🛠️ Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download) (or latest)
- [SQL Server Express LocalDB](https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb)
- [VS Code](https://code.visualstudio.com/) or [Visual Studio 2022](https://visualstudio.microsoft.com/)

### Running the Application (VS Code)

1. **Clone the repository**:
   ```bash
   git clone <your-repository-url>
   ```
2. **Navigate to the project folder**:
   ```bash
   cd CivicFix
   ```
3. **Restore dependencies**:
   ```bash
   dotnet restore
   ```
4. **Run the app**:
   ```bash
   dotnet watch run
   ```
5. Open your browser to the URL displayed in the terminal (usually `https://localhost:7285`).

## 📁 Project Structure

- `Controllers/`: Handles the logic for routing and processing requests.
- `Models/`: Data structures representing Users, Complaints, etc.
- `Views/`: User interface templates (Razor/HTML).
- `wwwroot/`: Static files (CSS, JS, Images).

## 🤝 Contributing

This is a community-focused project. Feel free to fork, report bugs, or submit pull requests!

---
*Built with ❤️ to improve our cities.*

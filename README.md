# Digital Wellbeing

A comprehensive ASP.NET web application designed to help users manage and improve their digital wellness by tracking focus sessions, monitoring app usage, setting reminders, and customizing their experience.

## 📋 Overview

Digital Wellbeing is a web-based application that promotes healthy digital habits by providing tools to:
- Track and manage focus sessions with a built-in timer
- Monitor application usage statistics
- Set and manage custom reminders
- Customize user preferences including theme settings
- View comprehensive dashboards with usage analytics

## ✨ Features

### 🔐 User Authentication
- **User Registration**: Secure account creation with password encryption
- **User Login**: Session-based authentication system
- **Logout**: Secure session termination

### 📊 Dashboard
- **Focus Mode Summary**: View total focus sessions and time spent
- **App Usage Statistics**: Track time spent on different applications
- **Visual Data Display**: GridView components for easy data visualization

### 🎯 Focus Mode
- **Start/Pause/End Sessions**: Control focus sessions with intuitive buttons
- **Real-time Timer**: Track session duration in real-time
- **Session History**: View past focus sessions with timestamps and durations
- **Automatic Session Management**: Handles ongoing sessions across page reloads

### ⏰ Reminders
- **Create Reminders**: Set custom reminders with title and date/time
- **View Reminders**: See all upcoming reminders in a sorted list
- **Edit Reminders**: Modify existing reminder details
- **Delete Reminders**: Remove completed or unwanted reminders

### ⚙️ Settings
- **Theme Customization**: Choose between light and dark themes
- **Reminder Preferences**: Enable or disable reminder notifications
- **Persistent Settings**: User preferences saved to database
- **Real-time Theme Application**: Theme changes apply immediately

## 🛠️ Technologies Used

- **Framework**: ASP.NET Web Forms (.NET Framework 4.8)
- **Language**: C#
- **Database**: SQL Server LocalDB (MDF file)
- **Frontend**: HTML, CSS, JavaScript
- **IDE**: Visual Studio 2022
- **Build Tools**: MSBuild, Roslyn Compiler

## 📋 Prerequisites

Before running this application, ensure you have:

1. **Visual Studio 2019 or later** with the following workloads:
   - ASP.NET and web development
   - .NET desktop development

2. **SQL Server LocalDB** (typically included with Visual Studio)

3. **.NET Framework 4.8** or later

## 🚀 Installation & Setup

### 1. Clone the Repository
```bash
git clone https://github.com/pkrockz/DigitalWellbeing.git
cd DigitalWellbeing
```

### 2. Extract Application Files
Extract the `DGWellbing.7z` archive to access the application files:
```bash
7z x DGWellbing.7z
```

### 3. Open in Visual Studio
1. Navigate to the `DGWellbing` folder
2. Open `DGWellbing.sln` in Visual Studio

### 4. Restore NuGet Packages
Visual Studio should automatically restore packages. If not:
- Right-click on Solution in Solution Explorer
- Select "Restore NuGet Packages"

### 5. Configure Database

The application uses SQL Server LocalDB with a database file located at:
```
DGWellbing/App_Data/DGWellbeingDB.mdf
```

**Database Schema:**

The database includes the following tables:
- **Users**: Stores user credentials and settings
- **FocusSessions**: Tracks focus mode sessions
- **FocusLogs**: Detailed logs of focus activities
- **AppUsageLogs**: Records application usage data
- **Reminders**: Stores user-created reminders

**Connection String** (configured in Web.config):
```xml
<connectionStrings>
  <add name="DGWellbeingDB"
       connectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\DGWellbeingDB.mdf;Integrated Security=True"
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

### 6. Run the Application

1. Press `F5` or click the "Start" button in Visual Studio
2. The application will open in your default browser at `https://localhost:44308/`
3. Start by registering a new account on the Registration page

## 📁 Project Structure

```
DGWellbing/
├── App_Data/                  # Database files
│   ├── DGWellbeingDB.mdf
│   └── DGWellbeingDB_log.ldf
├── bin/                       # Compiled binaries (auto-generated)
├── obj/                       # Build artifacts (auto-generated)
├── packages/                  # NuGet packages
├── Properties/                # Assembly information
│   └── AssemblyInfo.cs
├── Dashboard.aspx            # Dashboard page (view)
├── Dashboard.aspx.cs         # Dashboard logic
├── Focus.aspx                # Focus mode page (view)
├── Focus.aspx.cs             # Focus mode logic
├── Login.aspx                # Login page (view)
├── Login.aspx.cs             # Login logic
├── Register.aspx             # Registration page (view)
├── Register.aspx.cs          # Registration logic
├── Reminders.aspx            # Reminders page (view)
├── Reminders.aspx.cs         # Reminders logic
├── Settings.aspx             # Settings page (view)
├── Settings.aspx.cs          # Settings logic
├── Site.Master               # Master page template
├── Site.Master.cs            # Master page logic
├── styles.css                # Light theme styles
├── dark-theme.css            # Dark theme styles
├── Web.config                # Application configuration
├── packages.config           # NuGet package references
└── DGWellbing.csproj        # Project file
```

## 🎨 Themes

The application supports two themes:

1. **Light Theme** (`styles.css`): Default clean and bright interface
2. **Dark Theme** (`dark-theme.css`): Eye-friendly dark color scheme

Users can switch themes from the Settings page, and the preference is persisted in the database.

## 🔒 Security Notes

⚠️ **Important**: This application uses Base64 encoding for password storage, which is **NOT secure** for production use. For production environments, implement proper password hashing using:
- BCrypt
- PBKDF2
- Argon2
- Or ASP.NET Identity with built-in security features

## 📝 Usage Guide

### Getting Started
1. **Register**: Create a new account with your name, email, and password
2. **Login**: Access your account using your credentials
3. **Dashboard**: View your focus statistics and app usage summary

### Using Focus Mode
1. Navigate to the **Focus** page from the navigation menu
2. Click **Start Focus** to begin a new session
3. The timer will track your session duration
4. Click **End Focus** when you're done to save the session
5. View your session history in the grid below

### Managing Reminders
1. Go to the **Reminders** page
2. Enter a reminder title and date/time (format: YYYY-MM-DD HH:MM)
3. Click **Add Reminder** to save
4. Use Edit/Delete options in the grid to manage reminders

### Customizing Settings
1. Visit the **Settings** page
2. Toggle reminder notifications on/off
3. Select your preferred theme (Light/Dark)
4. Click **Save Settings** to apply changes
5. Use **Logout** button to end your session

## 🤝 Contributing

Contributions are welcome! To contribute:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📄 License

This project is available for educational and personal use.

## 👨‍💻 Developer

Developed by pkrockz

## 🐛 Known Issues

- Password encryption uses Base64 (should be upgraded to proper hashing)
- Session management could be enhanced with timeout handling
- Database migrations not included (manual schema setup required)

## 🔮 Future Enhancements

- Implement proper password hashing
- Add email verification for registration
- Include data export functionality
- Add mobile responsiveness
- Implement real-time notifications
- Add charts and graphs for better data visualization
- Include password reset functionality
- Add multi-language support

## 📞 Support

For issues, questions, or suggestions, please open an issue on GitHub.

---

**Note**: This application is designed for educational purposes and local development. Additional security hardening is recommended before deploying to production environments.
# Database Schema Documentation

This document describes the database structure for the Digital Wellbeing application.

## Overview

The application uses **SQL Server LocalDB** with a database file located at:
```
DGWellbing/App_Data/DGWellbeingDB.mdf
```

**Connection String:**
```
Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\DGWellbeingDB.mdf;Integrated Security=True
```

## Database Tables

### 1. Users

Stores user account information and settings.

**Table Structure:**
```sql
CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    Password NVARCHAR(MAX) NOT NULL,  -- Base64 encoded (upgrade recommended)
    EnableReminders BIT DEFAULT 1,
    Theme NVARCHAR(20) DEFAULT 'Light',
    CreatedAt DATETIME DEFAULT GETDATE()
)
```

**Columns:**
- `UserId` (INT, Primary Key): Auto-incrementing unique identifier
- `Name` (NVARCHAR(100)): User's full name
- `Email` (NVARCHAR(100)): User's email address (unique)
- `Password` (NVARCHAR(MAX)): User's password (currently Base64 encoded)
- `EnableReminders` (BIT): Whether reminders are enabled (1 = enabled, 0 = disabled)
- `Theme` (NVARCHAR(20)): User's preferred theme ('Light' or 'Dark')
- `CreatedAt` (DATETIME): Account creation timestamp

**Usage:**
- Referenced by: `FocusSessions`, `FocusLogs`, `AppUsageLogs`, `Reminders`
- Used in: Login, Registration, Settings pages

**Security Note:**
⚠️ Passwords are currently stored using Base64 encoding, which is NOT secure. For production use, implement proper password hashing using BCrypt, PBKDF2, or Argon2.

---

### 2. FocusSessions

Tracks user focus mode sessions with start/end times and duration.

**Table Structure:**
```sql
CREATE TABLE FocusSessions (
    SessionId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    StartTime DATETIME NOT NULL,
    EndTime DATETIME NULL,
    Duration INT NULL,  -- Duration in seconds
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
)
```

**Columns:**
- `SessionId` (INT, Primary Key): Auto-incrementing unique identifier
- `UserId` (INT, Foreign Key): Reference to Users table
- `StartTime` (DATETIME): When the focus session started
- `EndTime` (DATETIME, nullable): When the focus session ended (NULL for ongoing sessions)
- `Duration` (INT, nullable): Total session duration in seconds
- `CreatedAt` (DATETIME): Record creation timestamp

**Usage:**
- Used in: Focus page, Dashboard
- Queries:
  - Count total sessions per user
  - Calculate total focus time
  - Display session history
  - Check for ongoing sessions

**Business Rules:**
- `EndTime` is NULL while a session is active
- `Duration` is calculated when session ends: `DATEDIFF(SECOND, StartTime, EndTime)`
- Only one active session (EndTime IS NULL) per user at a time

---

### 3. FocusLogs

Detailed logs of focus activities and durations.

**Table Structure:**
```sql
CREATE TABLE FocusLogs (
    LogId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    Duration INT NOT NULL,  -- Duration in minutes
    LogDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
)
```

**Columns:**
- `LogId` (INT, Primary Key): Auto-incrementing unique identifier
- `UserId` (INT, Foreign Key): Reference to Users table
- `Duration` (INT): Focus duration in minutes
- `LogDate` (DATETIME): When the log entry was created

**Usage:**
- Used in: Dashboard for aggregating focus time statistics
- Queries:
  - Calculate total focus time per user
  - Aggregate focus data for reports

**Note:**
This table appears to store duration in minutes, while FocusSessions stores it in seconds. Consider standardizing the time unit across tables.

---

### 4. AppUsageLogs

Tracks time spent on different applications.

**Table Structure:**
```sql
CREATE TABLE AppUsageLogs (
    LogId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    AppName NVARCHAR(100) NOT NULL,
    UsageTime INT NOT NULL,  -- Usage time in minutes
    LogDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
)
```

**Columns:**
- `LogId` (INT, Primary Key): Auto-incrementing unique identifier
- `UserId` (INT, Foreign Key): Reference to Users table
- `AppName` (NVARCHAR(100)): Name of the application
- `UsageTime` (INT): Time spent on the app in minutes
- `LogDate` (DATETIME): When the usage was logged

**Usage:**
- Used in: Dashboard for displaying app usage statistics
- Queries:
  - Display app usage by user
  - Track which apps consume most time
  - Generate usage reports

---

### 5. Reminders

Stores user-created reminders with titles and scheduled times.

**Table Structure:**
```sql
CREATE TABLE Reminders (
    ReminderID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    Title NVARCHAR(200) NOT NULL,
    ReminderDateTime DATETIME NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserID) REFERENCES Users(UserId)
)
```

**Columns:**
- `ReminderID` (INT, Primary Key): Auto-incrementing unique identifier
- `UserID` (INT, Foreign Key): Reference to Users table
- `Title` (NVARCHAR(200)): Reminder title/description
- `ReminderDateTime` (DATETIME): When the reminder should trigger
- `CreatedAt` (DATETIME): When the reminder was created

**Usage:**
- Used in: Reminders page
- Queries:
  - Display all reminders for a user (ordered by ReminderDateTime)
  - Add new reminders
  - Edit existing reminders
  - Delete reminders

**Note:**
The foreign key references `Users(UserId)` but the column is named `UserID` (inconsistent casing). Consider standardizing column naming conventions.

---

## Entity Relationships

```
Users (1) ----< (Many) FocusSessions
Users (1) ----< (Many) FocusLogs
Users (1) ----< (Many) AppUsageLogs
Users (1) ----< (Many) Reminders
```

**Relationship Details:**
- One user can have many focus sessions
- One user can have many focus logs
- One user can have many app usage logs
- One user can have many reminders

**Referential Integrity:**
All foreign key constraints enforce referential integrity. Deleting a user would cascade to related records (if cascade delete is configured) or prevent deletion if related records exist.

---

## Common Queries

### User Authentication
```sql
-- Login verification
SELECT UserId, Name, Email, Theme, EnableReminders
FROM Users
WHERE Email = @Email AND Password = @Password
```

### Dashboard Statistics
```sql
-- Total focus sessions
SELECT COUNT(*) as TotalSessions
FROM FocusSessions
WHERE UserId = @UserId

-- Total focus time
SELECT SUM(Duration) as TotalMinutes
FROM FocusLogs
WHERE UserId = @UserId

-- App usage summary
SELECT AppName, UsageTime
FROM AppUsageLogs
WHERE UserId = @UserId
```

### Focus Mode
```sql
-- Check for ongoing session
SELECT StartTime
FROM FocusSessions
WHERE UserId = @UserId AND EndTime IS NULL

-- Start new session
INSERT INTO FocusSessions (UserId, StartTime)
VALUES (@UserId, GETDATE())

-- End session
UPDATE FocusSessions
SET EndTime = @EndTime, Duration = @Duration
WHERE UserId = @UserId AND EndTime IS NULL

-- Get session history
SELECT StartTime, EndTime,
       FORMAT(DATEADD(SECOND, Duration, 0), 'HH:mm:ss') AS Duration
FROM FocusSessions
WHERE UserId = @UserId
ORDER BY StartTime DESC
```

### Reminders
```sql
-- Get all reminders
SELECT ReminderID, Title, ReminderDateTime
FROM Reminders
WHERE UserID = @UserID
ORDER BY ReminderDateTime ASC

-- Add reminder
INSERT INTO Reminders (UserID, Title, ReminderDateTime)
VALUES (@UserID, @Title, @ReminderDateTime)

-- Delete reminder
DELETE FROM Reminders
WHERE ReminderID = @ReminderID
```

---

## Database Initialization

### Creating the Database

The database is automatically created by LocalDB when the application first runs. However, you may need to create the tables manually.

### Table Creation Scripts

Run these scripts in order to set up the database:

```sql
-- 1. Create Users table
CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    Password NVARCHAR(MAX) NOT NULL,
    EnableReminders BIT DEFAULT 1,
    Theme NVARCHAR(20) DEFAULT 'Light',
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- 2. Create FocusSessions table
CREATE TABLE FocusSessions (
    SessionId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    StartTime DATETIME NOT NULL,
    EndTime DATETIME NULL,
    Duration INT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

-- 3. Create FocusLogs table
CREATE TABLE FocusLogs (
    LogId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    Duration INT NOT NULL,
    LogDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

-- 4. Create AppUsageLogs table
CREATE TABLE AppUsageLogs (
    LogId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    AppName NVARCHAR(100) NOT NULL,
    UsageTime INT NOT NULL,
    LogDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

-- 5. Create Reminders table
CREATE TABLE Reminders (
    ReminderID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    Title NVARCHAR(200) NOT NULL,
    ReminderDateTime DATETIME NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserID) REFERENCES Users(UserId)
);
```

### Sample Data (Optional)

For testing purposes, you can insert sample data:

```sql
-- Sample user
INSERT INTO Users (Name, Email, Password, Theme, EnableReminders)
VALUES ('Test User', 'test@example.com', 'dGVzdDEyMzQ=', 'Light', 1);
-- Password is Base64 for 'test1234'
```

---

## Database Maintenance

### Backup

To backup the database:
1. Navigate to `DGWellbing/App_Data/`
2. Copy `DGWellbeingDB.mdf` and `DGWellbeingDB_log.ldf` to a safe location

### Restore

To restore:
1. Stop the application and Visual Studio
2. Replace the `.mdf` and `.ldf` files in `App_Data/` with backup copies
3. Restart the application

### Reset Database

To start fresh:
1. Delete `DGWellbeingDB.mdf` and `DGWellbeingDB_log.ldf`
2. Create a new database file
3. Run the table creation scripts above

---

## Recommendations for Future Improvements

### Security
1. **Password Hashing**: Replace Base64 encoding with BCrypt or Argon2
2. **Stored Procedures**: Use stored procedures instead of inline SQL
3. **Data Encryption**: Encrypt sensitive data at rest

### Performance
1. **Indexes**: Add indexes on frequently queried columns:
   - `Users.Email`
   - `FocusSessions.UserId`
   - `Reminders.UserID` and `ReminderDateTime`

2. **Composite Indexes**: For common query patterns:
   ```sql
   CREATE INDEX IX_FocusSessions_UserDate ON FocusSessions(UserId, StartTime DESC);
   CREATE INDEX IX_Reminders_UserDate ON Reminders(UserID, ReminderDateTime);
   ```

### Data Integrity
1. **Constraints**: Add CHECK constraints:
   - Ensure `Duration` is positive
   - Ensure `EndTime` > `StartTime`

2. **Default Values**: Add more default values for consistency

3. **Naming Consistency**: Standardize column naming (UserId vs UserID)

### Auditing
1. **UpdatedAt Column**: Add to track last modification time
2. **Soft Deletes**: Add `IsDeleted` flag instead of hard deletes
3. **Audit Log Table**: Track all data changes

---

## Troubleshooting

### Common Issues

**Issue**: "Cannot attach database file"
- **Solution**: Ensure SQL Server LocalDB is installed and running

**Issue**: "Database file is in use"
- **Solution**: Close all Visual Studio instances and restart

**Issue**: "Foreign key constraint violation"
- **Solution**: Ensure referenced user exists before inserting related records

**Issue**: "Column name inconsistency (UserId vs UserID)"
- **Solution**: Update queries to match actual column names in database

---

## Developer Notes

- Always use parameterized queries to prevent SQL injection
- Dispose of database connections properly using `using` statements
- Handle `DBNull` values when reading nullable columns
- Time units vary between tables (seconds vs minutes) - be careful with calculations
- Session management relies on `EndTime IS NULL` to identify active sessions

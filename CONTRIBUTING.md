# Contributing to Digital Wellbeing

Thank you for your interest in contributing to Digital Wellbeing! This document provides guidelines and instructions for contributing to this project.

## Table of Contents

- [Code of Conduct](#code-of-conduct)
- [Getting Started](#getting-started)
- [How to Contribute](#how-to-contribute)
- [Development Guidelines](#development-guidelines)
- [Coding Standards](#coding-standards)
- [Pull Request Process](#pull-request-process)
- [Bug Reports](#bug-reports)
- [Feature Requests](#feature-requests)

## Code of Conduct

By participating in this project, you agree to maintain a respectful and inclusive environment for all contributors. Please:

- Be respectful and considerate in your communications
- Welcome newcomers and help them get started
- Accept constructive criticism gracefully
- Focus on what is best for the project and community

## Getting Started

1. **Fork the Repository**
   ```bash
   # Click the "Fork" button on GitHub
   ```

2. **Clone Your Fork**
   ```bash
   git clone https://github.com/YOUR_USERNAME/DigitalWellbeing.git
   cd DigitalWellbeing
   ```

3. **Set Up Development Environment**
   - Install Visual Studio 2019 or later with ASP.NET workload
   - Extract the `DGWellbing.7z` archive
   - Open `DGWellbing/DGWellbing.sln` in Visual Studio
   - Restore NuGet packages

4. **Create a Branch**
   ```bash
   git checkout -b feature/your-feature-name
   ```

## How to Contribute

### Types of Contributions

We welcome various types of contributions:

- **Bug Fixes**: Fix existing issues or bugs
- **New Features**: Add new functionality
- **Documentation**: Improve or add documentation
- **Code Quality**: Refactor code, improve performance
- **Tests**: Add or improve test coverage
- **UI/UX**: Enhance user interface and experience

### Before You Start

1. **Check Existing Issues**: Look for existing issues or create a new one to discuss your proposed changes
2. **Discuss Large Changes**: For significant changes, open an issue first to discuss your approach
3. **Stay Focused**: Keep your contributions focused on a single issue or feature

## Development Guidelines

### Setting Up the Database

The application uses SQL Server LocalDB. The database file is located at `DGWellbing/App_Data/DGWellbeingDB.mdf`.

**Database Tables:**
- `Users` - User accounts and settings
- `FocusSessions` - Focus mode session tracking
- `FocusLogs` - Detailed focus activity logs
- `AppUsageLogs` - Application usage data
- `Reminders` - User reminders

### Building the Project

1. Open the solution in Visual Studio
2. Press `Ctrl+Shift+B` or go to Build > Build Solution
3. Fix any compilation errors before proceeding

### Running the Application

1. Press `F5` or click Start
2. The application will open at `https://localhost:44308/`
3. Test your changes thoroughly

## Coding Standards

### C# Code Style

- **Naming Conventions**:
  - Use PascalCase for class names, method names, and properties
  - Use camelCase for local variables and parameters
  - Use meaningful, descriptive names

- **Code Organization**:
  - One class per file
  - Organize using statements alphabetically
  - Group related methods together

- **Comments**:
  - Add comments for complex logic
  - Use XML documentation comments for public methods
  - Keep comments up-to-date with code changes

- **Error Handling**:
  - Use try-catch blocks appropriately
  - Provide meaningful error messages
  - Log errors where appropriate

### ASP.NET Web Forms Guidelines

- **Page Structure**:
  - Keep code-behind files clean and focused
  - Separate business logic from presentation logic
  - Use master pages for consistent layout

- **Database Access**:
  - Always use parameterized queries to prevent SQL injection
  - Dispose of database connections properly using `using` statements
  - Handle database exceptions gracefully

- **Security**:
  - Never store passwords in plain text
  - Validate user input on both client and server side
  - Use HTTPS for all communications

### CSS Style

- Use meaningful class names
- Keep selectors simple and efficient
- Organize styles logically (layout, typography, components)
- Maintain both light and dark theme styles consistently

## Pull Request Process

### Before Submitting

1. **Test Thoroughly**:
   - Test all functionality affected by your changes
   - Test on different browsers if UI changes were made
   - Verify the application builds without errors

2. **Update Documentation**:
   - Update README.md if you've changed functionality
   - Add code comments where necessary
   - Update this CONTRIBUTING.md if you've changed the development process

3. **Clean Up**:
   - Remove debug code and console logs
   - Remove commented-out code
   - Ensure proper code formatting

### Submitting the Pull Request

1. **Commit Your Changes**:
   ```bash
   git add .
   git commit -m "Brief description of changes"
   ```

2. **Push to Your Fork**:
   ```bash
   git push origin feature/your-feature-name
   ```

3. **Create Pull Request**:
   - Go to the original repository on GitHub
   - Click "New Pull Request"
   - Select your fork and branch
   - Fill in the PR template with:
     - Clear description of changes
     - Related issue number (if applicable)
     - Screenshots (for UI changes)
     - Testing performed

4. **PR Title Format**:
   - Use clear, descriptive titles
   - Examples:
     - `Fix: Resolve login validation issue`
     - `Feature: Add password reset functionality`
     - `Docs: Update installation instructions`
     - `Refactor: Improve database connection handling`

### Review Process

1. A maintainer will review your PR
2. Address any requested changes
3. Once approved, your PR will be merged

## Bug Reports

When reporting bugs, please include:

1. **Description**: Clear description of the bug
2. **Steps to Reproduce**: Detailed steps to reproduce the issue
3. **Expected Behavior**: What you expected to happen
4. **Actual Behavior**: What actually happened
5. **Environment**:
   - Visual Studio version
   - .NET Framework version
   - SQL Server version
   - Browser (if applicable)
6. **Screenshots**: If applicable, add screenshots

### Bug Report Template

```markdown
**Description**
A clear and concise description of the bug.

**Steps to Reproduce**
1. Go to '...'
2. Click on '...'
3. Enter '...'
4. See error

**Expected Behavior**
What should happen.

**Actual Behavior**
What actually happens.

**Environment**
- Visual Studio: [version]
- .NET Framework: [version]
- Browser: [name and version]

**Screenshots**
If applicable, add screenshots.

**Additional Context**
Any other relevant information.
```

## Feature Requests

We welcome feature requests! Please:

1. **Check Existing Requests**: Search issues to see if someone already requested it
2. **Provide Context**: Explain why this feature would be useful
3. **Describe the Feature**: Be specific about what you'd like to see
4. **Consider Alternatives**: Have you considered alternative solutions?

### Feature Request Template

```markdown
**Feature Description**
A clear description of the feature.

**Problem it Solves**
What problem does this feature address?

**Proposed Solution**
How do you envision this feature working?

**Alternative Solutions**
Have you considered any alternative approaches?

**Additional Context**
Any other relevant information, mockups, or examples.
```

## Development Best Practices

### Security Considerations

- **Never commit**:
  - Passwords or API keys
  - Database connection strings with credentials
  - Personal information
  - Production configuration files

- **Password Security**:
  - The current implementation uses Base64 (NOT secure)
  - Contributors working on authentication should implement proper hashing (BCrypt, PBKDF2, Argon2)

- **SQL Injection Prevention**:
  - Always use parameterized queries
  - Never concatenate user input into SQL strings

- **Input Validation**:
  - Validate all user input on the server side
  - Sanitize data before displaying or storing

### Performance Tips

- Use `using` statements for IDisposable objects
- Minimize database round trips
- Cache frequently accessed data when appropriate
- Optimize database queries

### Testing

While this project doesn't currently have automated tests:
- Manually test all code paths
- Test edge cases and error conditions
- Verify database operations work correctly
- Test UI on different browsers and screen sizes

## Questions?

If you have questions about contributing:
- Open an issue with the "question" label
- Review existing documentation
- Check closed issues for similar questions

Thank you for contributing to Digital Wellbeing! Your efforts help make digital wellness more accessible to everyone.

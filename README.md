# Interactive Data Visualization Web Application

## Overview
This web application is built using DotNet Core MVC to manage and visualize a large dataset. It features user authentication, data pagination, search functionality, and interactive data visualizations using JavaScript libraries.

## Features
- **Data Management**:
  - View paginated data (50-100 elements per page)
  - Search functionality to filter records
  - Add new records to the database
- **User Authentication**:
  - User registration and login
  - Secure password hashing
  - Profile photo upload to Minio object storage
- **Data Visualization**:
  - Interactive charts and graphs powered by JavaScript libraries (Plotly/D3.js)
- **Technical Implementation**:
  - EntityFramework for database access
  - PostgreSQL database backend
  - Containerized deployment with Docker

## Technology Stack
- **Backend**: .NET 8.0 with ASP.NET Core MVC
- **Database**: PostgreSQL with EntityFrameworkCore 8.0.2
- **Authentication**: ASP.NET Identity 8.0.2
- **Storage**: Minio 6.0.1 Object Storage
- **Frontend**: HTML, CSS, JavaScript
- **Visualization**: Plotly/D3.js
- **Deployment**: Docker, CI/CD pipeline with GitLab CI
- **Configuration Management**: Ansible

## Project Dependencies
```xml
<ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.Authorization" Version="8.0.2" />
    <PackageReference Include="Microsoft.AspNetCore.Identity.EntityFrameworkCore" Version="8.0.2" />
    <PackageReference Include="Microsoft.EntityFrameworkCore" Version="8.0.2" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="8.0.2">
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
      <PrivateAssets>all</PrivateAssets>
    </PackageReference>
    <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="8.0.2" />
    <PackageReference Include="Npgsql.EntityFrameworkCore.PostgreSQL" Version="8.0.2" />
    <PackageReference Include="Minio" Version="6.0.1" />
</ItemGroup>
```

## Development Setup

### Prerequisites
- .NET 8.0 SDK
- PostgreSQL
- Docker
- Git

### Getting Started
1. Clone the repository:
   ```
   git clone https://gitlab.cnsalab.net/your-username/project-name.git
   ```

2. Navigate to the project directory:
   ```
   cd project-name
   ```

3. Install dependencies:
   ```
   dotnet restore
   ```

4. Set up your connection strings in `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Database=yourdbname;Username=youruser;Password=yourpassword"
     },
     "MinioConfiguration": {
       "Endpoint": "your-minio-endpoint",
       "AccessKey": "your-access-key",
       "SecretKey": "your-secret-key",
       "BucketName": "profile-photos"
     }
   }
   ```

5. Run database migrations:
   ```
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

6. Run the application:
   ```
   dotnet run
   ```

7. Access the application at `http://localhost:5000`

## CI/CD Pipeline
The project uses GitLab CI/CD for automated deployment with the following stages:

### Build Stage
- Uses the latest .NET SDK Docker image
- Restores NuGet packages
- Builds the application
- Publishes the application in Release configuration

### Test Stage
- Runs unit tests to ensure code quality

### Deploy Stage
- Only runs on the main branch
- Uses SSH to connect to deployment servers
- Pulls the latest code from git
- Uses Ansible for configuration management and deployment
- Targets multiple servers (half-server-2, half-server-3)

### Key Files
- `.gitlab-ci.yml`: Defines the CI/CD pipeline
- `dockerfile`: Used for containerizing the application
- `ansible_project/`: Contains Ansible playbooks for deployment
- `inventory.ini`: Defines the deployment targets
- `nrc-20250203.sql`: Database initialization script

## Infrastructure
- Application is served through a web application proxy
- Deployed across multiple instances for reliability
- Uses Ansible for configuration management

## Project Structure
```
/
├── Controllers/         # MVC Controllers
├── Models/              # Data models and ViewModels
├── Services/            # Business logic and services
├── Data/                # DbContext and database configurations
├── Views/               # Razor views
├── wwwroot/             # Static files (CSS, JS, images)
├── Areas/               # Feature areas (e.g., Identity)
├── Migrations/          # Database migrations
├── Properties/          # Application properties and launch settings
├── ansible_project/     # Ansible playbooks and configurations
├── dockerfile           # Docker container definition
└── .gitlab-ci.yml       # CI/CD pipeline configuration
```

## Database
The application uses the NRC database (nrc-20250203.sql) which contains incident data with over 1000 records.

## Project Management
- Issue tracking via GitLab issues
- Feature branches and merge requests for development workflow
- CI/CD pipeline for automated testing and deployment

## Contributors
- Byamungu Ruhigita
- Anthony Harvey, Kyle Nolt, Nathaniel Grines

## License
MIT License

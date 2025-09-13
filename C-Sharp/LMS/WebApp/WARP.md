`
# WARP.md

This file provides guidance to WARP (warp.dev) when working with code in this repository.
``

Project: LMS/WebApp (ASP.NET Core 8 Razor Pages)

Quick commands (PowerShell on Windows)
- Restore: dotnet restore .\WebApp.sln
- Build (Debug): dotnet build .\WebApp.sln -c Debug
- Build (Release): dotnet build .\WebApp.sln -c Release
- Run (Development):
  - $env:ASPNETCORE_ENVIRONMENT = "Development"
  - dotnet run --project .\WebApp\WebApp.csproj
- Hot reload: dotnet watch --project .\WebApp\WebApp.csproj run
- Trust HTTPS dev cert (first time): dotnet dev-certs https --trust
- Format/lint C# sources: dotnet format .\WebApp.sln
- Tests: No test projects detected. If/when tests are added:
  - Run all tests: dotnet test .\WebApp.sln
  - Run a single test by name: dotnet test .\path\to\Your.Tests.csproj --filter "FullyQualifiedName~Namespace.ClassName.TestMethod"

Environment and configuration
- Target framework: net8.0
- Configuration files: WebApp/appsettings.json and WebApp/appsettings.Development.json
- API base URL is configured in two places:
  1) Razor Pages via IConfiguration: ApiSettings:BaseUrl in appsettings.json (consumed in views like @Configuration["ApiSettings:BaseUrl"])
  2) A separate WWW root file: WebApp/wwwroot/js/apiConfig.js (defines window-level baseUrl but most pages read from appsettings)
- Keep ApiSettings:BaseUrl aligned with your backend service. Current default is https://localhost:7020.

How to run locally
1) Ensure your backend API is running at https://localhost:7020 (or update ApiSettings:BaseUrl accordingly).
2) In this repo root:
   - dotnet restore .\WebApp.sln
   - $env:ASPNETCORE_ENVIRONMENT = "Development"
   - dotnet run --project .\WebApp\WebApp.csproj
3) The app redirects “/” to “/Login”. Authentication and page access are managed on the client side (see Architecture below).

High-level architecture and flow
- Hosting and middleware (WebApp/Program.cs)
  - Minimal hosting model. Registers Razor Pages.
  - Adds cookie authentication with scheme "Identity.Application" (LoginPath/AccessDeniedPath => /Login). Middleware is enabled (UseAuthentication/UseAuthorization), but the pages themselves enforce access primarily via client-side checks, not server-side [Authorize] attributes.
  - In non-development, uses exception handler and HSTS; always serves static files and maps Razor Pages.
  - Maps GET "/" to redirect users to "/Login".

- UI composition (Razor Pages)
  - Pages/* contains feature folders (Admin, Users, Programs, Courses, ProgramCourses, CourseResources, CourseLecturerAssignments, CourseStudentEnrollments, Templates, etc.). Code-behind files are mostly minimal; data is fetched via JavaScript from an external API.
  - Layouts under Pages/Shared:
    - _AuthLayout.cshtml: used by authentication pages (e.g., Login). Minimal chrome.
    - _Layout.cshtml: main application layout with navigation and a client-side auth gate. On load, it checks localStorage for a token and user object; if missing and not on /Login, redirects to /Login. It also wraps window.fetch to inject the Authorization: Bearer <token> header for all requests.
    - _AdminLayout.cshtml: admin dashboard chrome; includes apiConfig.js and common scripts.
  - _ViewImports.cshtml injects IConfiguration for views so pages can read @Configuration["ApiSettings:BaseUrl"].

- Data flow and API integration
  - The app is a Razor Pages UI that relies on an external REST API (configured via ApiSettings:BaseUrl) for all data. Examples:
    - Login (Pages/Login.cshtml): POSTs to {BaseUrl}/api/User/login, stores { user, token } in localStorage, and redirects based on user.role.
    - Admin dashboard and index pages (Users, Courses, etc.) fetch lists from endpoints like /api/User, /api/Course, etc., then render tables client-side.
  - Authentication token handling is entirely client-side:
    - On successful login, localStorage stores "user" and "token".
    - _Layout.cshtml patches window.fetch to attach the Authorization: Bearer token automatically.
    - This means server-side auth attributes are not currently the primary access control mechanism; the UI enforces auth state by redirecting to /Login when token/user is absent.

- Important conventions and gotchas
  - API URL source: Most pages read @Configuration["ApiSettings:BaseUrl"]. A small set of scripts (e.g., wwwroot/js/apiConfig.js and lookups.js) also consider window.apiBaseUrl. Prefer the Razor-injected configuration for consistency or expose window.apiBaseUrl from server-rendered pages if you need to use those scripts directly.
  - Port alignment: Several pages assume API routes like /api/User, /api/Course, etc. Ensure your backend exposes matching routes at the configured BaseUrl, defaulting to https://localhost:7020.
  - Cookie vs JWT: Program.cs configures cookie auth, but the app primarily relies on JWT stored in localStorage and injected into fetch. If you add server-side authorization ([Authorize] on pages/handlers), align the auth scheme with how tokens are validated on the server or add server-side validation for the JWT.

Repository pointers
- Solution: WebApp.sln (single project solution)
- Project: WebApp/WebApp.csproj (net8.0, Razor Pages)
- Entry point: WebApp/Program.cs
- Views/Pages: WebApp/Pages/* (feature directories)
- Static assets: WebApp/wwwroot/* (css/js/libs)
- App config: WebApp/appsettings*.json (contains ApiSettings:BaseUrl)


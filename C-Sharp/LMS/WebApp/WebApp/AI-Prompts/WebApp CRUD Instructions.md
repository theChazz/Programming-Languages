# WebApp CRUD Generation Instructions for ASP.NET Core Razor Pages LMS

## Use Sequential Thinking MCP

## 📋 LMS Navigation & Login Testing with Playwright

### Login Instructions
```javascript
// Use Playwright MCP to login to the LMS system
const apiBaseUrl = '@Configuration["ApiSettings:BaseUrl"]';

// Navigate to login page
await page.goto('http://localhost:5151/Login');

// Fill login form
await page.fill('#Email', 'kasgem@gmail.com');  // Admin email
await page.fill('#Password', '12345678');       // Admin password

// Submit login
await page.click('button:has-text("Login")');

// Wait for navigation to dashboard
await page.waitForURL('**/Courses/Index');

// Verify login success
const currentURL = page.url();
console.log('Logged in successfully, redirected to:', currentURL);
```

### Navigation Testing
```javascript
// Test Academic dropdown navigation
await page.click('text=Academic');
await page.click('text=Programs');  // Should navigate to /Programs
await page.waitForURL('**/Programs');

// Test Course Management dropdown
await page.click('text=Course Management');
await page.click('text=Lecturer Assignments');  // Should navigate to /CourseLecturerAssignments
await page.waitForURL('**/CourseLecturerAssignments');
```

## 🎯 **ENHANCED FEATURES FROM CONVERSATION**

### ✅ **Standardized Index Pages with Advanced Features**

#### **Stats Cards Dashboard**
```html
<!-- Stats Cards with Real-time Data -->
<div class="row mb-4">
    <div class="col-md-3">
        <div class="card border-left-primary">
            <div class="card-body">
                <div class="row align-items-center">
                    <div class="col">
                        <div class="text-xs font-weight-bold text-primary text-uppercase mb-1">Total [Entities]</div>
                        <div class="h5 mb-0 font-weight-bold text-gray-800" id="total[Entities]"></div>
                    </div>
                    <div class="col-auto">
                        <i class="fas fa-[icon] fa-2x text-primary"></i>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Additional stats cards... -->
</div>
```

#### **Advanced Filtering Section**
```html
<!-- Filter Section with Multiple Criteria -->
<div class="card mb-4">
    <div class="card-body">
        <div class="row g-3">
            <div class="col-md-3">
                <label class="form-label">Search [Entities]</label>
                <input type="text" class="form-control" id="searchInput" placeholder="Search by name...">
            </div>
            <div class="col-md-3">
                <label class="form-label">Filter by [Category]</label>
                <select class="form-select" id="[category]Filter">
                    <option value="">All [Categories]</option>
                </select>
            </div>
            <div class="col-md-3 d-flex align-items-end">
                <button class="btn btn-outline-primary w-100" onclick="applyFilters()">
                    <i class="fas fa-search"></i> Search & Filter
                </button>
            </div>
        </div>
    </div>
</div>
```

#### **Grid/List View Toggle**
```html
<!-- View Toggle with Sticky Positioning -->
<div class="d-flex justify-content-end mb-3" style="position: sticky; top: 20px; z-index: 100;">
    <div class="btn-group" role="group">
        <button type="button" class="btn btn-outline-info" id="gridViewBtn">
            <i class="fas fa-th"></i> Grid View
        </button>
        <button type="button" class="btn btn-outline-info active" id="listViewBtn">
            <i class="fas fa-list"></i> List View
        </button>
    </div>
</div>
```

### ✅ **Grouped Table Structure (Advanced Feature)**

#### **Grouped Table CSS**
```css
<style>
    .course-header-row {
        cursor: pointer;
        transition: background-color 0.2s ease;
    }
    .course-header-row:hover {
        background-color: #e3f2fd !important;
    }
    .course-resource-row {
        background-color: #f8f9fa;
    }
    .course-resource-row:hover {
        background-color: #e9ecef;
    }
    .course-resource-row td:first-child {
        border-left: 3px solid #007bff;
    }
    .btn-group .btn {
        margin-right: 2px;
    }
    .table-primary {
        background-color: #e3f2fd;
    }
    .course-subheader-row {
        background-color: #f8f9fa !important;
        border-top: 2px solid #dee2e6;
        border-bottom: 1px solid #dee2e6;
    }
    .course-subheader-row td {
        padding: 8px 12px;
        font-weight: 600;
        color: #495057;
    }
</style>
```

#### **Grouped Table HTML Structure**
```html
<!-- Grouped Table with Expandable Sections -->
<table class="table">
    <thead class="table-dark">
        <tr>
            <th>Course</th>
        </tr>
    </thead>
    <tbody id="[entity]TableBody">
        <!-- Course Header Row -->
        <tr class="table-primary course-header-row" onclick="toggleCourse[Entities]('[courseRowId]')">
            <td>
                <div class="d-flex align-items-center">
                    <i class="fas fa-graduation-cap text-primary me-2"></i>
                    <strong>Course Name</strong>
                    <small class="text-muted ms-2">(Category)</small>
                    <span class="badge bg-primary ms-2">5 resources</span>
                </div>
            </td>
        </tr>

        <!-- Subheader Row (Hidden initially) -->
        <tr class="table-secondary course-subheader-row [courseRowId]" style="display: none;">
            <td>
                <div class="row">
                    <div class="col-md-4"><strong>Title</strong></div>
                    <div class="col-md-2"><strong>Type</strong></div>
                    <div class="col-md-2"><strong>Status</strong></div>
                    <div class="col-md-2"><strong>Actions</strong></div>
                </div>
            </td>
        </tr>

        <!-- Child Rows (Hidden initially) -->
        <tr class="course-resource-row [courseRowId]" style="display: none;">
            <td>
                <div class="row align-items-center">
                    <div class="col-md-4">Resource Title</div>
                    <div class="col-md-2"><span class="badge bg-secondary">Document</span></div>
                    <div class="col-md-2"><span class="badge bg-success">Published</span></div>
                    <div class="col-md-2">
                        <div class="btn-group" role="group">
                            <a href="/[Entity]/Details/[id]" class="btn btn-info btn-sm"><i class="fas fa-eye"></i></a>
                            <a href="/[Entity]/Edit/[id]" class="btn btn-primary btn-sm"><i class="fas fa-edit"></i></a>
                            <button class="btn btn-danger btn-sm" onclick="deleteEntity([id])"><i class="fas fa-trash"></i></button>
                        </div>
                    </div>
                </div>
            </td>
        </tr>
    </tbody>
</table>
```

#### **Grouped Table JavaScript**
```javascript
// Group data by parent entity (course)
function displayTableView(entities) {
    const tableBody = document.getElementById('[entity]TableBody');

    // Group entities by course
    const groupedByCourse = entities.reduce((groups, entity) => {
        const courseId = entity.course?.id || entity.courseId || 'unknown';
        const courseName = entity.course?.courseName || 'Unknown Course';

        if (!groups[courseId]) {
            groups[courseId] = {
                course: entity.course || { id: courseId, courseName: courseName },
                entities: []
            };
        }
        groups[courseId].entities.push(entity);
        return groups;
    }, {});

    // Create HTML for grouped display
    const html = Object.values(groupedByCourse).map(courseGroup => {
        const course = courseGroup.course;
        const entities = courseGroup.entities;
        const courseRowId = `course-${course.id}`;

        // Main course row + subheader + child rows...
        return courseHtml + subheaderHtml + entitiesHtml;
    }).join('');

    tableBody.innerHTML = html;
}

// Toggle grouped sections
function toggleCourseEntities(courseRowId) {
    const entityRows = document.querySelectorAll(`.course-entity-row.${courseRowId}`);
    const subheaderRow = document.querySelector(`.course-subheader-row.${courseRowId}`);

    const isVisible = entityRows.length > 0 && entityRows[0].style.display !== 'none';

    // Toggle entity rows
    entityRows.forEach(row => {
        row.style.display = isVisible ? 'none' : 'table-row';
    });

    // Toggle subheader row
    if (subheaderRow) {
        subheaderRow.style.display = isVisible ? 'none' : 'table-row';
    }
}
```

### ✅ **Advanced JavaScript Features**

#### **Real-time Statistics**
```javascript
function updateStatistics(entities) {
    // Total entities
    document.getElementById('totalEntities').textContent = entities.length;

    // Active entities (custom logic)
    const activeCount = entities.filter(e => e.isActive).length;
    document.getElementById('activeEntities').textContent = activeCount;

    // Categories count
    const categories = [...new Set(entities.map(e => e.category).filter(Boolean))];
    document.getElementById('categoriesCount').textContent = categories.length;

    // Additional metrics...
}
```

#### **Advanced Search with Autocomplete**
```javascript
async function searchCourses(term) {
    const list = document.getElementById('courseSuggestions');
    if (!term || term.length < 2) {
        list.style.display = 'none';
        list.innerHTML = '';
        return;
    }

    try {
        const res = await fetch(`${apiBaseUrl}/api/Course/search?q=${encodeURIComponent(term)}`);
        if (!res.ok) return;

        const items = await res.json();
        list.innerHTML = items.map(c =>
            `<button type="button" class="list-group-item list-group-item-action"
                     data-id="${c.id}" data-name="${c.courseName}">${c.courseName}</button>`
        ).join('');
        list.style.display = items.length ? 'block' : 'none';

        // Add click handlers
        [...list.querySelectorAll('button')].forEach(btn => {
            btn.addEventListener('click', () => {
                document.getElementById('courseIdFilter').value = btn.getAttribute('data-id');
                document.getElementById('courseSearch').value = btn.getAttribute('data-name');
                list.style.display = 'none';
                applyFilters();
            });
        });
    } catch (error) {
        console.error('Error searching courses:', error);
    }
}
```

#### **File Size Formatting**
```javascript
function formatFileSize(bytes) {
    if (!bytes) return 'N/A';
    const sizes = ['Bytes', 'KB', 'MB', 'GB'];
    const i = Math.floor(Math.log(bytes) / Math.log(1024));
    return Math.round(bytes / Math.pow(1024, i) * 100) / 100 + ' ' + sizes[i];
}
```

### ✅ **Enhanced Progress Tracking**
```html
<!-- Progress Bar Display -->
<div class="col-md-3">
    <div class="d-flex align-items-center">
        <div class="progress flex-grow-1 me-2" style="height: 15px;">
            <div class="progress-bar ${enrollment.progress >= 100 ? 'bg-success' : enrollment.progress > 0 ? 'bg-warning' : 'bg-secondary'}"
                 role="progressbar" style="width: ${enrollment.progress || 0}%"
                 aria-valuenow="${enrollment.progress || 0}" aria-valuemin="0" aria-valuemax="100">
            </div>
        </div>
        <small class="text-nowrap">${enrollment.progress || 0}%</small>
    </div>
</div>
```

### ✅ **Enhanced Error Handling**
```javascript
async function deleteEntity(id) {
    if (!confirm('Are you sure you want to delete this entity?')) {
        return;
    }

    try {
        const response = await fetch(`${apiBaseUrl}/api/Entity/${id}`, {
            method: 'DELETE'
        });

        if (!response.ok) {
            let errorMsg = 'Failed to delete entity';
            try {
                const errorData = await response.json();
                errorMsg = errorData.message || errorData.title || errorMsg;
            } catch (e) {
                // Ignore if response body is not JSON
            }
            throw new Error(errorMsg);
        }

        const responseText = await response.text();
        if (response.status === 204 || !responseText) {
            showAlert('Entity deleted successfully!', 'success');
        } else {
            try {
                const result = JSON.parse(responseText);
                showAlert(result.message || 'Entity deleted successfully!', 'success');
            } catch (e) {
                showAlert('Entity deleted successfully! (non-JSON response)', 'success');
            }
        }

        loadEntities(); // Reload data
    } catch (error) {
        console.error('Error deleting entity:', error);
        showAlert(error.message || 'Failed to delete entity', 'danger');
    }
}
```

## 🎯 Overview
This document provides standardized instructions for generating complete CRUD functionality for new entities in the ASP.NET Core Razor Pages Learning Management System (LMS). The application uses modern UI patterns with Bootstrap 5, FontAwesome icons, and client-side JavaScript for API communication.

## 📚 Base Templates (UPDATED)
Use these enhanced templates for each CRUD operation:
- **Programs** (`/Pages/Programs/`) - Card-based layout with stats dashboard, filtering, and grid/list views
- **Courses** (`/Pages/Courses/`) - Grid layout with filtering and search
- **ProgramCourses** (`/Pages/ProgramCourses/`) - Relationship management with grouped tables and filters
- **CourseLecturerAssignments** (`/Pages/CourseLecturerAssignments/`) - Assignment management with grouped tables by course
- **CourseStudentEnrollments** (`/Pages/CourseStudentEnrollments/`) - Enrollment tracking with progress bars and grouped tables by course
- **CourseResources** (`/Pages/CourseResources/`) - Resource management with grouped tables by course and file handling

## 🔄 **KEY IMPLEMENTATIONS FROM CONVERSATION**

### ✅ **Standardized Index Pages**
All index pages now include:
- **Real-time Statistics Cards**: Dynamic counts and metrics with icons
- **Advanced Filtering**: Multi-field search with autocomplete dropdowns
- **Grid/List View Toggle**: Switch between card and table layouts
- **Consistent Styling**: Bootstrap 5 components with professional appearance
- **Responsive Design**: Mobile-friendly layouts

### ✅ **Grouped Table Structure**
Implemented across CourseResources, CourseLecturerAssignments, and CourseStudentEnrollments:
- **Course Header Rows**: Clickable rows showing course name and item count
- **Expandable Sections**: Toggle visibility of child rows
- **Subheader Rows**: Column headers that appear when expanded
- **Child Rows**: Individual items with proper indentation and styling
- **Smooth Transitions**: CSS animations for expand/collapse actions

### ✅ **Enhanced Features**
- **Autocomplete Search**: Real-time course/user search with suggestions
- **File Size Formatting**: Human-readable file sizes for resources
- **Progress Tracking**: Visual progress bars for enrollments
- **Advanced Error Handling**: Comprehensive error messages and user feedback
- **API Integration**: Proper fetch API usage with authentication
- **Real-time Updates**: Statistics update automatically after operations

## 🎨 **STANDARDIZED UI COMPONENTS**

### Page Headers (STANDARD)
```html
<!-- Page Header Structure -->
<div class="d-flex justify-content-between align-items-center mb-4">
    <div>
        <h2 class="mb-1">
            <i class="fas fa-[icon] text-[color] me-2"></i>[Page Title]
        </h2>
        <p class="text-muted mb-0">[Page Description]</p>
    </div>
    <a href="/[Entity]/Create" class="btn btn-[color]">
        <i class="fas fa-plus"></i> [Action Text]
    </a>
</div>
```

### Action Buttons (STANDARD)
```html
<!-- Primary Actions -->
<div class="btn-group" role="group">
    <a href="/[Entity]/Details/[id]" class="btn btn-info btn-sm">
        <i class="fas fa-eye"></i> View
    </a>
    <a href="/[Entity]/Edit/[id]" class="btn btn-primary btn-sm">
        <i class="fas fa-edit"></i> Edit
    </a>
    <button class="btn btn-danger btn-sm" onclick="deleteEntity([id])">
        <i class="fas fa-trash"></i> Delete
    </button>
</div>
```

### HTML Tables (STANDARD)
```html
<div class="card">
    <div class="card-body">
        <div class="table-responsive">
            <table class="table table-striped table-hover">
                <thead class="table-dark">
                    <tr>
                        <th>[Column 1]</th>
                        <th>[Column 2]</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody id="[entity]TableBody">
                    <!-- Data loaded via JavaScript -->
                </tbody>
            </table>
        </div>
    </div>
</div>
```

### Form Structure (STANDARD)
```html
<div class="card">
    <div class="card-header">
        <h4><i class="fas fa-plus"></i> [Form Title]</h4>
    </div>
    <div class="card-body">
        <form id="[entity]Form">
            <div class="row">
                <div class="col-md-6">
                    <div class="mb-3">
                        <label class="form-label">[Field Label]</label>
                        <input type="text" class="form-control" id="[fieldId]" required>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="mb-3">
                        <label class="form-label">[Field Label]</label>
                        <select class="form-select" id="[selectId]">
                            <option value="">Choose...</option>
                        </select>
                    </div>
                </div>
            </div>
            <div class="d-flex gap-2">
                <button type="submit" class="btn btn-primary">
                    <i class="fas fa-save"></i> Save
                </button>
                <a href="/[Entity]" class="btn btn-secondary">
                    <i class="fas fa-arrow-left"></i> Cancel
                </a>
            </div>
        </form>
    </div>
</div>
```

### Alert Messages (STANDARD)
```html
<div id="alert" class="alert mt-3" style="display: none;"></div>

<script>
// Standard alert function
function showAlert(message, type) {
    const alert = document.getElementById('alert');
    alert.className = `alert alert-${type} mt-3`;
    alert.textContent = message;
    alert.style.display = 'block';

    setTimeout(() => {
        alert.style.display = 'none';
    }, 5000);
}
</script>
```

### Filter Section (STANDARD)
```html
<div class="card mb-4">
    <div class="card-body">
        <div class="row g-3">
            <div class="col-md-4">
                <label class="form-label">Search</label>
                <input type="text" class="form-control" id="searchInput" placeholder="Search...">
            </div>
            <div class="col-md-4">
                <label class="form-label">Filter</label>
                <select class="form-select" id="filterSelect">
                    <option value="">All</option>
                </select>
            </div>
            <div class="col-md-4 d-flex align-items-end">
                <button class="btn btn-outline-primary w-100" onclick="applyFilters()">
                    <i class="fas fa-filter"></i> Apply Filters
                </button>
            </div>
        </div>
    </div>
</div>
```

## 🏗️ **Project Structure**
```
WebApp/WebApp/
├── Pages/
│   └── [EntityName]/
│       ├── Index.cshtml (Enhanced list view with cards/tables)
│       ├── Index.cshtml.cs (Page model)
│       ├── Create.cshtml (Standardized form)
│       ├── Create.cshtml.cs (Page model)
│       ├── Details.cshtml (Detailed view with actions)
│       ├── Details.cshtml.cs (Page model)
│       ├── Edit.cshtml (Edit form)
│       └── Edit.cshtml.cs (Page model)
├── wwwroot/
│   ├── css/site.css (Enhanced with LMS styling)
│   ├── js/
│   └── lib/
├── Program.cs
└── appsettings.json
```

## 🔗 **API Configuration**
The LMS application communicates with a REST API backend:
- **Base URL**: `@Configuration["ApiSettings:BaseUrl"]`
- **Default**: `https://localhost:7020`
- **API Endpoints**: `/api/[EntityName]` for all CRUD operations
- **Authentication**: Bearer token in Authorization header

## 🎓 **LMS-Specific Page Layout Guidelines**

### Programs Page Layout
- **Purpose**: Display educational programs with key metrics
- **Features**: Card-based layout, statistics dashboard, dual view (Card/Table)
- **Stats to Include**: Total programs, active programs, average duration, enrolled students
- **Actions**: View, Edit, Delete, Create New Program

### Courses Page Layout
- **Purpose**: Course catalog with filtering and search
- **Features**: Grid layout, advanced filtering, category/difficulty filters
- **Stats to Include**: Total courses, active courses, categories count, enrolled students
- **Actions**: View, Edit, Delete, Create New Course

### Program-Course Relationships Page Layout
- **Purpose**: Manage which courses belong to which programs
- **Features**: Filter by program/course/compulsory status, relationship overview
- **Stats to Include**: Total relationships, compulsory courses, optional courses
- **Actions**: Add course to program, remove course from program

### Assignment & Enrollment Pages Layout
- **Purpose**: Manage lecturer assignments and student enrollments
- **Features**: Relationship management, progress tracking (for enrollments)
- **Actions**: Create assignments/enrollments, update status, view details

## 📋 **Enhanced CRUD Generation Steps**

## Step 1: Create Page Models (.cs files)

### Index.cshtml.cs (ENHANCED)
```csharp
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace WebApp.Pages.[EntityName]
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnGet()
        {
            // Set API configuration for JavaScript
            ViewData["ApiBaseUrl"] = _configuration["ApiSettings:BaseUrl"];
        }
    }
}
```

### Create.cshtml.cs
```csharp
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.[EntityName]
{
    public class CreateModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
```

### Details.cshtml.cs
```csharp
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.[EntityName]
{
    public class DetailsModel : PageModel
    {
        public int Id { get; set; }

        public void OnGet(int id)
        {
            Id = id;
        }
    }
}
```

### Edit.cshtml.cs
```csharp
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.[EntityName]
{
    public class EditModel : PageModel
    {
        public int Id { get; set; }

        public void OnGet(int id)
        {
            Id = id;
        }
    }
}
```

## Step 2: Create Razor Pages (.cshtml files)

### Index.cshtml (List View)
**Template Structure:**
```html
@page
@model WebApp.Pages.[EntityName].IndexModel
@{
    ViewData["Title"] = "[EntityName]s";
}

<div class="container mt-4">
    <div class="d-flex justify-content-between align-items-center mb-4">
        <h2>[EntityName]s</h2>
        <a href="/[EntityName]/Create" class="btn btn-primary">
            <i class="fas fa-plus"></i> Create New [EntityName]
        </a>
    </div>

    <div class="card">
        <div class="card-body">
            <div class="table-responsive">
                <table class="table table-striped">
                    <thead>
                        <tr>
                            <!-- Define table headers based on entity properties -->
                            <th>Property 1</th>
                            <th>Property 2</th>
                            <th>Property 3</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody id="[entityName]TableBody">
                        <!-- Data will be loaded dynamically -->
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    <div id="alert" class="alert mt-3" style="display: none;"></div>
</div>

@section Scripts {
    <script>
        const apiBaseUrl = '@Configuration["ApiSettings:BaseUrl"]';
        
        async function load[EntityName]s() {
            try {
                const response = await fetch(`${apiBaseUrl}/api/[EntityName]`);
                if (!response.ok) throw new Error('Failed to fetch [entityName]s');
                
                const [entityName]s = await response.json();
                const tableBody = document.getElementById('[entityName]TableBody');
                
                tableBody.innerHTML = [entityName]s.map([entityName] => `
                    <tr>
                        <td>${escapeHtml([entityName].property1)}</td>
                        <td>${escapeHtml([entityName].property2)}</td>
                        <td>${escapeHtml([entityName].property3)}</td>
                        <td>
                            <div class="btn-group" role="group">
                                <a href="/[EntityName]/Details/${[entityName].id}" class="btn btn-info btn-sm">
                                    <i class="fas fa-eye"></i> View
                                </a>
                                <a href="/[EntityName]/Edit/${[entityName].id}" class="btn btn-primary btn-sm">
                                    <i class="fas fa-edit"></i> Edit
                                </a>
                                <button class="btn btn-danger btn-sm" onclick="delete[EntityName](${[entityName].id})">
                                    <i class="fas fa-trash"></i> Delete
                                </button>
                            </div>
                        </td>
                    </tr>
                `).join('');
            } catch (error) {
                console.error('Error:', error);
                showAlert('Failed to load [entityName]s', 'danger');
            }
        }

        async function delete[EntityName](id) {
            if (!confirm('Are you sure you want to delete this [entityName]?')) {
                return;
            }

            try {
                const response = await fetch(`${apiBaseUrl}/api/[EntityName]/${id}`, {
                    method: 'DELETE'
                });

                if (!response.ok) {
                    let errorMsg = 'Failed to delete [entityName]';
                    try {
                        const errorData = await response.json();
                        errorMsg = errorData.message || errorData.title || errorMsg;
                    } catch (e) { /* Ignore if response body is not JSON */ }
                    throw new Error(errorMsg);
                }

                const responseText = await response.text();
                if (response.status === 204 || !responseText) {
                    showAlert('[EntityName] deleted successfully!', 'success');
                } else {
                    try {
                        const result = JSON.parse(responseText);
                        showAlert(result.message || '[EntityName] deleted successfully!', 'success');
                    } catch (e) {
                        showAlert('[EntityName] deleted successfully! (non-JSON response)', 'success');
                    }
                }

                load[EntityName]s();
            } catch (error) {
                console.error('Error deleting [entityName]:', error);
                showAlert(error.message || 'Failed to delete [entityName]', 'danger');
            }
        }

        function showAlert(message, type) {
            const alert = document.getElementById('alert');
            alert.className = `alert alert-${type} mt-3`;
            alert.textContent = message;
            alert.style.display = 'block';
        }

        function escapeHtml(unsafe) {
            if (!unsafe) return '';
            return unsafe
                .toString()
                .replace(/&/g, "&amp;")
                .replace(/</g, "&lt;")
                .replace(/>/g, "&gt;")
                .replace(/"/g, "&quot;")
                .replace(/'/g, "&#039;");
        }

        document.addEventListener('DOMContentLoaded', load[EntityName]s);
    </script>
}
```

### Create.cshtml (Create Form)
**Template Structure:**
```html
@page
@model WebApp.Pages.[EntityName].CreateModel
@{
    ViewData["Title"] = "Create [EntityName]";
}

<div class="container mt-4">
    <div class="row justify-content-center">
        <div class="col-md-8">
            <div class="card">
                <div class="card-header">
                    <h3 class="mb-0">Create New [EntityName]</h3>
                </div>
                <div class="card-body">
                    <form id="create[EntityName]Form">
                        <!-- Form fields based on entity properties -->
                        <div class="mb-3">
                            <label for="property1" class="form-label">Property 1</label>
                            <input type="text" class="form-control" id="property1" name="property1" required>
                        </div>

                        <div class="mb-3">
                            <label for="property2" class="form-label">Property 2</label>
                            <input type="text" class="form-control" id="property2" name="property2">
                        </div>

                        <!-- For dropdowns (if entity has relationships) -->
                        <div class="mb-3">
                            <label for="relatedEntityId" class="form-label">Related Entity</label>
                            <select class="form-select" id="relatedEntityId" name="relatedEntityId" required>
                                <option value="">Select a related entity...</option>
                            </select>
                        </div>

                        <div class="d-flex justify-content-between">
                            <a href="/[EntityName]" class="btn btn-secondary">
                                <i class="fas fa-arrow-left"></i> Back to List
                            </a>
                            <button type="submit" class="btn btn-primary">
                                <i class="fas fa-save"></i> Create [EntityName]
                            </button>
                        </div>
                    </form>
                </div>
            </div>
            <div id="alert" class="alert mt-3" style="display: none;"></div>
        </div>
    </div>
</div>

@section Scripts {
    <script>
        const apiBaseUrl = '@Configuration["ApiSettings:BaseUrl"]';
        
        // Load related entities for dropdowns (if needed)
        async function loadRelatedEntities() {
            try {
                const response = await fetch(`${apiBaseUrl}/api/RelatedEntity`);
                if (!response.ok) throw new Error('Failed to fetch related entities');
                
                const relatedEntities = await response.json();
                const select = document.getElementById('relatedEntityId');
                
                relatedEntities.forEach(entity => {
                    const option = document.createElement('option');
                    option.value = entity.id;
                    option.textContent = entity.name;
                    select.appendChild(option);
                });
            } catch (error) {
                console.error('Error:', error);
                showAlert('Failed to load related entities', 'danger');
            }
        }
        
        document.getElementById('create[EntityName]Form').addEventListener('submit', async (e) => {
            e.preventDefault();
            
            const [entityName]Data = {
                property1: document.getElementById('property1').value,
                property2: document.getElementById('property2').value,
                relatedEntityId: document.getElementById('relatedEntityId').value
            };

            try {
                const response = await fetch(`${apiBaseUrl}/api/[EntityName]`, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify([entityName]Data)
                });

                if (!response.ok) {
                    const errorData = await response.json();
                    throw new Error(errorData.message || 'Failed to create [entityName]');
                }

                showAlert('[EntityName] created successfully!', 'success');
                setTimeout(() => {
                    window.location.href = '/[EntityName]';
                }, 1500);
            } catch (error) {
                console.error('Error:', error);
                showAlert(error.message || 'Failed to create [entityName]', 'danger');
            }
        });

        function showAlert(message, type) {
            const alert = document.getElementById('alert');
            alert.className = `alert alert-${type} mt-3`;
            alert.textContent = message;
            alert.style.display = 'block';
        }

        // Load related entities when page loads (if needed)
        document.addEventListener('DOMContentLoaded', () => {
            loadRelatedEntities();
        });
    </script>
}
```

### Details.cshtml (View Details)
**Template Structure:**
```html
@page "{id:int}"
@model WebApp.Pages.[EntityName].DetailsModel
@{
    ViewData["Title"] = "[EntityName] Details";
}

<div class="container mt-4">
    <div class="d-flex justify-content-between align-items-center mb-4">
        <h2>[EntityName] Details</h2>
        <div>
            <a href="/[EntityName]/Edit/@Model.Id" class="btn btn-primary">
                <i class="fas fa-edit"></i> Edit
            </a>
            <a href="/[EntityName]" class="btn btn-secondary">
                <i class="fas fa-arrow-left"></i> Back to List
            </a>
        </div>
    </div>

    <div class="card">
        <div class="card-body">
            <div class="row mb-3">
                <div class="col-md-3 fw-bold">Property 1:</div>
                <div class="col-md-9" id="property1Display"></div>
            </div>
            <div class="row mb-3">
                <div class="col-md-3 fw-bold">Property 2:</div>
                <div class="col-md-9" id="property2Display"></div>
            </div>
            <div class="row mb-3">
                <div class="col-md-3 fw-bold">Related Entity:</div>
                <div class="col-md-9" id="relatedEntityDisplay"></div>
            </div>
        </div>
    </div>
    <div id="alert" class="alert mt-3" style="display: none;"></div>
</div>

@section Scripts {
    <script>
        const apiBaseUrl = '@Configuration["ApiSettings:BaseUrl"]';
        const [entityName]Id = '@Model.Id';
        
        async function load[EntityName]Details() {
            try {
                const response = await fetch(`${apiBaseUrl}/api/[EntityName]/${[entityName]Id}`);
                if (!response.ok) throw new Error('Failed to load [entityName] details');
                
                const [entityName] = await response.json();
                document.getElementById('property1Display').textContent = [entityName].property1 || 'N/A';
                document.getElementById('property2Display').textContent = [entityName].property2 || 'N/A';
                document.getElementById('relatedEntityDisplay').textContent = [entityName].relatedEntity?.name || 'N/A';
            } catch (error) {
                console.error('Error:', error);
                showAlert('Failed to load [entityName] details', 'danger');
            }
        }

        function showAlert(message, type) {
            const alert = document.getElementById('alert');
            alert.className = `alert alert-${type} mt-3`;
            alert.textContent = message;
            alert.style.display = 'block';
        }

        document.addEventListener('DOMContentLoaded', load[EntityName]Details);
    </script>
}
```

### Edit.cshtml (Edit Form)
**Template Structure:**
```html
@page "{id:int}"
@model WebApp.Pages.[EntityName].EditModel
@{
    ViewData["Title"] = "Edit [EntityName]";
}

<div class="container mt-4">
    <div class="row justify-content-center">
        <div class="col-md-8">
            <div class="card">
                <div class="card-header">
                    <h3 class="mb-0">Edit [EntityName]</h3>
                </div>
                <div class="card-body">
                    <form id="edit[EntityName]Form">
                        <input type="hidden" id="[entityName]Id">
                        
                        <div class="mb-3">
                            <label for="property1" class="form-label">Property 1</label>
                            <input type="text" class="form-control" id="property1" name="property1" required>
                        </div>

                        <div class="mb-3">
                            <label for="property2" class="form-label">Property 2</label>
                            <input type="text" class="form-control" id="property2" name="property2">
                        </div>

                        <div class="mb-3">
                            <label for="relatedEntityId" class="form-label">Related Entity</label>
                            <select class="form-select" id="relatedEntityId" name="relatedEntityId" required>
                                <option value="">Select a related entity...</option>
                            </select>
                        </div>

                        <div class="d-flex justify-content-between">
                            <a href="/[EntityName]" class="btn btn-secondary">
                                <i class="fas fa-arrow-left"></i> Back to List
                            </a>
                            <button type="submit" class="btn btn-primary">
                                <i class="fas fa-save"></i> Save Changes
                            </button>
                        </div>
                    </form>
                </div>
            </div>
            <div id="alert" class="alert mt-3" style="display: none;"></div>
        </div>
    </div>
</div>

@section Scripts {
    <script>
        const apiBaseUrl = '@Configuration["ApiSettings:BaseUrl"]';
        const [entityName]Id = '@Model.Id';
        
        async function load[EntityName]() {
            try {
                const response = await fetch(`${apiBaseUrl}/api/[EntityName]/${[entityName]Id}`);
                if (!response.ok) {
                    throw new Error('Failed to fetch [entityName] details');
                }
                
                const [entityName] = await response.json();
                document.getElementById('[entityName]Id').value = [entityName].id;
                document.getElementById('property1').value = [entityName].property1 || '';
                document.getElementById('property2').value = [entityName].property2 || '';
                document.getElementById('relatedEntityId').value = [entityName].relatedEntityId || '';
            } catch (error) {
                console.error('Error:', error);
                showAlert(error.message || 'Failed to load [entityName] details', 'danger');
            }
        }

        async function loadRelatedEntities() {
            try {
                const response = await fetch(`${apiBaseUrl}/api/RelatedEntity`);
                if (!response.ok) throw new Error('Failed to fetch related entities');
                
                const relatedEntities = await response.json();
                const select = document.getElementById('relatedEntityId');
                
                relatedEntities.forEach(entity => {
                    const option = document.createElement('option');
                    option.value = entity.id;
                    option.textContent = entity.name;
                    select.appendChild(option);
                });
            } catch (error) {
                console.error('Error:', error);
                showAlert('Failed to load related entities', 'danger');
            }
        }

        document.getElementById('edit[EntityName]Form').addEventListener('submit', async (e) => {
            e.preventDefault();
            
            const [entityName]Data = {
                id: document.getElementById('[entityName]Id').value,
                property1: document.getElementById('property1').value,
                property2: document.getElementById('property2').value,
                relatedEntityId: document.getElementById('relatedEntityId').value
            };

            try {
                const response = await fetch(`${apiBaseUrl}/api/[EntityName]/${[entityName]Id}`, {
                    method: 'PUT',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify([entityName]Data)
                });

                if (!response.ok) {
                    const errorData = await response.json();
                    throw new Error(errorData.message || 'Failed to update [entityName]');
                }

                showAlert('[EntityName] updated successfully!', 'success');
                setTimeout(() => {
                    window.location.href = '/[EntityName]';
                }, 1500);
            } catch (error) {
                console.error('Error:', error);
                showAlert(error.message || 'Failed to update [entityName]', 'danger');
            }
        });

        function showAlert(message, type) {
            const alert = document.getElementById('alert');
            alert.className = `alert alert-${type} mt-3`;
            alert.textContent = message;
            alert.style.display = 'block';
        }

        // Load [entityName] data and related entities when the page loads
        document.addEventListener('DOMContentLoaded', () => {
            load[EntityName]();
            loadRelatedEntities();
        });
    </script>
}
```

## Step 3: Special Considerations

### For Entities with Relationships (like ProgramCourses)
1. **Dropdown Loading**: Include functions to load related entities for dropdowns
2. **Nested Properties**: Handle nested object properties in display (e.g., `program.name`, `course.courseName`)
3. **Foreign Keys**: Include foreign key fields in forms and handle them properly

### For Boolean Fields
Use badges or checkboxes for display:
```html
<span class="badge ${entity.isActive ? 'bg-success' : 'bg-secondary'}">
    ${entity.isActive ? 'Yes' : 'No'}
</span>
```

### For Date Fields
Format dates appropriately:
```javascript
${new Date(entity.createdAt).toLocaleDateString()}
```

## Step 4: Common Patterns

### Error Handling
- Always use try-catch blocks in JavaScript
- Show user-friendly error messages
- Log errors to console for debugging

### Success Messages
- Show success alerts after operations
- Redirect to list page after successful create/edit
- Auto-dismiss alerts after 3 seconds

### Form Validation
- Use HTML5 validation attributes (required, maxlength, etc.)
- Client-side validation before API calls
- Server-side validation handled by API

### Security
- Escape HTML content to prevent XSS
- Use proper HTTP methods (GET, POST, PUT, DELETE)
- Include proper Content-Type headers

## Step 5: File Naming Conventions

- **Entity Name**: Use PascalCase for entity names (e.g., `Student`, `CourseEnrollment`)
- **File Names**: Use PascalCase for .cs files, lowercase for .cshtml files
- **JavaScript Variables**: Use camelCase for JavaScript variables
- **API Endpoints**: Use PascalCase for API controller names

## Step 6: Navigation Integration

### Layout Files
The application uses different layout files based on the page type:

#### _Layout.cshtml (Main Layout)
- **Used by**: Most CRUD pages (Courses, Programs, ProgramCourses, etc.)
- **Navigation**: Full navigation menu with all entity links
- **Features**: Authentication check, token management, user info display

#### _AdminLayout.cshtml (Admin Layout)
- **Used by**: Admin-specific pages
- **Navigation**: Same as main layout but with "Admin Dashboard" branding
- **Features**: Admin-specific functionality

#### _AuthLayout.cshtml (Authentication Layout)
- **Used by**: Login, registration pages
- **Navigation**: No navigation menu (clean layout for auth pages)
- **Features**: Minimal layout for authentication forms

### Navigation Menu Structure
The main navigation includes these links:
```html
<ul class="navbar-nav flex-grow-1">
    <li class="nav-item">
        <a class="nav-link text-dark" asp-area="" asp-page="/Admin/Index">Dashboard</a>
    </li>
    <li class="nav-item">
        <a class="nav-link text-dark" asp-area="" asp-page="/Users/Index">Users</a>
    </li>
    <li class="nav-item">
        <a class="nav-link text-dark" asp-area="" asp-page="/UserProgramEnrollments/Index">Users Program Enrollments</a>
    </li>
    <li class="nav-item">
        <a class="nav-link text-dark" asp-area="" asp-page="/Programs/Index">Programs</a>
    </li>
    <li class="nav-item">
        <a class="nav-link text-dark" asp-area="" asp-page="/ProgramCourses/Index">Program Courses</a>
    </li>
    <li class="nav-item">
        <a class="nav-link text-dark" asp-area="" asp-page="/Courses/Index">Courses</a>
    </li>
    <li class="nav-item">
        <a class="nav-link text-dark" asp-area="" asp-page="/Admin/Reports/Index">Reports</a>
    </li>
    <li class="nav-item">
        <a class="nav-link text-dark" asp-area="" asp-page="/Admin/Settings/Index">Settings</a>
    </li>
</ul>
```

### Adding New Entity to Navigation
When creating CRUD for a new entity, add it to the navigation menu in both `_Layout.cshtml` and `_AdminLayout.cshtml`:

```html
<li class="nav-item">
    <a class="nav-link text-dark" asp-area="" asp-page="/[EntityName]/Index">[EntityName]s</a>
</li>
```

**Placement**: Add new entity links in alphabetical order or logical grouping within the existing navigation structure.

### Authentication Integration
The layout includes JavaScript for:
- **Token Management**: Automatically adds Authorization header to all fetch requests
- **User Session**: Checks for user authentication on page load
- **Role-based Access**: Shows admin navigation for admin users
- **Auto-logout**: Redirects to login if token is invalid

### Layout Selection Guidelines
- **Use _Layout.cshtml**: For standard CRUD pages (Courses, Programs, Students, etc.)
- **Use _AdminLayout.cshtml**: For admin-specific functionality (Reports, Settings, etc.)
- **Use _AuthLayout.cshtml**: For authentication pages (Login, Register, etc.)

## Step 7: Testing Checklist

After generating CRUD pages, verify:
- [ ] List page loads and displays data correctly
- [ ] Statistics cards show real-time data and update properly
- [ ] Filtering and search functionality works (including autocomplete)
- [ ] Grid/List view toggle functions correctly
- [ ] Grouped table structure (if implemented) expands/collapses properly
- [ ] Create form submits data successfully
- [ ] Details page shows all entity properties
- [ ] Edit form loads existing data and updates successfully
- [ ] Delete operation works with confirmation
- [ ] Error handling works for all operations
- [ ] Navigation between pages works correctly
- [ ] Form validation works as expected
- [ ] Navigation menu includes the new entity link
- [ ] Authentication and authorization work properly
- [ ] Layout is consistent with existing pages
- [ ] Progress bars display correctly (for enrollment pages)
- [ ] File size formatting works (for resource pages)
- [ ] Autocomplete search provides relevant suggestions
- [ ] Real-time statistics update after CRUD operations

## Example Entity: Student

**Please generate:**
- StudentModel.cs (if needed for page models)
- Pages/Students/Index.cshtml and Index.cshtml.cs
- Pages/Students/Create.cshtml and Create.cshtml.cs
- Pages/Students/Details.cshtml and Details.cshtml.cs
- Pages/Students/Edit.cshtml and Edit.cshtml.cs

**Follow the structure and conventions of the provided templates.**
**Ensure all CRUD operations are fully functional and ready to use.**
**Include proper error handling, validation, and user feedback.**

---

**End of Prompt** 
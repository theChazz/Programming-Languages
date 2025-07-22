-- Create Database
CREATE DATABASE lms_system;
GO

USE lms_system;
GO

-- Users Table
CREATE TABLE users (
    id INT IDENTITY(1,1) PRIMARY KEY,
    full_name VARCHAR(255) NOT NULL,
    email VARCHAR(255) UNIQUE NOT NULL,
    password_hash VARCHAR(255) NOT NULL,
    role VARCHAR(50) NOT NULL CHECK (role IN ('admin', 'lecturer', 'learner', 'support_staff')),
    created_at DATETIME DEFAULT GETDATE()
);

-- Programs Table 
CREATE TABLE programs (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    code VARCHAR(50) UNIQUE NOT NULL,
    level VARCHAR(50),
    department VARCHAR(100),
    status VARCHAR(50) CHECK (status IN ('active', 'inactive')) DEFAULT 'active',
    description TEXT,
    type VARCHAR(50) CHECK (type IN ('Degree', 'Short-course')),
    duration_months INT,
    created_at DATETIME DEFAULT GETDATE()
);

-- Insert dummy data for Programs
INSERT INTO programs (name, code, level, department, description, type, duration_months) VALUES
('Bachelor of Science in Computer Science', 'BSC-CS', 'NQF Level 7', 'Computer Science', 'Comprehensive degree in computer science covering programming, algorithms, and software development', 'Degree', 36),
('Bachelor of Commerce in Accounting', 'BCOM-ACC', 'NQF Level 7', 'Commerce', 'Degree program focusing on accounting principles and practices', 'Degree', 36),
('Data Science Certificate', 'CERT-DS', 'NQF Level 5', 'Computer Science', 'Short course in data science and analytics', 'Short-course', 6),
('Project Management Professional', 'CERT-PMP', 'NQF Level 6', 'Business Management', 'Professional certification in project management', 'Short-course', 3),
('Bachelor of Arts in Psychology', 'BA-PSYCH', 'NQF Level 7', 'Psychology', 'Comprehensive degree in psychology and human behavior', 'Degree', 36);

-- Courses Table
CREATE TABLE courses (
    id INT IDENTITY(1,1) PRIMARY KEY,
    course_name VARCHAR(255) NOT NULL,
    description TEXT,
    category VARCHAR(100),
    difficulty VARCHAR(20) CHECK (difficulty IN ('Beginner', 'Intermediate', 'Advanced')),
    syllabus TEXT,
    prerequisites TEXT,
    credits INT DEFAULT 0,
    semester INT CHECK (semester BETWEEN 1 AND 8),
    created_at DATETIME DEFAULT GETDATE()
);

-- Insert dummy data for Courses
INSERT INTO courses (course_name, description, category, difficulty, syllabus, prerequisites, credits, semester) VALUES
('Introduction to Programming', 'Basic programming concepts using Python', 'Computer Science', 'Beginner', 'Variables, Control Flow, Functions, Basic Data Structures', 'None', 15, 1),
('Advanced Database Systems', 'Complex database design and management', 'Computer Science', 'Advanced', 'Normalization, Transaction Management, Query Optimization', 'Basic SQL knowledge', 15, 3),
('Financial Accounting 101', 'Introduction to accounting principles', 'Accounting', 'Beginner', 'Basic Accounting Equations, Journal Entries, Financial Statements', 'None', 15, 1),
('Data Analysis with Python', 'Data analysis using Python libraries', 'Data Science', 'Intermediate', 'Pandas, NumPy, Data Visualization, Basic Statistics', 'Basic Python knowledge', 10, 2),
('Project Management Fundamentals', 'Basic project management concepts', 'Business', 'Beginner', 'Project Planning, Risk Management, Stakeholder Management', 'None', 10, 1),
('Cognitive Psychology', 'Study of mental processes', 'Psychology', 'Intermediate', 'Memory, Perception, Learning, Decision Making', 'Introduction to Psychology', 15, 2);

-- Program Courses Mapping Table (New)
CREATE TABLE program_courses (
    id INT IDENTITY(1,1) PRIMARY KEY,
    program_id INT,
    course_id INT,
    is_compulsory BIT DEFAULT 1,
    created_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (program_id) REFERENCES programs(id) ON DELETE CASCADE,
    FOREIGN KEY (course_id) REFERENCES courses(id) ON DELETE CASCADE
);

-- Insert dummy data for Program Courses mapping
INSERT INTO program_courses (program_id, course_id, is_compulsory) VALUES
(1, 1, 1), -- BSC-CS - Intro to Programming (Compulsory)
(1, 2, 1), -- BSC-CS - Advanced Database Systems (Compulsory)
(1, 4, 0), -- BSC-CS - Data Analysis (Optional)
(2, 3, 1), -- BCOM-ACC - Financial Accounting (Compulsory)
(3, 4, 1), -- CERT-DS - Data Analysis (Compulsory)
(4, 5, 1), -- CERT-PMP - Project Management (Compulsory)
(5, 6, 1); -- BA-PSYCH - Cognitive Psychology (Compulsory)

CREATE TABLE program_enrollments (
    id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT,
    program_id INT,
    status VARCHAR(50) CHECK (status IN ('active', 'completed', 'withdrawn', 'suspended','dropped', 'failed')) DEFAULT 'active',
    enrolled_at DATETIME DEFAULT GETDATE(),
    expected_completion_date DATE,
    created_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE CASCADE,
    FOREIGN KEY (program_id) REFERENCES programs(id) ON DELETE CASCADE
);

-- Insert dummy data for Enrollments (using existing user IDs)
-- Note: These INSERT statements will only work if these user IDs exist in your users table
INSERT INTO program_enrollments (user_id, program_id, status, expected_completion_date) VALUES
(1, 1, 'active', '2027-12-31'),
(1, 2, 'completed', '2027-12-31'),
(2, 3, 'active', '2027-12-31'),
(2, 4, 'dropped', '2027-12-31'),
(3, 3, 'active', '2027-12-31'),
(3, 2, 'failed', '2027-12-31');

-- Class Schedule Table
CREATE TABLE class_schedule (
    id INT IDENTITY(1,1) PRIMARY KEY,
    course_id INT,
    lecturer_id INT,
    class_date DATETIME,
    teams_link VARCHAR(500),
    created_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (course_id) REFERENCES courses(id) ON DELETE CASCADE,
    FOREIGN KEY (lecturer_id) REFERENCES users(id) ON DELETE CASCADE
);

-- Learning Materials Table
CREATE TABLE learning_materials (
    id INT IDENTITY(1,1) PRIMARY KEY,
    course_id INT,
    lecturer_id INT,
    material_title VARCHAR(255),
    file_path VARCHAR(500),
    file_type VARCHAR(50) CHECK (file_type IN ('pdf', 'docx', 'pptx', 'video', 'image', 'other')),
    created_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (course_id) REFERENCES courses(id) ON DELETE CASCADE,
    FOREIGN KEY (lecturer_id) REFERENCES users(id) ON DELETE CASCADE
);

-- Assignments Table
CREATE TABLE assignments (
    id INT IDENTITY(1,1) PRIMARY KEY,
    course_id INT,
    lecturer_id INT,
    title VARCHAR(255),
    description TEXT,
    due_date DATETIME,
    created_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (course_id) REFERENCES courses(id) ON DELETE CASCADE,
    FOREIGN KEY (lecturer_id) REFERENCES users(id) ON DELETE CASCADE
);

-- Submissions Table
CREATE TABLE submissions (
    id INT IDENTITY(1,1) PRIMARY KEY,
    assignment_id INT,
    user_id INT,
    file_path VARCHAR(500),
    submitted_at DATETIME DEFAULT GETDATE(),
    grade FLOAT DEFAULT NULL,
    feedback TEXT DEFAULT NULL,
    FOREIGN KEY (assignment_id) REFERENCES assignments(id) ON DELETE CASCADE,
    FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE CASCADE
);

-- Quizzes Table
CREATE TABLE quizzes (
    id INT IDENTITY(1,1) PRIMARY KEY,
    course_id INT,
    title VARCHAR(255),
    created_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (course_id) REFERENCES courses(id) ON DELETE CASCADE
);

-- Questions Table
CREATE TABLE questions (
    id INT IDENTITY(1,1) PRIMARY KEY,
    quiz_id INT,
    question_text TEXT,
    question_type VARCHAR(50) CHECK (question_type IN ('multiple_choice', 'true_false', 'open_ended')),
    created_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (quiz_id) REFERENCES quizzes(id) ON DELETE CASCADE
);

-- Answers Table
CREATE TABLE answers (
    id INT IDENTITY(1,1) PRIMARY KEY,
    question_id INT,
    answer_text TEXT,
    is_correct BIT DEFAULT 0,
    FOREIGN KEY (question_id) REFERENCES questions(id) ON DELETE CASCADE
);

-- Forum Discussions Table
CREATE TABLE forum_discussions (
    id INT IDENTITY(1,1) PRIMARY KEY,
    course_id INT,
    user_id INT,
    title VARCHAR(255),
    content TEXT,
    created_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (course_id) REFERENCES courses(id) ON DELETE CASCADE,
    FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE CASCADE
);

-- Chat Messages Table
CREATE TABLE chat_messages (
    id INT IDENTITY(1,1) PRIMARY KEY,
    sender_id INT,
    receiver_id INT,
    message TEXT,
    sent_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (sender_id) REFERENCES users(id) ON DELETE CASCADE,
    FOREIGN KEY (receiver_id) REFERENCES users(id) -- no ON DELETE CASCADE here
);

-- Notifications Table
CREATE TABLE notifications (
    id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT,
    message TEXT,
    is_read BIT DEFAULT 0,
    created_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE CASCADE
);

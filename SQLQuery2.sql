-- Insert data into Roles table
INSERT INTO Roles (RoleName) VALUES 
('Student'),
('Instructor'),
('Admin');

-- Insert data into Users table
INSERT INTO Users (Name, Email, PasswordHash, RegistrationDate) VALUES
('John Doe', 'john.doe@example.com', 'hashed_password_1', GETDATE()),
('Jane Smith', 'jane.smith@example.com', 'hashed_password_2', GETDATE()),
('Admin User', 'admin@example.com', 'hashed_password_3', GETDATE()),
('Instructor One', 'instructor1@example.com', 'hashed_password_4', GETDATE()),
('Instructor Two', 'instructor2@example.com', 'hashed_password_5', GETDATE());

-- Insert data into UserRoles table
INSERT INTO UserRoles (UserID, RoleID) VALUES
(1, 1), -- John Doe as Student
(2, 1), -- Jane Smith as Student
(3, 3), -- Admin User as Admin
(4, 2), -- Instructor One as Instructor
(5, 2); -- Instructor Two as Instructor

-- Insert data into Categories table
INSERT INTO Categories (CategoryName) VALUES
('Programming'),
('Data Science'),
('Web Development'),
('Mobile Development'),
('Machine Learning');

-- Insert data into Courses table
INSERT INTO Courses (Title, Description, DifficultyLevel, Price, CreationDate, LastUpdateDate, PublicationStatus, MaxEnrollment) VALUES
('Introduction to Python', 'Learn the basics of Python programming', 'Beginner', 49.99, GETDATE(), GETDATE(), 'Published', 100),
('Advanced Machine Learning', 'Dive deep into ML algorithms', 'Advanced', 99.99, GETDATE(), GETDATE(), 'Published', 50),
('Web Development with React', 'Build modern web applications with React', 'Intermediate', 79.99, GETDATE(), GETDATE(), 'Published', 75);

-- Insert data into CourseCategories table
INSERT INTO CourseCategories (CourseID, CategoryID) VALUES
(1, 1), -- Introduction to Python in Programming category
(2, 5), -- Advanced Machine Learning in Machine Learning category
(3, 3); -- Web Development with React in Web Development category

-- Insert data into CourseInstructors table
INSERT INTO CourseInstructors (CourseID, UserID) VALUES
(1, 4), -- Instructor One teaching Introduction to Python
(2, 5), -- Instructor Two teaching Advanced Machine Learning
(3, 4); -- Instructor One teaching Web Development with React

-- Insert data into Modules table
INSERT INTO Modules (CourseID, Title, Description, OrderInCourse) VALUES
(1, 'Python Basics', 'Introduction to Python syntax and basic concepts', 1),
(1, 'Data Structures', 'Learn about lists, dictionaries, and sets in Python', 2),
(2, 'Supervised Learning', 'Understanding supervised learning algorithms', 1),
(2, 'Unsupervised Learning', 'Exploring unsupervised learning techniques', 2),
(3, 'React Fundamentals', 'Getting started with React components and JSX', 1),
(3, 'State Management', 'Managing application state with React hooks', 2);

-- Insert data into Parts table
INSERT INTO Parts (ModuleID, Title, Description, OrderInModule) VALUES
(1, 'Variables and Data Types', 'Understanding Python variables and basic data types', 1),
(1, 'Control Structures', 'Learning about if statements and loops', 2),
(2, 'Working with Lists', 'Manipulating and accessing list data', 1),
(2, 'Dictionary Operations', 'Creating and using dictionaries effectively', 2),
(3, 'Linear Regression', 'Implementation and analysis of linear regression', 1),
(3, 'Decision Trees', 'Understanding and applying decision tree algorithms', 2),
(4, 'K-means Clustering', 'Exploring k-means clustering technique', 1),
(4, 'Principal Component Analysis', 'Dimensionality reduction with PCA', 2),
(5, 'Creating Components', 'Building and structuring React components', 1),
(5, 'Props and State', 'Understanding data flow in React applications', 2),
(6, 'useState Hook', 'Managing component state with useState', 1),
(6, 'useEffect Hook', 'Handling side effects in functional components', 2);

-- Insert data into LessonTypes table
INSERT INTO LessonTypes (TypeName) VALUES
('Video'),
('Text'),
('Quiz');

-- Insert data into Lessons table
INSERT INTO Lessons (PartID, LessonTypeID, Title, Description, OrderInPart) VALUES
(1, 1, 'Introduction to Variables', 'Video lesson on Python variables', 1),
(1, 2, 'Data Types in Python', 'Text lesson explaining Python data types', 2),
(1, 3, 'Variables and Data Types Quiz', 'Test your knowledge of variables and data types', 3),
(2, 1, 'If Statements in Python', 'Video lesson on conditional statements', 1),
(2, 2, 'Loops in Python', 'Text lesson on for and while loops', 2),
(2, 3, 'Control Structures Quiz', 'Quiz on if statements and loops', 3);

-- Insert data into LessonVideos table
INSERT INTO LessonVideos (LessonID, VideoURL, Duration) VALUES
(1, 'https://example.com/videos/python-variables', 600),
(4, 'https://example.com/videos/python-if-statements', 720);

-- Insert data into LessonTexts table
INSERT INTO LessonTexts (LessonID, Content) VALUES
(2, 'Python has several built-in data types including int, float, str, bool, list, tuple, dict, and set. Each type has its own characteristics and use cases.'),
(5, 'Python provides two main types of loops: for loops and while loops. For loops are used to iterate over a sequence, while while loops continue as long as a condition is true.');

-- Insert data into LessonQuizzes table
INSERT INTO LessonQuizzes (LessonID, PassingScore) VALUES
(3, 70),
(6, 70);

-- Insert data into QuizQuestions table
INSERT INTO QuizQuestions (LessonID, QuestionText) VALUES
(3, 'What is the correct way to declare a variable in Python?'),
(3, 'Which of the following is not a basic data type in Python?'),
(6, 'What keyword is used to start an if statement in Python?'),
(6, 'Which loop is used when you want to repeat a block of code a specific number of times?');

-- Insert data into QuizAnswers table
INSERT INTO QuizAnswers (QuestionID, AnswerText, IsCorrect) VALUES
(1, 'var x = 5', 0),
(1, 'x = 5', 1),
(1, 'int x = 5', 0),
(1, 'let x = 5', 0),
(2, 'int', 0),
(2, 'float', 0),
(2, 'string', 0),
(2, 'array', 1),
(3, 'if', 1),
(3, 'when', 0),
(3, 'check', 0),
(3, 'case', 0),
(4, 'while loop', 0),
(4, 'for loop', 1),
(4, 'do-while loop', 0),
(4, 'repeat loop', 0);

-- Insert data into PaymentMethods table
INSERT INTO PaymentMethods (Country, PaymentType, CardName, CardNumber, ExpiryDate, CVC) VALUES
('United States', 'Credit Card', 'John Doe', '1234567890123456', '12/25', '123'),
('United Kingdom', 'Credit Card', 'Jane Smith', '9876543210987654', '06/24', '456');

-- Insert data into Enrollments table
INSERT INTO Enrollments (UserID, CourseID, PaymentMethodID, EnrollmentDate) VALUES
(1, 1, 1, GETDATE()),
(2, 2, 2, GETDATE()),
(1, 3, 1, GETDATE());

-- Insert data into Progresses table
INSERT INTO Progresses (UserID, PartID, CompletionStatus, CompletionDate) VALUES
(1, 1, 1, GETDATE()),
(1, 2, 0, NULL),
(2, 5, 1, GETDATE()),
(2, 6, 0, NULL);

-- Insert data into Transactions table
INSERT INTO Transactions (UserID, CourseID, Amount, TransactionDate, Status) VALUES
(1, 1, 49.99, GETDATE(), 'Completed'),
(2, 2, 99.99, GETDATE(), 'Completed'),
(1, 3, 79.99, GETDATE(), 'Completed');

-- Insert data into Certificates table
INSERT INTO Certificates (UserID, CourseID, IssueDate) VALUES
(1, 1, GETDATE());

-- Insert data into AdminLogs table
INSERT INTO AdminLogs (AdminID, ActionType, ActionDescription, ActionDate) VALUES
(3, 'Course Creation', 'Created new course: Introduction to Python', GETDATE()),
(3, 'User Management', 'Added new instructor: Instructor Two', GETDATE());
select * from Roles;
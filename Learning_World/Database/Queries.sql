            --------------------------------------------------------------------------------------------------------------------
-- Clear all Data in  Database

            --------------------------------------------------------------------------------------------------------------------
-- Insert data into Roles table
INSERT INTO Roles (RoleName) VALUES 
('Student'), ('Instructor'), ('Admin');

-- Insert data into Users table
INSERT INTO Users (Name, Email, PasswordHash, RegistrationDate) VALUES
('John Doe', 'john.doe@email.com', 'hashed_password_1', '2023-01-15'),
('Jane Smith', 'jane.smith@email.com', 'hashed_password_2', '2023-02-20'),
('Bob Johnson', 'bob.johnson@email.com', 'hashed_password_3', '2023-03-10'),
('Alice Brown', 'alice.brown@email.com', 'hashed_password_4', '2023-04-05'),
('Charlie Davis', 'charlie.davis@email.com', 'hashed_password_5', '2023-05-12');

-- Assign roles to users
INSERT INTO UserRoles (UserID, RoleID) VALUES
(1, 1), -- John Doe as Student
(2, 2), -- Jane Smith as Instructor
(3, 1), -- Bob Johnson as Student
(4, 2), -- Alice Brown as Instructor
(5, 3); -- Charlie Davis as Admin

-- Insert data into Categories table
INSERT INTO Categories (CategoryName) VALUES
('Programming'), ('Data Science'), ('Business'), ('Design'), ('Language');

-- Insert data into Courses table
INSERT INTO Courses (Title, Description, Image, DifficultyLevel, Price, CreationDate, LastUpdateDate, PublicationStatus, MaxEnrollment, AverageRating) VALUES
('Introduction to Python', 'Learn the basics of Python programming', 'python_intro.jpg', 'Beginner', 49.99, '2023-06-01', '2023-06-15', 'Published', 100, 4.5),
('Data Analysis with R', 'Master data analysis techniques using R', 'r_data_analysis.jpg', 'Intermediate', 79.99, '2023-07-01', '2023-07-20', 'Published', 75, 4.7),
('Digital Marketing Fundamentals', 'Understand the basics of digital marketing', 'digital_marketing.jpg', 'Beginner', 39.99, '2023-08-01', '2023-08-10', 'Published', 150, 4.2),
('UI/UX Design Principles', 'Learn the core principles of UI/UX design', 'uiux_design.jpg', 'Intermediate', 69.99, '2023-09-01', '2023-09-18', 'Published', 80, 4.8),
('Spanish for Beginners', 'Start your journey to learn Spanish', 'spanish_beginners.jpg', 'Beginner', 29.99, '2023-10-01', '2023-10-12', 'Published', 200, 4.4);

-- Assign categories to courses
INSERT INTO CourseCategories (CourseID, CategoryID) VALUES
(1, 1), -- Python course in Programming category
(2, 2), -- R course in Data Science category
(3, 3), -- Digital Marketing in Business category
(4, 4), -- UI/UX in Design category
(5, 5); -- Spanish in Language category

-- Assign instructors to courses
INSERT INTO CourseInstructors (CourseID, UserID) VALUES
(1, 2), -- Jane Smith teaching Python
(2, 2), -- Jane Smith teaching R
(3, 4), -- Alice Brown teaching Digital Marketing
(4, 4), -- Alice Brown teaching UI/UX
(5, 2); -- Jane Smith teaching Spanish

-- Insert data into Modules table
INSERT INTO Modules (CourseID, Title, Description, OrderInCourse) VALUES
(1, 'Python Basics', 'Introduction to Python syntax and basic concepts', 1),
(1, 'Python Data Structures', 'Learn about lists, dictionaries, and tuples', 2),
(2, 'R Fundamentals', 'Basic R programming and data types', 1),
(2, 'Data Visualization in R', 'Creating plots and charts with R', 2),
(3, 'Digital Marketing Overview', 'Introduction to digital marketing channels', 1),
(3, 'Social Media Marketing', 'Leveraging social media for business', 2),
(4, 'Design Thinking', 'Understanding the design thinking process', 1),
(4, 'Wireframing and Prototyping', 'Creating wireframes and interactive prototypes', 2),
(5, 'Spanish Alphabet and Pronunciation', 'Learn Spanish sounds and letters', 1),
(5, 'Basic Spanish Phrases', 'Essential phrases for beginners', 2);

-- Insert data into Parts table
INSERT INTO Parts (ModuleID, Title, Description, OrderInModule) VALUES
(1, 'Variables and Data Types', 'Understanding Python variables and data types', 1),
(1, 'Control Flow', 'If statements and loops in Python', 2),
(2, 'Working with Lists', 'Creating and manipulating Python lists', 1),
(2, 'Dictionary Operations', 'Using Python dictionaries effectively', 2),
(3, 'R Data Types', 'Understanding different data types in R', 1),
(3, 'Basic R Operations', 'Performing calculations and data manipulations in R', 2),
(4, 'ggplot2 Basics', 'Introduction to ggplot2 for data visualization', 1),
(4, 'Advanced Plotting Techniques', 'Creating complex plots in R', 2),
(5, 'Digital Marketing Ecosystem', 'Overview of digital marketing channels and tools', 1),
(5, 'Customer Journey Mapping', 'Understanding the digital customer journey', 2);

-- Insert data into LessonTypes table
INSERT INTO LessonTypes (TypeName) VALUES
('Video'), ('Text'), ('Quiz');

-- Insert data into Lessons table
INSERT INTO Lessons (PartID, LessonTypeID, Title, Description, OrderInPart) VALUES
(1, 1, 'Introduction to Python Variables', 'Learn how to declare and use variables in Python', 1),
(1, 2, 'Python Data Types Overview', 'Detailed explanation of Python data types', 2),
(1, 3, 'Variables and Data Types Quiz', 'Test your knowledge of Python variables and data types', 3),
(2, 1, 'If Statements in Python', 'Understanding conditional statements in Python', 1),
(2, 2, 'Loops in Python', 'Learn about for and while loops in Python', 2),
(2, 3, 'Control Flow Quiz', 'Quiz on Python control flow concepts', 3);

-- Insert data into LessonVideos table
INSERT INTO LessonVideos (LessonID, VideoURL, Duration) VALUES
(1, 'https://example.com/python_variables_video', 600), -- 10 minutes
(4, 'https://example.com/python_if_statements_video', 720); -- 12 minutes

-- Insert data into LessonTexts table
INSERT INTO LessonTexts (LessonID, Content) VALUES
(2, 'Python has several built-in data types including integers, floats, strings, and booleans. Each data type has its own characteristics and uses...'),
(5, 'Loops in Python allow you to iterate over a sequence of elements. The two main types of loops are for loops and while loops...');

-- Insert data into LessonQuizzes table
INSERT INTO LessonQuizzes (LessonID, PassingScore) VALUES
(3, 70),
(6, 75);

-- Insert data into QuizQuestions table
INSERT INTO QuizQuestions (LessonID, QuestionText) VALUES
(3, 'What is the correct way to declare a variable in Python?'),
(3, 'Which of the following is not a valid Python data type?'),
(6, 'What is the purpose of an "else" statement in a Python if-else construct?'),
(6, 'How many times will a "while True" loop execute if not explicitly broken?');

-- Insert data into QuizAnswers table
INSERT INTO QuizAnswers (QuestionID, AnswerText, IsCorrect) VALUES
(1, 'var x = 5', 0),
(1, 'x = 5', 1),
(1, 'int x = 5', 0),
(1, 'let x = 5', 0),
(2, 'Integer', 0),
(2, 'Float', 0),
(2, 'String', 0),
(2, 'Char', 1),
(3, 'To execute when the if condition is true', 0),
(3, 'To execute when the if condition is false', 1),
(3, 'To execute regardless of the if condition', 0),
(3, 'To end the if statement', 0),
(4, 'Once', 0),
(4, 'Ten times', 0),
(4, 'Infinitely', 1),
(4, 'It depends on the condition inside the loop', 0);

-- Insert data into PaymentMethods table
INSERT INTO PaymentMethods (Country, PaymentType, CardName, CardNumber, ExpiryDate, CVC, PayPalEmail) VALUES
('USA', 'Credit Card', 'John Doe', '4111111111111111', '12/25', '123', NULL),
('UK', 'PayPal', NULL, NULL, NULL, NULL, 'jane.smith@email.com'),
('Canada', 'Credit Card', 'Bob Johnson', '5555555555554444', '09/24', '456', NULL);

-- Insert data into Enrollments table
INSERT INTO Enrollments (UserID, CourseID, PaymentMethodID, EnrollmentDate) VALUES
(1, 1, 1, '2023-06-20'),
(1, 2, 1, '2023-07-25'),
(3, 3, 3, '2023-08-15'),
(3, 4, 3, '2023-09-22');

-- Insert data into Progresses table
INSERT INTO Progresses (UserID, PartID, CompletionStatus, CompletionDate) VALUES
(1, 1, 1, '2023-06-25'),
(1, 2, 1, '2023-06-30'),
(1, 3, 0, NULL),
(3, 9, 1, '2023-08-20'),
(3, 10, 0, NULL);

-- Insert data into Transactions table
INSERT INTO Transactions (UserID, CourseID, Amount, TransactionDate, Status) VALUES
(1, 1, 49.99, '2023-06-20', 'Completed'),
(1, 2, 79.99, '2023-07-25', 'Completed'),
(3, 3, 39.99, '2023-08-15', 'Completed'),
(3, 4, 69.99, '2023-09-22', 'Completed');

-- Insert data into Certificates table
INSERT INTO Certificates (UserID, CourseID, IssueDate) VALUES
(1, 1, '2023-07-15'),
(3, 3, '2023-09-10');

-- Insert data into AdminLogs table
INSERT INTO AdminLogs (AdminID, ActionType, ActionDescription, ActionDate) VALUES
(5, 'Course Creation', 'Created new course: Introduction to Python', '2023-06-01'),
(5, 'User Management', 'Assigned instructor role to Jane Smith', '2023-06-02'),
(5, 'Course Update', 'Updated course content for Data Analysis with R', '2023-07-20'),
(5, 'User Support', 'Resolved login issue for user Bob Johnson', '2023-08-05');
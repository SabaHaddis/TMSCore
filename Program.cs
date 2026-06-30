string studentName = "Abeba";
string studentId = "STU-001";
int enrollmentCount = 3;
decimal grantAmount = 1999.99m; // 'm' suffix marks a decimal literal
DateTime enrolledAt = DateTime.UtcNow;
string? campusRegion = null;
Console.WriteLine($"Student: {studentName} ({studentId})");
Console.WriteLine($"Courses: {enrollmentCount}");
Console.WriteLine($"Grant: {grantAmount:F2}");
Console.WriteLine($"Enrolled: {enrolledAt:yyyy-MM-dd}");
Console.WriteLine($"Campus: {campusRegion ?? "Not assigned"}");

decimal grantPerStudent = 1999.99m;
decimal totalAllocation = grantPerStudent * 100_000m;
Console.WriteLine($"Total allocated (decimal): {totalAllocation}");
Console.WriteLine($"Total allocated (formatted): {totalAllocation:F2}");

var enrollment = new EnrollmentRecord("STU-001", "CS-401", DateTime.UtcNow);
Console.WriteLine(enrollment);
// Try to mutate it — uncomment this line and see the compiler error:
// enrollment.CourseCode = "HACKED"; // ERROR: init-only property
// Non-destructive copy — creates a NEW record with one field changed
var corrected = enrollment with { CourseCode = "CS-402" };
Console.WriteLine(corrected);
// Value equality — two records with the same data are equal
var duplicate = new EnrollmentRecord("STU-001", "CS-401", enrollment.EnrolledAt);
Console.WriteLine($"Same data? {enrollment == duplicate}"); // True

var course = new Course
{
    Code = "CS-401",
    Title = "Advanced C#",
    Capacity = 30
};

Console.WriteLine($"Course: {course.Title} (Capacity: {course.Capacity})");

// Invalid capacity
try
{
    course.Capacity = -5;
}
catch (ArgumentOutOfRangeException ex)
{
    Console.WriteLine($"Caught: {ex.Message}");
}

// Invalid title
try
{
    course.Title = "";
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Caught: {ex.Message}");
}

void PrintGradeReport(IEnumerable<IGradable> assessments)
{
    Console.WriteLine("--- Grade Report ---");

    foreach (var item in assessments)
    {
        Console.WriteLine($"{item.Title}: {item.CalculateGrade():F2}%");
    }
}

// Test it — one array holds two completely different types
IGradable[] cohortAssessments =
[
    new Quiz
    {
        Title = "C# Basics",
        CorrectAnswers = 18,
        TotalQuestions = 20
    },

    new LabAssignment
    {
        Title = "Registration API",
        FunctionalityScore = 90m,
        CodeQualityScore = 85m
    }
];

PrintGradeReport(cohortAssessments);

var s = new Student
{
    Id = "S1",
    Name = "Abeba",
    Age = 20,
    GPA = 3.8m
};

Console.WriteLine($"Student: {s.Name}, GPA: {s.GPA}");

// Uncomment one at a time to test validation:

// new Student { Id = "S2", Name = "", Age = 20, GPA = 3.0m };
// new Student { Id = "S3", Name = "Test", Age = 12, GPA = 3.0m };
// new Student { Id = "S4", Name = "Test", Age = 20, GPA = 5.0m };
Console.WriteLine("\n--- Exercise 4 ---");

var service = new EnrollmentService();

// Test 1
var validStudent = new Student
{
    Id = "S1",
    Name = "Abeba",
    Age = 20,
    GPA = 3.8m
};

var validCourse = new Course
{
    Code = "CS-401",
    Title = "Advanced C#",
    Capacity = 30
};

var result = service.ProcessRegistration(validStudent, validCourse);

Console.WriteLine($"Enrolled: {result.StudentId} in {result.CourseCode}");

// Test 2
try
{
    service.ProcessRegistration(null, validCourse);
}
catch (ArgumentNullException ex)
{
    Console.WriteLine($"Guard caught: {ex.ParamName}");
}

// Test 3
var fullCourse = new Course
{
    Code = "CS-402",
    Title = "Full Course",
    Capacity = 1
};

fullCourse.EnrolledCount = 1;

try
{
    service.ProcessRegistration(validStudent, fullCourse);
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Business rule: {ex.Message}");
}
// C# 12+ Collection Expressions
List<Student> students =
[
    new Student { Id = "S1", Name = "Abeba", Age = 22, GPA = 3.8m },
    new Student { Id = "S2", Name = "Kidane", Age = 21, GPA = 2.4m },
    new Student { Id = "S3", Name = "Dawit", Age = 20, GPA = 3.1m },
    new Student { Id = "S4", Name = "Sara", Age = 23, GPA = 3.9m },
    new Student { Id = "S5", Name = "Frehiwot", Age = 19, GPA = 2.0m },
    new Student { Id = "S6", Name = "Yonas", Age = 24, GPA = 3.5m },
    new Student { Id = "S7", Name = "Meron", Age = 22, GPA = 1.8m },
    new Student { Id = "S8", Name = "Tesfaye", Age = 21, GPA = 2.9m }
];

var leaderboard = students
    .Where(s => s.GPA >= 3.5m)
    .OrderByDescending(s => s.GPA)
    .Select(s => s.Name)
    .ToList();

Console.WriteLine($"Found {leaderboard.Count} Honors Students:");

foreach (var name in leaderboard)
{
    Console.WriteLine($"- {name}");
}

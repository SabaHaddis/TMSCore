public class EnrollmentService
{
    public EnrollmentRecord ProcessRegistration(Student? student, Course? course)
    {
        // TODO
        if (student is null)
            throw new ArgumentNullException(nameof(student));

        if (course is null)
            throw new ArgumentNullException(nameof(course));

        if (course.EnrolledCount >= course.Capacity)
            throw new InvalidOperationException("Course is already full.");
        
        throw new NotImplementedException();
    }
}
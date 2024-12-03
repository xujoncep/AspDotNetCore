using dotnettuitorials.net.Models;

namespace dotnettuitorials.net.Repository
{
    public class StudentRepository : IStudentRepository
    {

        public StudentRepository() 
        {
            string filePath = @"D:\DotNet\MVC\dotnettuitorials.net\dotnettuitorials.net\Log\Log.txt";
            string contentToWrite = $"StudentRepository Object Created: @{DateTime.Now.ToString()}";
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(contentToWrite);
            }

        }
        public List<Student> DataSource()
        {
            return new List<Student>()
            {
                new Student(){ StudentId=101, StudentName="Jhon", Branch="Dhaka", Gender="Male", Section="Day"},
                new Student(){ StudentId=102, StudentName="Bob", Branch="Rajshahi", Gender="Male", Section="Night"},
                new Student(){ StudentId=103, StudentName="Alice", Branch="Dhaka", Gender="Female", Section="Day"},
                new Student(){ StudentId=104, StudentName="Jemas", Branch="Rajshahi", Gender="Male", Section="Night"},
                new Student(){ StudentId=105, StudentName="Doe", Branch="Dhaka", Gender="Male", Section="Day"},
                new Student(){ StudentId=106, StudentName="Alex", Branch="Rajshahi", Gender="Male", Section="Night"},
                new Student(){ StudentId=107, StudentName="Siri", Branch="Dhaka", Gender="Female", Section="Day"},
                new Student(){ StudentId=108, StudentName="Mhon", Branch="Rajshahi", Gender="Male", Section="Day"},

            }; 
        }

        public List<Student> GetAllStudents()
        {
            return DataSource();
        }

        public Student GetStudentById(int StudentId)
        {
            return DataSource().FirstOrDefault(s => s.StudentId == StudentId)?? new Student();
            //Ensures that if the result of .FirstOrDefault(...) is null, it returns a new Student object instead.
        }
    }
}

namespace CampusLink
{
    // Student inherits from our base class Person
    public class Student : Person
    {
        // Student ID is assigned automatically by StudentManager
        public string StudentID { get; private set; }

        // Constructor with ID which is used by StudentManager after generating ID
        public Student(string id, string name, string gender, int age, string course)
            : base(name, gender, age, course)
        {
            StudentID = id;
        }

        // Constructor without ID which is used when user is registering a student
        public Student(string name, string gender, int age, string course)
            : base(name, gender, age, course)
        {
            StudentID = null;
        }

        // formats student details when printing
        public override string ToString()
        {
            return $"{StudentID,-10}  {Name,-20}  {Gender,-6}  {Age,3} {Course,-20}";
        }
    }
}
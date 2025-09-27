namespace CampusLink
{
    public class Person
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Course { get; set; }

        // constructor runs when creating a new person
        public Person(string name, string gender, int age, string course)
        {
            //basic checks to ensure valid data
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Name cannot be empty");
            if (string.IsNullOrWhiteSpace(gender))
                throw new Exception("Gender cannot be empty");
            if (string.IsNullOrWhiteSpace(course))
                throw new Exception("Course cannot be empty");

        // we assign trimmed values to avoid leading/trailing spaces
            Name = name.Trim();
            Gender = gender.Trim();
            Age = age;
            Course = course.Trim();
        }
    }
}

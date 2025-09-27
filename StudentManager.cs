using System;
using System.Collections.Generic;
using System.Linq;

namespace CampusLink
{
    // Manages student records like adding, editing, deleting, listing
    public class StudentManager : IManageStudents<Student>
    {
        //storing student records in memory
        private readonly List<Student> _students = new List<Student>();
        private int _counter = 0; // used for autogenerating IDs

        // Generating custom IDs like STU-0001
        private string GenerateStudentId()
        {
            _counter++;
            return $"STU-{_counter:0000}";
        }
        // Interface methods
        public void Add(Student item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            var id = GenerateStudentId();
            var s = new Student(id, item.Name, item.Gender, item.Age, item.Course);
            _students.Add(s);
        }

        //editing student by StudentID
        public bool Edit(Student item)
        {
            var existing = _students.FirstOrDefault(s => s.StudentID == item.StudentID);
            if (existing == null) return false;

            // Update details
            existing.Name = item.Name;
            existing.Gender = item.Gender;
            existing.Age = item.Age;
            existing.Course = item.Course;
            return true;
        }

        //deleting student by StudentID
        public bool Delete(Student item)
        {
            var existing = _students.FirstOrDefault(s => s.StudentID == item.StudentID);
            if (existing == null) return false;

            _students.Remove(existing);
            return true;
        }

    // returns a copy of the student list
        public List<Student> List()
        {
            return new List<Student>(_students);
        }


        // find student by ID
        public Student FindById(string id) => _students.FirstOrDefault(s => s.StudentID == id);

        // sorting methods
        public List<Student> SortByName() => _students.OrderBy(s => s.Name).ToList();

        public List<Student> SortByAge() => _students.OrderBy(s => s.Age).ToList();
    }
}

using System;
using System.Collections.Generic;

namespace CampusLink
{
    class Program
    {
        static void Main(string[] args)
        {
            var manager = new StudentManager();// manages student records

            // Menu options 
            while (true)
            {
                Console.WriteLine("\n--- CampusLink ---");
                Console.WriteLine("1. Register Student");
                Console.WriteLine("2. View Student Roster");
                Console.WriteLine("3. Edit Student");
                Console.WriteLine("4. Delete Student");
                Console.WriteLine("5. Sort Roster");
                Console.WriteLine("6. Exit");
                Console.Write("Choose option (1-6): ");
                var choice = Console.ReadLine()?.Trim(); //read user input

            // Menu loop
                switch (choice)
                {
                    case "1": Register(manager); break;
                    case "2": ViewRoster(manager.List()); break;
                    case "3": EditStudent(manager); break;
                    case "4": DeleteStudent(manager); break;
                    case "5": SortMenu(manager); break;
                    case "6": Console.WriteLine("Exiting..."); return;
                    default: Console.WriteLine("Invalid choice. Try again."); break;
                }
            }
        }

        // Register new student
        static void Register(StudentManager manager)
        {
            try
            {
                Console.WriteLine("\n-- Register new student --");
                var name = ReadNonEmpty("Name: ");
                var gender = ReadGender("Gender: ");
                var age = ReadInt("Age: ");
                var course = ReadCourse("Course: ");

                var student = new Student(name, gender, age, course); // creating student object
                manager.Add(student);
                Console.WriteLine("Student details registered successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // Viewing all students
        static void ViewRoster(List<Student> students)
        {
            Console.WriteLine("\n-- Student Roster --");
            if (students.Count == 0)
            {
                Console.WriteLine("No students registered yet.");
                return;
            }

        // table header
            Console.WriteLine($"{"ID",-10}  {"Name",-20}  {"Gender",-6}  {"Age",3} {"Course",-20}");
            Console.WriteLine(new string('-', 46));
            foreach (var s in students)
            {
                Console.WriteLine(s);
            }
        }

        // Edit existing student
        static void EditStudent(StudentManager manager)
        {
            Console.WriteLine("\n-- Edit Student --");
            var id = ReadNonEmpty("Enter Student ID to edit (e.g., STU-0001): ");
            var existing = manager.FindById(id);
            if (existing == null)
            {
                Console.WriteLine($"⚠ No student found with ID '{id}'.");
                return;
            }

            Console.WriteLine($"Editing {existing.Name} ({existing.StudentID}). Leave blank to keep existing value.");
            var name = ReadOptional("Name", existing.Name);
            var gender = ReadOptionalGender("Gender (Male/Female)", existing.Gender);
            var age = ReadOptionalInt("Age", existing.Age);
            var course = ReadOptionalCourse("Course", existing.Course);

            try
            {
                // creating updated student details
                var updated = new Student(existing.StudentID, name, gender, age, course);
                var ok = manager.Edit(updated);
                Console.WriteLine(ok ? "✅ Student updated." : "❌ Update failed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // Delete student by ID
        static void DeleteStudent(StudentManager manager)
        {
            Console.WriteLine("\n-- Delete Student --");
            var id = ReadNonEmpty("Enter Student ID to delete: ");
            var existing = manager.FindById(id);
            if (existing == null)
            {
                Console.WriteLine($"⚠ No student found with ID '{id}'.");
                return;
            }

            Console.Write($"Are you sure you want to delete {existing.Name} (y/n)? ");
            var confirm = Console.ReadLine()?.Trim().ToLower();
            if (confirm == "y" || confirm == "yes")
            {
                var ok = manager.Delete(existing);
                Console.WriteLine(ok ? "✅ Student deleted." : "❌ Deletion failed.");
            }
            else Console.WriteLine("Cancelled.");
        }

        // Sorting menu
        static void SortMenu(StudentManager manager)
        {
            Console.WriteLine("\n-- Sort Roster --");
            Console.WriteLine("1. Sort by Name");
            Console.WriteLine("2. Sort by Age");
            Console.Write("Choose (1-2): ");
            var c = Console.ReadLine()?.Trim();
            switch (c)
            {
                case "1":
                    ViewRoster(manager.SortByName());
                    break;
                case "2":
                    ViewRoster(manager.SortByAge());
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }

        // ---------------- INPUT HELPERS ----------------
        // Reads non-empty string input
        static string ReadNonEmpty(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input)) return input.Trim();
                Console.WriteLine("⚠ Value cannot be empty.");
            }
        }

        static string ReadGender(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input)) { Console.WriteLine("⚠ Gender is required."); continue; }
                var val = input.Trim().ToLower();
                if (val == "m" || val == "male") return "Male";
                if (val == "f" || val == "female") return "Female";
                Console.WriteLine("Enter 'Male' or 'Female' (or M/F).");
            }
        }

        static int ReadInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var input = Console.ReadLine();
                if (int.TryParse(input, out int v))
                {
                    if (v >= 18 && v <= 80) return v;
                    Console.WriteLine("⚠ Enter a valid age between 18 and 80.");
                }
                else Console.WriteLine("⚠ Enter a numeric age.");
            }
        }

        // For editing, we allow blank input to keep old value
        static string ReadOptional(string fieldName, string existing)
        {
            Console.Write($"{fieldName} ({existing}): ");
            var input = Console.ReadLine();
            return string.IsNullOrWhiteSpace(input) ? existing : input.Trim();
        }

        static string ReadOptionalGender(string prompt, string existing)
        {
            while (true)
            {
                Console.Write($"{prompt} ({existing}): ");
                var input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input)) return existing;
                var val = input.Trim().ToLower();
                if (val == "m" || val == "male") return "Male";
                if (val == "f" || val == "female") return "Female";
                Console.WriteLine("Enter 'Male' or 'Female' (or M/F) or leave blank to keep existing.");
            }
        }

        static int ReadOptionalInt(string prompt, int existing)
        {
            while (true)
            {
                Console.Write($"{prompt} ({existing}): ");
                var input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input)) return existing;
                if (int.TryParse(input, out int v) && v >= 18 && v <= 80) return v;
                Console.WriteLine("⚠ Enter a numeric age between 18 and 80 or leave blank.");
            }
        }
        // Reads non-empty course input
                static string ReadCourse(string prompt)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
                throw new Exception("Course cannot be empty");
            return input.Trim();
        }

        // Reads optional course input for editing/ update
        static string ReadOptionalCourse(string fieldName, string existing)
        {
            Console.Write($"{fieldName} ({existing}): ");
            var input = Console.ReadLine();
            return string.IsNullOrWhiteSpace(input) ? existing : input.Trim();
        }
    }
}


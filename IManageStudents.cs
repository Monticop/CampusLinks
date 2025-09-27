using System.Collections.Generic;

namespace CampusLink
{
    // Interface which defines "rules" that StudentManager must follow
    // <T> is a generic type placeholder which will be replaced by Student class or any other class
    public interface IManageStudents<T>
    {
        void Add(T item); // add new student
        bool Edit(T item); // edit existing student
        bool Delete(T item); // delete existing student
        List<T> List(); // list all students
    }
}
using System.Collections.Generic;
using SA.Data.Entities;

namespace SA.Data.Interfaces
{
    public interface IStudentRepository
    {
        void SaveStudent(Student student);
        IEnumerable<Student> GetAllStudents();
        Student GetStudent(long id);
        void DeleteStudent(long id);
        void UpdateStudent(Student student);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SA.Data.DataContexts;
using SA.Data.Entities;
using SA.Data.Interfaces;

namespace SA.Data.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private StudentContext context;
        private DbSet<Student> studentEntity;

        public StudentRepository(StudentContext context)
        {
            this.context = context;
            this.studentEntity = context.Set<Student>();
        }

        public void DeleteStudent(long id)
        {
            var student = GetStudent(id);
            studentEntity.Remove(student);
            context.SaveChanges();
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return studentEntity.AsEnumerable();
        }

        public Student GetStudent(long id)
        {
            return studentEntity.SingleOrDefault(s => s.Id == id);
        }

        public void SaveStudent(Student student)
        {
            context.Entry(student).State = EntityState.Added;
            context.SaveChanges();
        }

        public void UpdateStudent(Student student)
        {
            context.SaveChanges();
        }
    }
}

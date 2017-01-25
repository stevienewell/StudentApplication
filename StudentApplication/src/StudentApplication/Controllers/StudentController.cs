using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SA.Data.Entities;
using SA.Data.Interfaces;
using StudentApplication.ViewModels;

namespace StudentApplication.Controllers
{
    public class StudentController : Controller
    {
        private IStudentRepository studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = studentRepository.GetAllStudents()
                .Select(s => new StudentViewModel
                {
                    Id = s.Id,
                    Name = $"{s.FirstName} {s.LastName}",
                    EnrollmentNo = s.EnrollmentNo,
                    Email = s.Email
                });
            return View(model);
        }

        [HttpGet]
        public IActionResult AddEditStudent(long? id)
        {
            var model = new StudentViewModel();
            if (id.HasValue)
            {
                var student = studentRepository.GetStudent(id.Value);
                if (student != null)
                {
                    model.Id = student.Id;
                    model.FirstName = student.FirstName;
                    model.LastName = student.LastName;
                    model.Email = student.Email;
                    model.EnrollmentNo = student.EnrollmentNo;
                }
            }
            return PartialView("~/Views/Student/_AddEditStudent.cshtml", model);
        }

        [HttpPost]
        public IActionResult AddEditStudent(long? id, StudentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var isNew = !id.HasValue;
                var student = isNew ? new Student { AddedDate = DateTime.UtcNow } : studentRepository.GetStudent(id.Value);
                student.FirstName = viewModel.FirstName;
                student.LastName = viewModel.LastName;
                student.Email = viewModel.Email;
                student.EnrollmentNo = viewModel.EnrollmentNo;
                student.IpAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                student.ModifiedDate = DateTime.UtcNow;

                if (isNew)
                    studentRepository.SaveStudent(student);
                else
                    studentRepository.UpdateStudent(student);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DeleteStudent(long id)
        {
            var student = studentRepository.GetStudent(id);
            var viewModel = new StudentViewModel { Name = $"{student.FirstName} {student.LastName}" };
            return PartialView("~/Views/Student/_DeleteStudent.cshtml", viewModel);
        }

        [HttpPost]
        public IActionResult DeleteStudent(long id, FormCollection form)
        {
            studentRepository.DeleteStudent(id);
            return RedirectToAction("Index");
        }
    }
}


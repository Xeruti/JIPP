using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;
using Project.Models.Domain;

namespace Project.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ProjectDbContext dbContext;

        public StudentsController(ProjectDbContext DbContext)
        {
            dbContext = DbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var students = await dbContext.Students.ToListAsync();
            return View(students);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Add(AddStudentViewModel addStudentRequest)
        {
            var student = new Student()
            {
                Id = Guid.NewGuid(),
                Name = addStudentRequest.Name,
                Email = addStudentRequest.Email,
                Semester = addStudentRequest.Semester,
                Specialization = addStudentRequest.Specialization
            };

            await dbContext.Students.AddAsync(student);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var student = await dbContext.Students.FirstOrDefaultAsync(x => x.Id == id);

            if (student != null) {

                var viewModel = new UpdateStudentViewModel()
                {
                    Id = student.Id,
                    Name = student.Name,
                    Email = student.Email,
                    Semester = student.Semester,
                    Specialization = student.Specialization
                };

                return await Task.Run( () => View("View",viewModel));

            };


            return RedirectToAction("Index");
        }

        [HttpPost]

        public async Task<IActionResult> View(UpdateStudentViewModel model)
        {
            var student = await dbContext.Students.FindAsync(model.Id);

            if (student != null)
            {
                student.Name = model.Name;
                student.Email = model.Email;
                student.Semester = model.Semester;
                student.Specialization = model.Specialization;

                await dbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateStudentViewModel model)
        {
            var student = await dbContext.Students.FindAsync(model.Id);

            if (student != null)
            {
                dbContext.Students.Remove(student);
                await dbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

    }
}

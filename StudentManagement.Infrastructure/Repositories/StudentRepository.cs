using Microsoft.EntityFrameworkCore;
using StudentManagement.Domain.Interface;
using StudentManagement.Domain.Models;
using StudentManagement.Infrastructure.DbContext;

namespace StudentManagement.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext dbContext;

        public StudentRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Add(Student student)
        {
            this.dbContext.Students.Add(student);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<Student>> Get()
        {
            var data = await dbContext.Students.ToListAsync();
            return data;
        }

        public async Task<Student> GetById(int id)
        {
            var data = await dbContext.Students.FirstOrDefaultAsync(x => x.Id == id);
            return data;
        }

        public async Task Update(int Id, Student student)
        {
            var data = await dbContext.Students.FindAsync(Id);
            if (data != null)
            {
                data.StudentName = student.StudentName;
                data.Age = student.Age;
                data.Grade = student.Grade;
                await dbContext.SaveChangesAsync();
            }
            return;
        }

        public async Task Delete(int id)
        {
            var data = await dbContext.Students.FindAsync(id);
            if (data != null)
            {
                dbContext.Students.Remove(data);
                dbContext.SaveChanges();
            }
            return;
        }
    }
}

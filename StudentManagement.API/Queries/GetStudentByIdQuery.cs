using MediatR;
using StudentManagement.Domain.Models;

namespace StudentManagement.API.Queries
{
    public record GetStudentByIdQuery(int id) : IRequest<Student>;
}

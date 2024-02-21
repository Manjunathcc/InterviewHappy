using MediatR;
using StudentManagement.Domain.Models;

namespace StudentManagement.API.Queries
{
    public record GetStudentsQuery : IRequest<List<Student>>;
}

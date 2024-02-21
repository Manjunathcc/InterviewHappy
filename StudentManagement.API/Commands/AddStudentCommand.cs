using MediatR;
using StudentManagement.Domain.Models;

namespace StudentManagement.API.Commands
{
    public record AddStudentCommand(Student student) : IRequest<Unit>;
}

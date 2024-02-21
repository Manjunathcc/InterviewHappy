using MediatR;
using StudentManagement.Domain.Models;

namespace StudentManagement.API.Commands
{
    public record UpdateStudentCommand(int Id, Student student) : IRequest<Unit>;
}

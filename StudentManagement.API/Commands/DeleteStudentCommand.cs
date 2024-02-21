using MediatR;

namespace StudentManagement.API.Commands
{
    public record DeleteStudentCommand(int Id) : IRequest<Unit>;
}

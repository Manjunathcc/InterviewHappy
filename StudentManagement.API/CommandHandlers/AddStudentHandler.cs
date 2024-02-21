using MediatR;
using StudentManagement.API.Commands;
using StudentManagement.Domain.Interface;

namespace StudentManagement.API.CommandHandlersCommandHandlers
{
    public class AddStudentHandler : IRequestHandler<AddStudentCommand, Unit>
    {
        private readonly IStudentRepository studentRepository;

        public AddStudentHandler(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }
        public async Task<Unit> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await this.studentRepository.Add(request.student);

                return Unit.Value;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

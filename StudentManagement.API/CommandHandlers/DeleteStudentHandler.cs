using MediatR;
using StudentManagement.API.Commands;
using StudentManagement.Domain.Interface;

namespace SchoolManagament.API.CommandHandlers
{
    public class DeleteStudentHandler : IRequestHandler<DeleteStudentCommand, Unit>
    {
        private readonly IStudentRepository studentRepository;

        public DeleteStudentHandler(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }
        public async Task<Unit> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await this.studentRepository.Delete(request.Id);

                return Unit.Value;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

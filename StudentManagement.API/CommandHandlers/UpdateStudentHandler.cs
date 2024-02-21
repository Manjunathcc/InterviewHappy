using MediatR;
using StudentManagement.API.Commands;
using StudentManagement.Domain.Interface;

namespace SchoolManagament.API.CommandHandlers
{
    public class UpdateStudentHandler : IRequestHandler<UpdateStudentCommand, Unit>
    {
        private readonly IStudentRepository studentRepository;

        public UpdateStudentHandler(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }
        public async Task<Unit> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await this.studentRepository.Update(request.Id, request.student);

                return Unit.Value;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

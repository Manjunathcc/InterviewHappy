using MediatR;
using StudentManagement.API.Queries;
using StudentManagement.Domain.Interface;
using StudentManagement.Domain.Models;

namespace StudentManagement.API.QueryHandlers
{
    public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, Student>
    {
        private readonly IStudentRepository studentRepository;
        public GetStudentByIdQueryHandler(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }
        public Task<Student> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = this.studentRepository.GetById(request.id);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

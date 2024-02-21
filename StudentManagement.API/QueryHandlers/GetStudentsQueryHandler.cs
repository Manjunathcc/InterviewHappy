using MediatR;
using StudentManagement.API.Queries;
using StudentManagement.Domain.Interface;
using StudentManagement.Domain.Models;

namespace SchoolManagament.API.QueryHandlers
{
    public class GetStudentsQueryHandler : IRequestHandler<GetStudentsQuery, List<Student>>
    {
        private readonly IStudentRepository studentRepository;
        public GetStudentsQueryHandler(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }
        public Task<List<Student>> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var data = this.studentRepository.Get();

                return data;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

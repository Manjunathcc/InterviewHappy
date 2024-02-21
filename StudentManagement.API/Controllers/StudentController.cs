using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using StudentManagement.API.Commands;
using StudentManagement.API.Queries;
using StudentManagement.Domain.Models;

namespace StudentManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public class StudentController : ControllerBase
    {
        private readonly IMediator mediator;

        public StudentController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Gets the list of All Students
        /// </summary>
        /// <returns>The list of Students</returns>
        [HttpGet]
        [EnableRateLimiting("Fixed")]
        public async Task<IActionResult> GetStudents()
        {
            var result = await mediator.Send(new GetStudentsQuery());
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        /// <summary>
        /// Retrieves a specific student by its ID.
        /// </summary>
        /// <param name="id">The ID of the item to retrieve.</param>
        /// <returns>The student associated to specified ID.</returns>
        [HttpGet("{id:int}", Name = "GetStudentById")]
        [DisableRateLimiting]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var result = await mediator.Send(new GetStudentByIdQuery(id));
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        /// <summary>
        /// Creates an Student
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///    POST api/Student
        ///    {
        ///       "studentName":"Manju"
        ///       "age":22,
        ///       "grade":"A"
        ///    }
        /// </remarks>
        /// <param name="student">The item to create</param>
        /// <response code = "201">Returns No Conteent</response>
        /// <response code = "400">If the item is null</response>
        [HttpPost]
        public async Task<ActionResult> AddStudent([FromBody] Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await mediator.Send(new AddStudentCommand(student));

            return StatusCode(201);
        }


        /// <summary>
        /// Updates an Student
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///    PUT api/Student
        ///    {
        ///       "id": 1
        ///       "studentName":"Manju"
        ///       "age":22,
        ///       "grade":"A"
        ///    }
        /// </remarks>
        /// <param name="Id">The ID of student to update</param>
        /// <param name="student">The updated student data</param>
        /// <response code = "200">Returns 200 status code for successful Updation</response>
        /// <response code = "400">If the item is null</response>
        [HttpPut]
        public async Task<ActionResult> UpdateStudent(int Id, [FromBody] Student student)
        {
            if (student is null)
            {
                throw new ArgumentNullException(nameof(student));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await mediator.Send(new UpdateStudentCommand(Id, student));

            return StatusCode(200);
        }

        /// <summary>
        /// Deletes an student by its ID.
        /// </summary>
        /// <param name="id">The ID of the student to delete.</param>
        /// <returns>No content.</returns>
        [HttpDelete]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            await mediator.Send(new DeleteStudentCommand(id));

            return StatusCode(204);
        }
    }
}

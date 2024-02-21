using AutoFixture;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StudentManagement.API.Controllers;
using StudentManagement.API.Queries;
using StudentManagement.Domain.Models;

namespace StudentManagement.Tests
{
    [TestClass]
        public class StudentControllerTest
        {
            private Mock<IMediator> mediatorMock;
            private StudentController studentController;
            private IFixture fixture;

            [TestInitialize]
            public void TestInitialize()
            {
                this.mediatorMock = new Mock<IMediator>();
                fixture = new Fixture();
                this.studentController = new StudentController(this.mediatorMock.Object);
            }

            [TestMethod]
            public void ConstructorTest()
            {
                // Arrange & Act
                var controller = new StudentController(this.mediatorMock.Object);

                //Assert
                Assert.IsNotNull(controller);
            }

            [TestMethod]
            public async Task GetStudents_ReturnsOk_WhenStudentsExist()
            {
                // Arrange
                var students = this.fixture.CreateMany<Student>().ToList();
                this.mediatorMock.Setup(m => m.Send(It.IsAny<GetStudentsQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(students);

                // Act
                var response = await studentController.GetStudents();

                // Assert
                this.mediatorMock.Verify(x => x.Send(It.IsAny<GetStudentsQuery>(), It.IsAny<CancellationToken>()), Times.Once());
                var okResult = response as OkObjectResult;
                Assert.IsNotNull(okResult);
                Assert.AreEqual(200, okResult.StatusCode);
            }

            [TestMethod]
            public async Task GetStudents_ReturnsNotFound_WhenNoStudentsExist()
            {
                // Arrange
                List<Student> students = null;

                var mediatorMock = new Mock<IMediator>();
                mediatorMock.Setup(m => m.Send(It.IsAny<GetStudentsQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(students);

                // Act
                var response = await studentController.GetStudents();

                // Assert
                var notFoundResult = response as NotFoundResult;
                Assert.IsNotNull(notFoundResult);
                Assert.AreEqual(404, notFoundResult.StatusCode);
            }

            [TestMethod]
            public async Task GetStudentById_ReturnsOk_WhenStudentExists()
            {
                // Arrange
                var id = 1;
                var student = this.fixture.Build<Student>().With(x => x.StudentName, "manjunathcc").With(x => x.Id, 1).Create();
                mediatorMock.Setup(m => m.Send(It.IsAny<GetStudentByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(student);

                // Act
                var response = await studentController.GetStudentById(id);

                // Assert
                var okResult = response as OkObjectResult;
                Assert.IsNotNull(okResult);
                Assert.AreEqual(200, okResult.StatusCode);

                var returnedStudent = okResult.Value as Student;
                Assert.IsNotNull(returnedStudent);
                Assert.AreEqual(student.Id, returnedStudent.Id);
                Assert.AreEqual(student.StudentName, returnedStudent.StudentName);
            }

            [TestMethod]
            public async Task GetStudentById_ReturnsNotFound_WhenStudentDoesNotExist()
            {
                // Arrange
                var id = 1;

                Student student = null;
                this.mediatorMock.Setup(m => m.Send(It.IsAny<GetStudentByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(student);

                // Act
                var response = await studentController.GetStudentById(id);

                // Assert
                var notFoundResult = response as NotFoundResult;
                Assert.IsNotNull(notFoundResult);
                Assert.AreEqual(404, notFoundResult.StatusCode);
            }
        }
    }
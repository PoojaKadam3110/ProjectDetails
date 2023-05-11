using Microsoft.AspNetCore.Mvc;
using Moq;
using ProjectDetailsAPI.Controllers;
using ProjectDetailsAPI.Models.Domain;
using ProjectDetailsAPI.Services.IProjects;

namespace ProjectDetails.API.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

        }

        [Fact]
        public async Task GetAll_ReturnsListOfProjects()
        {
            // Arrange
            var mockDataStore = new Mock<IAddProjectRepository<Projects>>();
            var controller = new ProjectsController(mockDataStore.Object);
            var expectedProjects = new List<Projects> 
            {
            new Projects { Id = 1, ProjectName = "My Project1", ClientName = "Test1", projectManager = "Test1", projectUsers = "PoojaK", projectCost = 10000, CreatedBy = "Pooja1", CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now, UpdatedBy = "Pooja1",isActive = true, isDeleted = false },
            new Projects { Id = 2, ProjectName = "My Project2", ClientName = "Test2", projectManager = "Test2", projectUsers = "PoojaS", projectCost = 11000, CreatedBy = "Pooja2", CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now, UpdatedBy = "Pooja2", isActive = true, isDeleted = false },
            new Projects { Id = 3, ProjectName = "My Project3", ClientName = "Test3", projectManager = "Test3", projectUsers = "PoojaSK", projectCost = 12000, CreatedBy = "Pooja3", CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now, UpdatedBy = "Pooja3", isActive = true, isDeleted = false }
            };
            mockDataStore.Setup(ds => ds.GetAll(/* specify mock parameters */)).ReturnsAsync(expectedProjects);

            // Act
            var result = await controller.GetAll("Project", "Reza","Date",true,1,1000);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualProjects = Assert.IsAssignableFrom<IEnumerable<Projects>>(okResult.Value);
            Assert.Equal(expectedProjects.Count, actualProjects.Count());
            Assert.False(actualProjects.Any(p => p.isDeleted == true));

        }
    }
}
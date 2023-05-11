using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Moq;
using ProjectDetailsAPI.Controllers;
using ProjectDetailsAPI.Data;
using ProjectDetailsAPI.Models.Domain;
using ProjectDetailsAPI.Services;
using ProjectDetailsAPI.Services.IProjects;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;

namespace ProjectDetailsAPI.Test
{
    public class UnitTest1
    {

        [Fact]
        public async Task Test11()
        {
            // Arrange
            //ProjectDetailsDbContext db = new();
            //int count = 5;
            //var fakeData = A.CollectionOfDummy<Projects>(count).AsEnumerable();
            var dataStore = A.Fake<IAddProjectsRepository>();
            //A.CallTo(() => dataStore.AddProjects(db.Projects));
            var controller = new ProjectsController(dataStore);
            // Act
              //A.CallTo(() => dataStore.AddProjects(db.Projects));
            //var actionResult = await controller.AddProjects(db.Projects);

            // Assert
            //var result = await actionResult.Result as OkObjectResult;
            //var returnData = result.Value as IEnumerable<Projects>;
        }


    }
}
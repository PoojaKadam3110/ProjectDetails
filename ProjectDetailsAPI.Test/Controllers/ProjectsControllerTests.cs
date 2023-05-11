using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;
using ProjectDetailsAPI.Controllers;
using ProjectDetailsAPI.Data;
using ProjectDetailsAPI.Models.Domain;
using ProjectDetailsAPI.Services;
using System;
using System.Threading.Tasks;
using Xunit;
using ProjectDetailsAPI.Data.Command;

namespace ProjectDetailsAPI.Test.Controllers
{
    public class ProjectsControllerTests 
    {
        private MockRepository mockRepository;

        private Mock<ProjectDetailsDbContext> mockProjectDetailsDbContext;
        private readonly IConfiguration _configuration;
        private Mock<IMapper> mockMapper;
        private Mock<IMediator> mockMediator;
        private Mock<IUnitOfWork> mockUnitOfWork;

        public ProjectsControllerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockProjectDetailsDbContext = this.mockRepository.Create<ProjectDetailsDbContext>();
            this.mockMapper = this.mockRepository.Create<IMapper>();
            this.mockMediator = this.mockRepository.Create<IMediator>();
            this.mockUnitOfWork = this.mockRepository.Create<IUnitOfWork>();
        }

        //[Fact]
        //private ProjectsController CreateProjectsController()
        //{
        //    return new ProjectsController(
        //        this.mockProjectDetailsDbContext.Object,
        //        this.mockMapper.Object,
        //        this.mockMediator.Object,
        //        this.mockUnitOfWork.Object);
        //}

        [Fact]
        public void Get_StateUnderTest_ExpectedBehavior()
        {
            //// Arrange
            //var projectsController = this.CreateProjectsController();
            //int pageNumber = 0;
            //int pageSize = 0;

            //// Act
            //var result = projectsController.Get(
            //    pageNumber,
            //    pageSize);

            //// Assert
            //Assert.True(false);
            //this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task AddProjects_StateUnderTest_ExpectedBehavior()
        {
            //// Arrange
            //var projectsController = this.CreateProjectsController();
            //Projects projects = new Projects();
            //projects.ClientName = "String";
            //projects.ProjectName= "String";
            //projects.projectManager = "string";
            //projects.projectUsers = "string";
            //projects.ratePerHour = 10;
            //projects.projectCost = 100;
            //projects.CreatedDate = DateTime.Now;
            //projects.UpdatedDate = DateTime.Now;
            //projects.CreatedBy = "Pooja";
            //projects.UpdatedBy = "String";
            //projects.description= "Description";
            //projects.isActive = true;
            //projects.isDeleted = true;


            //// Act
            //var result = await projectsController.AddProjects(projects);

            //// Assert
            //Assert.True(true);
            //this.mockRepository.VerifyAll();
        }

        [Fact]
        public void GetById_StateUnderTest_ExpectedBehavior()
        {
            //// Arrange
            //var projectsController = this.CreateProjectsController();
            //int id = 0;

            //// Act
            //var result = projectsController.GetById(
            //    id);

            //// Assert
            //Assert.True(false);
            //this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task UpdateProjects_StateUnderTest_ExpectedBehavior()
        {
            //// Arrange
            //var projectsController = this.CreateProjectsController();
            //int id = 0;
            //Projects projects = null;

            //// Act
            //var result = await projectsController.UpdateProjects(
            //    id,
            //    projects);

            //// Assert
            //Assert.True(false);
            //this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task SoftDeleteProject_StateUnderTest_ExpectedBehavior()
        {
            //// Arrange
            //var projectsController = this.CreateProjectsController();
            //int id = 0;

            //// Act
            //var result = await projectsController.SoftDeleteProject(
            //    id);

            //// Assert
            //Assert.True(false);
            //this.mockRepository.VerifyAll();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Recademy.Application.Services.Abstractions;
using Recademy.Application.Services.Implementations;
using Recademy.Core.Models.Users;
using Recademy.Core.Types;
using Recademy.DataAccess;
using Recademy.DataAccess.Seeding;
using Recademy.Dto.Enums;
using Recademy.Dto.Projects;
using Recademy.Dto.Reviews;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recademy.Tests;

[TestFixture]
public sealed class ProjectTests : IDisposable
{
    private RecademyContext _context;

    private IProjectService _projectService;
    private IReviewService _reviewService;

    [OneTimeSetUp]
    public void SetUp()
    {
        _context = new RecademyContext(new DbContextOptionsBuilder<RecademyContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .UseLazyLoadingProxies()
            .Options,
            new DbContextSeeder());

        _context.Database.EnsureCreated();

        _projectService = new ProjectsService(_context);
        _reviewService = new ReviewService(_context);
    }

    [Test]
    public async Task CreateProject_ProjectExists()
    {
        ProjectInfoDto createdProject = await CreateProject();
        ProjectInfoDto project = _projectService.GetProjectInfo(createdProject.ProjectId);

        Assert.IsNotNull(project);
    }

    [Test]
    public async Task CreateProjectReviewRequest_RequestExists()
    {
        ReviewRequestInfoDto createdReviewRequest = await CreateReviewRequest();
        ReviewRequestInfoDto reviewRequest = _reviewService.GetReviewRequestById(createdReviewRequest.Id);

        Assert.IsNotNull(reviewRequest);
    }

    [Test]
    public async Task CreateProjectReviewResponse_ResponseExists()
    {
        ReviewResponseInfoDto createdReviewResponse = await CreateReviewResponse();
        ReviewResponseInfoDto reviewResponse = _reviewService.GetReviewResponseById(createdReviewResponse.Id);

        Assert.IsNotNull(reviewResponse);
    }

    private async Task<ProjectInfoDto> CreateProject()
    {
        var createArguments = new CreateProjectDto(AuthorId: 1, Title: "Project1", Description: "Project1 Description", Link: "Project1 Link", Tags: new List<string>());
        ProjectInfoDto createdProject = await _projectService.CreateProject(createArguments);

        return createdProject;
    }

    private async Task<ReviewRequestInfoDto> CreateReviewRequest()
    {
        ProjectInfoDto createdProject = await CreateProject();

        var createReviewRequest = new CreateReviewRequestDto
        {
            UserId = 1,
            ProjectId = createdProject.ProjectId,
            Comment = "Comment"
        };

        ReviewRequestInfoDto createdReviewRequest = await _reviewService.CreateReviewRequest(createReviewRequest);

        return createdReviewRequest;
    }

    private async Task<ReviewResponseInfoDto> CreateReviewResponse()
    {
        var mentor = new User
        {
            GithubUsername = "Mentor",
            Name = "Mentor",
            UserType = UserType.Mentor
        };

        User user = _context.Users.Add(mentor).Entity;
        await _context.SaveChangesAsync();

        ReviewRequestInfoDto reviewRequest = await CreateReviewRequest();

        var createReviewResponse = new CreateReviewResponseDto
        {
            RequestId = reviewRequest.Id,
            ReviewerId = user.Id,
            ReviewConclusion = ReviewConclusionDto.LooksGood,
            Comment = string.Empty
        };

        ReviewResponseInfoDto reviewResponse = await _reviewService.CreateReviewResponse(createReviewResponse);

        return reviewResponse;
    }

    public void Dispose()
    {
        _context?.Dispose();
    }
}
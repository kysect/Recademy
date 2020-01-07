using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Mock.Generators;
using Recademy.Context;
using Recademy.Models;
using Recademy.Types;

namespace Mock
{
    public class Mocker : IDisposable
    {
        private const int UsersGenCount = 15;
        private const int ProjectForUserCount = 4;

        private static readonly string ConnectionString =
            ConfigurationManager.ConnectionStrings["Database"].ConnectionString;

        private static readonly TypesGenerator TypesGenerator = new TypesGenerator();
        private static readonly PrimitiveGenerator PrimitiveGenerator = new PrimitiveGenerator();
        private static readonly Random Random = new Random();
        private static readonly HashSet<string> HashSet = new HashSet<string>();

        private readonly RecademyContext _db;

        public Mocker()
        {
            _db = CreateContext();
        }

        public void Dispose()
        {
            _db?.SaveChanges();
            _db?.Dispose();
        }

        public void Mock()
        {
            _db.Skills.RemoveRange(_db.Skills);
            AddSkills();

            _db.SaveChanges();
            for (int i = 0; i < UsersGenCount; i++)
                GenerateUsers();
        }

        private void AddSkills()
        {
            List<Skill> techs = TypesGenerator.GetTechnologiesList();
            _db.Skills.AddRange(techs);
        }

        private void GenerateUsers()
        {
            User newUser = TypesGenerator.GetUser();
            _db.Users.Add(newUser);
            _db.SaveChanges();

            List<UserSkill> userSkills = GenerateUserSkills(newUser);
            _db.UserSkills.AddRange(userSkills);
            _db.SaveChanges();

            for (int j = 0; j < ProjectForUserCount; ++j)
                GenerateProjectsInfo(newUser);
        }

        private static RecademyContext CreateContext()
        {
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder().UseSqlServer(ConnectionString);
            return new RecademyContext(builder.Options);
        }

        private void GenerateProjectsInfo(User user)
        {
            ProjectInfo newProject = TypesGenerator.GetProjectInfo(user);
            _db.ProjectInfos.Add(newProject);
            _db.SaveChanges();

            List<ProjectSkill> projectSkills = GenerateProjectSkills(newProject);
            _db.ProjectSkills.AddRange(projectSkills);
            _db.SaveChanges();

            if (Random.Next(0, 2) != 0)
                GenerateRequestResponse(newProject, user);
        }

        private void GenerateRequestResponse(ProjectInfo projectInfo, User user)
        {
            ReviewRequest newRequest = TypesGenerator.GetRequest(projectInfo, user, user.Id - 1);
            _db.ReviewRequests.Add(newRequest);

            if (newRequest.State != ProjectState.Requested)
            {
                ReviewResponse newResponse = TypesGenerator.GetResponse(newRequest, user.Id - 1);
                _db.ReviewResponses.Add(newResponse);
            }
        }

        private static List<ProjectSkill> GenerateProjectSkills(ProjectInfo projectInfo)
        {
            List<ProjectSkill> projectSkills = new List<ProjectSkill>();
            for (int k = 0; k < Random.Next(0, 3); k++)
            {
                string skillName = PrimitiveGenerator.GetSkillName();
                if (HashSet.Add(skillName))
                    projectSkills.Add(new ProjectSkill {ProjectId = projectInfo.Id, SkillName = skillName});
            }

            return projectSkills;
        }

        private static List<UserSkill> GenerateUserSkills(User user)
        {
            List<UserSkill> userSkills = new List<UserSkill>();
            int rndVal = Random.Next(0, 4);
            for (int k = 0; k < rndVal; k++)
            {
                string skillName = PrimitiveGenerator.GetSkillName();
                if (HashSet.Add(skillName))
                    userSkills.Add(new UserSkill {SkillName = skillName, UserId = user.Id});
            }

            return userSkills;
        }
    }
}
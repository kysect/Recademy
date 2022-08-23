using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Recademy.Api;
using Recademy.Core.Models;
using Recademy.Core.Types;
using Recademy.Mock.Extensions;
using Recademy.Mock.Generators;

namespace Recademy.Mock
{
    public class Mocker : IDisposable
    {
        private static readonly TypesGenerator TypesGenerator = new TypesGenerator();

        private readonly RecademyContext _db;

        public Mocker()
        {
            _db = CreateContext();
        }

        public Mocker(RecademyContext context)
        {
            _db = context ?? CreateContext();
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
            for (int i = 0; i < Configuration.UsersGenCount; i++)
                GenerateUser();
        }

        private void AddSkills()
        {
            List<Skill> techs = TypesGenerator.GetTechnologiesList();
            _db.Skills.AddRange(techs);
        }

        public User GenerateUser() => GenerateUser(UserType.CommonUser);
        public User GenerateMentorUser() => GenerateUser(UserType.Mentor);
        public User GenerateAdminUser() => GenerateUser(UserType.Admin);

        public User GenerateUser(UserType userType)
        {
            User newUser = TypesGenerator.GetUser(userType);
            _db.Users.Add(newUser);
            _db.SaveChanges();

            List<UserSkill> userSkills = GenerateUserSkills(newUser, Utilities.Random.Next(0, Configuration.MaxSkillsForUser));
            _db.UserSkills.AddRange(userSkills);
            _db.SaveChanges();

            for (int j = 0; j < Configuration.ProjectForUserCount; ++j)
                GenerateProjectsInfo(newUser);

            return newUser;
        }

        private static RecademyContext CreateContext()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder().UseSqlServer(connectionString);
            return new RecademyContext(builder.Options);
        }

        private void GenerateProjectsInfo(User user)
        {
            ProjectInfo newProject = TypesGenerator.GetProjectInfo(user);
            _db.ProjectInfos.Add(newProject);
            _db.SaveChanges();

            List<ProjectSkill> projectSkills = GenerateProjectSkills(newProject, Utilities.Random.Next(0, Configuration.MaxProjectsForUser));
            _db.ProjectSkills.AddRange(projectSkills);
            _db.SaveChanges();

            //TODO: Check that it is work ok
            GenerateRequestResponse(newProject, user);
            _db.SaveChanges();
        }

        private void GenerateRequestResponse(ProjectInfo projectInfo, User userRequest)
        {
            ProjectState state = Utilities.Random.Next(0, 2) == 0 ? ProjectState.Requested : ProjectState.Reviewed;

            int[] userIds = _db.Users.Select(s => s.Id).ToArray();
            int randomUserId = userIds[Utilities.Random.Next(0, userIds.Length)];
            User randomUser = _db.Users.Find(randomUserId);

            ReviewRequest newRequest = TypesGenerator.GetRequest(projectInfo, userRequest, state);
            _db.ReviewRequests.Add(newRequest);

            if (state == ProjectState.Reviewed)
            {
                ReviewResponse newResponse = TypesGenerator.GetResponse(newRequest, randomUser.Id);
                _db.ReviewResponses.Add(newResponse);
            }
        }

        private static List<ProjectSkill> GenerateProjectSkills(ProjectInfo projectInfo, int projectCount)
        {
            List<ProjectSkill> projectSkills = new List<ProjectSkill>();
            List<string> skills = DataLists.Skills;

            for (int k = 0; k < projectCount; k++)
            {
                if (skills.Count <= 0)
                    break;

                string skillName = skills.GetRandomValue();
                skills.Remove(skillName);

                projectSkills.Add(new ProjectSkill { ProjectId = projectInfo.Id, SkillName = skillName });
            }

            return projectSkills;
        }

        private static List<UserSkill> GenerateUserSkills(User user, int skillsCount)
        {
            List<UserSkill> userSkills = new List<UserSkill>();
            List<string> skills = DataLists.Skills;

            for (int k = 0; k < skillsCount; k++)
            {
                if (skills.Count <= 0)
                    break;

                string skillName = skills.GetRandomValue();
                skills.Remove(skillName);

                userSkills.Add(new UserSkill { SkillName = skillName, UserId = user.Id });
            }

            return userSkills;
        }
    }
}
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
        private static readonly string ConnectionString =
            ConfigurationManager.ConnectionStrings["Database"].ConnectionString;

        private static readonly TypesGenerator TypesGenerator = new TypesGenerator();

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
            for (int i = 0; i < Configuration.UsersGenCount; i++)
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

            List<UserSkill> userSkills = GenerateUserSkills(newUser, Utilities.Random.Next(0, Configuration.MaxSkillsForUser));
            _db.UserSkills.AddRange(userSkills);
            _db.SaveChanges();

            for (int j = 0; j < Configuration.ProjectForUserCount; ++j)
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

            List<ProjectSkill> projectSkills = GenerateProjectSkills(newProject, Utilities.Random.Next(0, Configuration.MaxProjectsForUser));
            _db.ProjectSkills.AddRange(projectSkills);
            _db.SaveChanges();

            User user2 = TypesGenerator.GetUser();

            GenerateRequestResponse(newProject, user, user2);
            _db.SaveChanges();
        }

        private void GenerateRequestResponse(ProjectInfo projectInfo, User userRequest, User userResponse)
        {
            ProjectState state = Utilities.Random.Next(0, 2) == 0 ? ProjectState.Requested : ProjectState.Reviewed;

            ReviewRequest newRequest = TypesGenerator.GetRequest(projectInfo, userRequest, state);
            _db.ReviewRequests.Add(newRequest);

            if (state == ProjectState.Reviewed)
            {
                _db.Users.Add(userResponse);
                ReviewResponse newResponse = TypesGenerator.GetResponse(newRequest, userResponse.Id);
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

                int random = Utilities.Random.Next(skills.Count);

                string skillName = skills[random];

                skills.Remove(skillName);

                projectSkills.Add(new ProjectSkill { ProjectId = projectInfo.Id, SkillName = skillName});
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

                int random = Utilities.Random.Next(skills.Count);

                string skillName = skills[random];

                skills.Remove(skillName);

                userSkills.Add(new UserSkill { SkillName = skillName, UserId = user.Id});
            }

            return userSkills;
        }
    }
}
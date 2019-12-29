using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Mock.Generators;
using Recademy.Context;
using Recademy.Models;
using Recademy.Types;

namespace Mock
{
    internal class Program
    {
        private const string ConnectionString =
            "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=RecademyDB;Integrated Security=True;";

        private const int UsersGenCount = 15;
        private const int ProjectForUserCount = 4;
        private static readonly TypesGenerator TypesGenerator = new TypesGenerator();
        private static readonly PrimitiveGenerator PrimitiveGenerator = new PrimitiveGenerator();
        private static readonly Random Random = new Random();
        private static readonly HashSet<string> HashSet = new HashSet<string>();

        private static void Main(string[] args)
        {
            int rndVal;

            using RecademyContext db = CreateContext();

            db.Skills.RemoveRange(db.Skills);
            db.SaveChanges();   

            List<Skill> techs = TypesGenerator.GetTechnologiesList();
            db.Skills.AddRange(techs);
            db.SaveChanges();

            for (int i = 0; i < UsersGenCount; i++)
            {
                User newUser = TypesGenerator.GetUser();
                db.Users.Add(newUser);
                db.SaveChanges();

                List<UserSkill> userSkills = GenerateUserSkills(newUser);
                db.UserSkills.AddRange(userSkills);
                db.SaveChanges();

                for (int j = 0; j < ProjectForUserCount; ++j)
                {
                    ProjectInfo newProject = TypesGenerator.GetProjectInfo(newUser);
                    db.ProjectInfos.Add(newProject);
                    db.SaveChanges();
                        
                    List<ProjectSkill> projectSkills = GenerateProjectSkills(newProject);
                    db.ProjectSkills.AddRange(projectSkills);
                    db.SaveChanges();

                    rndVal = Random.Next(0, 2);
                    if (rndVal == 0)
                        continue;

                    ReviewRequest newRequest = TypesGenerator.GetRequest(newProject, newUser, newUser.Id - 1);
                    db.ReviewRequests.Add(newRequest);
                    db.SaveChanges();

                    if (newRequest.State == ProjectState.Requested)
                        continue;

                    ReviewResponse newResponse = TypesGenerator.GetResponse(newRequest, newUser.Id - 1);
                    db.ReviewResponses.Add(newResponse);
                    db.SaveChanges();
                }
            }
            db.SaveChanges();
        }

        private static RecademyContext CreateContext()
        {
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder().UseSqlServer(ConnectionString);
            return new RecademyContext(builder.Options);
        }

        private static List<ProjectSkill> GenerateProjectSkills(ProjectInfo projectInfo)
        {
            List<ProjectSkill> projectSkills = new List<ProjectSkill>();
            int rndVal = Random.Next(0, 3);

            for (int k = 0; k < rndVal; ++k)
            {
                string skillName = PrimitiveGenerator.GetSkillName();
                if (HashSet.Add(skillName))
                    projectSkills.Add(new ProjectSkill { ProjectId = projectInfo.Id, SkillName = skillName });
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
                    userSkills.Add(new UserSkill { SkillName = skillName, UserId = user.Id });
            }

            return userSkills;
        }
    }
}
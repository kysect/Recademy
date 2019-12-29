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

        private static void Main(string[] args)
        {
            TypesGenerator typesGenerator = new TypesGenerator();
            SkillGenerator skillGen = new SkillGenerator();

            Random _rnd = new Random();
            int rndVal;
            HashSet<string> a;

            using (RecademyContext db = CreateContext())
            {
                db.Skills.RemoveRange(db.Skills);
                db.SaveChanges();   

                List<Skill> techs = skillGen.GetTechnologiesList();
                db.Skills.AddRange(techs);
                db.SaveChanges();
            }

            using (RecademyContext db = CreateContext())
            {
                for (int i = 0; i < UsersGenCount; ++i)
                {
                    User newUser = typesGenerator.GetUser();
                    db.Users.Add(newUser);
                    db.SaveChanges();

                    rndVal = _rnd.Next(0, 4);
                    a = new HashSet<string>();
                    for (int k = 0; k < rndVal; ++k)
                    {
                        string skillName = skillGen.GetSkillName();

                        if (a.Add(skillName))
                            db.UserSkills.Add(new UserSkill {SkillName = skillName, UserId = newUser.Id});
                    }

                    db.SaveChanges();

                    for (int j = 0; j < ProjectForUserCount; ++j)
                    {
                        ProjectInfo newProject = typesGenerator.GetProjectInfo(newUser);
                        db.ProjectInfos.Add(newProject);
                        db.SaveChanges();

                        rndVal = _rnd.Next(0, 3);

                        a = new HashSet<string>();
                        for (int k = 0; k < rndVal; ++k)
                        {
                            string skillName = skillGen.GetSkillName();
                            if (a.Add(skillName))
                                db.ProjectSkills.Add(new ProjectSkill {ProjectId = newProject.Id, SkillName = skillName});
                        }
                        db.SaveChanges();

                        rndVal = _rnd.Next(0, 2);
                        if (rndVal == 0)
                            continue;

                        ReviewRequest newRequest = typesGenerator.GetRequest(newProject, newUser, newUser.Id - 1);
                        db.ReviewRequests.Add(newRequest);
                        db.SaveChanges();

                        if (newRequest.State == ProjectState.Requested)
                            continue;

                        ReviewResponse newResponse = typesGenerator.GetResponse(newRequest, newUser.Id - 1);
                        db.ReviewResponses.Add(newResponse);
                        db.SaveChanges();
                    }
                }
                db.SaveChanges();
            }
        }

        private static RecademyContext CreateContext()
        {
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder().UseSqlServer(ConnectionString);
            return new RecademyContext(builder.Options);
        }
    }
}
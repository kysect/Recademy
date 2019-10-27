using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Mock.Generators;
using Recademy.Models;
using Recademy.Context;
using Recademy.Types;

namespace Mock
{
    class Program
    {
        public const string ConnectionString =
            "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=RecademyDB;Integrated Security=True;";

        public const int UsersGenCount = 16;
        public const int ProjectForUserCount = 6;


        static void Main(string[] args)
        {
            UserGenerator userGen = new UserGenerator();
            ProjectInfoesGenerator projGen = new ProjectInfoesGenerator();
            SkillGenerator skillGen = new SkillGenerator();
            ReviewRequestGenerator rewGen = new ReviewRequestGenerator();
            ReviewResponseGenerator rewresGen = new ReviewResponseGenerator();

            List<User> users = new List<User>();
            List<ProjectInfo> projectInfos = new List<ProjectInfo>();
            List<Skill> skills = new List<Skill>();
            List<ReviewRequest> rewRequests = new List<ReviewRequest>();

            Random _rnd = new Random();
            int rndVal;

            using (var db = CreateContext())
            {
                var skillsForDeleteList = db.Skills.ToList();
                db.Skills.RemoveRange(skillsForDeleteList);
                db.SaveChanges();

                var techs = skillGen.GetTechnologiesList();
                db.Skills.AddRange(techs);

                db.SaveChanges();
            }

            using (var db = CreateContext())
            {
                for (int i = 0; i < UsersGenCount; ++i)
                {
                    var newUser = userGen.GetUser();
                    db.Users.Add(newUser);
                    db.SaveChanges();

                    rndVal = _rnd.Next(0, 4);
                    for (int k = 0; k < rndVal; ++k)
                    {
                        db.UserSkills.Add(new UserSkill() { SkillName = skillGen.GetSkillName(), UserId = newUser.Id});
                    }

                    db.SaveChanges();

                    for (int j = 0; j < ProjectForUserCount; ++j)
                    {
                        var newProject = projGen.GetProjectInfo(newUser);

                        db.ProjectInfos.Add(newProject);

                        db.SaveChanges();

                        rndVal = _rnd.Next(0, 3);

                        for(int k = 0;k<rndVal;++k)
                        {
                            db.ProjectSkills.Add(new ProjectSkill() { ProjectId = newProject.Id, SkillName = skillGen.GetSkillName() });
                        }

                        db.SaveChanges();


                        rndVal = _rnd.Next(0, 2);



                        if (rndVal == 0)
                            continue;

                        var newRequest = rewGen.GetRequest(newProject, newUser, newUser.Id - 1);

                        db.ReviewRequests.Add(newRequest);

                        db.SaveChanges();

                        if (newRequest.State == ProjectState.Requested)
                            continue;

                        var newResponse = rewresGen.GetResponse(newRequest, newUser.Id - 1);


                        db.ReviewResponses.Add(newResponse);

                        db.SaveChanges();
                    }
                }


                db.SaveChanges();





            }
        }

        private static RecademyContext CreateContext()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseSqlServer(ConnectionString);
            var context = new RecademyContext(builder.Options);
            return context;
        }
    }
}

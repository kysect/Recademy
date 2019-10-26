using System;
using System.Collections.Generic;
using System.Text;
using Recademy.Models;
using Recademy.Types;

namespace Mock.Generators
{
    class ReviewRequestGenerator
    {
        private Random gen = new Random();
        DateTime RandomDay()
        {
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range));
        }



        public ReviewRequest GetRequest(ProjectInfo project, User user, int randomId)
        {
            if (user.Id == randomId)
                return null;

            var randVal = gen.Next(0, 2);

            ReviewRequest result;
            if (randVal == 0)
                result = new ReviewRequest() { User = user, DateCreate = RandomDay(), State = ProjectState.Requested, ProjectInfo = project, ProjectId = project.Id };
            else
            {
                result = new ReviewRequest() { User = user, DateCreate = RandomDay(), State = ProjectState.Reviewed, ProjectInfo = project, ProjectId = project.Id };
            }
            return result;
        }
    }
}

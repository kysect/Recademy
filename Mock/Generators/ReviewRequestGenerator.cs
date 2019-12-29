﻿using System;
using Recademy.Models;
using Recademy.Types;

namespace Mock.Generators
{
    public class ReviewRequestGenerator
    {
        private readonly Random _gen = new Random();

        private DateTime RandomDay()
        {
            DateTime start = new DateTime(DateTime.Now.Year, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(_gen.Next(range));
        }

        public ReviewRequest GetRequest(ProjectInfo project, User user, int randomId)
        {
            if (user.Id == randomId)
                return null;

            return new ReviewRequest
            {
                User = user,
                DateCreate = RandomDay(),
                ProjectInfo = project,
                ProjectId = project.Id,
                State = _gen.Next(0, 2) == 0 ? ProjectState.Requested : ProjectState.Reviewed
            };
        }
    }
}
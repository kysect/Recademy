using System;
using System.Collections.Generic;
using System.Linq;
using Recademy.Core.Dto;
using Recademy.Core.Types;

namespace Recademy.Mock.Generators
{
    public static class InstanceFactory
    {
        public static string GenerateString(int size = 15)
        {
            const string chars = "qwertyuiopasdfghjklzxcvbnmABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            List<char> result = CreateRange(size, () => chars[Utilities.Random.Next(chars.Length)]);
            return new string(result.ToArray());
        }

        public static AddProjectDto CreateAddProjectDto(int userId)
        {
            return new AddProjectDto(GenerateString(), userId, GenerateString());
        }

        public static AddProjectDto CreateAddProjectDtoWithTags(int userId, int tagCount = 1)
        {
            List<string> tags = CreateRange(tagCount, () => GenerateString());

            return new AddProjectDto(GenerateString(), userId, GenerateString())
            {
                Tags = tags
            };
        }

        public static ReviewRequestAddDto CreateReviewRequestAddDto(int userId, int projectId)
        {
            return new ReviewRequestAddDto()
            {
                UserId = userId,
                ProjectId = projectId,
                Description = GenerateString()
            };
        }

        public static ReviewResponseCreateDto CreateReviewResponseCreateDto(int userId, int reviewRequestId)
        {
            return new ReviewResponseCreateDto()
            {
                UserId = userId,
                ReviewConclusion = ReviewConclusion.LooksGood,
                ReviewRequestId = reviewRequestId,
                ReviewText = GenerateString()
            };
        }

        public static List<T> CreateRange<T>(int count, Func<T> creator)
        {
            return Enumerable
                .Range(1, count)
                .Select(_ => creator())
                .ToList();
        }
    }
}
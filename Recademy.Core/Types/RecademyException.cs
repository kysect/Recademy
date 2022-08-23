using System;

namespace Recademy.Core.Types
{
    public class RecademyException : Exception
    {
        public RecademyException(string message) : base(message)
        {

        }

        public static RecademyException UserNotFound(int id) => new RecademyException($"User wasn't found, id: {id}");
        public static RecademyException ProjectNotFound(int id) => new RecademyException($"Project wasn't found, id: {id}");
        public static RecademyException ReviewRequestNotFound(int id) => new RecademyException($"Review request wasn't found, id: {id}");

        public static RecademyException MissedArgument(string argumentName) => new RecademyException($"Missed argument: {argumentName}");
        public static RecademyException InvalidArgument(string argumentName) => new RecademyException($"Get invalid argument: {argumentName}");

        public static RecademyException InvalidArgument<T>(string argumentName, T value) =>
            new RecademyException($"Get invalid argument: {argumentName} with value: {value?.ToString()}");

        public static RecademyException NotEnoughPermission(int userId, UserType actualType, UserType expectedType) => 
            new RecademyException($"User with id [{userId}] do not have enough permission. Actual: {actualType}, expected: {expectedType}");

    }
}

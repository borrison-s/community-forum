using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ForumApp.Shared
{
    public static class ValidationHelper
    {
        public static void ValidateNotNullOrEmpty(string value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ValidationException($"{fieldName} cannot be null or empty.");
            }
        }

        public static void ValidatePasswordsMatch(string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                throw new ValidationException("Passwords do not match.");
            }
        }

        public static void ValidateRequiredStringColumn(string value, string field, int maxNumOfChars)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new DataException($"{field} is required field");
            }

            if (value.Length > maxNumOfChars)
            {
                throw new DataException($"{field} can not contain more than {maxNumOfChars} characters");
            }
        }

        public static void ValidateStringColumnLength(string value, string field, int maxNumOfChars)
        {
            if (value.Length > maxNumOfChars)
            {
                throw new DataException($"{field} can not contain more than {maxNumOfChars} characters");
            }
        }

    }

}
